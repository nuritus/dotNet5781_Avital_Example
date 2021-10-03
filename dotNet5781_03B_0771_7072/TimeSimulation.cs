using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading; 

namespace dotNet5781_03B_0771_7072
{
    /// <summary>
    /// class for calculate the simulation clock with thread
    /// </summary>
    class TimeSimulation
    {
        public BackgroundWorker TimeUntilTravel { set; get; }//set- to enable to add functions to the events of the thread

        bool timeOut=false;//check if the bus return to be able to travel
        long timeLeft;//the time that left until the bus can travel again
        Random rand = new Random(DateTime.Now.Millisecond);
        Bus currentBus;

        //-----------------------------------------------
        //constractor:
        //-----------------------------------------------
        public TimeSimulation(Bus bus,int kilometerToTravel=0)
        {
            if (bus.BusStatus == Status.Infualing)//refueling takes two hours -(7200 seconds) 
                timeLeft = 7200;
            else if (bus.BusStatus == Status.InCare)//care takes a day- (86400 seconds)
                timeLeft = 86400;
            else if(bus.BusStatus==Status.InMiddle)//travel time depends on the speed of the bus
            {
                int speed = rand.Next(20, 50);//chosing a random speed between 20 to 50
                timeLeft = (long)(((float)kilometerToTravel / speed) * 3600);//calculate the time according to the speed
            }
            currentBus = bus;
            TimeUntilTravel = new BackgroundWorker();//bulding the thread
            TimeUntilTravel.DoWork += TimeUntilTravel_DoWork;
            TimeUntilTravel.ProgressChanged += TimeUntilTravel_ProgressChanged;
            TimeUntilTravel.WorkerReportsProgress = true;//enable report about changes
            TimeUntilTravel.RunWorkerCompleted += TimeUntilTravel_RunWorkerCompleted;
            TimeUntilTravel.RunWorkerAsync();//start the tread
        }
       
        //-----------------------------------------------
        //methods in the events of the thread:
        //-----------------------------------------------
        
        /// <summary>
        /// the main function of the thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeUntilTravel_DoWork(object sender, DoWorkEventArgs e)
        {

            while (!timeOut)//check that the isn't finish
            {
                TimeUntilTravel.ReportProgress(1);//call to TimeUntilTravel_ProgressChanged function
                Thread.Sleep(100);//stop the thread to 0.1 seconds
            }
        }
     
        /// <summary>
        /// every report of changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeUntilTravel_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            timeLeft--; //the time that left until the thread will finish lose second
            //string hour = "" + timeLeft / 3600;
            //string min = "" + (timeLeft % 3600) / 60;
            //string sec = "" + (timeLeft % 3600) % 60;
            if (timeLeft == 0)//if the time is finish
                timeOut = true;
        }

        /// <summary>
      /// when the thread is finish
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void TimeUntilTravel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            currentBus.BusStatus = Status.ReadyToGo;//change the status of the bus to readyToGo..
        }

        /// <summary>
        /// ToString- show the time in the "clock".. format xx:xx:xx..
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string hour = "" + timeLeft / 3600;
            string min = "" + (timeLeft % 3600) / 60;
            string sec = "" + (timeLeft % 3600) % 60;
            return hour+":"+min+":"+sec;
        }
    }
}
