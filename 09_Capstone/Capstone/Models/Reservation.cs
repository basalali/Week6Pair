using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Reservation
    {
        public int reservationId { get; set; }
        public int spaceId { get; set; }
        public int numberOfAttendees { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string reservedFor { get; set; }
        public double totalCost { get; set; }
        public int howManyDays { get; set; }

    }
}
