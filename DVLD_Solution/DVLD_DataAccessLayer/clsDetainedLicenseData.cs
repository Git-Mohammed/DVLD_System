using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class clsDetainedLicenseData
    {
        public static bool GetDetainedLicenseByID(int DetainedLicense_ID, ref int LicenseID, ref DateTime DetainDate, ref float FineFees, ref int CreatedByUserID, ref DateTime? ReleaseDate, ref bool IsReleased, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isFound = false;
            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "SELECT * FROM DetainedLicenses WHERE DetainID = @DetainID;";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@DetainID", DetainedLicense_ID);

            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    LicenseID = (int)reader["LicenseID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    DetainDate = (DateTime)reader["DetainDate"];
                    IsReleased = (bool)reader["IsReleased"];
                    ReleaseDate = reader["ReleaseDate"] != DBNull.Value ? (DateTime?)reader["ReleaseDate"] : null;
                    ReleasedByUserID = reader["ReleasedByUserID"] != DBNull.Value ? (int)reader["ReleasedByUserID"] : -1;
                    ReleaseApplicationID = reader["ReleaseApplicationID"] != DBNull.Value ? (int)reader["ReleaseApplicationID"] : -1;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
                // Log the exception or handle it as needed
            }
            finally
            {
                con.Close();
            }
            return isFound;
        }


        public static bool GetDetainedLicenseByLicenseID(ref int detainedLicenseID, int licenseID, ref DateTime detainDate, ref float fineFees, ref int createdByUserID, ref DateTime? releaseDate, ref bool isReleased, ref int releasedByUserID, ref int releaseApplicationID)
        {
            bool isFound = false;
            string query = "SELECT * FROM DetainedLicenses WHERE LicenseID = @LicenseID and  IsReleased = 0";
            using (SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString))
            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@LicenseID", licenseID);
                try
                {

                    con.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            detainedLicenseID = (int)reader["DetainID"];
                            detainDate = (DateTime)reader["DetainDate"];
                            fineFees = Convert.ToSingle(reader["FineFees"]);
                            createdByUserID = (int)reader["CreatedByUserID"];
                            releaseDate = reader["ReleaseDate"] != DBNull.Value ? (DateTime?)reader["ReleaseDate"] : null;
                            isReleased = (bool)reader["IsReleased"];
                            releasedByUserID = reader["ReleasedByUserID"] != DBNull.Value ? (int)reader["ReleasedByUserID"] : -1;
                            releaseApplicationID = reader["ReleaseApplicationID"] != DBNull.Value ? (int)reader["ReleaseApplicationID"] : -1;

                        }
                    }
                }
                catch (Exception ex)
                { // Log the exception
                    isFound = false;
                }
                finally
                {
                    con.Close();
                }
            }
            return isFound;
        }

        
        public static bool UpdateDetainedLicense(int DetainID,
            int LicenseID, DateTime DetainDate,
            float FineFees, int CreatedByUserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE dbo.DetainedLicenses
                              SET LicenseID = @LicenseID, 
                              DetainDate = @DetainDate, 
                              FineFees = @FineFees,
                              CreatedByUserID = @CreatedByUserID,   
                              WHERE DetainID=@DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainedLicenseID", DetainID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
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


        public static DataTable AllDetainedLicenses()
        {

            DataTable dataTable = new DataTable();
            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "select * from DetainedLicenses_View";
            SqlCommand command = new SqlCommand(query, con);
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


        public static int AddNewDetainedLicense(int licenseID, DateTime detainDate, float fineFees, int createdByUserID, DateTime? releaseDate, bool isReleased, int releasedByUserID, int releaseApplicationID)
        {
            int detainedLicenseID = -1;
            string query = @"
                            INSERT INTO 
                                    DetainedLicenses
                            (LicenseID, DetainDate, FineFees, CreatedByUserID, ReleaseDate, IsReleased, ReleasedByUserID,ReleaseApplicationID)
                            VALUES 
                            (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID, @ReleaseDate, @IsReleased, @ReleasedByUserID,@ReleaseApplicationID);
                            SELECT SCOPE_IDENTITY();";

            using (SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString)) 
            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@LicenseID", licenseID); 
                command.Parameters.AddWithValue("@DetainDate", detainDate);
                command.Parameters.AddWithValue("@FineFees", fineFees); 
                command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);
                command.Parameters.AddWithValue("@ReleaseDate", (object)releaseDate ?? DBNull.Value);
                command.Parameters.AddWithValue("@IsReleased", isReleased);

                if((releasedByUserID != -1))
                    command.Parameters.AddWithValue("@ReleasedByUserID", releasedByUserID);
                else
                    command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);

                if ((releaseApplicationID != -1))
                    command.Parameters.AddWithValue("@ReleaseApplicationID", releaseApplicationID);
                else
                    command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);

                try
                {
                    con.Open(); 
                    object result = command.ExecuteScalar(); 
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    { 
                        detainedLicenseID = insertedID; 
                    } 
                }
                catch (Exception ex)
                { // Log the exception
                    detainedLicenseID = -1;
                }
            } 
            return detainedLicenseID;
        }
        public static bool ReleaseDetainedLicense(int DetainID,
              int ReleasedByUserID, int ReleaseApplicationID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString) ;

            string query = @"UPDATE dbo.DetainedLicenses
                              SET IsReleased = 1, 
                              ReleaseDate = @ReleaseDate, 
                              ReleaseApplicationID = @ReleaseApplicationID   
                              WHERE DetainID=@DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
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
        public static bool UpdateDetain(int detainID,int licenseID, DateTime detainDate, float fineFees, int createdByUserID, DateTime? releaseDate, bool isReleased, int releasedByUserID, int releaseApplicationID)
        {
            int rowsAffected = -1;
            string query = @"
                            UPDATE 
                                      DetainedLicenses
                            SET
                                      LicenseID =@LicenseID 
                                    , DetainDate = @DetainDate
                                    , FineFees = @FineFees
                                    , CreatedByUserID = @CreatedByUserID
                                    , ReleaseDate = @ReleaseDate
                                    , IsReleased = @IsReleased
                                    , ReleasedByUserID = @ReleasedByUserID
                                    , ReleaseApplicationID = @ReleaseApplicationID
                            WHERE
                                   DetainID = @DetainID ";

            using (SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString))
            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@LicenseID", licenseID);
                command.Parameters.AddWithValue("@DetainID", detainID);

                command.Parameters.AddWithValue("@DetainDate", detainDate);
                command.Parameters.AddWithValue("@FineFees", fineFees);
                command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);
                command.Parameters.AddWithValue("@IsReleased", isReleased);

                if ((releaseDate != null))
                    command.Parameters.AddWithValue("@ReleaseDate", releaseDate);
                else
                    command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);

                if ((releasedByUserID != -1))
                    command.Parameters.AddWithValue("@ReleasedByUserID", releasedByUserID);
                else
                    command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);

                if ((releaseApplicationID != -1))
                    command.Parameters.AddWithValue("@ReleaseApplicationID", releaseApplicationID);
                else
                    command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);

                try
                {
                    con.Open();
                    rowsAffected = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                { // Log the exception
                }
            }
            return (rowsAffected > 0);
        }
        public static bool isDetainedLicenseExists(int LicenseID)
        {
            bool isFound = false;

            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT fo= 1 from DetainedLicenses 
                            WHERE LicenseID  = @LicenseID and IsReleased = 0";

            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
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

      
    }
}
