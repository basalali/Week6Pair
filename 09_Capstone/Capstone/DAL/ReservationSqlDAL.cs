using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public class ReservationSqlDAL
    {
        private string connectionString;
        private int venue_id = 1;
        private string sql_GetVenueId = "SELECT * FROM space WHERE venue_id = '???'";
        private string sql_GetSpaceId = "SELECT * FROM reservation WHERE space_id <= 7";

        public ReservationSqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }
    }
}
