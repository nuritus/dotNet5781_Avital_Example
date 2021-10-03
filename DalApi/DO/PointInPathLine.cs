using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{/// <summary>
/// Information about the stations in a path of a specific line
/// </summary>
   public class PointInPathLine
    {
        /// <summary>
        /// The number of the poinr in path line 
        /// </summary>
       public int PointInPathLineNumber { set; get; }
        /// <summary>
        /// The code of the station in the path
        /// </summary>
        public int StationCode { set; get; }
        /// <summary>
        /// The number of the point in the bus line, if it is the 1st, the 2nd...
        /// </summary>
        public int NumberInPath { set; get; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
