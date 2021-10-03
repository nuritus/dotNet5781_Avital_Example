using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data; 


namespace dotNet5781_03B_0771_7072
{
    class StatusToHebrew : IValueConverter
    {
        //convert the status of the bus to hebrow- for show the bus status in hebrow in the info window
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Status busStatus = (Status)value;
            if (busStatus == Status.ReadyToGo)
                return "מוכן לנסיעה";
            if (busStatus == Status.InMiddle)
                return "באמצע נסיעה";
            if (busStatus == Status.Infualing)
                return "בתדלוק";
            if (busStatus == Status.InCare)
                return "בטיפול";
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
