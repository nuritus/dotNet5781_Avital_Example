using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_0771_7072
{
    class Bus
    {
        public string BusNumber { get; } // The license plate number of the bus, it can't be changed
        public DateTime FirstDateUse { get; } // The first date when the bus was used, can't be changed
        public DateTime LastCareDate { set; get; } // The date of the last care of the bus
        public int KilloFromLastCare { set; get; } // number of kilometers of the bus from the last care, over 20 000 it's dangerous
                                                   //  private int SumOfKilometers =0 ; // sum of the kilometers of all the buses 
        public int SumOfKilometers { set; get; }  // how much kilometers the bus travel total
        public int KilometersWithFuel { private set; get; } // The number of kilometers that last to the bus until the next time it gets fuel
                                                            // methods:
        public Bus(string myPlateNumber, DateTime myFirstDateUse)
        {
            BusNumber = myPlateNumber;
            FirstDateUse = new DateTime(myFirstDateUse.Year, myFirstDateUse.Month, myFirstDateUse.Day);
            LastCareDate = DateTime.Now; // if there is a new bus without last care date, we are doing a care now
            KilloFromLastCare = 0;
            KilometersWithFuel = 1200; // The fuel is completely full
            SumOfKilometers = 0;
        } // constructor-> platenumber, firstdate
        public void travel(int myNumOfKilometers) // checks if the bus can travel according to 3 conditions
        {
            bool canTravel = true; // a variable that shows if the bus can travel

            if (checkDangerous(myNumOfKilometers)) // calls the help function that checks if the number of kilometers travelled by the bus is dangerous
            {
                Console.WriteLine("ERROR: The bus travelled to much kilometers, it's dangerous\n"); // prints the suited message
                canTravel = false;
            }
            else if ((DateTime.Now-LastCareDate).Days > 665) // checks if the bus has to do a maintenance, if it does, it can't travel
            {
                Console.WriteLine("ERROR: The bus has to be cared\n");
                canTravel = false;
            }
            else if (KilometersWithFuel < myNumOfKilometers) // cheks if the amount of fuel is ok
            {

                Console.WriteLine("ERROR: The bus doesn't have enough fuel\n");
                canTravel = false;
            }
            if (canTravel) // changes the parameter if the bus is able to travel 
            {
                KilometersWithFuel -= myNumOfKilometers;
                KilloFromLastCare += myNumOfKilometers;
                SumOfKilometers += myNumOfKilometers;
            }

        }
        /// <summary>
        /// refueling
        /// </summary>
        public void refueling() // The maximum amount of kilometer with a full fuel is 1200
        {
            KilometersWithFuel = 1200;
        }
        /// <summary>
        /// care for the bus
        /// </summary>
        public void busCare() // The time to the last care turns to 0
        {
            LastCareDate = DateTime.Now; //update the date of the last care
            KilloFromLastCare = 0; //update the number of kilometers from the last care
        }
        private bool checkDangerous(int addKilometers) // help function that checks if the number of kilometers is dangerous
        {
            if (KilloFromLastCare + addKilometers < 20000) // over 2000 kilometers since the next maintenance
                return false;
            return true;
        }
        /// <summary>
        /// return the bus number in fromat xx-xxx-xx or xxx-xx-xxx
        /// </summary>
        /// <returns></returns>
        public string LicensePlateNumber() // this function builds the bus number
        {
            string licPlatNum; ;//the string of the number
            string firstNumbers, finalNumbers, middleNumbers; // a license number is buid of 3 groups of numbers
            if (FirstDateUse.Year < 2018) // if the bus have been started to be used before 2018
            {
                firstNumbers = BusNumber.Substring(0, 2);
                middleNumbers = BusNumber.Substring(2, 3);
                finalNumbers = BusNumber.Substring(5, 2);

            }
            else // if the bus have been started to be used after 2018
            {
                firstNumbers = BusNumber.Substring(0, 3);
                middleNumbers = BusNumber.Substring(3, 2);
                finalNumbers = BusNumber.Substring(5, 3);
            }
            licPlatNum = firstNumbers + "-" + middleNumbers + "-" + finalNumbers;
            return licPlatNum;
        }


    }
}
