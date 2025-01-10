using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class clsDLA_Data
    {

        public static bool GetDLAByID(int DLA_ID, ref int ApplicationID, ref int LicenseClassID)
        {
            bool isFound = false;
            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "Select * From LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", DLA_ID);

            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
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
        public static bool GetDLAByApplicationID(ref int DLA_ID,  int ApplicationID, ref int LicenseClassID)
        {
            bool isFound = false;
            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "Select * From LocalDrivingLicenseApplications WHERE ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    DLA_ID = (int)reader["LocalDrivingLicenseApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
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

        public static DataTable LocalDrivingLicenseApplications_View()
         {
            
            DataTable dataTable = new DataTable();
            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "select * from LocalDrivingLicenseApplications_View;";
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
        public static string getClassNameByID(int DLA_ID)
        {
            string ClassName = "";
            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "Select ClassName From LocalDrivingLicenseApplications_View\r\nWhere LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", DLA_ID);
            try
            {
                con.Open();
                object result = command.ExecuteScalar();
                if(result != null)
                {
                    ClassName = (string)result;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return ClassName;
        }

        public static string getNationalNoByID(int DLA_ID)
        {
            string NationalNo = "";
            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "Select NationalNo From LocalDrivingLicenseApplications_View Where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", DLA_ID);
            try
            {
                con.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    NationalNo = (string)result;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return NationalNo;
        }

        public static bool GetDLAViewByID(int DLA_ID,ref string ClassName, ref string NationalNo, ref string FullName, ref DateTime ApplicationDate, ref int PassedTestCount,ref string Status)
        {
            bool isFound  = false;
            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "Select * From LocalDrivingLicenseApplications_View Where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", DLA_ID);
            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    NationalNo = (string)reader["NationalNo"];
                    ClassName = (string)reader["ClassName"];
                    FullName = (string)reader["FullName"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    if(reader["PassedTestCount"] != null)
                    {
                        PassedTestCount = (int)reader["PassedTestCount"];

                    }
                    else
                    {
                        PassedTestCount = 0;
                    }
                    if (reader["Status"] != null)
                        Status = (string)reader["Status"];
                    else
                        Status = "";
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return isFound;
        }
        public static int AddNewDLA(int ApplicationID, int LicenseClassID)
        {
            int DLA_ID = -1;

            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO  LocalDrivingLicenseApplications 
                                   ( ApplicationID 
                                   , LicenseClassID )
                             VALUES
                                   ( @ApplicationID 
                                   , @LicenseClassID );
                               SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                con.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    DLA_ID = InsertedID;
                }
            }
             catch (Exception ex)
            {
                DLA_ID = -1;
            }
            finally
            {
                con.Close();
            }

            return DLA_ID;
        }
        public static bool isDLAExist(int LocalDrivingLicenseApplicationID)
        {
            bool isFound = false;

            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT fo= 1 from LocalDrivingLicenseApplications 
                            WHERE LocalDrivingLicenseApplicationID  = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
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
        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {


            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @" SELECT top 1 TestResult
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && bool.TryParse(result.ToString(), out bool returnedResult))
                {
                    Result = returnedResult;
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

            return Result;

        }


        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {


            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @" SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsFound = true;
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

            return IsFound;

        }

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {


            byte TotalTrialsPerTest = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @" SELECT TotalTrialsPerTest = count(TestID)
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                       ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte Trials))
                {
                    TotalTrialsPerTest = Trials;
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

            return TotalTrialsPerTest;

        }
        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {

            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @" SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)  
                            AND(TestAppointments.TestTypeID = @TestTypeID) and isLocked=0
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null)
                {
                    Result = true;
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

            return Result;

        }
        public static bool DeleteDLA(int LocalDrivingLicenseApplicationID)
        {
            int rowsAffcted = -1;

            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"DELETE FROM LocalDrivingLicenseApplications 
                                    WHERE LocalDrivingLicenseApplicationID =  @LocalDrivingLicenseApplicationID ;
                               ";

            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try
            {
                con.Open();
                rowsAffcted = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }

            return (rowsAffcted > 0);
        }
    }
}
