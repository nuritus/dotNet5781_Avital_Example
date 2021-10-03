using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PlGui.PO
{
    public enum Area { General, North, South, Center, Jerusalem }
    public class BusLine : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;//event to update the line number in live
        public int BusLineIndentificator { set; get; }

        private int privLineNumber;
        public int LineNumber
        {
            set
            {//for ensure that the changes will be update immediately in the window
                privLineNumber = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LineNumber"));
                }
            }
            get { return privLineNumber; }
        }
        public Area LineArea { set; get; }
        public int FirstLineStation { set; get; }
        public int LastLineStation { set; get; }
        public IEnumerable<BO.StationInPath> StationsInPath { set; get; }

    }
}
