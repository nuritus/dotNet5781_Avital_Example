using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;  //aaa

namespace dotNet5781_02_0771_7072
{
    public enum Area { General, North, South, Center, Jerusalem }// different options of areas in israel
    public class BusLine : IComparable
    {
        //-------------------------------------------------
        //Properties + Fields
        //-------------------------------------------------
        static Random random = new Random(DateTime.Now.Millisecond);
        public List<PointInPath> path;// a list with all the bus line station
        public int BusNumber
        {
            get;
        }
        public PointInPath FirstStation //return the details of the first station in the bus path
        {
            get { return path[0]; }
        }
        public PointInPath LastStation //return the details of the last station in the bus path
        {
            get { return path[path.Count - 1]; }
        }
        public Area BusArea { set; get; } //the area where the bus travels

        //--------------------------------------------------
        //internal class
        //--------------------------------------------------
        public class PointInPath
        {
            public int StationNumber { get; }
            public double DistanceFromLastStation { get; } //the distance from the last station
            public TimeSpan TimeFromPreviousStation //the time that the bus travelled from the previous station 
            {
                set;
                get;
            }
            /// <summary>
            /// constractor
            /// </summary>
            /// <param name="myStationNumber"></param>
            /// <param name="myDistance"></param>
            public PointInPath(int myStationNumber, double myDistance)
            {
                StationNumber = myStationNumber;
                DistanceFromLastStation = myDistance;
                double seconds = myDistance * 0.1; //we assume that any meter took 0.1 seconds to drive
                TimeSpan time = TimeSpan.FromSeconds(seconds);// convert from seconds to minutes and hours
                TimeFromPreviousStation = new TimeSpan(time.Hours, time.Minutes, time.Seconds);
            }
            public override string ToString()
            {
                string str = "Station number: " + StationNumber;

                return str+" ";
            }
        }
        //--------------------------------------------------
        //constractor
        //--------------------------------------------------

        public BusLine(int busNum = 0)
        {
            path = new List<PointInPath>();
            BusNumber = busNum;
            //number++;
            BusArea = (Area)random.Next(0, 4);
        }

        //--------------------------------------------------
        //Methods
        //--------------------------------------------------

        public override string ToString()
        {
            string busPath = ""; //the path from the beginning to the end
            int i = 0;
            for (; i < path.Count - 1; i++) //build the string..
            {
                busPath = busPath + path[i].StationNumber + "-> ";
            }
            busPath += path[i].StationNumber;
            return "Bus line:" + BusNumber; //+ ", Area:" + BusArea + ", path from beginning: " + busPath;
        }

        /// <summary>
        /// check if a station exists in the path according it code number
        /// </summary>
        /// <param name="stationNum"></param>
        /// <returns></returns>
        public bool checkStation(int stationNum)
        {
            if (!AllBusStations.stationExists(stationNum))
                throw new ArgumentException("ERROR: the station doesn't exist");
            foreach (PointInPath station in path) // check any station in the bus path
                if (station.StationNumber == stationNum) return true;
            return false;
        }

        /// <summary>
        /// add a station to the path of the bus
        /// </summary>
        /// <param name="stationCode">the code number of the station that we want to add to the path</param>
        /// <param name="afterStation">the number of station that will be after the station we 
        /// add to the path. in case we want to add in the last station we will get- afterStation=-1 </param>
        public void addStation(int stationCode, int afterStation)
        {
            if (checkStation(stationCode)) //check if the station is exists already in the path of the line
                throw new ArgumentException("ERROR: the station already exists in path");
            if (!AllBusStations.stationExists(stationCode)) //check that there is a station with this number
                throw new ArgumentException("ERROR: The station doesn't exists");
            if (afterStation == -1) //add the last station
            {
                if (path.Count == 0)
                    path.Add(new PointInPath(stationCode, 0));
                else
                {
                    double distance = AllBusStations.distanceBetweenStations(stationCode, path[path.Count - 1].StationNumber);
                    path.Add(new PointInPath(stationCode, distance));
                }
            }
            else if (!checkStation(afterStation))
                throw new ArgumentException("ERROR: the station already exists in path");
            else
            { //search the index of the station that will be after the asation we want to add:
                int index = indexStation(afterStation);
                if (index == 0)
                    path.Insert(index, new PointInPath(stationCode, 0));//insert the station in the suitable place in the list
                else
                {
                    double distance = AllBusStations.distanceBetweenStations(stationCode, path[index - 1].StationNumber);
                    path.Insert(index, new PointInPath(stationCode, distance));
                }
            }
        }

