using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_0771_7072
{
    enum Options { INSERT, DRIVE, MAINTENANCE, BUSTRAVELED, EXIT }; // the different choices that the user can choose
    class MainMenu
    {
        busService Buses;
        public MainMenu()
        {
            Buses = new busService();
        }
        private Options AskForChoice()// function to get an order from the user
        {
            int choice;
            Console.WriteLine // @ is a sign that keeps the spaces of what will be printed, the different choices that the user adds
(@"what do you want to do?: 
0: to add a bus to the system
1: to travel with a bus
2: to fuel or do treatment to a bus
3: to get all the buses travel
4: to exit from the program");
            choice = int.Parse(Console.ReadLine());
            return (Options)choice;
        }
        public void BusesOptions()
        {
            Options choice = AskForChoice();
            while (choice != Options.EXIT)
            {
                switch (choice)
                {
                    case Options.INSERT:
                        Buses.InsertBus(); // calls the function that insert a bus
                        break;
                    case Options.DRIVE:
                        Buses.Drive(); // checks if the bus can drive
                        break;
                    case Options.MAINTENANCE:
                        Console.WriteLine("please enter the bus number:\n");
                        string busNum = Console.ReadLine();
                        Buses.FuelingOrCareBus(busNum); // the option is to refuel the car
                        break;
                    case Options.BUSTRAVELED:
                        Buses.PrintListKillometers(); // this function prints the list of each bus and is num of kilometers
                        break;
                    case Options.EXIT:
                        break;
                    default:
                        break;
                }
                choice = AskForChoice();
            }
        }
    }

}
