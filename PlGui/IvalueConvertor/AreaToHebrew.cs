using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PlGui.PO;

namespace PlGui
{
    class AreaToHebrew : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PO.Area myArea = (PO.Area)value;
            if (myArea == PO.Area.Jerusalem)
                return "ירושלים";
            if (myArea == PO.Area.Center)
                return "מרכז";
            if (myArea == PO.Area.General)
                return "ארצי";
            if (myArea == PO.Area.North)
                return "צפון";
            return "דרום";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
