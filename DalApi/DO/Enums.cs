using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// The status of the bus: if it is in travel, in fueling, in care or ready to go
    /// </summary>
    public enum BusStatus { InTravel, InFueling, InCare, ReadyToGo }
    /// <summary>
    /// The area where the bus travels: if it is general, north, south, cneter or jerusalem
    /// </summary>
    public enum Area { General, North, South, Center, Jerusalem }

}
