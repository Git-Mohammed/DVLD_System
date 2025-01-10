using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer;

namespace DVLD_BusinessLayer
{
    public class clsTest
    {
        public enum enMode {AddNew = 0, Update = 1};
        public enMode _Mode = enMode.Update;
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public clsTestAppointment TestAppointmentInfo { set; get; }

        public string Notes { get; set; }
        public bool TestResult { get; set; }
        public int CreatedByUserID { get; set; }

        public clsTest()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.Notes = "";
            this.TestResult = false;
            this.CreatedByUserID = -1;
            _Mode = enMode.AddNew;
        }
        private clsTest(int LicenseID,int TestAppointmentID, bool TestResult,string Notes,int CreatedByUserID) 
        {
            this.TestID = LicenseID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestAppointmentInfo = clsTestAppointment.Find(TestAppointmentID);
            this.Notes = Notes;
            this.TestResult = TestResult;
            this.CreatedByUserID = CreatedByUserID;
            _Mode = enMode.Update;
        }

        public static clsTest Find(int TestID)
        {
            int TestAppointmentID = -1;
            string Notes = "";
            bool TestResult = false;
            int CreatedByUserID = -1;
            if (clsTestData.GetTestByID(TestID, ref TestAppointmentID, ref TestResult, ref Notes,ref CreatedByUserID))
            {
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes,CreatedByUserID);
            }
            else
                return null;
        }


        public static clsTest FindByTestAppointmentID(int TestAppointmentID)
        {
            int TestID = -1;
            string Notes = "";
            bool TestResult = false;
            int CreatedByUserID = -1;
            if (clsTestData.GetTestByTestAppointmentID(ref TestID,  TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }
            else
                return null;
        }
        public static clsTest FindLastTestPerPersonAndLicenseClass
            (int PersonID, int LicenseClassID, clsTestTypes.enTestType TestTypeID)
        {
            int TestID = -1;
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

            if (clsTestData.GetLastTestByPersonAndTestTypeAndLicenseClass
                (PersonID, LicenseClassID, (int)TestTypeID, ref TestID,
            ref TestAppointmentID, ref TestResult,
            ref Notes, ref CreatedByUserID))

                return new clsTest(TestID,
                        TestAppointmentID, TestResult,
                        Notes, CreatedByUserID);
            else
                return null;

        }
        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(this.TestID,this.TestAppointmentID,this.TestResult,this.Notes,this.CreatedByUserID);
        }

        private bool _AddNewTest()
        {
            this.TestID = clsTestData.AddNewTest(this.TestAppointmentID,this.TestResult,this.Notes,this.CreatedByUserID);
            return (TestID != -1);
        }
        public static DataTable GetAllTestTypes()
        {
            return clsTestData.GetAllTests();
        }
        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.Update:
                    return _UpdateTest();
                case enMode.AddNew:
                    if(_AddNewTest())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    return false;
                default:
                    return false;
            }
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }
        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }
    }
}
