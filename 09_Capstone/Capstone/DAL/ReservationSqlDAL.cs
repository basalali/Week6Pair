using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public class ReservationSqlDAL
    {
        private string connectionString;
        private int venue_id = 1;

        public ReservationSqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }
    }
}
