using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace dotNet5781_03B_0771_7072
{
    class StatusToVisability : IValueConverter
    {
        //convert from bus status to visability- used to hidden and show the clock according the status of the bus
        //if the bus isn't able to go for a ride show the clock..
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Status busStatus = (Status)value;
            if (busStatus == Status.ReadyToGo)
                return Visibility.Hidden; // the clock is hidden
            else
                return Visibility.Visible;// the clock is visible
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
