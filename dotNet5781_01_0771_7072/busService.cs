using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_0771_7072
{
    class busService
    {
        List<Bus> ListOfBuses;
        static Random randNumber = new Random();//random number

        public busService()//constractor
        {
            ListOfBuses = new List<Bus>();
        }

        /// <summary>
        /// Get a date from the user and check if the date is valid- print the user exception in case it doesn't
        /// </summary>
        /// <param name="myDay">the day the user will put in</param>
        /// <param name="myMonth">the month the user will put in</param>
        /// <param name="myYear">the year the user will put in</param>
        /// <returns>true- if the date is valid</returns>false-if the date is invalid
        private bool GetAndCheckDate(out string myDay, out string myMonth, out string myYear)
        {
            bool valid;
            Console.WriteLine("Day:");
            myDay = Console.ReadLine();//get the day (in the date) from the user
            Console.WriteLine("Month:");
            myMonth = Console.ReadLine();//get the month (in the date) from the user
            Console.WriteLine("Year:");
            myYear = Console.ReadLine();//get the year (in the date) from the user
            valid = true;
            int ChangedToNum;
            if (!int.TryParse(myDay, out ChangedToNum) || ChangedToNum > 31 || ChangedToNum < 1)//check if the day is valid
                valid = false;
            if (!int.TryParse(myMonth, out ChangedToNum) || ChangedToNum < 1 || ChangedToNum > 12)//check if the month is valid
                valid = false;
            if (!int.TryParse(myYear, out ChangedToNum) || ChangedToNum < 0 || ChangedToNum > 2020)//check if the year is valid
                valid = false;
            if (!valid)
            {
                Console.WriteLine("ERROR: The date is invalid");
            }
            return valid;
        }
        /// <summary>
        /// check if the number of the bus is valid and print the user exception in case it doesn't
        /// </summary>
        /// <param name="busNum">the number of the bus</param>
        /// <param name="firstYear">the year the bus was on the rode</param>
        /// <returns>true-for valid number</returns>false- for invalid number
        private bool BusNumIsValid(string busNum, int firstYear)
        {
            int tryChange; //to this variable we check if we get a real number
            if (firstYear < 2018 && busNum.Length == 7 && int.TryParse(busNum, out tryChange))
                return true;
            if (firstYear >= 2018 && busNum.Length == 8 && int.TryParse(busNum, out tryChange))
                return true;
            Console.WriteLine("ERROR: The license number is invalid");
            return false;
        }
        /// <summary>
        /// check if a number of bus is already exist in the list
        /// </summary>
        /// <param name="busNum">the number of the bus</param>
        /// <returns>true-if the bus is exist</returns>false-if the bus isn't exist
        private bool BusExist(string busNum)
        {
            if (ListOfBuses == null)
                return false;
            for (int i = 0; i < ListOfBuses.Count; i++)//check the number of any bus in the list
            {
                if (ListOfBuses[i].BusNumber == busNum)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// search a bus (according his number)
        /// </summary>
        /// <param name="busNum">the bus number</param>
        /// <returns>the bus in the list</returns>if not exist- return num
        private Bus SearchBus(String busNum)
        {
            for (int i = 0; i < ListOfBuses.Count; i++)//check all the buses in the list
            {
                if (ListOfBuses[i].BusNumber == busNum)
                    return ListOfBuses[i];
            }
            return null;
        }
        /// <summary>
        /// add a new bus to the system
        /// </summary>
        public void InsertBus()
        {
            string myDay, myMonth, myYear;
            Console.WriteLine("Enter the first date the bus has been used:");//ask the user to put in the first date the bus was used
            string busNum;
            if (GetAndCheckDate(out myDay, out myMonth, out myYear))
            {
                Console.WriteLine("Enter the bus license plate number :");//ask the user toput in the number of the bus
                busNum = Console.ReadLine();//get the number of the bus
                if (BusNumIsValid(busNum, int.Parse(myYear)) && !BusExist(busNum))//check that the number is valid and that there is 
                                                                                  //no already bus with this number in the system
                {
                    DateTime firstDate = new DateTime(int.Parse(myYear), int.Parse(myMonth), int.Parse(myDay));//arrange the date
                    ListOfBuses.Add(new Bus(busNum, firstDate));//add the bus to the list of all the buses
                    Console.WriteLine
                    (@"what is the bus mileage? (press N: new bus without mileage A: add mileage");
                    char answer = char.Parse(Console.ReadLine());
                    while (answer != 'a' && answer != 'A' && answer != 'n' && answer != 'N') // as soon as the user doesn't enter a or n, we ask again for it
                    {
                        Console.WriteLine("you must press A or N");
                        answer = char.Parse(Console.ReadLine());
                    }

                    int kilometers; // the numbers of kilometers of the bus that we define
                    Bus thisBus = SearchBus(busNum);
                    if (answer != 'n' && answer != 'N')
                    {
                        Console.WriteLine("enter mileage:");
                        while (!int.TryParse(Console.ReadLine(), out kilometers) || kilometers < 0) // if the number of the bus that the user entered is not a int, please enter again
                        {
                            Console.WriteLine("Enter valid number:"); // tell the user that the number is not a int, so it's not valid
                        }
                        thisBus.SumOfKilometers = kilometers;
                        Console.WriteLine
(@"Do you want to add when was the last care of the bus and how much
killometers it traveled since? (Y-yes,N -no)");
                        answer = char.Parse(Console.ReadLine());
                        if (answer != 'n' && answer != 'N') // as soon as he wants to add
                        {
                            if (answer == 'y' || answer == 'Y') // if he wants to add the last care and the number of km
                            {
                                while (!GetAndCheckDate(out myDay, out myMonth, out myYear)) // as soon as the date is a valid one
                                {
                                    Console.WriteLine("please enter valid date:"); // ask from the user to enter a valid date
                                }
                                thisBus.LastCareDate = new DateTime(int.Parse(myYear), int.Parse(myMonth), int.Parse(myDay));
                                Console.WriteLine("how much killometers it does since the care?");
                                while (!int.TryParse(Console.ReadLine(), out kilometers)) // if the number of kilometers is not valid, not a int
                                {
                                    Console.WriteLine("Enter valid number:"); // a valid number of kilometees
                                }
                                thisBus.KilloFromLastCare = kilometers;
                            }
                            else
                            {
                                Console.WriteLine("please answer the question with Y or N");
                                answer = char.Parse(Console.ReadLine());
                            }
                        }
                    }
                }
                else if (BusExist(busNum)) // if the bus have already been entered in the system
                    Console.WriteLine("ERROR: The bus is already exist in the system");
            }
        }

        /// <summary>
        /// fueling or take care the bus according to the user request
        /// </summary>
        /// <param name="BusNum">the number of the bus</param>
        public void FuelingOrCareBus(string BusNum)
        {
            if (!BusExist(BusNum))//if the bus isnwt exist in the list- print massage to the user
            {
                Console.WriteLine("ERROR: The bus is not exist in the system\n");
            }
            else
            {
                Console.WriteLine("Do you want to fueling or to car the bus? (F: to fueling, C: to care)");//ask the user for the action
                bool valid;
                do//get answer from the user until he put in a valid answer
                {
                    valid = true;
                    char answer = char.Parse(Console.ReadLine());//get the user answer
                    if (answer == 'f' || answer == 'F')//if the user want to fuel
                        SearchBus(BusNum).refueling();
                    else if (answer == 'c' || answer == 'C')//if the user want to care
                        SearchBus(BusNum).busCare();
                    else//if the user put in invalid answer print a massage and ask to put in valid answer
                    {
                        Console.WriteLine("please enter just F or C");
                        answer = (char)Console.Read();//read the user answer
                        valid = false;
                    }
                } while (!valid); // if it's not valid
            }
        }

        public void PrintListKillometers() //prints a list of kilometers 
        {
            for (int i = 0; i < ListOfBuses.Count; i++) // it will print a list of buses with their license plate number 
            {
                Console.WriteLine("{0}    {1}\n", ListOfBuses[i].LicensePlateNumber(), ListOfBuses[i].KilloFromLastCare);
            }
        }
        public void Drive() // checks if the bus number is valid, 
        {

            Console.WriteLine("Enter a lincense plate number:");
            String myLicense2 = Console.ReadLine();
            int num2;

            while (!int.TryParse(myLicense2, out num2)) // if the number is not valid
            {
                Console.WriteLine("Error- Please enter a new Number:"); //prints the suited message
                myLicense2 = Console.ReadLine();
            }
            int kiloNum = randNumber.Next(1, 1050);//1050- the distence btween Mtula and Eilat back and forth
                                                   //convert Rand into int

            if (!BusExist(myLicense2)) // if the bus doesn't exist
            {
                Console.WriteLine("The bus doen't exist- please enter another one. "); //prints the suited message
            }
            else
                SearchBus(myLicense2).travel(kiloNum); // searchs the bus acoording to his number and adds the number of kilometers

        }
    }
}

