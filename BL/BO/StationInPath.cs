using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StationInPath
    {
        /// <summary>
        /// The code of the station in the path
        /// </summary>
        public int StationCode { set; get; }
        /// <summary>
        /// The name of the station in the path
        /// </summary>
        public string StationName { set; get; }
        /// <summary>
        /// The number of the station in the path 
        /// </summary>
        public int StationNumberInPath { set; get; }
        /// <summary>
        /// The distance between this station and the one that is before
        /// </summary>
        public double DistanceFromPreStation { set; get; }
        /// <summary>
        /// The time of travel from the last station
        /// </summary>
        public TimeSpan TimeFromPreStaion { set; get; }

        public override string ToString()
        {
            return "" + StationCode;
        }

    }
}
