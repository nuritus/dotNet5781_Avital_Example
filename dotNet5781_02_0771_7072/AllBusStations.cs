using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace dotNet5781_02_0771_7072
{
     static class AllBusStations
    {
        static public List<BusStation> listOfStations = new List<BusStation>();// the list of all the stations


        //------------------------------------------
        //Methods:
        //------------------------------------------
        /// <summary>
        /// add new station to the list
        /// </summary>
        /// <param name="newStation"></param>
        static public void addStation(BusStation newStation) 
        {
            listOfStations.Add(newStation);
        }
     
        /// <summary>
        /// remove an existing station from the list
        /// </summary>
        /// <param name="station"></param>
        static public void removeStation(BusStation station)
        {
            listOfStations.Remove(station);
        }
        /// <summary>
        /// check if station with a number is exists
        /// </summary>
        /// <param name="myStationCode"></param>
        /// <returns></returns>
        static public bool stationExists(int myStationCode)
        {
            foreach (BusStation station in listOfStations) //check any number of station in the list
                if (station.CodeStation == myStationCode) return true;
            return false;
        }
        /// <summary>
        /// check if station with a number is exists and return the station in case it is
        /// </summary>
        /// <param name="myStationCode"></param>
        /// <returns></returns>
        static public BusStation SearchStation(int myStationCode)
        {
            foreach (BusStation station in listOfStations) //check any number of station in the list
                if (station.CodeStation == myStationCode) return station;
            return null;
        }
        
        /// <summary>
        /// calculate the distance between two different stations
        /// </summary>
        /// <param name="station1"></param>
        /// <param name="station2"></param>
        /// <returns></returns>
        static public double distanceBetweenStations(int station1, int station2)
        {
            if(stationExists(station1) && stationExists(station2))//check that both two stations are really exists
            {
                BusStation myStation1 = SearchStation(station1);
                BusStation myStation2 = SearchStation(station2);
                var sCoord = new GeoCoordinate(myStation1.Location.Latitude, myStation1.Location.Longitude);
                var eCoord = new GeoCoordinate(myStation2.Location.Latitude, myStation2.Location.Longitude);
                return sCoord.GetDistanceTo(eCoord);//calculate the distance between the stations
            }
            throw new AggregateException("ERROR: At ieast one of the stations doesn't exists");
        }
    }
}

