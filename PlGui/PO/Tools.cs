using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace PlGui
{
    static class Tools
    {
        #region convert from BO.BusLine to PO.BusLine
        internal static PO.BusLine CopyLine(this BO.BusLine busBO)
        {
            PO.BusLine copyBus = new PO.BusLine()
            {
                LineNumber = busBO.LineNumber,
                FirstLineStation=busBO.FirstLineStation,
                LastLineStation=busBO.LastLineStation,
                BusLineIndentificator=busBO.BusLineIndentificator,
                LineArea=(PO.Area)busBO.LineArea,
                StationsInPath=from station in busBO.StationsInPath
                               select new BO.StationInPath 
                               {StationCode=station.StationCode,
                               StationName=station.StationName,
                               StationNumberInPath=station.StationNumberInPath,
                               DistanceFromPreStation=station.DistanceFromPreStation,
                               TimeFromPreStaion=station.TimeFromPreStaion}
            };
            return copyBus;
        }
        #endregion

        #region convert from Area to string (Hebrew) and from string(Hebrew) to Area
        public static PO.Area stringToArea(string hebrewArea)
        {
            if (hebrewArea == "ירושלים")
                return PO.Area.Jerusalem;
            if (hebrewArea == "מרכז")
                return PO.Area.Center;
            if (hebrewArea == "צפון")
                return PO.Area.North;
            if (hebrewArea == "דרום")
                return PO.Area.South;
            if (hebrewArea == "ארצי")
                return PO.Area.General;
            throw new Exception();
        }
        public static string areaToString(this PO.Area myArea)
        {
            if (myArea == PO.Area.Jerusalem)
                return "ירושלים";
            if (myArea == PO.Area.Center)
                return "מרכז";
            if (myArea == PO.Area.North)
                return "צפון";
            if (myArea == PO.Area.South)
                return "דרום";
            if (myArea == PO.Area.General)
                return "ארצי";
            throw new Exception();
        }

        #endregion





    }
}
