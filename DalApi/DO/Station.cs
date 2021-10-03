using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Informations about the stations
    /// </summary>
   public  class Station
    {
        /// <summary>
        /// The code of the station
        /// </summary>
        public int StationCode { set; get; }
        /// <summary>
        /// The geocoordination of the station, the latitude part
        /// </summary>
        public double LocationLatitude { set; get; }
        /// <summary>
        /// The geocoordination of the station, the longitude part
        /// </summary>
        public double LocationLongitude { set; get; }
        /// <summary>
        /// The name of the station
        /// </summary>
        public string StationName { set; get; }
        /// <summary>
        /// The address of the station
        /// </summary>
        public string StationAddress { set; get; }
        /// <summary>
        /// If there is places to sit in the station to wait for the bus
        /// </summary>
        public bool PlaceToSit { set; get; }
        /// <summary>
        /// If there is a board with the timings in the bus stations
        /// </summary>
        public bool BoardBusTiming { set; get; }
        /// <summary>
        /// The status of the station in the list, if it is deleted or not
        /// </summary>
        public bool Deleted { set; get; } = false;
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
