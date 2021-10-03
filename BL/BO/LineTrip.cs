using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// times of trips for a line
    /// </summary>
    public class LineTrip
    {
        /// <summary>
        /// the line identificator
        /// </summary>
        public int BusLineIndentificator { set; get; }
        /// <summary>
        /// the time of the first exit of the line
        /// </summary>
        public TimeSpan TimeFirstLineExit { set; get; }
       /// <summary>
       /// the frecuency of the exits between the first and the last exits
       /// </summary>
        public TimeSpan Frequency { set; get; }
        /// <summary>
        /// the time of the last exit of the line (in those frequency)
        /// </summary>
        public TimeSpan TimeLastLineExit { set; get; }
    }
}
