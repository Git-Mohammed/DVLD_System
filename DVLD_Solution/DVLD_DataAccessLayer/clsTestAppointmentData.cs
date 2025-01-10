using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class clsTestAppointmentData
    {
        static SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
        public static bool GetTestAppointmentByLicenseID(int TestAppointmentID, ref int TestTypeID, ref int LocalDrivingLicenseApplicationID, ref DateTime AppointmentDate,ref float PaidFees, ref int CreatedByUserID,ref bool IsLocked, ref int RetakeAppID)
        {
            bool isFound = false;
            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked =(bool)reader["IsLocked"];
                    if(reader["RetakeTestApplicationID"] != DBNull.Value)
                        RetakeAppID = (int)reader["RetakeTestApplicationID"];
                    else
                        RetakeAppID = -1;
                    AppointmentDate = Convert.ToDateTime( reader["AppointmentDate"]);
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }
        public static bool GetLastTestAppointment(
           int LocalDrivingLicenseApplicationID, int TestTypeID,
          ref int TestAppointmentID, ref DateTime AppointmentDate,
          ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT       top 1 *
                FROM            TestAppointments
                WHERE        (TestTypeID = @TestTypeID) 
                AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                order by TestAppointmentID Desc";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];

                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        RetakeTestApplicationID = -1;
                    else
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];


                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static bool UpdateTestAppIsLockced(int TestAppointmentID, bool IsLocked)
        {
            int rowsAffected = 0;
            string query = @"UPDATE 
                                TestAppointments 
                            SET 
                                IsLocked = @IsLocked,
                            WHERE
                                TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
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


        public static bool UpdateTestAppointmentDate(int TestAppointmentID,DateTime AppointmentDate, bool IsLocked)
        {
            int rowsAffected = 0;
            string query = @"UPDATE 
                                TestAppointments 
                            SET 
                                AppointmentDate = @AppointmentDate,
                                IsLocked    = @IsLocked
                            WHERE
                                TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

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

            return (rowsAffected>0);

        }

        public static int AddAppointmentTest(  int TestTypeID,  int LocalDrivingLicenseApplicationID,  DateTime AppointmentDate,  double PaidFees,  int CreatedByUserID,  bool IsLocked, int RetakeTestApplicationID)
        {
            int TestAppointmentID = -1;
            string query = @"INSERT INTO TestAppointments
                                   ( TestTypeID 
                                   , LocalDrivingLicenseApplicationID 
                                   , AppointmentDate 
                                   , PaidFees 
                                   , CreatedByUserID 
                                   , IsLocked
                                   , RetakeTestApplicationID)
                             VALUES
                                   ( @TestTypeID 
                                   , @LocalDrivingLicenseApplicationID 
                                   , @AppointmentDate
                                   , @PaidFees
                                   , @CreatedByUserID 
                                   , @IsLocked
                                   , @RetakeTestApplicationID);
                             SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            if(RetakeTestApplicationID == -1)
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", System.DBNull.Value);

            }
            else
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            }


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if ( result != null && int.TryParse(result.ToString(), out int InsertedLicenseID))
                {
                    TestAppointmentID = InsertedLicenseID;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return TestAppointmentID;

        }
        public static int CountOfTrials(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            int Trials = 0;
            string query = @"SELECT Count(Tests.TestResult)
                            FROM     LocalDrivingLicenseApplications INNER JOIN
                                                TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                                Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestTypeID = @TestTypeID and TestResult = 0";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int trials))
                {
                    Trials = trials;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return Trials;

        }
        public static bool LastTestResultIsPassed(int DLA_LicenseID, int TestTypeID)
        {
            bool Result = false;
            string query = @"
                               SELECT top 1 Tests.TestResult
                            FROM     LocalDrivingLicenseApplications INNER JOIN
                                                TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                                Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestTypeID = @TestTypeID 
                            order by TestAppointments.TestAppointmentID desc;
                                ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",DLA_LicenseID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result !=  null)
                {
                    Result = (bool)result;
                }
            }
            catch(Exception ex)
            {

            }
            finally { connection.Close(); }

            return Result;

        }

        public static bool isRetakeTest(int DLA_LicenseID, int TestTypeID)
        {
            bool Result = false;
            string query = @"
                               SELECT top 1 Tests.TestResult
                            FROM     LocalDrivingLicenseApplications INNER JOIN
                                                TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                                Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestTypeID = @TestTypeID 
                            order by TestAppointments.TestAppointmentID desc;
                                ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", DLA_LicenseID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    Result = !(bool)result;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return Result;

        }


        //public static bool LastTestResultIsFailed(int DLA_LicenseID, int TestTypeID)
        //{
        //    int Result = -1;
        //    string query = @"SELECT top 1 Tests.TestResult
        //                    FROM     LocalDrivingLicenseApplications INNER JOIN
        //                                        TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
        //                                        Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
        //                    WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestTypeID = @TestTypeID
        //                    order by TestAppointments.TestAppointmentID desc;
        //                        ";
        //    SqlCommand command = new SqlCommand(query, connection);
        //    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", DLA_LicenseID);
        //    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
        //    try
        //    {
        //        connection.Open();
        //        object result = command.ExecuteScalar();
        //        if (result != null && int.TryParse(result.ToString(), out int res))
        //        {
        //            Result = res;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally { connection.Close(); }

        //    return (Result == 0);

        //}
        public static DataTable GetAllTestAppointments()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM TestAppointments_View";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return dt;
        }
        public static bool isThereTestAppointmentActive(int DLA_LicenseID, int TestTypeID)
        {
            bool isActive = false;
            string query = "Select found = 1 FROM TestAppointments where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestTypeID = @TestTypeID and isLocked = 0 ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", DLA_LicenseID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null)
                {
                    isActive = true;
                }

               
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return isActive;

        }
        public static DataTable GetAllTestAppointments(int DLA_LicenseID, int TestTypeID)
        {
            DataTable dt = new DataTable();
            string query = "Select * FROM TestAppointments where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestTypeID = @TestTypeID ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", DLA_LicenseID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
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

            }
            finally { connection.Close(); }

            return dt;
        }

        public static int GetPersonIDByTestAppID(int TestAppointmentID)
        {
            int PersonID = -1;
            string query = @"
                                SELECT Applications.ApplicantPersonID
                                FROM     Applications INNER JOIN
                                                  LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID INNER JOIN
                                                  TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID

                                WHERE TestAppointmentID = @TestAppointmentID
                ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(),out int insertedId))
                {
                    PersonID = insertedId;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return PersonID;
        }

        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select TestID from Tests where TestAppointmentID=@TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
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


            return TestID;

        }
    }
}
