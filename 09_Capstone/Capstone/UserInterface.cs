using System;
using System.Collections.Generic;
using System.Text;
using Capstone;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone
{
    public class UserInterface // all info for console.read/write lines
    {

        // create classes level variable, create instances of DAL

        const string Command_GetAllVenues = "1";
        const string Command_VenueDetails = "2";
        const string Command_Quit = "Q";

        private SpaceSqlDAL spaceDAL;
        private IVenueDAO venueDAL;
        public UserInterface()
            {
     
            }


        public UserInterface(string connectionString)
        { 
          
            this.spaceDAL = new SpaceSqlDAL(connectionString);
            this.venueDAL = new VenueSqlDAL(connectionString);
            //CategorySqlDAL categoryDAL = new CategorySqlDAL(connectionString);
            //ReservationSqlDAL reservationDAL = new ReservationSqlDAL(connectionString)

        }

        public void RunInterface()
        {
            PrintHeader();

            while (true)
            {
                string command = Console.ReadLine();

                Console.Clear();
                switch (command.ToLower())
                {
                    case Command_GetAllVenues:
                        GetVenueName();
                        Console.ReadLine();
                        break;


                }
            }

        }

        private void PrintHeader()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine();
            Console.WriteLine("1) List Venues");
            Console.WriteLine("Q Quit");
        }

        private void GetVenueName()
        {
            IList<Venue> venues = venueDAL.GetVenueName();
            Console.WriteLine();
            Console.WriteLine("Printing all of the venues!");
            if (venues.Count > 0)
            {
                foreach (Venue ven in venues)
                {
                    Console.WriteLine(ven.venue_id.ToString() + ") " + ven.name);
                }
            }
            else
            {
                Console.WriteLine("No result!");
            }
        }
    }



}


