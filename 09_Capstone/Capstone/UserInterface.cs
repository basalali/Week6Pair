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
               // PrintHeader();
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
            List<Venue> venues = venueDAL.GetVenueName();
            Console.WriteLine();
            Console.WriteLine();
            if (venues.Count > 0)
            {
                foreach (Venue ven in venues)
                {
                    Console.WriteLine(ven.venue_id.ToString() + ") " + ven.name);
                   
                }
                Console.WriteLine("R) Return to previous window");
                Console.WriteLine();
              
            }
            else
            {
                Console.WriteLine("No result! Please try again.");
                return;
            }         
        }

        private void GetVenueDetails()
        {
            int id = UserInterfaceHelper.GetInteger("Enter the ID of the venue you want to search!");
           // Venue venue = venueDAL.GetVenueDetails(id);
           // Category catergory =  .GetCatergoyName 
            //Space 

            if (id >= 1 && id <= 15)
            {
                Venue venue = venueDAL.GetVenueDetails(id);

                Console.WriteLine(venue.name.PadRight(15));
                    Console.WriteLine();
                    //Console.WriteLine("Location" + cityname, stat abbrev); 
                    //Console.WriteLine("Categories: " ); from catergories 
                    Console.WriteLine(venue.description);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("What would you like to do next ?" );
                    Console.WriteLine("1) View Spaces");
                    Console.WriteLine("2 Search for Reservation");
                    Console.WriteLine("R) Return to Previous Screen");
                   
            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
            }


        }
    }



}


