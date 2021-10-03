using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace dotNet5781_01_0771_7072
{

    class Program
    {
        static void Main(string[] args) // the main
        {
            MainMenu busesProgram = new MainMenu();
            Console.WriteLine("Welcome to our bus system management!\n"); // calls to the class busServices and shows the option to the user
            busesProgram.BusesOptions();
        }
    }
}
