using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class SpaceSqlDAL
    {

   

        private string connectionString;
        private string sql_GetSpaces = "SELECT name, open_from, open_to, daily_rate, max_occupancy from space WHERE venue_id = @venue_id";

        /// <summary>
        /// Creates a new sql-based space DAL.
        /// </summary>
        /// <param name="databaseconnectionString"></param>
        public SpaceSqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        //get a list of all spaces within venue
        //list by id
        //include Space Name, when it opens, when it closes,
        //the daily $ rate and the max occupancy.
        //finally displays menu to either reserve space or return to previous screen.
       
        public List<Space> GetSpaceDetails(int id)
        {
            List<Space> space = new List<Space>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql_GetSpaces, conn))
                    {
                        cmd.Parameters.AddWithValue("@venue_id", id);

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Space result = ConvertReaderToSpace(reader);
                            space.Add(result);

                        }

                    }
                    return space;
                }
            }

            catch
            {
                space = new List<Space>();
            }

            return space;
        }

        private Space ConvertReaderToSpace(SqlDataReader reader)
        {
            Space space = new Space();
            space.venue_id = Convert.ToInt32(reader["venue_id"]);
            space.id = Convert.ToInt32(reader["id"]);
            space.name = Convert.ToString(reader["name"]);
            space.isAccessbile = Convert.ToInt32(reader["is_accessible"]);
            space.openFrom = Convert.ToInt32(reader["open_from"]);
            space.openTo = Convert.ToInt32(reader["open_to"]);
            space.dailyRate = Convert.ToDouble(reader["daily_rate"]);
            space.maxOccupancy = Convert.ToInt32(reader["max_occupancy"]);

            return space;
        }

    }
}
