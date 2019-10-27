using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
   public class Space
    {
        public int id { get; set; }

        public int venue_id { get; set; }

        public string name { get; set; } 

        public bool isAccessbile { get; set; }

        public int openFrom { get; set; } 

        public int openTo { get; set; }

        public double dailyRate { get; set; }

        public int maxOccupancy { get; set; }

        public override string ToString()
        {
            return String.Format("{0, -5} {1, -5} {2, -30} {3, -15} {4, -15} {5, -15} {6, -15} {7, -15}", id, venue_id, name, openFrom, isAccessbile, openTo, dailyRate, maxOccupancy);
        }

    }
}
