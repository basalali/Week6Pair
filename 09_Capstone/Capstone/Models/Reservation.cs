using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    class Reservation
    {
        public int reservationId { get; set; }
        public int spaceId { get; set; }
        public int numberOfAttendees { get; set; }
        public int startDate { get; set; }
        public int endDate { get; set; }
        public string reservedFor { get; set; }

    }
}
