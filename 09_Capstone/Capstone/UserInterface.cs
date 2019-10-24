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
        const string Command_GetAllVenues = "1";
        const string Command_VenueDetails = "2";
        const string Command_Quit = "Q";


        private SpaceSqlDAL spaceDAL;
        private VenueSqlDAL venueDAL;

        public UserInterface(SpaceSqlDAL spaceDAL, VenueSqlDAL venueDAL)
        { 
            this.spaceDAL = spaceDAL;
            this.venueDAL = venueDAL;
        }

        public void RunInterface()
        {
            //PrintHeader();

            while (true)
            {
                string command = Console.ReadLine();

                Console.Clear();
                switch (command.ToLower())
                {
                    case Command_GetAllVenues:
                        GetVenueName();
                        break;


                }
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
        List<Venue> venues = VenueSqlDAL.GetVenueName
    }


}


