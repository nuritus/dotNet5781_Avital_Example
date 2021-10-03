using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0771_7072
{
   public class StationLocation
    {
        //--------------------------------------
        //Properties
        //--------------------------------------
        public double Latitude
        {
            get;
            private set;
        }
        public double Longitude
        {
            get;
            private set;
        }
        public string Address { private set; get; }

        //--------------------------------------
        //constractor
        //--------------------------------------
        public StationLocation(double lati, double longi, string addr="")
        {
            Latitude = lati;
            Longitude = longi;
            Address = addr;
        }
    }
}
