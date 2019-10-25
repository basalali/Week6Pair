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

        //get all venues

        private string connectionString;
        private string sql_GetSpaces = "SELECT * FROM space ";

        /// <summary>
        /// Creates a new sql-based venue dao.
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
        public List<Space> GetSpaces()
        {
            List<Space> spaces = new List<Space>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql_GetSpaces, conn))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Space space = new Space();
                            space.id = Convert.ToInt32(reader["space_id"]);
                            space.name = Convert.ToString(reader["Name"]);
                            space.isAccessbile = Convert.ToInt32(reader["is_accessible"]);
                            space.openFrom = Convert.ToInt32(reader["open_from"]);
                            space.openTo = Convert.ToInt32(reader["open_to"]);
                            space.dailyRate = Convert.ToDouble(reader["daily_rate"]);
                            space.maxOccupancy = Convert.ToInt32(reader["max_occupancy"]);

                            spaces.Add(space);
                        }

                    }

                }
            }
            catch
            {
                spaces = new List<Space>();
            }
            return spaces;
        }



    }
}
