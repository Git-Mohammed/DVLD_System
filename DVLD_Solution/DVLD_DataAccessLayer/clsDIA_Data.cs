using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class clsDIA_Data
    {

        public static bool GetDIAByID(int DIA_ID, ref int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive,ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "Select * From InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID;";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@InternationalLicenseID", DIA_ID);

            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound =false;
            }
            finally
            {
                con.Close();
            }
            return isFound;
        }

        public static bool GetDIAByLicenseID(ref int DIA_ID,ref int ApplicationID, ref int DriverID,  int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "Select * From InternationalLicenses WHERE IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID;";

            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    DIA_ID = (int)reader["InternationalLicenseID"];
                    DriverID = (int)reader["DriverID"];
                    ApplicationID = (int)reader["ApplicationID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                con.Close();
            }
            return isFound;
        }
        public static bool GetDIAByApplicationID(ref int DIA_ID,  int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "Select * From InternationalLicenses WHERE ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    DIA_ID = (int)reader["InternationalLicenseID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                con.Close();
            }
            return isFound;
        }

        public static DataTable AllInternationalLicensesApplications()
         {
            
            DataTable dataTable = new DataTable();
            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "select * from InternationalLicenses";
            SqlCommand command = new SqlCommand(query, con);
            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return dataTable;
        }
        public static DataTable DriverInternationalLicenses(int DriverID)
        {

            DataTable dataTable = new DataTable();
            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "SELECT * from InternationalLicenses WHERE DriverID =@DriverID";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return dataTable;
        }


        public static bool UpdateInternationalLicense(
            int InternationalLicenseID, int ApplicationID,
           int DriverID, int IssuedUsingLocalLicenseID,
           DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE InternationalLicenses
                           SET 
                              ApplicationID=@ApplicationID,
                              DriverID = @DriverID,
                              IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID,
                              IssueDate = @IssueDate,
                              ExpirationDate = @ExpirationDate,
                              IsActive = @IsActive,
                              CreatedByUserID = @CreatedByUserID
                         WHERE InternationalLicenseID=@InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static int AddNewDIA(int ApplicationID,  int DriverID,  int IssuedUsingLocalLicenseID,  DateTime IssueDate,  DateTime ExpirationDate,  bool IsActive,  int CreatedByUserID)
        {
            int DIA_ID = -1;

            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"  Update InternationalLicenses 
                               set IsActive=0
                               where DriverID=@DriverID;


                                INSERT INTO  InternationalLicenses 
                                   ( ApplicationID 
                                   , DriverID
                                   , IssuedUsingLocalLicenseID
                                   , IssueDate
                                   , ExpirationDate
                                   , IsActive
                                   , CreatedByUserID)
                             VALUES
                                   ( @ApplicationID 
                                   , @DriverID
                                   , @IssuedUsingLocalLicenseID
                                   , @IssueDate
                                   , @ExpirationDate
                                   , @IsActive
                                   , @CreatedByUserID);
                            SELECT
                                   SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                con.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    DIA_ID = InsertedID;
                }
            }
             catch (Exception ex)
            {
                DIA_ID = -1;
            }
            finally
            {
                con.Close();
            }

            return DIA_ID;
        }
        public static bool isDIAExistByLocalLicenseID(int LocallLicenseID)
        {
            bool isFound = false;

            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT fo= 1 from InternationalLicenses 
                            WHERE IssuedUsingLocalLicenseID  = @IssuedUsingLocalLicenseID";

            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", LocallLicenseID);
            try
            {
                con.Open();
                isFound = command.ExecuteScalar() != null;

            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                con.Close();
            }

            return isFound;
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"  
                            SELECT Top 1 InternationalLicenseID
                            FROM InternationalLicenses 
                            where DriverID=@DriverID and GetDate() between IssueDate and ExpirationDate 
                            order by ExpirationDate Desc;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    InternationalLicenseID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return InternationalLicenseID;
        }
    }
}
