using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class UserInterface // all info for console.read/write lines
    {
        public UserInterface(string connectionString) // create classes level variable, create instances of DAL
        {
        }

        public void Run()
        {
            Console.WriteLine("What would you like to do?");
            Console.ReadLine();
        }
    }
}
