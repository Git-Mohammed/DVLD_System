using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer;

namespace DVLD_BusinessLayer
{
    public class clsTestAppointment
    {
        public enum enMode {AddNew = 0, Update = 1};
        public enMode Mode = enMode.AddNew;

        public int TestAppointmentID { get; set; }
        public clsTestTypes.enTestType TestTypeID{ get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked {  get; set; }
        public float PaidFees { get; set; }
        public DateTime AppointmentDate {  get; set; }
        public int RetakeTestAppID { get; set; }
        public clsApplication RetakeTestAppInfo { get; set; }

        public int TestID
        {
            get { return clsTestAppointmentData.GetTestID(this.TestAppointmentID); }
        }
        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            TestTypeID =clsTestTypes.enTestType.VisionTest;
            LocalDrivingLicenseApplicationID = -1;
            CreatedByUserID = -1;
            IsLocked = false;
            PaidFees = 0;
            AppointmentDate = DateTime.Now;
            RetakeTestAppID = -1;
            Mode = enMode.AddNew;
        }
        private clsTestAppointment(int TestAppointmentID,  clsTestTypes.enTestType TestTypeID,  int LocalDrivingLicenseApplicationID,  DateTime AppointmentDate,  float PaidFees,  int CreatedByUserID,  bool IsLocked, int RetakeTestAppID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.IsLocked = IsLocked;
            this.CreatedByUserID = CreatedByUserID;
            this.RetakeTestAppID = RetakeTestAppID;
            if(clsApplication.Find(RetakeTestAppID) != null)
                this.RetakeTestAppInfo = clsApplication.Find(RetakeTestAppID);
            Mode = enMode.Update;

        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = -1;
            int LocalDrivingLicenseApplicationID = -1;
            int CreatedByUserID = -1;
            bool IsLocked = false;
            float  PaidFees = 0;
            DateTime AppointmentDate = DateTime.Now;
            int RetakeTestAppID = -1;

            if (clsTestAppointmentData.GetTestAppointmentByLicenseID( TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked,ref RetakeTestAppID))
            {
                return new clsTestAppointment(TestAppointmentID, (clsTestTypes.enTestType) TestTypeID,  LocalDrivingLicenseApplicationID,  AppointmentDate,  PaidFees,  CreatedByUserID,  IsLocked, RetakeTestAppID);
            }
            else
                return null;
        }



        public static int CountOfTrials(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentData.CountOfTrials(LocalDrivingLicenseApplicationID,TestTypeID);
        }
        private bool _UpdateAppointmentDate()
        {
            return clsTestAppointmentData.UpdateTestAppointmentDate(this.TestAppointmentID,this.AppointmentDate,this.IsLocked);
        }
        private bool _AddNewAppDate()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddAppointmentTest((int)this.TestTypeID,this.LocalDrivingLicenseApplicationID,this.AppointmentDate,this.PaidFees,this.CreatedByUserID,this.IsLocked,this.RetakeTestAppID);
            return (this.TestAppointmentID != -1);
        }
        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentData.GetAllTestAppointments();
        }
        public static DataTable GetAllTestAppointments(int DLA_ID, clsTestTypes.enTestType TestTypeID)
        {
            return clsTestAppointmentData.GetAllTestAppointments(DLA_ID,(int) TestTypeID);
            
        }
        public  DataTable GetAllTestAppointments( clsTestTypes.enTestType TestTypeID)
        {
            return clsTestAppointmentData.GetAllTestAppointments(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }
        public static bool isRetakeTest(int DLA_LicenseID, int TestTypeID)
        {
            return clsTestAppointmentData.isRetakeTest(DLA_LicenseID, TestTypeID);
        }
        public static bool LastTestResultIsPassed(int DLA_LicenseID, int TestTypeID)
        {
            return clsTestAppointmentData.LastTestResultIsPassed(DLA_LicenseID, TestTypeID);
        }

      //  public static bool isFaildTestBefore(int DLA_LicenseID, int TestTypeID)
      //  {
          //  return clsTestAppointmentData.LastTestResultIsFailed(DLA_LicenseID, TestTypeID);
      //  }
        public static bool isThereTestAppointmentActive(int DLA_LicenseID, int TestTypeID)
        {
            return clsTestAppointmentData.isThereTestAppointmentActive(DLA_LicenseID,TestTypeID);
        }
        public static int FindPersonID(int TestAppointmentID)
        {
            return clsTestAppointmentData.GetPersonIDByTestAppID(TestAppointmentID);
        }
        public bool Save()
        {
            switch(Mode)
            {
                case enMode.Update:
                    return _UpdateAppointmentDate();
                case enMode.AddNew:
                    if(_AddNewAppDate())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    return false;
                default:
                    return false;
            }
        }

        private int _GetTestID()
        {
            return clsTestAppointmentData.GetTestID(TestAppointmentID);
        }
    }
}
