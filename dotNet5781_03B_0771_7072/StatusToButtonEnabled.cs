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

    // the class enable binding the buttons of - care, refueling and care accordig to the status of the bus
    // the buttons will be enable just if the bus is not disabled..
    class StatusToButtonEnabled : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Status busStatus = (Status)value;
            if (busStatus == Status.ReadyToGo)
                return true; // the buttons isEnable=true
            else //the bus is busy
                return false;// the buttons isEnable=false
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            throw new NotImplementedException();
        }
    }
}
