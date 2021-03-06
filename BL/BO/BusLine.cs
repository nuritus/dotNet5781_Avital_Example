using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public enum Area { General, North, South, Center, Jerusalem }
    public class BusLine
    {
        /// <summary>
        /// It's a number which is the identificator of the line
        /// </summary>
        public int BusLineIndentificator { set; get; }
        /// <summary>
        /// It is the number of the bus line
        /// </summary>
        public int LineNumber { set; get; }
        /// <summary>
        /// It is the geographic area of where does the bus travel: north, south, jerusalem, center or general
        /// </summary>
        public Area LineArea { set; get; }
        /// <summary>
        /// The first station the line goes through
        /// </summary>
        public int FirstLineStation { set; get; }
        /// <summary>
        /// The last station the line goes through
        /// </summary>
        public int LastLineStation { set; get; }
        /// <summary>
        /// The collection of all the stations in a path
        /// </summary>
        public IEnumerable<StationInPath> StationsInPath { set; get; }
    }
}
