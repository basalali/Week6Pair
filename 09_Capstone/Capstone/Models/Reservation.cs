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

        //public int Space_Id { get; set; }

        //public string Venue_Name { get; set; }

        //public decimal Daily_Rate { get; set; }

        //public int Number_Of_Attendees { get; set; }

        //public bool Is_Accessible { get; set; }

        //public decimal Total_Cost { get; set; }

        //public DateTime Start_Date { get; set; }

        //public DateTime End_Date { get; set; }

        //public string Reserved_For { get; set; }

        //public Int32 Confirmation_Number { get; set; }

        //public string Space_Name { get; set; }
    }
}
