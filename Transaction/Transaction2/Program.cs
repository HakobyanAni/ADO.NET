using System;
using System.Data.SqlClient;
using System.Data;

namespace Transaction2
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionStr = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DBforJoin; Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Companies", connection))
                    {
                        sqlCommand.Transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);  // Starts a database transaction with the specified isolation level.
                                                                                                             // By this isolation level transaction2 will wait until transaction1 ends and then it will read and print all the data written by query

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                if (sqlDataReader[2] == DBNull.Value) // check if exists any member equals null
                                {
                                    Console.WriteLine(sqlDataReader.GetSqlValue(0) + ". " +
                                        sqlDataReader.GetString(1) + " - there is no activity");
                                }
                                else
                                {
                                    Console.WriteLine(sqlDataReader.GetSqlValue(0) + ". " +
                                        sqlDataReader.GetString(1) + " - type of activity - " + sqlDataReader.GetString(2));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
