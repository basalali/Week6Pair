using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    class VenueSqlDAL
    {

        //get all venues provided a space_id

        private string connectionString;
        private string sql_GetVenueName = "SELECT name FROM venue";
        private string sql_GetVenueDetails = "SELECT venue.name, city.name, abbreviation, category.name, description FROM venue" +
            "JOIN city ON city.id = venue.city_id" +
            "JOIN state ON state.abbreviation = city.state_abbreviation" +
            "JOIN category_venue ON category_venue.venue_id = venue.id" +
            "JOIN category ON category.id = category_venue.category_id " +
            "WHERE venue_id = '???'";

        /// <summary>
        /// Creates a new sql-based venue dao.
        /// </summary>
        /// <param name="databaseconnectionString"></param>
        public VenueSqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

    //returns list of venue names
        public IList<Venue> GetVenueName()
        {
            IList<Venue> venues = new List<Venue>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql_GetVenueName, conn))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Venue venue = new Venue();
                            venue.name = Convert.ToString(reader["name"]);

                            venues.Add(venue);
                        }

                    }

                }
            }
            catch
            {
                venues = new List<Venue>();
            }
            return venues;
        }

        //returns "list" information about selected venue





    }
}
