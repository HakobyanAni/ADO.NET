using System;
using System.Data.SqlClient;

namespace Transaction
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DBforJoin; Integrated Security=True";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString)) // create connection
                {
                    string sqlQuery1 = "UPDATE Companies SET [Company name] = 'Coal Mining LLC' WHERE [Type of activity] = 'Mining of hard coal'"; // create a query

                    using (SqlCommand command1 = new SqlCommand(sqlQuery1, sqlConnection)) // create a command
                    {
                        sqlConnection.Open();  // Open() - method of SqlConnection, which opens a database connection.

                        command1.Transaction = sqlConnection.BeginTransaction();  // BeginTransaction() - method of SqlConnection, which starts a database transaction.
                        command1.ExecuteNonQuery(); // ExecuteNonQuery() - method of SqlCommand, which executes a T-SQL statement against the connection and returns the number of rows affected.

                        command1.Transaction.Rollback();  // Rollback() - method of SqlTransaction, which rolls back a transaction from a pending state.
                        Console.WriteLine("Transaction rollback");
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
