using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer;
using static System.Net.Mime.MediaTypeNames;
namespace DVLD_BusinessLayer
{
    public class clsApplication
    {
        public enum enMode {AddNew = 0, Update = 1};
        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };
        public enMode Mode = enMode.AddNew;
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public clsPerson ApplicantPersonInfo;
        public int ApplicationTypeID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime LastStatusDate { get; set; }
        public enApplicationStatus ApplicationStatus { set; get; }
        public string StatusText
        {
            get
            {

                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }

        }
        public float PaidFees { get; set; }
        public clsApplicationType ApplicationTypeInfo;
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;

        private clsApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate , float PaidFees,int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicantPersonInfo = clsPerson.Find(ApplicantPersonID);
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);
            this.ApplicationDate = ApplicationDate;
            this.LastStatusDate = LastStatusDate;
            this.ApplicationStatus = ApplicationStatus;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.Find(CreatedByUserID);
            Mode = enMode.Update;
        }
        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationTypeID = -1;
            this.ApplicationDate = DateTime.Now;
            this.LastStatusDate = DateTime.Now;
            this.ApplicationStatus = enApplicationStatus.New;
            this.CreatedByUserID = -1;
            this.PaidFees = 0;
            Mode = enMode.AddNew;
        }
        public static clsApplication Find(int ApplicationID)
        {
            int ApplicantPersonID = -1;
            int ApplicationTypeID = -1;
            DateTime ApplicationDate = DateTime.Now;
            DateTime LastStatusDate = DateTime.Now;
            byte ApplicationStatus = 0;
            float PaidFees = 0;

            int CreatedByUserID = -1;

            if (clsApplicationData.GetApplicationByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate,ref PaidFees, ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID,(enApplicationStatus)ApplicationStatus, LastStatusDate,PaidFees, CreatedByUserID);
            }
            else
                return null;
        }
        private bool _UpdateApplication()
        {
            //call DataAccess Layer 

            return clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte)this.ApplicationStatus,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

        }

        public bool Cancel()
        {
            return clsApplicationData.UpdateApplicationStatus(this.ApplicationID, 2);
        }

        public bool setCompeleted()
        {
            return clsApplicationData.UpdateApplicationStatus(this.ApplicationID, 3);
        }
        public static bool DeleteApplication(int ApplicationID)
        {
            if(!clsApplicationData.isApplicationExists(ApplicationID)) return false;

            return clsApplicationData.DeleteApplication(ApplicationID);
        }
        public static int GetActiveApplicationIDForLicenseClass(int ApplicantPersonID, int ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(ApplicantPersonID, ApplicationTypeID, LicenseClassID);
        }
        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHasActiveApplication(PersonID, ApplicationTypeID);
        }

        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return DoesPersonHaveActiveApplication(this.ApplicantPersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
        }

        private bool _UpdateApplicationStatus()
        {
            return clsApplicationData.UpdateApplicationStatus(this.ApplicationID,(byte) this.ApplicationStatus);
        }
        private bool _AddNewApplication()
        {
           this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicantPersonID,this.ApplicationDate,this.ApplicationTypeID,(byte)this.ApplicationStatus,this.LastStatusDate,this.PaidFees,this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }
        public static bool ValidateNewApplication(int PersonID, int ApplicationType, int LicenseClassID)
        {
           
            return clsApplicationData.PersonHasSameLicenseClassAndApplicationTypeOrCompeleted(PersonID, ApplicationType,LicenseClassID);
        }
        public static bool ValidateNewApplication(int PersonID, int ApplicationType)
        {
            return clsApplicationData.PersonHasSameApplicationTypeOrCompeleted(PersonID, ApplicationType);
        }
        public  bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdateApplication();
                default:
                    return false;
            }
        }
    }
}

