using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// data about line tha closing to a specific station
    /// </summary>
    public class LineTimingInStation
    {
        /// <summary>
        /// the line identificator
        /// </summary>
        public int BusLineIdentificator { set; get; }
        /// <summary>
        /// the line number
        /// </summary>
        public int LineNumber { set; get; }
        /// <summary>
        /// the time that left until the bus reach to the station
        /// </summary>
        public TimeSpan TimingLeft { set; get; }
        /// <summary>
        /// the code of the last station in the line path 
        /// </summary>
        public int LastStationCode { set; get; }
        /// <summary>
        /// the time when the line exit from the first sation
        /// </summary>
        public TimeSpan ExitTimeFromFirstStation { set; get; }
    }
}
