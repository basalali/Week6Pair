using System;
using System.Collections.Generic;
using System.Text;


namespace Capstone.Models
{
   public class Venue
    {


        public int venue_id { get; set; }

        public string name { get; set; } = "";

        public int cityId { get; set; }

        public string description { get; set; } = "";

        public override string ToString()
        {
            return venue_id.ToString() + ") ".PadRight(15) + name.PadRight(20);
        }

    }
}
