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

            //afasf 
        int selection;

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
            Console.WriteLine("Welcome to Excelsior Venues, please hit enter to continue!");

            string initialSelection = Console.ReadLine();
            Console.WriteLine();

            while (initialSelection != "3")
            {
                PrintHeader();
                initialSelection = Console.ReadLine();            

                switch (initialSelection)
                {
                    case "1":
                        GetVenueName(); // this method gets the names of all venues
                        Console.ReadLine();
                        break;
                    case "2":
                        GetVenueDetails(); // this will list the details of that venue -- searching by ID.
                        Console.ReadLine();
                        break;
                    case "3":
                      Console.WriteLine("Thank you for using the Excelsior Venues systems");
                      return;
                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        break;


                }
            }
        }
            
          

        private void PrintHeader()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine();
            Console.WriteLine("1) List Venues");
            Console.WriteLine("2) Get Details of Venue");
            Console.WriteLine("3) Quit");
        }

        private void GetVenueName() // gets list of all venue names
        {
            List<Venue> venues = venueDAL.GetVenueName();
            Console.WriteLine("List Of All Venues");
            Console.WriteLine();
            if (venues.Count > 0)
            {
                foreach (Venue ven in venues)
                {
                    Console.WriteLine(ven.venue_id.ToString() + ") " + ven.name);
                   
                }
                Console.WriteLine("16) *****Return to previous window*****");
            
            }
            else
            {
                Console.WriteLine("No result! Please try again.");
                return;
            }         
        }

        private void GetVenueDetails() //gets details of that venue
        {
            selection = Convert.ToInt32(UserInterfaceHelper.GetInteger("Enter the ID of the venue you want to search: "));
            Console.WriteLine();
            // Category catergory =  .GetCatergoyName 
            // Space 

            if (selection >= 1 && selection <= 15)
            {
                Venue venue = venueDAL.GetVenueDetails(selection);

                    Console.WriteLine(venue.name.PadRight(15));
                    Console.WriteLine();
                    Console.WriteLine("Location: " ); 
                    Console.WriteLine("Categories: " );
                    Console.WriteLine();
                    Console.WriteLine(venue.description);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("What would you like to do next ?" );
                    Console.WriteLine("1) View Spaces");
                    Console.WriteLine("2  Search for Reservation");
                    Console.WriteLine("3) Return to Previous Screen");
                   
            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
            }


        }
    }



}


