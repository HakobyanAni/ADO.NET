using System;
using System.Data.SqlClient;

namespace ADO.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Country; Integrated Security=True";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString)) // create connection
                {
                    sqlConnection.Open();
                    string query1 = "Insert into [Country] Values ('Switzerland', 'Bern', 'Europe', '8544034','River Aare', 'Albert Einstein', 'Christianity', 'Munster', 'Fondue', '01:00:00')"; // create a query

                    using (SqlCommand command1 = new SqlCommand(query1, sqlConnection)) // create command
                    {
                        var om = (object)command1.ExecuteScalar(); // insert a new row in the table
                    }                                              // ExecuteScalar() - method of SqlCommand, which executes the query, and returns 
                                                                   // the first column of the first row in the result set returned by the query.

                    string query2 = "select * from [Country]; select * from [Capital]"; // create another query

                    // Show all the table
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = sqlConnection;
                        command.CommandText = query2;
                        using (SqlDataReader reader = command.ExecuteReader()) // ExecuteReader() - method of SqlCommand, which sends the CommandText to the Connection and builds a SqlDataReader. 
                        {
                            while (reader.Read()) // Read() - method of SqlDataReader, which returns true if there are more rows in the table, otherwise false.
                            {
                                for (int i = 0; i < reader.FieldCount; i++) // FieldCount - property of SqlDataReader, which gets the number of columns in the current row.
                                {
                                    Console.WriteLine(reader.GetName(i) + ":" + reader[i]); // GetName() - method of SqlDataReader, which gets the name of the specified column.
                                }

                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine(new string('_', 20));
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }

                            reader.NextResult(); // go to the next query written in query2

                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Some capitals with their population");

                            while (reader.Read())
                            {
                                if (reader[2] == DBNull.Value)
                                {
                                    Console.WriteLine("   " + reader["Capital"] + " - " + "No Data");  // use of indexer
                                }
                                else
                                {
                                    Console.WriteLine("   " + reader["Capital"] + " - " + reader[2]);  // use of indexers
                                }
                            }

                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }

                    // Show all the table as SQL
                    using (SqlCommand c = new SqlCommand(query2, sqlConnection))
                    {
                        using (SqlDataReader sqlDataReader = c.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                for (int i = 0; i < sqlDataReader.FieldCount; i++)
                                {
                                    Console.Write(sqlDataReader[i] + " ");
                                }
                                Console.WriteLine();
                            }
                        }
                    }

                    Console.WriteLine("-------------------------------------------------------------------");

                    using (SqlCommand command4 = new SqlCommand(query2, sqlConnection))
                    {
                        SqlDataReader dataReader = command4.ExecuteReader();
                        while (dataReader.Read())
                        {
                            Console.WriteLine(dataReader.GetFieldValue<int>(0) + " Country: " +     // GetFieldValue<T>(int i) - method of SqlDataReader, which gets the value of the specified column as a type.
                                dataReader.GetString(2) + " Capital: " + dataReader.GetString(3)    // GetString(int i) - method of SqlDataReader, which gets the value of the specified column as a string.
                                + " Famous place: " + dataReader["Famous place"] + " Famous food: " + dataReader[9]);  // use of indexers
                        }
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}
