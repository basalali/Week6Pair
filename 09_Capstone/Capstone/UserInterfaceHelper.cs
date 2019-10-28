using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class UserInterfaceHelper
    {
        public static DateTime GetDateTime(string message)
        {
            string userInput = String.Empty;
            DateTime dateValue = DateTime.MinValue;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid date format. Please try again");
                }

                Console.Write(message + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (!DateTime.TryParse(userInput, out dateValue));

            return dateValue;
        }

        public static int GetInteger(string message)
        {
            string userInput = String.Empty;
            int intValue = 0;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }

                Console.Write(message + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (!int.TryParse(userInput, out intValue));

            return intValue;

        }


        public static double GetDouble(string message)
        {
            string userInput = String.Empty;
            double doubleValue = 0.0;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }

                Console.Write(message + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (!double.TryParse(userInput, out doubleValue));

            return doubleValue;

        }

        public static bool GetBool(string message)
        {
            string userInput = String.Empty;
            bool boolValue = false;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }

                Console.Write(message + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (!bool.TryParse(userInput, out boolValue));

            return boolValue;
        }

        public static string GetString(string message)
        {
            string userInput = String.Empty;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }

                Console.Write(message + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (String.IsNullOrEmpty(userInput));

            return userInput;
        }

        public static string ConvertIntToMonth(int date)
        {
            string input = string.Empty;

            if (date == 1)
            {
                input = "Jan";
            }
            else if (date == 2)
            {
                input = "Feb";
            }
            else if (date == 3)
            {
                input = "Mar";
            }
            else if (date == 4)
            {
                input = "Apr";
            }
            else if (date == 5)
            {
                input = "May";
            }
            else if (date == 6)
            {
                input = "Jun";
            }
            else if (date == 7)
            {
                input = "July";
            }
            else if (date == 8)
            {
                input = "Aug";
            }
            else if (date == 9)
            {
                input = "Sep";
            }
            else if (date == 10)
            {
                input = "Oct";
            }
            else if (date == 11)
            {
                input = "Nov";
            }
            else if (date == 12)
            {
                input = "Dec";
            }
            else
            {
                input = "";
            }


            return input;
        }

        public static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


    }
}
