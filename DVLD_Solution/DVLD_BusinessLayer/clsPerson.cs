using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsPerson
    {
        enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }
                
        }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int CountryID { get; set; }
        public int Gender { get; set; }

        public clsCountry clsCountryInfo {  get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImagePath { get; set; }

        public clsPerson()
        {
            PersonID = -1;
            NationalNo = "";
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            Email = "";
            Phone = "";
            Address = "";
            CountryID = -1;
            DateOfBirth = DateTime.Now;
            ImagePath = "";
            _Mode = enMode.AddNew;
        }
        private clsPerson(int personID, string nationalNo, string firstName, string secondName, string thirdName, string lastName, string email, string phone, int Gender, string address, int countryID, DateTime dateOfBirth, string imagePath)
        {
            PersonID = personID;
            NationalNo = nationalNo;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
            CountryID = countryID;
            this.Gender = Gender;
            DateOfBirth = dateOfBirth;
            clsCountryInfo = clsCountry.Find(countryID);
            ImagePath = imagePath;

            _Mode = enMode.Update;
        }



        private bool _AddNewPerson()
        {
             this.PersonID = clsPersonData.AddNewPerson(NationalNo, FirstName, SecondName, ThirdName, LastName, Gender, CountryID, Phone, Email, Address, DateOfBirth, ImagePath);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, Gender, CountryID, Phone, Email, Address, DateOfBirth, ImagePath);

        }


        public static clsPerson Find(int personID)
        {
            string nationalNo = "";
            string firstName = ""; 
            string secondName = "";
            string thirdName = "";
            string lastName = "";
            string email = "";
            string phone = "";
            int Gender = -1;
            string address = "";
            int countryID = -1;
            DateTime dateOfBirth = DateTime.Now;
            string imagePath = "";
            if(clsPersonData.GetPeronDetailsByID(personID,ref nationalNo,ref firstName, ref secondName, ref thirdName, ref lastName, ref Gender, ref countryID, ref phone, ref email, ref address, ref dateOfBirth, ref imagePath))
            {
                return new clsPerson(personID,nationalNo,firstName,secondName,thirdName,lastName,email,phone,Gender,address,countryID,dateOfBirth,imagePath);
            }
            else
                return null;
            
        }

        public static clsPerson Find(string nationalNo)
        {
            int ID = -1;
            string firstName = "";
            string secondName = "";
            string thirdName = "";
            string lastName = "";
            string email = "";
            string phone = "";
            int Gender = -1;
            string address = "";
            int countryID = -1;
            DateTime dateOfBirth = DateTime.Now;
            string imagePath = "";
            if (clsPersonData.GetPeronDetailsByNationalNo(ref ID, nationalNo, ref firstName, ref secondName, ref thirdName, ref lastName, ref Gender, ref countryID, ref phone, ref email, ref address, ref dateOfBirth, ref imagePath))
            {
                return new clsPerson(ID, nationalNo, firstName, secondName, thirdName, lastName, email, phone, Gender, address, countryID, dateOfBirth, imagePath);
            }
            else
                return null;

        }

        public static bool IsPersonExists(string NationalNo)
        {
            return clsPersonData.isPersonExists(NationalNo);
        }

        public static bool IsPersonExists(int ID)
        {
            return clsPersonData.isPersonExists(ID);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdatePerson();

                default:
                    return false;
            }
        }

        public static bool Delete(string NationalNo)
        {
            bool isDelete = false;

            if(clsPerson.IsPersonExists(NationalNo))
            {
                return clsPersonData.DeletePeronByNationalNo(NationalNo);
            }

            return isDelete;
        }

        public static bool Delete(int ID)
        {
            bool isDelete = false;

            if (clsPerson.IsPersonExists(ID))
            {
                 
                return clsPersonData.DeletePeronByID(ID);
            }

            return isDelete;
        }
        public static DataTable GetAllPeople()
        {
           
            return clsPersonData.GetPeopleList();
        }
    }
}