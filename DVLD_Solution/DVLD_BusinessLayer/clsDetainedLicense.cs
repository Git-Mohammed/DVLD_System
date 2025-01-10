using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsDetainedLicense
    {
        private enum enMode { AddNew = 0, Update = 1}
        private enMode _Mode;
        public int DetainID {  get; set; }
        public clsUser DetainedByUserInfo;

        public int LicenseID  { get; set; }
        public DateTime DetainDate { get; set; }

        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public int ReleasedByUserID { get; set; }
        public clsUser ReleasedByUserInfo {  get; set; }
        public DateTime? ReleaseDate {  get; set; }
        public bool IsReleased { get; set; }
        public int ReleaseApplicationID {  get; set; }


        public clsDetainedLicense()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.CreatedByUserID = -1;
            this.FineFees = 0;
            this.ReleasedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseApplicationID = -1;
            this.DetainDate = DateTime.Now;
            this.ReleaseDate = null;
            _Mode = enMode.AddNew;
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID)
        {
            return clsDetainedLicenseData.ReleaseDetainedLicense(this.DetainID,
                   ReleasedByUserID, ReleaseApplicationID);
        }
        private clsDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate, float FineFees,  int CreatedByUserID,bool IsReleased,
            DateTime? ReleaseDate, int ReleasedByUserID,int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.DetainedByUserInfo = clsUser.Find(CreatedByUserID);

            this.IsReleased =IsReleased;
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.DetainDate = DetainDate;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;

            if(ReleasedByUserID != -1)
                this.ReleasedByUserInfo = clsUser.Find(ReleasedByUserID);

            _Mode = enMode.Update;
        }

        private bool _AddNewDIA()
        {
            this.DetainID = clsDetainedLicenseData.AddNewDetainedLicense(this.LicenseID,this.DetainDate,this.FineFees,this.CreatedByUserID,this.ReleaseDate,this.IsReleased,this.ReleasedByUserID,this.ReleaseApplicationID);
            return (DetainID != -1);
        }


        public static DataTable AllDetainedLicenses()
        {
           return clsDetainedLicenseData.AllDetainedLicenses();
        }
        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {
            int DetainID = -1;
            int CreatedByUserID = -1;
            int ReleasedByUserID = -1;
            DateTime DetainDate = DateTime.Now;
            DateTime? ReleaseDate = null;
            bool IsReleased = false;
            int ReleaseApplicationID = -1;
            float FineFees = 0;

            if (clsDetainedLicenseData.GetDetainedLicenseByLicenseID(ref DetainID, LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID,
                ref ReleaseDate, ref IsReleased, ref ReleasedByUserID, ref ReleaseApplicationID))
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            else
                return null;

        }
        public static clsDetainedLicense Find(int DetainID)
        {
            int LicenseID = -1;
            int CreatedByUserID = -1;
            int ReleasedByUserID = -1;
            DateTime DetainDate = DateTime.Now;
            DateTime? ReleaseDate = null;
            bool IsReleased = false;
            int ReleaseApplicationID = -1;
            float FineFees = 0;

            if(clsDetainedLicenseData.GetDetainedLicenseByID(  DetainID,  ref LicenseID, ref DetainDate,ref FineFees,ref CreatedByUserID,
                ref ReleaseDate,ref IsReleased,ref ReleasedByUserID,ref ReleaseApplicationID))
                return new clsDetainedLicense(DetainID,LicenseID,DetainDate,FineFees,CreatedByUserID,IsReleased,ReleaseDate,ReleasedByUserID,ReleaseApplicationID);
            else
                return null;
           
        }
        private bool _UpdateDetainedLicense()
        {
            return clsDetainedLicenseData.UpdateDetain(this.DetainID,this.LicenseID,this.DetainDate,this.FineFees,
                this.CreatedByUserID,this.ReleaseDate,this.IsReleased,this.ReleasedByUserID,this.ReleaseApplicationID);
        }
        public static bool DetainedLicenseIsExsist(int LicenseID)
        {
            return clsDetainedLicenseData.isDetainedLicenseExists(LicenseID);
        }

        //public static bool DeleteDIA(int LocalDrivingLicenseLicenseID)
        //{
            
        //    return clsDetainedLicense_Data.DeleteDIA(LocalDrivingLicenseLicenseID);

        //}
        public bool Save()
        {
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
                    return _UpdateDetainedLicense();
                default:
                    return false;
            }

        }
        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicenseData.isDetainedLicenseExists(LicenseID);
        }

      

    }
}
