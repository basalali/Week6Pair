using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Capstone.DAL
{
    public class ReservationSqlDAL
    {
        private string connectionString;


        private string sql_MakeReservation = "INSERT into reservation (space_id, start_date, end_date, reserved_for) VALUES(@id, @start, @end, @reservation_for)";
      

        public ReservationSqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        

        public int MakeReservation(int id, DateTime start, DateTime end, string reservation_for)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql_MakeReservation, conn))
                    {

                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@start", start);
                        cmd.Parameters.AddWithValue("@end", end);
                        cmd.Parameters.AddWithValue("@reservation_for", reservation_for);

                        result = cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (SqlException)
            {
                return result;

            }
            return result;
        }

    }
}
