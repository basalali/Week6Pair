using System;
using System.Collections.Generic;
using System.Text;
using Capstone;
using Capstone.Models;
using Capstone.DAL;
using System.Linq;

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

            string initialSelection = "";
            Console.WriteLine();

            while (initialSelection != "6")
            {
                PrintHeader();
                initialSelection = Console.ReadLine();

                switch (initialSelection)
                {
                    case "1":
                        GetVenueName(); // this method gets the names of all venues
                        Console.WriteLine();
                        break;
                    case "2":
                        GetVenueDetails(); // this will list the details of that venue -- searching by ID.
                        Console.WriteLine();
                        break;
                    case "3":
                        GetSpaces(); // view spaces by ID.
                        Console.WriteLine();
                        break;
                    case "4":
                        CheckAvailableSpaces();
                        Console.WriteLine();
                        // search for availability -- only requires the desired space, a start date, and an end date
                        //  a space is unavailable if any part of their preferred date range overlaops with an existing reservation
                        // if no spaces are available, indicate to user that are no available spaces and ask them if they would like to try a different search,
                        // if they say yes, restart the search dialog.
                        break;
                    case "5":
                        MakeReservation();
                        Console.WriteLine();
                        break;
                    case "6":
                        break;
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
            Console.WriteLine("2) Venue Details");
            Console.WriteLine("3) View Spaces for a Specific Venue");
            Console.WriteLine("4) Search for Reservation");
            Console.WriteLine("5) Reserve a Space");
            Console.WriteLine("6) Quit");
        }

        private void ListVenueSpacesHeader()
        {
            Console.WriteLine(String.Format("{0, -10} {1, -25} {2, -15} {3, -15} {4, -15} {5, -15}", "Space #", "Name", "Open", "Close", "Daily Rate", "Max. Occupancy"));
        }
        
        private void GetVenueName() // gets list of all venue names
        {
            Console.WriteLine();
            List<Venue> venues = venueDAL.GetVenueName();
            Console.WriteLine(String.Format("{0, -10} {1, -10}", "ID", "Venue Name"));
            Console.WriteLine();
            if (venues.Count > 0)
            {
                foreach (Venue ven in venues)
                {
                    Console.WriteLine(String.Format("{0, -10} {1, -10}", ven.venue_id, ven.name));
                }
                Console.WriteLine();
                Console.WriteLine(" *****Please press enter to continue*****");
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

                Console.WriteLine(venue.name);
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
        private void GetSpaces()
        {
            selection = Convert.ToInt32(UserInterfaceHelper.GetInteger("Enter the ID of the venue to view spaces available: "));
            Console.WriteLine();
            ListVenueSpacesHeader();
            List<Space> spaces = spaceDAL.GetSpaceDetails(selection);
            if (selection > 0 && selection <= 15)
            {               
                foreach(Space item in spaces)
                {
                    string date1 = UserInterfaceHelper.ConvertIntToMonth(item.openFrom);
                    string date2 = UserInterfaceHelper.ConvertIntToMonth(item.openTo);
                    
                    Console.WriteLine(String.Format("{0, -10} {1, -25} {2, -15} {3, -15} {4, -15} {5, -15}", item.id, item.name, date1, date2, item.dailyRate, item.maxOccupancy));
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("No result, please try again!");
            }
        }

        private void MakeReservation()
        {

            int resSpaceId = CLIHelper.GetInteger("Please enter the ID of the space you would like to reserve:");
            Console.WriteLine();
            DateTime startDates = CLIHelper.GetDateTime("When do you need the space from ? (MM/DD/YYYY)");
            Console.WriteLine();
            DateTime endDates = CLIHelper.GetDateTime("When do you need the space until ? (MM/DD/YYYY)");
            Console.WriteLine();
            int numOfPeople = CLIHelper.GetInteger("How many people will be in attendance? ");
            Console.WriteLine();
            string reserved_for = CLIHelper.GetString("Please enter the name of person or group reserving the space: ");
            Console.WriteLine();
            Console.WriteLine();
            Reservation newRes = new Reservation
            {
                spaceId = resSpaceId,
                startDate = startDates,
                endDate= endDates,
                reservedFor = reserved_for
            };
            int dal = reservationDAL.MakeReservation(resSpaceId, startDates, endDates, reserved_for);
            if (dal > 0)
            {
                Space space = spaceDAL.GetASpaceName(resSpaceId);
                Venue venue = venueDAL.GetVenueNameGivenSpaceId(resSpaceId);
                Console.WriteLine(String.Format("{0, -15}", "Thanks for submitting your reservation! The details for your event are listed below: "));
                    Console.WriteLine();
                    Console.WriteLine("Confirmation #: " + RandomString(resSpaceId));
                    Console.WriteLine("Venue: " + venue.name);
                    Console.WriteLine("Space: " + space.name);
                    Console.WriteLine("Reserved For: " + reserved_for);
                    Console.WriteLine("Attendees: " + numOfPeople);
                    Console.WriteLine("Arrival Date: " + Convert.ToDateTime(startDates));
                    Console.WriteLine("Departure Date: " + Convert.ToDateTime(endDates));
                    Console.WriteLine("Total Cost: $" + space.dailyRate);

            }
            else
            {
                Console.WriteLine("*** DID NOT CREATE ***");
            }
        }

        private void CheckAvailableSpaces()
        {
            int resSpaceId = CLIHelper.GetInteger("Please enter the ID of the venue you would like to reserve:");
            Console.WriteLine();

            DateTime startDates = CLIHelper.GetDateTime("When do you need the space from ? (MM/DD/YYYY)");
            Console.WriteLine();
            DateTime endDates = CLIHelper.GetDateTime("When do you need the space until ? (MM/DD/YYYY)");

            Console.WriteLine();


            List<Space> spaces = spaceDAL.CheckAvailableSpaces(resSpaceId, startDates, endDates);

            if (spaces.Count > 0)
            { Console.WriteLine("The following spaces are available based on your needs: ");
                Console.WriteLine();
                Console.WriteLine(String.Format("{0, -10} {1, -25} {2, -15} {3, -15}", "Space #", "Name", "Daily Rate", "Max. Occupancy"));
                foreach (Space space in spaces)
                { 
          
                 Console.WriteLine(String.Format("{0, -10} {1, -25} {2, -15} {3, -15}", space.id, space.name, space.dailyRate, space.maxOccupancy));
        
                }
            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
            }


        }


        public static Random random = new Random();
        public static string RandomString(int length)
        {
            length = 10;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }



    }
}


