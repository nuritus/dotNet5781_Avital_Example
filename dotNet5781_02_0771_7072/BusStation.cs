using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0771_7072
{
    public class BusStation
    {
        //------------------------------------------
        //Properties
        //------------------------------------------
    
        public int CodeStation { get; }
        public StationLocation Location
        {get;}
        //------------------------------------------
        //static fields for help
        //------------------------------------------
        private static int codeNumbers = 0;
     
        static Random random = new Random(DateTime.Now.Millisecond);
     
        //------------------------------------------
        //Methods
        //------------------------------------------
        public override string ToString() //it builds a string for describe the station
        {
            return "Bus Station Code: " + CodeStation + ", " + Location.Latitude + "°N " + Location.Longitude + "°E";
        }

        //------------------------------------------
        //constractor
        //------------------------------------------
        public BusStation()
        {
            codeNumbers++;
            if (codeNumbers > 999999)//if the code of station have more than 6 digits
                throw new ArgumentException("ERROR- invalid code");
            CodeStation = codeNumbers;
            double randLatitude = random.NextDouble() * (33.3 - 31) + 31; //random number for latitude in israel
            double randLongitude = random.NextDouble() * (35.5 - 34.3) + 34.3; //random number for longitude in israel
            Location = new StationLocation(randLatitude, randLongitude);
            AllBusStations.addStation(this); //add the station to the list of all the stations
        }
    }
}
