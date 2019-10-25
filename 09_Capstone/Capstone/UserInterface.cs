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
        const string Command_PreviousMenu = "R";

        private SpaceSqlDAL spaceDAL;
        private VenueSqlDAL venueDAL;
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
                        GetVenueName(); // this method gets the names of all venues
                        Console.ReadLine();
                        break;
                    case Command_VenueDetails:
                        GetVenueDetails(); // pressing 2 will list the details of that venue -- searching by ID.
                        Console.ReadLine();
                        break;
                    case Command_Quit:
                        Console.WriteLine("Thank you for using the venue system");
                        return;
                    case Command_PreviousMenu:
                        return;
                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        break;



                }
                PrintHeader();
            }

        }

        private void PrintHeader()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine();
            Console.WriteLine("1) List Venues");
            Console.WriteLine("Q) Quit");
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
                Console.WriteLine("R) Return to previous window");
                //Console.WriteLine();
                Console.WriteLine("****) Press 2 to get details of the Venue");
            }
            else
            {
                Console.WriteLine("No result! Please try again.");
                return;
            }         
        }

        private void GetVenueDetails()
        {
            int id = CLIHelper.GetInteger("Enter the ID of the venue you want to search!");
            IList<Venue> venue = venueDAL.GetVenueDetails(id);

            if (venue.Count > 0)
            {
                foreach (Venue ven in venue)
                {
                    Console.WriteLine(ven.name.PadRight(15));
                    Console.WriteLine();
                    Console.WriteLine(ven.cityId);
                    Console.WriteLine(ven.description);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("What would you like to do next ?" );
                    Console.WriteLine("1) View Spaces");
                    Console.WriteLine("2 Search for Reservation");
                    Console.WriteLine("R) Return to Previous Screen");
                    }
            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
            }


        }
    }



}


