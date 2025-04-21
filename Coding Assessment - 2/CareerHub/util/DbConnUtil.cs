using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace CareerHub.Util
{
    public class DBConnUtil
    {
        private static readonly string connectionString = "Server=JARVIS-LAPTOP;Database=CareerHubDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;";

        public static SqlConnection GetConnectionString()
        {
            try
            {
                // Create connection using the connection string
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open(); // Open the connection
                return conn;
            }
            catch (SqlException ex)
            {
                // SQL specific error
                Console.WriteLine("SQL Error while connecting to db: " + ex.Message);
                Console.WriteLine("Error Number: " + ex.Number);
                Console.WriteLine("Error State: " + ex.State);
                Console.WriteLine("Error Line: " + ex.LineNumber);
                throw;
            }
            catch (Exception ex)
            {
                // General error
                Console.WriteLine("General Error: " + ex.Message);
                throw;
            }
        }
    }
}
