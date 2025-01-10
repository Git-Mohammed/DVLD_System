using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsDIA : clsApplication
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode _Mode = enMode.AddNew;
        public int DIA_ID {  get; set; }
        public int ApplicationID  { get; set; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo;
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate {  get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID {  get; set; }


        public clsDIA()
        {
            this.DIA_ID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IsActive = false;
            this.CreatedByUserID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            _Mode = enMode.AddNew;
        }
        private clsDIA(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID,
             int InternationalLicenseID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive)
        {
            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            base.ApplicationStatus = ApplicationStatus;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;

            this.DIA_ID = DIA_ID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.DriverInfo = clsDriver.FindByDriverID(DriverID);
            this.IsActive =IsActive;
            this.CreatedByUserID = CreatedByUserID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            _Mode = enMode.Update;
        }

        private bool _AddNewDIA()
        {
            this.DIA_ID = clsDIA_Data.AddNewDIA(this.ApplicationID, this.DriverID,this.IssuedUsingLocalLicenseID,this.IssueDate,this.ExpirationDate,this.IsActive,this.CreatedByUserID);

            return (DIA_ID != -1);
        }

        public static DataTable AllDriverInternationalLicenses(int DriverID)
        {

           return clsDIA_Data.DriverInternationalLicenses(DriverID);
        }
        public static DataTable InternationalDrivingLicenseApplications()
        {
            return clsDIA_Data.AllInternationalLicensesApplications();
        }
        public static clsDIA FindByAppID(int AppID)
        {
            int DIA_ID = -1;
            int DriverID = -1;


            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            if (clsDIA_Data.GetDIAByApplicationID(ref DIA_ID,  AppID, ref DriverID,ref IssuedUsingLocalLicenseID, ref IssueDate,ref ExpirationDate,ref IsActive,ref CreatedByUserID))
            {
                clsApplication Application = clsApplication.Find(AppID);


                return new clsDIA(Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID,
                                     DIA_ID, DriverID, IssuedUsingLocalLicenseID,
                                         IssueDate, ExpirationDate, IsActive);

            }
            else
                return null;
        }
        public static clsDIA Find(int DIA_ID)
        {
            int ApplicationID = -1;
            int DriverID = -1; int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            bool IsActive = true; int CreatedByUserID = 1;

            if (clsDIA_Data.GetDIAByID(DIA_ID, ref ApplicationID, ref DriverID,
                ref IssuedUsingLocalLicenseID,
            ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                //now we find the base application
                clsApplication Application = clsApplication.Find(ApplicationID);


                return new clsDIA(Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID,
                                     DIA_ID, DriverID, IssuedUsingLocalLicenseID,
                                         IssueDate, ExpirationDate, IsActive);

            }

            else
                return null;
        }
        

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return clsDIA_Data.DriverInternationalLicenses(DriverID);
        }
        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {

            return clsDIA_Data.GetActiveInternationalLicenseIDByDriverID(DriverID);

        }
        public static bool isDIAExists(int LocalDrivingLicenseApplicationID)
        {
            return clsDIA_Data.isDIAExistByLocalLicenseID(LocalDrivingLicenseApplicationID);
        }
        //public static bool DeleteDIA(int LocalDrivingLicenseApplicationID)
        //{

        //    return clsDIA_Data.DeleteDIA(LocalDrivingLicenseApplicationID);

        //}

        private bool _UpdateInternationalLicense()
        {
            //call DataAccess Layer 

            return clsDIA_Data.UpdateInternationalLicense(
                this.DIA_ID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
               this.IssueDate, this.ExpirationDate,
               this.IsActive, this.CreatedByUserID);
        }
        public bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;

            switch (_Mode)
            {
                case enMode.AddNew:
                    if(_AddNewDIA())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }    
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateInternationalLicense();
                default:
                    return false;
            }

        }

    }
}
