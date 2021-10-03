using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// a schedule for a line and data between the first station to a specific station
    /// </summary>
    internal class ScheduleAtAstation
    {
        /// <summary>
        /// the line bus identificator
        /// </summary>
        public int BusLineIdentificator { set; get; }
        /// <summary>
        /// the code of the station we want to get the data about it (times the line pass in)
        /// </summary>
         public int StationToCheckTimesCode { set; get; }
        /// <summary>
        /// detailed schedule of all the exit times of the specific line (according to the line id)
        /// </summary>
        public IEnumerable<TimeSpan> DepartureLineTimes { set; get; }
        /// <summary>
        /// the traveling time between the first station to the specific station
        /// </summary>
        public TimeSpan timeFromFirstStation { set; get; }
    }
}
