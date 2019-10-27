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


        private string sql_MakeReservation = "INSERT into reservation ()";
        private string sql_GetAvailableSpaces = "SELECT* FROM space s WHERE venue_id = @venue_id AND s.id NOT IN " +
            "(SELECT s.id from reservation r JOIN space s on r.space_id = s.id WHERE s.venue_id= @venue_id " +
            "AND r.end_date >= @req_from_date AND r.start_date <= @req_to_date ";

        public ReservationSqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public List<Reservation> MakeReservation()
        {
            List<Reservation> reservations = new List<Reservation>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql_GetAvailableSpaces, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Reservation reservation = new Reservation();
                            reservation.reservationId = Convert.ToInt32(reader["reservation_id"]);
                            reservation.spaceId = Convert.ToInt32(reader["space_id"]);
                            reservation.numberOfAttendees = Convert.ToInt32(reader["number_of_attendies"]);
                            reservation.startDate = Convert.ToInt32(reader["start_date"]);
                            reservation.endDate = Convert.ToInt32(reader["end_date"]);
                            reservation.reservedFor = Convert.ToString(reader["reserved_for"]);
                            //reservation.totalCost = Convert.ToInt32(reader[]);
                            reservations.Add(reservation);
                        }

                    }
                }
            }
            catch
            {
                reservations = new List<Reservation>();
            }
            return reservations;
        }
    }
}
