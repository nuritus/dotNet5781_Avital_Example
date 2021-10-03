using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DataAboutTwoStations
    {
        /// <summary>
        /// The code of the first station of both of the two stations
        /// </summary>
        public int FirstStationCode { set; get; }
        /// <summary>
        /// The code of the second station of both of thr two station
        /// </summary>
        public int SecondStationCode { set; get; }
        /// <summary>
        /// The distant between the two following stations
        /// </summary>
        public double DistanceBetweenStations { set; get; }
        /// <summary>
        /// The average time of travel between the two station according to the distance, and the speed that is choosed randomally
        /// </summary>

        public TimeSpan AverageTimeBetweenStations { set; get; }
    }
}
