using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer;

namespace DVLD_BusinessLayer
{
    public class clsTestTypes
    {
        public enum enMode {AddNew = 0, Update = 1};
        
        public enMode _Mode = enMode.Update;
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest= 3}

        public clsTestTypes.enTestType ID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestDescription { get; set; }
        public float TestTypeFees { get; set; }

        public clsTestTypes()
        {
            ID = enTestType.VisionTest;
            TestTypeTitle = "";
            TestDescription = "";
            TestTypeFees = 0;
            _Mode = enMode.AddNew;
        }
        private clsTestTypes(clsTestTypes.enTestType ID,string Title,string Description, float Fees) 
        {
            this.ID = ID;
            TestTypeTitle = Title;
            TestDescription = Description;
            TestTypeFees = Fees;
            _Mode = enMode.Update;

        }

        public static clsTestTypes Find(clsTestTypes.enTestType TestTypeID)
        {
            string title = "";
            string description = "";

            float fees = 0;
            if (clsTestTypesData.GetTestTypeByID((int)TestTypeID, ref title,ref description, ref fees))
            {
                return new clsTestTypes((clsTestTypes.enTestType)TestTypeID, title,description ,fees);
            }
            else
                return null;
        }


        public static clsTestTypes Find(string TestTypeTitle)
        {
            int ID = -1;
            string description = "";
            float fees = 0;
            if (clsTestTypesData.GetTestTypeByTitle(ref ID,  TestTypeTitle, ref description,ref fees))
            {
                return new clsTestTypes((clsTestTypes.enTestType)ID, TestTypeTitle, description, fees);
            }
            else
                return null;
        }

        private bool _UpdateTestType()
        {
            return clsTestTypesData.UpdateTestType((int)this.ID,this.TestTypeTitle,this.TestDescription,this.TestTypeFees);
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTestTypes();
        }
        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.Update:
                    return _UpdateTestType();
                case enMode.AddNew:
                    //Feature maybe
                    return false;
                default:
                    return false;
            }
        }
    }
}
