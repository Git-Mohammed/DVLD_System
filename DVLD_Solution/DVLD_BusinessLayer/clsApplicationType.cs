using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer;
namespace DVLD_BusinessLayer
{
    public class clsApplicationType
    {
        public enum enMode {AddNew = 0, Update = 1};
        public enMode _Mode = enMode.Update;
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public float ApplicationTypeFees { get; set; }
        public clsApplicationType()
        {
            this.ApplicationTypeID =-1;
            ApplicationTypeTitle = "";
            ApplicationTypeFees = 0;
            _Mode = enMode.AddNew;
        }
        private clsApplicationType(int ApplicationTypeID, string Title, float Fees) 
        {
            this.ApplicationTypeID = ApplicationTypeID;
            ApplicationTypeTitle = Title;
            ApplicationTypeFees = Fees;
            _Mode = enMode.Update;
        }

        public static clsApplicationType Find(int ApplicationTypeID)
        {
            string title = "";
            float fees = 0;
            if (clsApplicationTypesData.GetApplicationTypeByID(ApplicationTypeID, ref title, ref fees))
            {
                return new clsApplicationType(ApplicationTypeID, title, fees);
            }
            else
                return null;
        }


        public static clsApplicationType Find(string ApplicationTypeTitle)
        {
            int ApplicationTypeID = -1;
            float fees = 0;
            if (clsApplicationTypesData.GetApplicationTypeByTitle(ref ApplicationTypeID,  ApplicationTypeTitle, ref fees))
            {
                return new clsApplicationType(ApplicationTypeID, ApplicationTypeTitle, fees);
            }
            else
                return null;
        }

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypesData.UpdateApplicationType(this.ApplicationTypeID,this.ApplicationTypeTitle,this.ApplicationTypeFees);
        }

        public static DataTable GetAllApllicationTypes()
        {
            return clsApplicationTypesData.GetAllApplicationTypes();
        }
        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.Update:
                    return _UpdateApplicationType();
                case enMode.AddNew:
                    //Feature maybe
                    return false;
                default:
                    return false;
            }
        }
    }
}
