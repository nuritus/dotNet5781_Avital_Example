using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace dotNet5781_03B_0771_7072
{
    class StatusToBackground : IValueConverter
    {
        //the class binding the background of the rows of the buses in the list to the bus status
        //if the bus is free to go to a ride the backround will be AliceBlue else it will be blue
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Status busStatus = (Status)value;
            if (busStatus == Status.ReadyToGo)
                return Brushes.AliceBlue;// the bus ready to go to a ride
            else
                return Brushes.CadetBlue;// the bus isn't free to go..
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
