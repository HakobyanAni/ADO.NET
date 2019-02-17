using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace From_DB_to_C_Sharp
{
    public class Country
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public string Continent { get; set; }
        public long? Population { get; set; }
        public string WaterSpace { get; set; }
        public string FamousPeople { get; set; }
        public string Religion { get; set; }
        public string FamousPlace { get; set; }
        public string TimeZone { get; set; }

        public Country()
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Country> listCountriesFromSql = new List<Country>();

            string stringToConnect = @"Data source=(localdb)\MSSQLLocalDB; Initial Catalog=MY DATABASE; Integrated Security= True";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(stringToConnect))
                {
                    sqlConnection.Open();

                    string query = "SELECT * FROM Country";

                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        using (SqlDataReader sqlDataReader = command.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                string name = sqlDataReader.GetString(1);
                                string capital = sqlDataReader.GetString(2);
                                string continent = sqlDataReader.GetString(3);
                                SqlInt64 population = sqlDataReader.GetSqlInt64(4);
                                string waterSpace = sqlDataReader.GetString(5);
                                string famousPeople = sqlDataReader.GetString(6);
                                string religion = sqlDataReader.GetString(7);
                                string famousPlace = sqlDataReader.GetString(8);
                                string timeZone = sqlDataReader.GetString(9);

                                listCountriesFromSql.Add(new Country
                                {
                                    Name = sqlDataReader.GetString(1).Replace(" ", ""),
                                    Capital = capital.Replace(" ", ""),
                                    Continent = continent.Replace(" ", ""),
                                    Population = population.IsNull ? default(long?) : population.Value,
                                    WaterSpace = waterSpace.Replace(" ", ""),
                                    FamousPeople = famousPeople.Replace(" ", ""),
                                    Religion = religion.Replace(" ", ""),
                                    FamousPlace = famousPlace.Replace(" ", ""),
                                    TimeZone = timeZone.Replace(" ", "")
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            foreach (var item in listCountriesFromSql)
            {
                Console.WriteLine($"The capital of {item.Name} is {item.Capital}. {item.Name} is situated in " +
                    $"{item.Continent}. Population is {item.Population}. Religion of this country is {item.Religion}." +
                    $"One of the most famous place in {item.Name} is {item.FamousPlace}.");
            }
        }
    }
}
