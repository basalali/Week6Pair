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
        private string sql_GetSpaces = "SELECT * from space WHERE venue_id = @venue_id;";

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
 
                            if (reader["open_from"] != DBNull.Value)
                            {
                                Space result = new Space();

                                result.id = Convert.ToInt32(reader["id"]);
                                result.venue_id = Convert.ToInt32(reader["venue_id"]);
                                result.name = Convert.ToString(reader["name"]);
                                result.isAccessbile = Convert.ToBoolean(reader["is_accessible"]);
                                result.openFrom = Convert.ToInt32(reader["open_from"]);
                                result.openTo = Convert.ToInt32(reader["open_to"]);
                                result.dailyRate = Convert.ToDouble(reader["daily_rate"]);
                                result.maxOccupancy = Convert.ToInt32(reader["max_occupancy"]);

                                space.Add(result);
                            }
                            else
                            {
                                Space result = new Space();
                                result.id = Convert.ToInt32(reader["id"]);
                                result.venue_id = Convert.ToInt32(reader["venue_id"]);
                                result.name = Convert.ToString(reader["name"]);
                                result.isAccessbile = Convert.ToBoolean(reader["is_accessible"]);
                                result.dailyRate = Convert.ToDouble(reader["daily_rate"]);
                                result.maxOccupancy = Convert.ToInt32(reader["max_occupancy"]);

                                space.Add(result);
                            }
             

                        }

                    }
                    return space;
                }
            }

            catch(SqlException ex)
            {
                space = new List<Space>();
                Console.WriteLine(ex.Message);
            }

            return space;
        }

  

    }
}
