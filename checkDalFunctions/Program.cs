using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
using DS;
using System.Device.Location;
using System.Xml.Linq;


namespace checkDalFunctions
{
    class Program
    {
        static readonly IDal dal = DalFactory.GetDal();
        static void Main(string[] args)
        {
            #region 21 7 stations 
            dal.AddStation(new Station
            {
                StationCode = 1819,
                StationName = "מסןף אגד קדיש לוז",
                StationAddress = "קדיש לוז 22",
                PlaceToSit = false,
                BoardBusTiming = false,
                LocationLatitude = 31.7614,
                LocationLongitude = 35.1833
            });

            dal.AddStation(new Station
            {
                StationCode = 2072,
                StationName = "קדיש לוז משה שרת",
                StationAddress = "ירושלים קדיש לוז 2",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.7588,
                LocationLongitude = 35.1838
            });
            dal.AddStation(new Station
            {
                StationCode = 2592,
                StationName = "פרץ ברנשטיין משה שרת",
                StationAddress = "  ירושלים פרץ ברנשטיין 7",
                PlaceToSit = true,
                BoardBusTiming = false,
                LocationLatitude = 31.7584,
                LocationLongitude = 35.186
            });
            dal.AddStation(new Station
            {
                StationCode = 2783,
                StationName = "פרץ ברנשטיין",
                StationAddress = " ירושלים פרץ ברנשטיין 17",
                PlaceToSit = true,
                BoardBusTiming = false,
                LocationLatitude = 31.7592,
                LocationLongitude = 35.1873
            });
            dal.AddStation(new Station
            {
                StationCode = 1518,
                StationName = "פרץ ברנשטיין נזר דוד",
                StationAddress = " ירושלים פרץ ברנשטיין 37",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.7592,
                LocationLongitude = 35.1873
            });
            dal.AddStation(new Station
            {
                StationCode = 9871,
                StationName = "הולילנד אבי יונה",
                StationAddress = " ירושלים אבי יונה",
                PlaceToSit = true,
                BoardBusTiming = false,
                LocationLatitude = 31.8174,
                LocationLongitude = 35.1949
            });
            dal.AddStation(new Station
            {
                StationCode = 2747,
                StationName = "מכללה לבנות ירושלים דובדבני",
                StationAddress = " ברוך דובדבני ירושלים",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.76238,
                LocationLongitude = 35.191845

            });




            dal.AddBusLine(new BusLine
            {
                FirstLineStation = 1819,
                LastLineStation = 9871,
                BusLineIndentificator = Configuration.BusInLineIndentificator++,
                LineArea = Area.Jerusalem,
                LineNumber = 21
            });
            dal.AddTwoFollowingStations(new TwoFollowingStations
            {
                FirstStationCode = 1819,
                SecondStationCode = 2072,
                DistanceBetweenStations = 0.29,
                AverageTimeBetweenStations = new TimeSpan(0, 0, 20)

            });
            dal.AddTwoFollowingStations(new TwoFollowingStations
            {
                FirstStationCode = 2072,
                SecondStationCode = 2592,
                DistanceBetweenStations = 1.48,
                AverageTimeBetweenStations = new TimeSpan(0, 1, 0)
            });
            dal.AddTwoFollowingStations(new TwoFollowingStations
            {
                FirstStationCode = 2592,
                SecondStationCode = 2783,
                DistanceBetweenStations = 0.2,
                AverageTimeBetweenStations = new TimeSpan(0, 0, 20)
            });
            dal.AddTwoFollowingStations(new TwoFollowingStations
            {
                FirstStationCode = 2783,
                SecondStationCode = 1518,
                DistanceBetweenStations = 0.24,
                AverageTimeBetweenStations = new TimeSpan(0, 0, 20)
            });
            dal.AddTwoFollowingStations(new TwoFollowingStations
            {
                FirstStationCode = 1518,
                SecondStationCode = 9871,
                DistanceBetweenStations = 0.26,
                AverageTimeBetweenStations = new TimeSpan(0, 0, 20)
            });
            dal.AddTwoFollowingStations(new TwoFollowingStations
            {
                FirstStationCode = 9871,
                SecondStationCode = 2747,
                DistanceBetweenStations = 0.25,
                AverageTimeBetweenStations = new TimeSpan(0, 0, 20)
            });

            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 1,
                StationCode = 1819
            });
            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 2,
                StationCode = 2072
            });
            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 3,
                StationCode = 2592
            });
            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 4,
                StationCode = 2783
            });

            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 5,
                StationCode = 1518
            });
            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 6,
                StationCode = 9871
            });
            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 7,
                StationCode = 2747
            });



            #endregion

            #region 74 8 stations
            dal.AddStation(new Station
            {
                StationCode = 20277,
                StationName = "מסוף רידינג",
                StationAddress = "תל אביב יפו  רידינג",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.0989,
                LocationLongitude = 34.78
            });

            dal.AddStation(new Station
            {
                StationCode = 28596,
                StationName = "אבן גבירול שדרות רוקח",
                StationAddress = "תל אביב יפו שדרות רוקח",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.098,
                LocationLongitude = 34.7834
            });
            dal.AddStation(new Station
            {
                StationCode = 25893,
                StationName = "כיכר מילאנו אבן גבירול",
                StationAddress = "  תל אביב יפו אבן גבירול 181",
                PlaceToSit = true,
                BoardBusTiming = false,
                LocationLatitude = 32.0943,
                LocationLongitude = 34.7836
            });
            dal.AddStation(new Station
            {
                StationCode = 21128,
                StationName = "אבן גבירול שדרות נורדאו",
                StationAddress = " אבן גבירול 155",
                PlaceToSit = true,
                BoardBusTiming = false,
                LocationLatitude = 32.0912,
                LocationLongitude = 34.7828
            });
            dal.AddStation(new Station
            {
                StationCode = 25852,
                StationName = "אבן גבירול פנקס",
                StationAddress = " תל אביב אבן גבירול 147",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.0898,
                LocationLongitude = 34.7827
            });
            dal.AddStation(new Station
            {
                StationCode = 25829,
                StationName = "אבן גבירול ארלוזורוב",
                StationAddress = " תל אביב יפו אבן גבירול 113",
                PlaceToSit = true,
                BoardBusTiming = false,
                LocationLatitude = 32.0852,
                LocationLongitude = 34.7825
            });
            dal.AddStation(new Station
            {
                StationCode = 25823,
                StationName = "עיריית תא אבן גבירול",
                StationAddress = " אבן גבירול תל אביב יפו",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.0619,
                LocationLongitude = 34.7814
            });
            dal.AddStation(new Station
            {
                StationCode = 21516,
                StationName = "ככר רבין אבן גבירול",
                StationAddress = " אבן גבירול 63",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.0788,
                LocationLongitude = 34.7812


            });




            dal.AddBusLine(new BusLine
            {
                FirstLineStation = 20277,
                LastLineStation = 28596,
                BusLineIndentificator = Configuration.BusInLineIndentificator++,
                LineArea = Area.Center,
                LineNumber = 74
            });
            dal.AddTwoFollowingStations(new TwoFollowingStations
            {
                FirstStationCode = 20277,
                SecondStationCode = 28596,
                DistanceBetweenStations = 0.2,
                AverageTimeBetweenStations = new TimeSpan(0, 0, 40)

            });
            dal.AddTwoFollowingStations(new TwoFollowingStations
            {
                FirstStationCode = 28596,
                SecondStationCode = 25893,
                DistanceBetweenStations = 1.42,
                AverageTimeBetweenStations = new TimeSpan(0, 0, 40)

            });
            dal.AddTwoFollowingStations(new TwoFollowingStations
            {
                FirstStationCode = 25893,
                SecondStationCode = 21128,
                DistanceBetweenStations = 0.27,
                AverageTimeBetweenStations = new TimeSpan(0, 1, 0)
            });
            dal.AddTwoFollowingStations(new TwoFollowingStations
            {
                FirstStationCode = 21128,
                SecondStationCode = 25852,
                DistanceBetweenStations = 0.39,
                AverageTimeBetweenStations = new TimeSpan(0, 2, 0)
            });
            dal.AddTwoFollowingStations(new TwoFollowingStations
            {
                FirstStationCode = 25852,
                SecondStationCode = 25829,
                DistanceBetweenStations = 0.35,
                AverageTimeBetweenStations = new TimeSpan(0, 1, 0)
            });
            dal.AddTwoFollowingStations(new TwoFollowingStations
            {
                FirstStationCode = 25829,
                SecondStationCode = 25823,
                DistanceBetweenStations = 0.6,
                AverageTimeBetweenStations = new TimeSpan(0, 1, 0)
            });
            dal.AddTwoFollowingStations(new TwoFollowingStations
            {
                FirstStationCode = 25823,
                SecondStationCode = 21516,
                DistanceBetweenStations = 0.6,
                AverageTimeBetweenStations = new TimeSpan(0, 2, 0)
            });


            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 1,
                StationCode = 20277
            });
            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 2,
                StationCode = 28596
            });
            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 3,
                StationCode = 25893
            });
            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 4,
                StationCode = 21128
            });
            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 5,
                StationCode = 25852
            });
            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 6,
                StationCode = 25829
            });
            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 7,
                StationCode = 25823
            });
            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 8,
                StationCode = 21516
            });


            #endregion

            #region 947 3 stations
            dal.AddStation(new Station
            {
                StationCode = 42658,
                StationName = "תחנה המרכזית חוף הכרמל ",
                StationAddress = "חיפה",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.7938,
                LocationLongitude = 34.959
            });

            dal.AddStation(new Station
            {
                StationCode = 30136,
                StationName = "ת.מרכזית נתניה רציפים",
                StationAddress = "   שדרות בינימין תחנה מרכזית נתניה",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.3269,
                LocationLongitude = 34.8587
            });
            dal.AddStation(new Station
            {
                StationCode = 37269,
                StationName = "מסוף רעננה",
                StationAddress = "רעננה",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.1751,
                LocationLongitude = 34.8901
            });
            dal.AddBusLine(new BusLine
            {
                FirstLineStation = 42658,
                LastLineStation = 37269,
                BusLineIndentificator = Configuration.BusInLineIndentificator++,
                LineArea = Area.North,
                LineNumber = 947
            });
            dal.AddTwoFollowingStations(new TwoFollowingStations
            {
                FirstStationCode = 42658,
                SecondStationCode = 30136,
                DistanceBetweenStations = 40.80,
                AverageTimeBetweenStations = new TimeSpan(0, 51, 0)

            });
            dal.AddTwoFollowingStations(new TwoFollowingStations
            {
                FirstStationCode = 30136,
                SecondStationCode = 37269,
                DistanceBetweenStations = 28.8,
                AverageTimeBetweenStations = new TimeSpan(0, 36, 0)
            });

            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 1,
                StationCode = 42658
            });
            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 2,
                StationCode = 30136
            });
            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 3,
                StationCode = 37269
            });


            #endregion

            #region 1 3 stations
            dal.AddStation(new Station
            {
                StationCode = 3374,
                StationName = "ביטוח לאומי ",
                StationAddress = "שדרות שזר ירושלים",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.7845,
                LocationLongitude = 35.2008
            });

            dal.AddStation(new Station
            {
                StationCode = 3632,
                StationName = "מלכי ישראל בן מתיתיהו",
                StationAddress = "מלכי ישראל 30",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.7892,
                LocationLongitude = 35.2152
            });
            dal.AddStation(new Station
            {
                StationCode = 3379,
                StationName = "הנביאים הרב קוק",
                StationAddress = "הנביאים 56 ירושלים",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.7839,
                LocationLongitude = 35.2209
            });
            dal.AddBusLine(new BusLine
            {
                FirstLineStation = 3374,
                LastLineStation = 3379,
                BusLineIndentificator = Configuration.BusInLineIndentificator++,
                LineArea = Area.Jerusalem,
                LineNumber = 1
            });
            dal.AddTwoFollowingStations(new TwoFollowingStations
            {
                FirstStationCode = 3374,
                SecondStationCode = 3632,
                DistanceBetweenStations = 3,
                AverageTimeBetweenStations = new TimeSpan(0, 6, 0)

            });
            dal.AddTwoFollowingStations(new TwoFollowingStations
            {
                FirstStationCode = 3632,
                SecondStationCode = 3379,
                DistanceBetweenStations = 2,
                AverageTimeBetweenStations = new TimeSpan(0, 4, 0)
            });

            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 1,
                StationCode = 3374
            });
            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 2,
                StationCode = 3632
            });
            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 3,
                StationCode = 3379
            });



            #endregion

            #region 23  2 stations

            dal.AddStation(new Station
            {
                StationCode = 26156,
                StationName = "כורזין",
                StationAddress = " גבעתיים כורזין 6",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.0599,
                LocationLongitude = 34.8186
            });

            dal.AddStation(new Station
            {
                StationCode = 26158,
                StationName = "הרב הרצוג טבנקין",
                StationAddress = " גבעתיים הרב הרצוג 38",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.0617,
                LocationLongitude = 34.8186
            });

            dal.AddBusLine(new BusLine
            {
                FirstLineStation = 26156,
                LastLineStation = 26158,
                BusLineIndentificator = Configuration.BusInLineIndentificator++,
                LineArea = Area.Center,
                LineNumber = 23
            });
            dal.AddTwoFollowingStations(new TwoFollowingStations
            {
                FirstStationCode = 26156,
                SecondStationCode = 26158,
                DistanceBetweenStations = 42.80,
                AverageTimeBetweenStations = new TimeSpan(0, 1, 0)

            });


            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 1,
                StationCode = 26156
            });
            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 2,
                StationCode = 26158
            });


            #endregion

            #region 15 2 stations

            dal.AddStation(new Station
            {
                StationCode = 19228,
                StationName = "מגדל מים",
                StationAddress = "תל שבע",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.2575,
                LocationLongitude = 34.8652
            });

            dal.AddStation(new Station
            {
                StationCode = 19227,
                StationName = "שכונה 7",
                StationAddress = "תל שבע",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.3973,
                LocationLongitude = 34.761
            });

            dal.AddBusLine(new BusLine
            {
                FirstLineStation = 19228,
                LastLineStation = 19227,
                BusLineIndentificator = Configuration.BusInLineIndentificator++,
                LineArea = Area.South,
                LineNumber = 15
            });
            dal.AddTwoFollowingStations(new TwoFollowingStations
            {
                FirstStationCode = 19228,
                SecondStationCode = 19227,
                DistanceBetweenStations = 0.5,
                AverageTimeBetweenStations = new TimeSpan(0, 1, 0)

            });

            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 1,
                StationCode = 19228
            });
            dal.AddPointInPathLine(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 2,
                StationCode = 19227
            });


            #endregion

            Console.ReadLine();

        }
    }
}
