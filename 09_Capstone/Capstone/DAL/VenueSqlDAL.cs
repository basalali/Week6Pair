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

        private string connectionString;
        private string sql_GetVenueName = "SELECT name FROM venue";

        /// <summary>
        /// Creates a new sql-based venue dao.
        /// </summary>
        /// <param name="databaseconnectionString"></param>
        public VenueSqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }
        //List<Venue> GetVenueName();
        //List<Venue> GetVenueName(string name);
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
        private Venue ConvertReaderToVenue(SqlDataReader reader)
        {
            Venue vnu = new Venue();

            vnu.venue_id = Convert.ToInt32(reader["venue_id"]);
            vnu.name = Convert.ToString(reader["name"]);
            vnu.cityId = Convert.ToInt32(reader["city_id"]);
            vnu.description = Convert.ToString(reader["description"]);

            return vnu;
        }
        
        public List<Venue> GetVenueName()
        {
            try
            {

            }
        }





    }
}
