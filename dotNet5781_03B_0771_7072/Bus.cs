using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace dotNet5781_03B_0771_7072
{
    public enum Status { ReadyToGo, InMiddle, Infualing, InCare}
    public class Bus : INotifyPropertyChanged
    {
       //--------------------------------------------
       //events:
       //--------------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;//event to update the status of the bus in live

        //-------------------------------------------
        // private fields:
        //-------------------------------------------
         
        private DateTime privLastCareDate;
        private int privKilloFromLastCare;
        private int privSumOfKilometers;
        private int privKilometersWithFuel;
        private Status privBusStatus;
        private string privTimer;
        private TimeSimulation clock; //simulation clock for when bus change status

        //--------------------------------------------
        //properties:
        //--------------------------------------------
        public bool TypeBusInCity { get; } //the type of the bus- true- in the city, false- between cities
        public string BusNumber { get; } // The license plate number of the bus, it can't be changed
        public DateTime FirstDateUse { set;  get; } // The first date when the bus was used, can't be changed   
        public DateTime LastCareDate  // The date of the last care of the bus
        { 
            set {
                if (value < FirstDateUse)
                    throw new invalidInputException("תאריך טיפול לא יכול להיות לפני תאריך ייצור");
                if (value > DateTime.Now)
                    throw new invalidInputException("תאריך טיפול לא יכול להיות יאוחר מהיום");
                privLastCareDate = value;
                } 
            get { return privLastCareDate; } 
        }  
        public int KilloFromLastCare // number of kilometers of the bus from the last care, over 20 000 it's dangerous
        {
            set
            {
                if(value<0)
                    throw new invalidInputException("קילומטראז' לא יכול להיות שלילי");
                privKilloFromLastCare = value;
            }
            get { return privKilloFromLastCare; }
        }      
        public int SumOfKilometers // how much kilometers the bus travel total
        {
            set
            {
                if (value < 0)
                    throw new invalidInputException("קילומטראז' לא יכול להיות שלילי");
                privSumOfKilometers = value;
            }
            get { return privSumOfKilometers; }
        }       
        public int KilometersWithFuel // The number of kilometers that last to the bus until the next time it gets fuel
        { 
            set
            {
                if (value < 0)
                    throw new invalidInputException("מספר קילומטרים לא יכול להיות שלילי");
                if(value>1200)
                    throw new invalidInputException("מיכל מלא מכיל דלק ל 1200 קילומטר");
                privKilometersWithFuel = value;
            }
            get { return privKilometersWithFuel; }
        }        
        public Status BusStatus 
        {
            set
            {//for ensure that the changes will be update immediately in the window
                privBusStatus = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("BusStatus"));
                }
            }
            get { return privBusStatus; } } //the status of the bus
        public string Timer
        {
            set
            { //for ensure that the changes will be update immediately in the window
                privTimer = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Timer"));
                }
            }
            get
            { return privTimer; }
        } //the timer that will be show when the bus is busy

        //-----------------------------------------------------  
        // constractor:
        //-----------------------------------------------------
        public Bus(string myPlateNumber, DateTime myFirstDateUse,DateTime LastCare, bool inCity = true,
            int fromLastCare = 0,int fuel=1200,int mileage=0)
        {
            if (FirstDateUse > DateTime.Now || LastCare> DateTime.Now)
                throw new invalidInputException("תאריך ייצור לא חוקי");
            FirstDateUse = new DateTime(myFirstDateUse.Year, myFirstDateUse.Month, myFirstDateUse.Day);
            BusNumber = LicensePlateNumber(myPlateNumber);
            LastCareDate = LastCare; // if there is a new bus without last care date, we are doing a care now
            KilloFromLastCare = fromLastCare;
            KilometersWithFuel = fuel; // The fuel 
            SumOfKilometers = mileage;
            BusStatus = Status.ReadyToGo;
            TypeBusInCity = inCity;
            Timer = "";
        }

        //-----------------------------------------------------
        //private methods:
        //-----------------------------------------------------

        // help function that checks if the number of kilometers is dangerous
        private bool checkDangerous(int addKilometers)
        {
            if (KilloFromLastCare + addKilometers < 20000) // over 2000 kilometers since the next maintenance
                return false;
            return true;
        }

        // return the bus number in fromat xx-xxx-xx or xxx-xx-xxx
        private string LicensePlateNumber(string simpleNumber) 
        {
            string licPlatNum; ;//the string of the number
            string firstNumbers, finalNumbers, middleNumbers; // a license number is buid of 3 groups of numbers
            if (FirstDateUse.Year < 2018 && simpleNumber.Length == 7) // if the bus have been started to be used before 2018
            {
                firstNumbers = simpleNumber.Substring(0, 2);
                middleNumbers = simpleNumber.Substring(2, 3);
                finalNumbers = simpleNumber.Substring(5, 2);

            }
            else if (FirstDateUse.Year >= 2018 && simpleNumber.Length == 8)// if the bus have been started to be used after 2018
            {
                firstNumbers = simpleNumber.Substring(0, 3);
                middleNumbers = simpleNumber.Substring(3, 2);
                finalNumbers = simpleNumber.Substring(5, 3);
            }
            else
                throw new invalidInputException("מספר רישוי לא חוקי");
            licPlatNum = firstNumbers + "-" + middleNumbers + "-" + finalNumbers;
            return licPlatNum;
        }

        //show the time that left until the bus will be ready to travel xx:xx:xx
        private void show_timer(object sender, ProgressChangedEventArgs e)
        {
            Timer = clock.ToString();
            //throw new NotImplementedException();
        }
         
        //-----------------------------------------------------
        //public methods:
        //-----------------------------------------------------

        /// <summary>
        /// refueling
        /// </summary>
        public void refueling() // The maximum amount of kilometer with a full fuel is 1200
        {
            BusStatus = Status.Infualing;//change the status of the bus
            KilometersWithFuel = 1200;
            clock = new TimeSimulation(this);
            clock.TimeUntilTravel.ProgressChanged += show_timer;//add the observer to the event if changes
        }

        /// <summary>
        /// care for the bus
        /// </summary>   
        public void busCare() 
        {
            BusStatus = Status.InCare;//change the status of the bus
            //update the date of the last care tpo be today..
            LastCareDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);                                                                                  
            KilloFromLastCare = 0; //update the number of kilometers from the last care
            clock = new TimeSimulation(this);
            clock.TimeUntilTravel.ProgressChanged += show_timer;//add the observer to the event of changes
            KilometersWithFuel = 1200;//refualing the bus
        }

    
        /// <summary>
        /// checks if the bus can travel according to 3 conditions and sent it to travel
        /// </summary>
        /// <param name="myNumOfKilometers"></param>
        public void travel(int myNumOfKilometers)
        {
            if (myNumOfKilometers == 0)
            {
                throw new invalidInputException ("כמות קילומטרים לא תקינה"); // if the user enter 0 kilometers
            }
            if (BusStatus != Status.ReadyToGo)
            {
                throw new invalidInputException("האוטובוס לא מוכן לנסיעה");
            }
            if (checkDangerous(myNumOfKilometers)) // calls the help function that checks if the number of kilometers travelled by the bus is dangerous
            {
                throw new invalidInputException("חריגה ממספר הקילומטרים המותר לנסיעה לפני טיפול"); // prints the suited message

            }
            else if ((DateTime.Now - LastCareDate).Days > 665) // checks if the bus has to do a maintenance, if it does, it can't travel
            {
                throw new invalidInputException("האוטובוס חייב לעבור טיפול");
            }
            else if (KilometersWithFuel < myNumOfKilometers) // cheks if the amount of fuel is ok
            {

                throw new invalidInputException("אין מספיק דלק לביצוע הנסיעה");
            }
            //if the bus is able to travel.. 
            KilometersWithFuel -= myNumOfKilometers;
            KilloFromLastCare += myNumOfKilometers;
            SumOfKilometers += myNumOfKilometers;
            BusStatus = Status.InMiddle; //change the status of the bus
            clock = new TimeSimulation(this, myNumOfKilometers);
            clock.TimeUntilTravel.ProgressChanged += show_timer;//add the observer to the event of changes
        }

    }
}
