using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class VenueSqlDAL
    {

        //get all venues provided a space_id
        //venue.name, city.name, abbreviation, category.name, description
        private string connectionString;
        private string sql_GetVenueName = "SELECT id, name FROM venue group by name, id order by name";
        private string sql_GetVenueDetails = "SELECT * FROM venue " +
            "JOIN city ON city.id = venue.city_id " +
            "JOIN state ON state.abbreviation = city.state_abbreviation " +
            "JOIN category_venue ON category_venue.venue_id = venue.id " +
            "JOIN category ON category.id = category_venue.category_id " +
            "WHERE venue_id = @venue_id ";


        public VenueSqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        //returns list of venue names
        public List<Venue> GetVenueName()
        {
            List<Venue> venues = new List<Venue>();
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

                            venue.venue_id = Convert.ToInt32(reader["id"]);
                            venue.name = Convert.ToString(reader["name"]);
                            //venue.cityId = Convert.ToInt32(reader["city_id"]);
                            //venue.description = Convert.ToString(reader["description"]);
                            venues.Add(venue);
                        }

                    }
                    return venues;

                }
            }
            catch (SqlException)
            {
                venues = new List<Venue>();
            }
            return venues;
        }
        //returns "list" information about selected venue
        private Venue ConvertReaderToVenue(SqlDataReader reader)
        {
            Venue vnu = new Venue();

            vnu.venue_id = Convert.ToInt32(reader["id"]);
            vnu.name = Convert.ToString(reader["name"]);
            vnu.cityId = Convert.ToInt32(reader["city_id"]);
            vnu.description = Convert.ToString(reader["description"]);

            return vnu;
        }

        public Venue GetVenueDetails(int id)
        {
            Venue vnu = new Venue();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql_GetVenueDetails, conn))
                    {
                        cmd.Parameters.AddWithValue("@venue_id", id);

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Venue result = ConvertReaderToVenue(reader);
                            vnu = result;

                        }

                    }
                    return vnu;
                }
            }

            catch
            {
                vnu = new Venue();
            }

            return vnu;
        }



    }
}
