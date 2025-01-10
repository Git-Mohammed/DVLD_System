using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsDLA : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1}
        public enMode Mode = enMode.AddNew;
        public int DLA_ID {  get; set; }

        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo;

        public string PersonFullName
        {
            get
            {
                return base.ApplicantPersonInfo.FullName;
            }
        
        }


        public clsDLA()
        {
            this.DLA_ID = -1;
            this.LicenseClassID = -1;
            Mode = enMode.AddNew;
        }
        private clsDLA(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID, int LicenseClassID)

        {
            this.DLA_ID = LocalDrivingLicenseApplicationID; ;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = (int)ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            Mode = enMode.Update;
        }

        private bool _AddNewDLA()
        {
            this.DLA_ID = clsDLA_Data.AddNewDLA(this.ApplicationID, this.LicenseClassID);

            return (DLA_ID != -1);
        }
        public static DataTable LocalDrivingLicenseApplications()
        {
            return clsDLA_Data.LocalDrivingLicenseApplications_View();
        }
        public static clsDLA FindByAppID(int AppID)
        {
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsDLA_Data.GetDLAByApplicationID
                (ref LocalDrivingLicenseApplicationID, AppID , ref LicenseClassID);


            if (IsFound)
            {
                //now we find the base application
                clsApplication Application = clsApplication.Find(AppID);

                //we return new object of that person with the right data
                return new clsDLA(
                    LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
                return null;

        }
        public static clsDLA Find(int DLA_ID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsDLA_Data.GetDLAByID
                (DLA_ID, ref ApplicationID, ref LicenseClassID);


            if (IsFound)
            {
                //now we find the base application
                clsApplication Application = clsApplication.Find(ApplicationID);

                //we return new object of that person with the right data
                return new clsDLA(
                    DLA_ID, Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
                return null;
        }

        public static bool isDLAExists(int LocalDrivingLicenseApplicationID)
        {
            return clsDLA_Data.isDLAExist(LocalDrivingLicenseApplicationID);
        }
        public static bool DeleteDLA(int LocalDrivingLicenseApplicationID)
        {
            
            return clsDLA_Data.DeleteDLA(LocalDrivingLicenseApplicationID);

        }
        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)

        {
            return clsDLA_Data.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public  bool DoesPassTestType( clsTestTypes.enTestType TestTypeID)

        {
            return clsDLA_Data.DoesPassTestType(this.DLA_ID, (int)TestTypeID);
        }

        public bool Delete()
        {
            bool IsLocalDrivingDeleted = false;
            bool IsBasedApplicationDeleted = false;

            IsLocalDrivingDeleted = clsDLA.DeleteDLA(this.DLA_ID);
            if(!IsLocalDrivingDeleted)
            {
                return false;
            }

            IsBasedApplicationDeleted = clsApplication.DeleteApplication(this.ApplicationID);
            return IsBasedApplicationDeleted;
        }
        public bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;
            if(!base.Save())
            {
                return false;
            }

            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewDLA())
                    {
                        Mode = enMode.Update;
                        return true;
                    }    
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return false;
                default:
                    return false;
            }

        }


        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDriver Driver = clsDriver.Find(this.ApplicantPersonID);

            if (Driver == null)
            {
                //we check if the driver already there for this person.
                Driver = new clsDriver();

                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }
            //now we diver is there, so we add new licesnse

            clsLicense License = new clsLicense();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClassID = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                //now we should set the application status to complete.
                this.setCompeleted();

                return License.LicenseID;
            }

            else
                return -1;
        }
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsDLA_Data.LocalDrivingLicenseApplications_View();
        }
        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }

        public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.DLA_ID);
        }

        public static bool DoesAttendTestType(int DLA_ID ,clsTestTypes.enTestType TestTypeID)
        {
            return clsDLA_Data.DoesAttendTestType(DLA_ID, (int)TestTypeID);
        }
        public bool DoesAttendTestType(clsTestTypes.enTestType TestTypeID)
        {
            return clsDLA_Data.DoesAttendTestType(this.DLA_ID,(int)TestTypeID);
        }
        public static byte TotalTrialsPerTest(int DLA_ID,clsTestTypes.enTestType TestTypeID)
        {
            return clsDLA_Data.TotalTrialsPerTest(DLA_ID, (int)TestTypeID);
        }
        public byte TotalTrialsPerTest(clsTestTypes.enTestType TestTypeID)
        {
            return clsDLA_Data.TotalTrialsPerTest(this.DLA_ID, (int)TestTypeID);
        }
        public int GetActiveLicenseID()
        {
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }
        public  bool IsThereAnActiveScheduledTest(clsTestTypes.enTestType TestTypeID)

        {
            return clsDLA_Data.IsThereAnActiveScheduledTest(this.DLA_ID,(int)TestTypeID);
        }
        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)

        {

            return clsDLA_Data.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public clsTest GetLastTestPerTestType(clsTestTypes.enTestType TestTypeID)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID, TestTypeID);
        }

    }
}
