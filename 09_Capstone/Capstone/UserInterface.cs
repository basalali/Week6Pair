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
        private CategorySqlDAL categoryDAL;

        private ReservationSqlDAL reservationDAL;

        private CitySqlDAL cityDAL;

        public UserInterface()
        {

        }


        public UserInterface(string connectionString)
        {

            this.spaceDAL = new SpaceSqlDAL(connectionString);
            this.venueDAL = new VenueSqlDAL(connectionString);
            this.categoryDAL = new CategorySqlDAL(connectionString);
            this.reservationDAL = new ReservationSqlDAL(connectionString);

            this.cityDAL = new CitySqlDAL(connectionString);

        }

        public void RunInterface()
        {
            Console.WriteLine("Welcome to Excelsior Venues, please hit enter to continue!");

            string initialSelection = Console.ReadLine();
            Console.WriteLine();


            while (initialSelection != "4")
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
                        GetSpaces(selection);
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

        private void ListVenueSpacesHeader()
        {
            Console.WriteLine(String.Format("{0, -10} {1, -15} {2, -15} {3, -15} {4, -15} {5, -15}", "Space #", "Name", "Open", "Close", "Daily Rate", "Max. Occupancy"));
        }

        public void SpacesInterface()
        {
            string spacesSelection = "";
            while (spacesSelection != "3")
            ListVenueSpacesHeader();
            {
                switch (spacesSelection)
                {
                    case "1":
                        //GetSpace();
                        break;
                    case "2":
                        // Search for Reservation
                        break;
                    case "3":
                        // Return to Previous screen
                        break;
                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        break;

                }
            }        
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
            Venue venue = venueDAL.GetVenueDetails(selection);

            if (selection <= 15 && selection >= 0)
            {
             
                Console.WriteLine(venue.name.ToString());
                Console.WriteLine();
                Console.Write("Location: ");
                GetCityAndStateAbbrev(selection);
                Console.WriteLine();
                Console.Write("Category: ");
                GetCategory(selection);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(venue.description);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("What would you like to do next ?");
                Console.WriteLine("1) View Spaces");
                Console.WriteLine("2  Search for Reservation");
                Console.WriteLine("3) Return to Previous Screen");

            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
            }

        }

        private void GetCategory(int selection) // gets list of all venue names
        {
            List<Category> categories = categoryDAL.GetCategories(selection);

            if (categories.Count > 0)
            {
                foreach (Category cat in categories)
                {
                    Console.Write(cat.category_name + ", ");

                }

            }
            else
            {
                Console.WriteLine("No result! Please try again.");
                return;
            }
        }

        private void GetCityAndStateAbbrev(int selection)
        {
            List<City> cityState = cityDAL.GetCityState(selection);

            if (cityState.Count > 0)
            {
                foreach (City item in cityState)
                {
                    Console.Write(item.cityName + ", " + item.stateAbreviation);

                }

            }
        }

        private void GetSpaces(int selection)
        {
            List<Space> spaces = spaceDAL.GetSpaceDetails(selection);
            selection = Convert.ToInt32(UserInterfaceHelper.GetInteger("Enter the ID of the venue you want to search: "));
            Console.WriteLine();

            if (spaces.Count > 0)
            {
                foreach(Space item in spaces)
                {
                    Console.WriteLine(String.Format("{0, -5} {1, -30} {2, -15} {3, -15} {4, -15}", item.name, item.openFrom, item.openTo, item.dailyRate, item.maxOccupancy));
                }
            }
            else
            {
                Console.WriteLine("*** NO RESULT ***");
            }


        }

    }
}


