using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
   public class Space
    {
        public int id;

        public int venue_id;

        public string name;

        public int isAccessbile;

        public int openFrom;

        public int openTo;

        public double dailyRate;

        public int maxOccupancy;

        public override string ToString()
        {
            return String.Format("{0, -5} {1, -30} {2, -15} {3, -15} {4, -15}", name, openFrom, openTo, dailyRate, maxOccupancy);
        }

    }
}
