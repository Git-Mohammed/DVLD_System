using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public  enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; }

        public clsUser()
        {
            UserID = -1;
            this.PersonID = -1;
            Username = "";
            Password = "";
            isActive = true;
            Mode = enMode.AddNew;
        }
        private clsUser(int userID,int PersonID, string username, string password, bool isactive)
        {
            UserID = userID;
            Username = username;
            Password = password;
            isActive = isactive;
            this.PersonID = PersonID;
            PersonInfo = clsPerson.Find(PersonID);
            Mode = enMode.Update;
        }



        private bool _AddNewUser()
        {
             this.UserID = clsUserData.AddNewUser(PersonID,Username,Password,isActive);

            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(UserID, Username, Password, isActive); ;

        }
        public string FullName()
        {
            return clsPerson.Find(PersonID).FullName;
        }

        public static clsUser Find(int UserID)
        {
            string username = "";
            string password = ""; 
            bool isactive = false;
            int personID = -1;


            if(clsUserData.GetUserDetailsByID(UserID, ref personID, ref username,ref password ,ref isactive))
            {
                return new clsUser(UserID,personID,username,password,isactive);
            }
            else
                return null;
            
        }

        public static clsUser Find(string username, string password)
        {
            bool isactive = false;
            int personID = -1;
            int UserID = -1;



            if (clsUserData.GetUserInfoByUsernameAndPassword(ref UserID, ref personID,  username,  password, ref isactive))
            {
                return new clsUser(UserID, personID, username, password, isactive);
            }
            else
                return null;

        }
        public static clsUser FindByPersonID(int PersonID)
        {
            string username = "";
            string password = "";
            bool isactive = false;
            int UserID = -1;


            if (clsUserData.GetUserDetailsByPersonId(ref UserID,  PersonID, ref username, ref password, ref isactive))
            {
                return new clsUser(UserID, PersonID, username, password, isactive);
            }
            else
                return null;

        }

        public static clsUser Find(string username)
        {
            int userID = -1;
            string password = "";
            int personID = -1;
            bool isactive = false;

            if (clsUserData.GetUserDetailsByUsername(ref userID, ref personID, username, ref password,ref isactive))
            {
                return new clsUser(userID,personID, username, password, isactive);
            }
            else
                return null;

        }

        public static  int AddNewUser(int personID,string username,string password,bool isActive)
        {
            return clsUserData.AddNewUser(personID,username,password,isActive);
        }
        public static bool IsUserExists(string NationalNo)
        {
            return clsUserData.isUserExists(NationalNo);
        }

        public static bool IsUserExists(int ID)
        {
            return clsUserData.isUserExistsByUserID(ID);
        }
        public static bool IsUserExistsByPersonID(int ID)
        {
            return clsUserData.isUserExistsByPersonID(ID);
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdateUser();

                default:
                    return false;
            }
        }
        public static bool ChangePassword(int UserID, string NewPassword)
        {
            return clsUserData.ChangePassword(UserID, NewPassword);
        }
        public static bool Delete(string NationalNo)
        {
            bool isDelete = false;

            if(clsUser.IsUserExists(NationalNo))
            {
                return clsUserData.DeleteUserByNationalNo(NationalNo);
            }

            return isDelete;
        }

        public static bool Delete(int ID)
        {
            bool isDelete = false;

            if (clsUser.IsUserExists(ID))
            {
                return clsUserData.DeleteUserByID(ID);
            }

            return isDelete;
        }
        public static DataTable GetAllUsers()
        {
            return clsUserData.GetUsersList();
        }

       
    }
}