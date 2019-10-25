using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class VenueSqlDAL : IVenueDAO
    {

        //get all venues provided a space_id

        private string connectionString;
        private string sql_GetVenueName = "SELECT * FROM venue";
        private string sql_GetVenueDetails = "SELECT venue.name, city.name, abbreviation, category.name, description FROM venue" +
            "JOIN city ON city.id = venue.city_id" +
            "JOIN state ON state.abbreviation = city.state_abbreviation" +
            "JOIN category_venue ON category_venue.venue_id = venue.id" +
            "JOIN category ON category.id = category_venue.category_id " +
            "WHERE venue_id = @id";

      
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
                            Venue venue = ConvertReaderToVenue(reader);
                     
                            venues.Add(venue);
                        }
                        
                    }
                    return venues;

                }
            }
            catch(SqlException)
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

        //public IList<Venue> GetVenueDetails(int id)
        //{
        //    IList<Venue> venues = new List<Venue>();

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            // column    // param name  
        //            SqlCommand cmd = new SqlCommand(sql_GetVenueDetails, conn);                  
                    
        //            // param name    // param value
        //            cmd.Parameters.AddWithValue("@venue_id", id);

        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                Venue vnu = ConvertReaderToVenue(reader);
        //                venues.Add(vnu);
        //            }

        //            return venues;
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine("An error occurred reading venues by ID.");
        //        Console.WriteLine(ex.Message);
        //        throw;
        //    }

            public IList<Venue> GetVenueDetails(int id)
            {
                IList<Venue> venueDetails = new List<Venue>();
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand(sql_GetVenueDetails, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                         
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                Venue vnu = new Venue();
                            vnu.venue_id = Convert.ToInt32(reader["id"]);
                            vnu.name = Convert.ToString(reader["name"]);
                            vnu.cityId = Convert.ToInt32(reader["city_id"]);
                            vnu.description = Convert.ToString(reader["description"]);

                           venueDetails.Add(vnu);
                            }
                            return venueDetails;
                        }
                    }
                }

                catch
                {
                    venueDetails= new List<Venue>();
                }

                return venueDetails;
            }

        
    }
}
