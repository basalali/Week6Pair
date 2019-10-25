using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class CitySqlDAL
    {

        //get all venues

        private string connectionString;
        private string sql_GetSpacesByVenueID = "SELECT city.name , city.state_abbreviation FROM venue JOIN city ON venue.city_id = city.id WHERE venue.id = @venue_id";

        /// <summary>
        /// Creates a new sql-based venue dao.
        /// </summary>
        /// <param name="databaseconnectionString"></param>
        public CitySqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        //get a list of all spaces within venue
        //list by id
        //include Space Name, when it opens, when it closes,
        //the daily $ rate and the max occupancy.
        //finally displays menu to either reserve space or return to previous screen.
        private City ConvertReaderToCityState(SqlDataReader reader)
        {
            City city= new City();

            city.cityName = Convert.ToString(reader["name"]);
            city.stateAbreviation = Convert.ToString(reader["state_abbreviation"]);

            return city;
        }

        public List<City> GetCityState(int id)
        {
            List<City> cityState = new List<City>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql_GetSpacesByVenueID, conn))
                    {
                        cmd.Parameters.AddWithValue("@venue_id", id);

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            City result = ConvertReaderToCityState(reader);
                            cityState.Add(result);

                        }

                    }
                    return cityState;
                }
            }

            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                cityState = new List<City>();
            }

            return cityState;
        }



    }
}
