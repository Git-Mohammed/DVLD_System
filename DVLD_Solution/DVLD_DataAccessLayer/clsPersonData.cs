using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;

namespace DVLD_DataAccessLayer
{
    public class clsPersonData
    {
        public static bool GetPeronDetailsByID(int ID, ref string NationalID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,ref int Gender,ref int CountryID, ref string Phone, ref string Email, ref string Address, ref DateTime DateOfBirth, ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "SELECT * FROM People WHERE PersonID = @PersonID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", ID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    NationalID = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    
                    if((string)reader["ThirdName"] == "")
                    {
                        ThirdName = "";
                    }
                    else
                    {
                        ThirdName = (string)reader["ThirdName"];

                    }
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Gender = Convert.ToInt32(reader["Gendor"]);
                    Email = (string)reader["Email"];
                    CountryID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] == DBNull.Value)
                        ImagePath = "";
                    else
                        ImagePath = (string)reader["ImagePath"];
                }
                reader.Close();
            }
             catch (Exception ex)
            {
               isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetPeronDetailsByNationalNo(ref int ID, string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref int Gender, ref int CountryID, ref string Phone, ref string Email, ref string Address, ref DateTime DateOfBirth, ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];

                    if ((string)reader["ThirdName"] == "")
                    {
                        ThirdName = "";
                    }
                    else
                    {
                        ThirdName = (string)reader["ThirdName"];

                    }
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Gender = Convert.ToInt32(reader["Gendor"]);
                    Email = (string)reader["Email"];
                    CountryID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] == "" || reader["ImagePath"] == DBNull.Value)
                        ImagePath = "";
                    else
                        ImagePath = (string)reader["ImagePath"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static bool DeletePeronByNationalNo(string NationalNo)
        {
            int rowsaffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "DELETE People WHERE NationalNo = @NationalNo;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                connection.Open();
                rowsaffected = (int)command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
             //   Console.WriteLine("error : " + ex.Message);

                rowsaffected = 0;
            }
            finally
            {
                connection.Close();
            }

            return (rowsaffected > 0);
        }

        public static bool DeletePeronByID(int ID)
        {
            int rowsaffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "DELETE People WHERE PersonID = @PersonID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", ID);
            try
            {
                connection.Open();
                rowsaffected = (int)command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
           //     Console.WriteLine("error : " + ex.Message);
                rowsaffected = 0;
            }
            finally
            {
                connection.Close();
            }

            return (rowsaffected > 0);
        }
        public static int AddNewPerson(string NationalNo,  string FirstName,  string SecondName,  string ThirdName,  string LastName,  int Gender,  int CountryID,  string Phone,  string Email,  string Address,  DateTime DateOfBirth,  string ImagePath)
        {
            int personID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO People
                                   (NationalNo
                                   ,FirstName
                                   ,SecondName
                                   ,ThirdName
                                   ,LastName
                                   ,DateOfBirth
                                   ,Gendor
                                   ,Address
                                   ,Phone
                                   ,Email,NationalityCountryID,ImagePath)
                             VALUES
                                   (@NationalNo
                                   ,@FirstName
                                   ,@SecondName
                                   ,@ThirdName
                                   ,@LastName
                                   ,@DateOfBirth
                                   ,@Gendor
                                   ,@Address
                                   ,@Phone
                                   ,@Email
                                   ,@NationalityCountryID
                                   ,@ImagePath);
                        SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            if(ThirdName != null || ThirdName == "")
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
            command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountryID", CountryID);
            if (ImagePath != null || ImagePath == "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(),out int insertedID))
                {
                    personID = insertedID;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return personID;
        }


        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, int Gender, int CountryID, string Phone, string Email, string Address, DateTime DateOfBirth, string ImagePath)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE People
                              SET NationalNo = @NationalNo
                                   ,FirstName = @FirstName
                                   ,SecondName = @SecondName
                                   ,ThirdName = @ThirdName
                                   ,LastName = @LastName
                                   ,DateOfBirth = @DateOfBirth
                                   ,Gendor = @Gendor
                                   ,Address = @Address
                                   ,Phone = @Phone
                                   ,Email = @Email
                                   ,NationalityCountryID = @NationalityCountryID
                                   ,ImagePath= @ImagePath
                               WHERE
                                    PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            if (!string.IsNullOrEmpty(ThirdName))
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountryID", CountryID);
            if (!string.IsNullOrEmpty(ImagePath))
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }


        public static bool isPersonExists(int ID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "SELECT found = 1 FROM People WHERE PersonID = @PersonID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", ID);
            try
            {
                connection.Open();
                isFound = (command.ExecuteScalar() != null);
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static bool isPersonExists(string NationalNo)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "SELECT found = 1 FROM People WHERE NationalNo = @NationalNo;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                connection.Open();
                isFound = (command.ExecuteScalar() != null);

            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static DataTable GetPeopleList()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName,
                            ThirdName =
                            CASE
	                            WHEN ThirdName is NULL  THEN '' 
	                            ELSE ThirdName 
                            END,
                            People.LastName,
                            GenderCaption = 
                            CASE 
	                            WHEN People.Gendor = 0 THEN 'Male'
	                            ELSE 'Female'
                            END,
                            People.DateOfBirth, Countries.CountryName, People.Address, People.Phone, People.Email
                            FROM     People INNER JOIN
                                              Countries ON People.NationalityCountryID = Countries.CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
              //  Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

    }
}
