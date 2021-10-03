using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BlApi;

namespace PlGui
{
    class StationCodeToName : IValueConverter
    {
        IBl bl;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bl = BlFactory.GetBl();
            return bl.GetStationDetails((int)value).StationName;
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
