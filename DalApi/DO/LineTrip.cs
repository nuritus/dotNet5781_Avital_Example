using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineTrip
    {
        public int BusLineIndentificator { set; get; }
        public TimeSpan TimeFirstLineExit { set; get; }
        public TimeSpan Frequency { set; get; }
        public TimeSpan TimeLastLineExit { set; get; }
    }
}