        /// <summary>
        /// remove a station from th path
        /// </summary>
        /// <param name="stationCode"></param>
        public void removeStation(int stationCode)
        {
            if (!checkStation(stationCode))//check if the station is in the path
                throw new ArgumentException("ERROR: The station doesn't exists in path");
            if (path.Count == 2)//check that the bus will have at least 2 stations after the removing
                throw new ArgumentException("ERROR: a bus must have al least 2 stations");
            int index = indexStation(stationCode);//search the index in the path to remove the station
            path.RemoveAt(index);
        }
        /// <summary>
        /// search the index of the station in the path
        /// </summary>
        /// <param name="myStationNum"></param>
        /// <returns></returns>
        public int indexStation (int myStationNum)  
        {
            for(int i=0; i<path.Count; i++)// check every station in the path
            {
                if (path[i].StationNumber == myStationNum)
                    return i;//return the index
            }
            return -1;
        }

        /// <summary>
        ///calculate the time to travel with the bus between two different stations
        /// </summary>
        /// <param name="station1"></param>
        /// <param name="station2"></param>
        /// <returns></returns>
        public TimeSpan timeBetweenTwoStations(int station1, int station2)
        {

            int indexS1 = indexStation(station1);//search station 1 in the list
            int indexS2 = indexStation(station2);//search station 2 in the list
            if (indexS1 != -1 && indexS2 != -1)
            {
                TimeSpan time = new TimeSpan(0, 0, 0);
                if (indexS1 < indexS2) //if station 1 is before station 2 than..
                {
                    for (int i = indexS1 + 1; i <= indexS2; i++)
                        time += path[i].TimeFromPreviousStation; //calculate the time btween the stations
                }
                else
                if (indexS2 < indexS1) //if station 2 is before station 1 than..
                {
                    for (int i = indexS2 + 1; i <= indexS1; i++)
                        time += path[i].TimeFromPreviousStation;//calculate the time btween the stations
                }
                return time;
            }
            throw new ArgumentException("ERROR-one of the station doesn't exist in the bus path");
        }
       
        /// <summary>
        /// //calculate the distance between two different stations in the path of the bus
        /// </summary>
        /// <param name="station1"></param>
        /// <param name="station2"></param>
        /// <returns></returns>
        public double distanceBetweenTwoStations(int station1, int station2)
        {

            int indexS1 = indexStation(station1);//search station 1 in the list
            int indexS2 = indexStation(station2);//search station 2 in the list
            if (indexS1 != -1 && indexS2 != -1)
            {
                double distance = 0;
                if (indexS1 < indexS2) //if station 1 is before station 2 than..
                {
                    for (int i = indexS1 + 1; i <= indexS2; i++)
                        distance += path[i].DistanceFromLastStation; //calculate the distance btween the stations
                }
                else
                if (indexS2 < indexS1) //if station 2 is before station 1 than..
                {
                    for (int i = indexS2 + 1; i <= indexS1; i++)
                        distance += path[i].DistanceFromLastStation;//calculate the distance btween the stations
                }
                return distance;
            }
            throw new ArgumentException("ERROR-one of the station doesn't exist in the bus path");
        }
        /// <summary>
        /// create "bus line" with sub- path of exists bus
        /// </summary>
        /// <param name="stationNum1"></param>
        /// <param name="stationNum2"></param>
        /// <returns></returns>
        public BusLine subPath(int stationNum1, int stationNum2)
        {
            BusLine bus = new BusLine(BusNumber);
            int indexS1 = indexStation(stationNum1);//search station 1 in the list
            int indexS2 = indexStation(stationNum2);//search station 2 in the list
            if (indexS1 != -1 && indexS2 != -1)
            {
                if (indexS1 < indexS2)//if station 1 is before station 2 in the bus path
                {
                    bus.path = path.GetRange(indexS1, indexS2 - indexS1 + 1);//update the sub path of the new bus
                }
                return bus;
            }
            throw new ArgumentException("ERROR: the order of the stations is incorrect ");
        }
        /// <summary>
        /// compare between two lines according their travel time
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            //the time between the first station to the last station of the first bus 
            TimeSpan timeLine1 = timeBetweenTwoStations(FirstStation.StationNumber, LastStation.StationNumber);
            BusLine bus = (BusLine)obj;
            //the time between the first station to the last station of the second bus 
            TimeSpan timeLine2 = bus.timeBetweenTwoStations(bus.FirstStation.StationNumber, bus.LastStation.StationNumber);
            return timeLine1.CompareTo(timeLine2);
        }

    }
}
