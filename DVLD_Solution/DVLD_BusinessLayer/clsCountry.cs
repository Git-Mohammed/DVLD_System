using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsCountry
    {
        enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public int CountryID { get; set; }
        public  string CountryName { get; set; }


        public clsCountry()
        {
            CountryID = -1;
            CountryName = "";

            _Mode = enMode.AddNew;
        }
        private clsCountry(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;

            _Mode = enMode.Update;
        }

        public static clsCountry Find(int ID)
        {
            string CountryName = "";
            if(clsCountryData.GetCountryNameByID(ID, ref CountryName))
            {
               return new clsCountry(ID,CountryName);
            }
            return null;

        }


        public static clsCountry Find(string CountryName)
        {
            int ID = -1;
            if (clsCountryData.GetCountryIDByName(ref ID, CountryName))
            {
                return new clsCountry(ID, CountryName);
            }
            return null;

        }
        public static DataTable AllCountries()
        {
            return clsCountryData.GetAllCountries();
        }
    }
}