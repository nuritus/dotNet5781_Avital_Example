using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device;
using DO;


namespace DS
{
    public static class DataSource
    {
        public static List<Bus> Buses; //בונוס..
        public static List<BusLine> Lines;
        public static List<PointInPathLine> PointsInLinePath;//++++++++
        public static List<Station> Stations;
        public static List<TwoFollowingStations> TwoStations = new List<TwoFollowingStations>();//++++++++
        public static List<User> Users;
        public static List<LineTrip> linesTimes;




        static Random random = new Random(DateTime.Now.Millisecond);
        static DataSource()
        {
            Stations = new List<Station>();
            Buses = new List<Bus>();
            Lines = new List<BusLine>();
            PointsInLinePath = new List<PointInPathLine>();
            TwoStations = new List<TwoFollowingStations>();
            Users = new List<User>();
            linesTimes = new List<LineTrip>();


            //---------------- 20 buses at least ---------------------
            int licenseNumbers = 1111111;
            int myMileage = 20;
            int myFuelTank = 1200;
            bool myInCity = true;
            bool mySelfPayement = false;


            for (int i = 0; i < 20; i++)
            {
                myMileage += 20;
                myFuelTank -= 5;
                myInCity = !(myInCity);
                mySelfPayement = !(mySelfPayement);

                Buses.Add(new Bus

                {
                    LicenseNumber = "" + licenseNumbers++,
                    LicensingDate = new DateTime(2010, 1, 1),
                    Mileage = myMileage,
                    FuelTank = myFuelTank,
                    InCity = myInCity,
                    SelfPayment = mySelfPayement,
                    Status = BusStatus.ReadyToGo
                });
            }
            //---------------- 50 stations at least ---------------------
            //int myCodeStation = 9;
            //bool myBoardBusTiming = false;
            #region stations lea lines
            //Stations.Add(new Station
            //{
            //    StationCode = 156,
            //    //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "ביטוח לאומי",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //}) ;
            //Stations.Add(new Station
            //{
            //    StationCode = 170,
            //    //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "בנייני האומה/ שדרות שזר",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 20001,
            //    //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "תחנת רכבת תל אביב מרכז הורדה",
            //    StationAddress = "תל אביב",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 37113,
            //    //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "הקוממיות",
            //    StationAddress = "בת ים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 4170,
            //  //  StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "תחנה המרכזית ירושלים קומה 3",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 42658,
            //   // StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = " תחנה המרכזית חוף הכרמל רציפים בינעירוני  ",
            //    StationAddress = "חיפה",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 1089,
            //    //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "המרפא",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 2595,
            //   // StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "מסוף אגד קדיש לוז",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 1089,
            //   // StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "המרפא",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 39719,
            //    //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "מסוף וולפסון",
            //    StationAddress = "חולון",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 31424,
            //   // StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "רכבת קריית אריה חנה וסע",
            //    StationAddress = "פתח תקווה",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 19215,
            //    //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "שכונה 4",
            //    StationAddress = "שגב שלום",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 19249,
            //    //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "יהושוע הצורף",
            //    StationAddress = "באר שבע",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 2524,
            //   // StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "מסוף אגד כנפי נשרים",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 146,
            //    //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "מירסקי/מינץ",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 3375,
            //   // StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "חניון הלאום/ שדרות הנשיא השישי",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 203,
            //   // StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "תחנת רכבת מלחה",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 5912,
            //    //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "מסוף רמת רחל",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 1953,
            //    //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "קמפוס ספרא/ מסוף כפר ההייטק",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station //19
            //{
            //    StationCode = 3608,
            //   // StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "הספרית הלאומית",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 4174,
            //    //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "תחנת רכבת קלה יפו מרכז /שטרוס",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 1083,
            //    //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "קרן היסוד /שלום עליכם",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode =638,
            //   // StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "מחלף חמד",
            //    StationAddress = " ליד ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 1953,
            //   // StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "קמפוס ספרא/ מסוף כפר ההייטק",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 39522,
            //    //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "תחנה המרכזית נתניה",
            //    StationAddress = "נתניה",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 1953,
            //    //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "קמפוס ספרא/ מסוף כפר ההייטק",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 2783,
            //    //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "פרץ ברנשטיין",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = false,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 2747,
            //   // StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "מכללה  לבנות ירושלים /דובדבני",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 1589,
            //    //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "תחנת רכבת קלה יפה נוף",
            //    StationAddress = "ירושלים",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 995,
            //    //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "בית השנהב /בית הדפוס",
            //    StationAddress ="גבעת שאול ירושלום",
            //    PlaceToSit = true,
            //    BoardBusTiming = false,
            //    Deleted = false,

            //});
            //Stations.Add(new Station
            //{
            //    StationCode = 992,
            //    //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
            //    StationName = "נגארה בן עוזיאל",
            //    StationAddress = "ירושלים גבעת שאול",
            //    PlaceToSit = true,
            //    BoardBusTiming = true,
            //    Deleted = false,

            //});
            #endregion





            //---------------- 10 bus lines at least ---------------------

            //Lines.Add(new BusLine
            //{
            //    LineNumber = 1,
            //    BusLineIndentificator = Configuration.BusInLineIndentificator++,
            //    FirstLineStation = 156,
            //    LastLineStation = 170,
            //    LineArea = (Area)4, 

            //});
            //Lines.Add(new BusLine
            //{
            //    LineNumber = 18,
            //    BusLineIndentificator = Configuration.BusInLineIndentificator++,
            //    FirstLineStation = 20001,
            //    LastLineStation = 37113,
            //    LineArea = (Area)3,

            //});
            //Lines.Add(new BusLine
            //{
            //    LineNumber = 947,
            //    BusLineIndentificator = Configuration.BusInLineIndentificator++,
            //    FirstLineStation = 4170,
            //    LastLineStation = 42658,
            //    LineArea = (Area)1,

            //});
            //Lines.Add(new BusLine
            //{
            //    LineNumber = 39,
            //    BusLineIndentificator = Configuration.BusInLineIndentificator++,
            //    FirstLineStation = 1089,
            //    LastLineStation = 2595,
            //    LineArea = (Area)4,

            //});
            //Lines.Add(new BusLine
            //{
            //    LineNumber = 21,
            //    BusLineIndentificator = Configuration.BusInLineIndentificator++,
            //    FirstLineStation = 2595,
            //    LastLineStation = 2595,
            //    LineArea = (Area)4,

            //});
            //Lines.Add(new BusLine
            //{
            //    LineNumber = 145,
            //    BusLineIndentificator = Configuration.BusInLineIndentificator++,
            //    FirstLineStation = 39719,
            //    LastLineStation = 31424,
            //    LineArea = (Area)3,

            //});
            //Lines.Add(new BusLine
            //{
            //    LineNumber = 70,
            //    BusLineIndentificator = Configuration.BusInLineIndentificator++,
            //    FirstLineStation = 19215,
            //    LastLineStation = 19249,
            //    LineArea = (Area)2,

            //});
            //Lines.Add(new BusLine
            //{
            //    LineNumber = 64,
            //    BusLineIndentificator = Configuration.BusInLineIndentificator++,
            //    FirstLineStation = 2524,
            //    LastLineStation = 146,
            //    LineArea = (Area)4,

            //});
            //Lines.Add(new BusLine
            //{
            //    LineNumber = 35,
            //    BusLineIndentificator = Configuration.BusInLineIndentificator++,
            //    FirstLineStation = 3375,
            //    LastLineStation = 203,
            //    LineArea = (Area)4,

            //});
            //Lines.Add(new BusLine
            //{
            //    LineNumber = 7,
            //    BusLineIndentificator = Configuration.BusInLineIndentificator++,
            //    FirstLineStation = 5912,
            //    LastLineStation = 1953,
            //    LineArea = (Area)4,

            //});

            //PointsInLinePath.Add(new PointInPathLine
            //{
            //    StationCode = 156,
            //    NumberInPath = 1,
            //    //PointInPathLineNumber = config,
            //});
            //PointsInLinePath.Add(new PointInPathLine
            //{
            //    StationCode = 170,
            //    NumberInPath = 31,
            //PointInPathLineNumber = config
            //});
            //    for (int j = 2; j < 10; j++)
            //    {
            //        PointsInLinePath.Add(new PointInPathLine
            //        {
            //            StationCode = random.Next(9, 58),
            //            NumberInPath = j,
            //            PointInPathLineNumber = config
            //        });
            //    }

            //    Users = new List<User>();
            //    TwoStations = new List<TwoFollowingStations>();
            //}
            Users.Add(new User { UserName = "Avital", UserPassword = "1234", UserAccessManagement = true });
            Users.Add(new User { UserName = "Léa", UserPassword = "6789", UserAccessManagement = false });
            Users.Add(new User { UserName = "mainManeger", UserPassword = "1234", UserAccessManagement = true });


            #region 480  3 stations
            Stations.Add(new Station
            {
                StationCode = 4170,
                //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
                StationName = "ת. מרכזית ירושלים קומה 3/רציפים",
                StationAddress = "יפו 224,ירושלים",
                LocationLatitude = 31.789567,
                LocationLongitude = 35.203721,
                PlaceToSit = true,
                BoardBusTiming = true,
                Deleted = false,

            });
            Stations.Add(new Station
            {
                StationCode = 638,
                //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
                StationName = "מחלף חמד",
                StationAddress = "מחלף חמד",
                LocationLatitude = 31.80133,
                LocationLongitude = 35.12450,
                PlaceToSit = true,
                BoardBusTiming = true,
                Deleted = false,

            });
            Stations.Add(new Station
            {
                StationCode = 20594,
                //StationLocation = new GeoCoordinate(random.NextDouble() * (33.3 - 31) + 31, random.NextDouble() * (35.5 - 34.3) + 34.3),
                StationName = "ת. רכבת ת''א מרכז/הורדה",
                StationAddress = "תל אביב יפו",
                LocationLatitude = 32.08301,
                LocationLongitude = 34.79722,
                PlaceToSit = true,
                BoardBusTiming = true,
                Deleted = false,

            });

            Lines.Add(new BusLine
            {
                FirstLineStation = 4170,
                LastLineStation = 20594,
                BusLineIndentificator = Configuration.BusInLineIndentificator++,
                LineArea = Area.General,
                LineNumber = 480
            });

            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 4170,
                SecondStationCode = 638,
                DistanceBetweenStations = 8,
                AverageTimeBetweenStations = new TimeSpan(0, 13, 0)

            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 638,
                SecondStationCode = 20594,
                DistanceBetweenStations = 44,
                AverageTimeBetweenStations = new TimeSpan(0, 50, 0)

            });
            PointsInLinePath.Add(new PointInPathLine
            {
                StationCode = 4170,
                NumberInPath = 1,
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                StationCode = 638,
                NumberInPath = 2,
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                StationCode = 20594,
                NumberInPath = 3,
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1
            });

            linesTimes.Add(new LineTrip
            { BusLineIndentificator = Configuration.BusInLineIndentificator - 1,
                TimeFirstLineExit = new TimeSpan(6, 0, 0),
                Frequency = new TimeSpan(0, 10, 0),
                TimeLastLineExit = new TimeSpan(1, 0, 0) });


            #endregion

            #region 63  10 stations
            #region stations
            Stations.Add(new Station
            {
                StationCode = 138,
                StationName = "מירסקי/מינץ",
                StationAddress = "יצחק מירסקי 33, ירושלים",
                PlaceToSit = true,
                BoardBusTiming = false,
                LocationLatitude = 31.82131,
                LocationLongitude = 35.19455
            });
            Stations.Add(new Station
            {
                StationCode = 720,
                StationName = "יצחק מירסקי/בנימין מינץ",
                StationAddress = "יצחק מירסקי 23, ירושלים",
                PlaceToSit = true,
                BoardBusTiming = false,
                LocationLatitude = 31.820562,
                LocationLongitude = 35.193561
            });
            Stations.Add(new Station
            {
                StationCode = 877,
                StationName = "יצחק מירסקי/אלימלך מליז'נסק",
                StationAddress = "יצחק מירסקי 7, ירושלים",
                PlaceToSit = true,
                BoardBusTiming = false,
                LocationLatitude = 31.820962,
                LocationLongitude = 35.191709
            });
            Stations.Add(new Station
            {
                StationCode = 906,
                StationName = "יצחק מירסקי/שדרות גולדה מאיר",
                StationAddress = "יצחק מירסקי , ירושלים",
                PlaceToSit = true,
                BoardBusTiming = false,
                LocationLatitude = 31.821795,
                LocationLongitude = 35.190116
            });
            Stations.Add(new Station
            {
                StationCode = 4007,
                StationName = "שדרות גולדה מאיר/אברהם רקנאטי",
                StationAddress = "שדרות גולדה מאיר , ירושלים",
                PlaceToSit = true,
                BoardBusTiming = false,
                LocationLatitude = 31.821048,
                LocationLongitude = 35.189544
            });
            Stations.Add(new Station
            {
                StationCode = 9855,
                StationName = "מרכז מסחרי/גולדה",
                StationAddress = "ירושלים",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.81742,
                LocationLongitude = 35.194919
            });
            Stations.Add(new Station
            {
                StationCode = 3593,
                StationName = "אסירי ציון/צונדק",
                StationAddress = "אסירי ציון, ירושלים",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.812665,
                LocationLongitude = 35.196069
            });
            Stations.Add(new Station
            {
                StationCode = 4179,
                StationName = "שיבת ציון/גולדה",
                StationAddress = "שיבת ציון, ירושלים",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.815286,
                LocationLongitude = 35.196741
            });
            Stations.Add(new Station
            {
                StationCode = 333,
                StationName = "שיבת ציון/טללים",
                StationAddress = "שיבת ציון, ירושלים",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.815281,
                LocationLongitude = 35.199427
            });
            Stations.Add(new Station
            {
                StationCode = 2183,
                StationName = "סילבר/שיבת ציון",
                StationAddress = "אבא הלל סילבר 38, ירושלים",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.815914,
                LocationLongitude = 35.201461
            });
            #endregion
            Lines.Add(new BusLine
            {
                FirstLineStation = 138,
                LastLineStation = 2183,
                BusLineIndentificator = Configuration.BusInLineIndentificator++,
                LineNumber = 63,
                LineArea = Area.Jerusalem
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                StationCode = 138,
                NumberInPath = 1
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                StationCode = 720,
                NumberInPath = 2
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                StationCode = 877,
                NumberInPath = 3
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                StationCode = 906,
                NumberInPath = 4
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                StationCode = 4007,
                NumberInPath = 5
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                StationCode = 9855,
                NumberInPath = 6
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                StationCode = 3593,
                NumberInPath = 7
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                StationCode = 4179,
                NumberInPath = 8
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                StationCode = 333,
                NumberInPath = 9
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                StationCode = 2183,
                NumberInPath = 10
            });

            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 138,
                SecondStationCode = 720,
                DistanceBetweenStations = 0.12,
                AverageTimeBetweenStations = new TimeSpan(0, 2, 0)

            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 720,
                SecondStationCode = 877,
                DistanceBetweenStations = 0.15,
                AverageTimeBetweenStations = new TimeSpan(0, 2, 30)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 877,
                SecondStationCode = 906,
                DistanceBetweenStations = 0.22,
                AverageTimeBetweenStations = new TimeSpan(0, 5, 0)

            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 906,
                SecondStationCode = 4007,
                DistanceBetweenStations = 0.09,
                AverageTimeBetweenStations = new TimeSpan(0, 1, 30)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 4007,
                SecondStationCode = 9855,
                DistanceBetweenStations = 0.70,
                AverageTimeBetweenStations = new TimeSpan(0, 5, 30)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 9855,
                SecondStationCode = 3593,
                DistanceBetweenStations = 0.52,
                AverageTimeBetweenStations = new TimeSpan(0, 4, 30)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 3593,
                SecondStationCode = 4179,
                DistanceBetweenStations = 0.30,
                AverageTimeBetweenStations = new TimeSpan(0, 3, 30)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 4179,
                SecondStationCode = 333,
                DistanceBetweenStations = 0.24,
                AverageTimeBetweenStations = new TimeSpan(0, 2, 0)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 333,
                SecondStationCode = 2183,
                DistanceBetweenStations = 0.21,
                AverageTimeBetweenStations = new TimeSpan(0, 2, 30)
            });
            linesTimes.Add(new LineTrip
            {
                BusLineIndentificator = Configuration.BusInLineIndentificator - 1,
                Frequency = new TimeSpan(1, 0, 0),
                TimeFirstLineExit = new TimeSpan(23, 58, 0),
                TimeLastLineExit = new TimeSpan(4, 0, 0)
            });
            //linesTimes.Add(new LineTrip
            //{
            //    BusLineIndentificator = Configuration.BusInLineIndentificator - 1,
            //    Frequency = new TimeSpan(0, 20, 0),
            //    TimeFirstLineExit = new TimeSpan(16, 30, 0),
            //    TimeLastLineExit = new TimeSpan(23, 0, 0)
            //});


            #endregion //להוסיף

            #region 67  6 stations
            Stations.Add(new Station
            {
                StationCode = 26438,
                StationName = "קניון איילון",
                StationAddress = "רמת גן",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.099079,
                LocationLongitude = 34.825456
            });
            Stations.Add(new Station
            {
                StationCode = 26355,
                StationName = "מוזיאון לאמנות/דרך אבא הלל",
                StationAddress = "דרך אבא הלל, רמת גן",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.095279,
                LocationLongitude = 34.819734
            });
            Stations.Add(new Station
            {
                StationCode = 26352,
                StationName = "דרך אבא הלל/רש''י",
                StationAddress = "דרך אבא הלל 125, רמת גן",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.0931,
                LocationLongitude = 34.816503
            });
            Stations.Add(new Station
            {
                StationCode = 26287,
                StationName = "דרך אבא הלל/התקווה",
                StationAddress = "דרך אבא הלל 85, רמת גן",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.090769,
                LocationLongitude = 34.811475
            });
            Stations.Add(new Station
            {
                StationCode = 26284,
                StationName = "דרך אבא הלל/רוקח",
                StationAddress = "דרך אבא הלל 51, רמת גן",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.088894,
                LocationLongitude = 34.808172
            });
            Stations.Add(new Station
            {
                StationCode = 26255,
                StationName = "דרך אבא הלל/ביאליק",
                StationAddress = "דרך אבא הלל 25, רמת גן",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.086969,
                LocationLongitude = 34.805391
            });
            Lines.Add(new BusLine
            {
                BusLineIndentificator = Configuration.BusInLineIndentificator++,
                FirstLineStation = 26438,
                LastLineStation = 26255,
                LineArea = Area.Center,
                LineNumber = 67
            });

            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                StationCode = 26438,
                NumberInPath = 1
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                StationCode = 26355,
                NumberInPath = 2
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                StationCode = 26352,
                NumberInPath = 3
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                StationCode = 26287,
                NumberInPath = 4
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                StationCode = 26284,
                NumberInPath = 5
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                StationCode = 26255,
                NumberInPath = 6
            });

            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 26438,
                SecondStationCode = 26355,
                DistanceBetweenStations = 0.68,
                AverageTimeBetweenStations = new TimeSpan(0, 2, 0)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 26355,
                SecondStationCode = 26352,
                DistanceBetweenStations = 0.39,
                AverageTimeBetweenStations = new TimeSpan(0, 1, 0)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 26352,
                SecondStationCode = 26287,
                DistanceBetweenStations = 0.34,
                AverageTimeBetweenStations = new TimeSpan(0, 2, 0)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 26287,
                SecondStationCode = 26284,
                DistanceBetweenStations = 0.58,
                AverageTimeBetweenStations = new TimeSpan(0, 1, 0)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 26284,
                SecondStationCode = 26255,
                DistanceBetweenStations = 0.10,
                AverageTimeBetweenStations = new TimeSpan(0, 1, 0)
            });
            #endregion

            #region 405  2 stations

            Stations.Add(new Station
            {
                StationCode = 21274,
                StationName = "מחלף לה גוורדיה",
                StationAddress = "לה גוורדיה, תל אביב יפו",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.059454,
                LocationLongitude = 34.786354
            });

            Stations.Add(new Station
            {
                StationCode = 22955,
                StationName = "ת.מרכזית ת''א ק.7/רציפים",
                StationAddress = "תחנה מרכזית קומה 7, תל אביב יפו",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.056413,
                LocationLongitude = 34.779452
            });

            Lines.Add(new BusLine
            {
                FirstLineStation = 21274,
                LastLineStation = 22955,
                BusLineIndentificator = Configuration.BusInLineIndentificator++,
                LineArea = Area.General,
                LineNumber = 405
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 638,
                SecondStationCode = 21274,
                DistanceBetweenStations = 42.80,
                AverageTimeBetweenStations = new TimeSpan(0, 35, 0)

            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 21274,
                SecondStationCode = 22955,
                DistanceBetweenStations = 0.78,
                AverageTimeBetweenStations = new TimeSpan(0, 4, 0)
            });

            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 1,
                StationCode = 4170
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 2,
                StationCode = 638
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 3,
                StationCode = 21274
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 4,
                StationCode = 22955
            });

            #endregion

            #region 67  6 stations
            Stations.Add(new Station
            {
                StationCode = 4169,
                StationName = "ת. מרכזית ירושלים/יפו",
                StationAddress = "יפו 185,ירושלים",
                LocationLatitude = 31.788654,
                LocationLongitude = 35.203665,
                PlaceToSit = true,
                BoardBusTiming = true,
                Deleted = false,

            });
            Stations.Add(new Station
            {
                StationCode = 2524,
                StationName = "מסוף אגד/כנפי נשרים",
                StationAddress = "כנפי נשרים 35, ירושלים",
                LocationLatitude = 31.787175,
                LocationLongitude = 35.181048,
                PlaceToSit = true,
                BoardBusTiming = true,
                Deleted = false,

            });
            Stations.Add(new Station
            {
                StationCode = 992,
                StationName = "נג'ארה/בן עוזיאל",
                StationAddress = "רבי ישראל נג'ארה 31, ירושלים",
                LocationLatitude = 31.790195,
                LocationLongitude = 35.191514,
                PlaceToSit = true,
                BoardBusTiming = true,
                Deleted = false,

            });
            Stations.Add(new Station
            {
                StationCode = 176,
                StationName = "גבעת שאול/נג'ארה",
                StationAddress = "גבעת שאול 17, ירושלים",
                LocationLatitude = 31.792105,
                LocationLongitude = 35.192721,
                PlaceToSit = true,
                BoardBusTiming = true,
                Deleted = false,

            });
            Stations.Add(new Station
            {
                StationCode = 174,
                StationName = "גבעת שאול/קוטלר",
                StationAddress = "גבעת שאול 9, ירושלים",
                LocationLatitude = 31.791653,
                LocationLongitude = 35.194683,
                PlaceToSit = true,
                BoardBusTiming = true,
                Deleted = false,

            });
            Stations.Add(new Station
            {
                StationCode = 173,
                StationName = "גבעת שאול/כתב סופר",
                StationAddress = "גבעת שאול 3,ירושלים",
                LocationLatitude = 31.791204,
                LocationLongitude = 35.195787,
                PlaceToSit = true,
                BoardBusTiming = true,
                Deleted = false,

            });

            Lines.Add(new BusLine
            {
                FirstLineStation = 4169,
                LastLineStation = 173,
                BusLineIndentificator = Configuration.BusInLineIndentificator++,
                LineArea = Area.Jerusalem,
                LineNumber = 67
            });

            PointsInLinePath.Add(new PointInPathLine
            {
                StationCode = 4169,
                NumberInPath = 6,
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                StationCode = 2524,
                NumberInPath = 1,
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                StationCode = 992,
                NumberInPath = 2,
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                StationCode = 176,
                NumberInPath = 3,
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                StationCode = 174,
                NumberInPath = 4,
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                StationCode = 173,
                NumberInPath = 5,
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1
            });

            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 173,
                SecondStationCode = 4169,
                DistanceBetweenStations = 3.30,
                AverageTimeBetweenStations = new TimeSpan(0, 3, 0)

            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 2524,
                SecondStationCode = 992,
                DistanceBetweenStations = 0.99,
                AverageTimeBetweenStations = new TimeSpan(0, 8, 0)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 992,
                SecondStationCode = 176,
                DistanceBetweenStations = 0.31,
                AverageTimeBetweenStations = new TimeSpan(0, 1, 0)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 176,
                SecondStationCode = 174,
                DistanceBetweenStations = 0.26,
                AverageTimeBetweenStations = new TimeSpan(0, 1, 0)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 174,
                SecondStationCode = 173,
                DistanceBetweenStations = 0.05,
                AverageTimeBetweenStations = new TimeSpan(0, 0, 40)
            });

            #endregion

            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 2183,
                SecondStationCode = 3593,
                DistanceBetweenStations = 0.21,
                AverageTimeBetweenStations = new TimeSpan(0, 2, 30)
            });
            #region 21 7 stations 
            Stations.Add(new Station
            {
                StationCode = 1819,
                StationName = "מסןף אגד קדיש לוז",
                StationAddress = "קדיש לוז 22",
                PlaceToSit = false,
                BoardBusTiming = false,
                LocationLatitude = 31.7614,
                LocationLongitude = 35.1833
            });

            Stations.Add(new Station
            {
                StationCode = 2072,
                StationName = "קדיש לוז משה שרת",
                StationAddress = "ירושלים קדיש לוז 2",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.7588,
                LocationLongitude = 35.1838
            });
            Stations.Add(new Station
            {
                StationCode = 2592,
                StationName = "פרץ ברנשטיין משה שרת",
                StationAddress = "  ירושלים פרץ ברנשטיין 7",
                PlaceToSit = true,
                BoardBusTiming = false,
                LocationLatitude = 31.7584,
                LocationLongitude = 35.186
            });
            Stations.Add(new Station
            {
                StationCode = 2783,
                StationName = "פרץ ברנשטיין",
                StationAddress = " ירושלים פרץ ברנשטיין 17",
                PlaceToSit = true,
                BoardBusTiming = false,
                LocationLatitude = 31.7592,
                LocationLongitude = 35.1873
            });
            Stations.Add(new Station
            {
                StationCode = 1518,
                StationName = "פרץ ברנשטיין נזר דוד",
                StationAddress = " ירושלים פרץ ברנשטיין 37",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.7592,
                LocationLongitude = 35.1873
            });
            Stations.Add(new Station
            {
                StationCode = 9871,
                StationName = "הולילנד אבי יונה",
                StationAddress = " ירושלים אבי יונה",
                PlaceToSit = true,
                BoardBusTiming = false,
                LocationLatitude = 31.8174,
                LocationLongitude = 35.1949
            });
            Stations.Add(new Station
            {
                StationCode = 2747,
                StationName = "מכללה לבנות ירושלים דובדבני",
                StationAddress = " ברוך דובדבני ירושלים",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.76238,
                LocationLongitude = 35.191845

            });




            Lines.Add(new BusLine
            {
                FirstLineStation = 1819,
                LastLineStation = 9871,
                BusLineIndentificator = Configuration.BusInLineIndentificator++,
                LineArea = Area.Jerusalem,
                LineNumber = 21
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 1819,
                SecondStationCode = 2072,
                DistanceBetweenStations = 0.29,
                AverageTimeBetweenStations = new TimeSpan(0, 0, 20)

            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 2072,
                SecondStationCode = 2592,
                DistanceBetweenStations = 1.48,
                AverageTimeBetweenStations = new TimeSpan(0, 1, 0)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 2592,
                SecondStationCode = 2783,
                DistanceBetweenStations = 0.2,
                AverageTimeBetweenStations = new TimeSpan(0, 0, 20)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 2783,
                SecondStationCode = 1518,
                DistanceBetweenStations = 0.24,
                AverageTimeBetweenStations = new TimeSpan(0, 0, 20)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 1518,
                SecondStationCode = 9871,
                DistanceBetweenStations = 0.26,
                AverageTimeBetweenStations = new TimeSpan(0, 0, 20)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 9871,
                SecondStationCode = 2747,
                DistanceBetweenStations = 0.25,
                AverageTimeBetweenStations = new TimeSpan(0, 0, 20)
            });

            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 1,
                StationCode = 1819
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 2,
                StationCode = 2072
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 3,
                StationCode = 2592
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 4,
                StationCode = 2783
            });

            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 5,
                StationCode = 1518
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 6,
                StationCode = 9871
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 7,
                StationCode = 2747
            });



            #endregion

            #region 74 8 stations
            Stations.Add(new Station
            {
                StationCode = 20277,
                StationName = "מסוף רידינג",
                StationAddress = "תל אביב יפו  רידינג",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.0989,
                LocationLongitude = 34.78
            });

            Stations.Add(new Station
            {
                StationCode = 28596,
                StationName = "אבן גבירול שדרות רוקח",
                StationAddress = "תל אביב יפו שדרות רוקח",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.098,
                LocationLongitude = 34.7834
            });
            Stations.Add(new Station
            {
                StationCode = 25893,
                StationName = "כיכר מילאנו אבן גבירול",
                StationAddress = "  תל אביב יפו אבן גבירול 181",
                PlaceToSit = true,
                BoardBusTiming = false,
                LocationLatitude = 32.0943,
                LocationLongitude = 34.7836
            });
            Stations.Add(new Station
            {
                StationCode = 21128,
                StationName = "אבן גבירול שדרות נורדאו",
                StationAddress = " אבן גבירול 155",
                PlaceToSit = true,
                BoardBusTiming = false,
                LocationLatitude = 32.0912,
                LocationLongitude = 34.7828
            });
            Stations.Add(new Station
            {
                StationCode = 25852,
                StationName = "אבן גבירול פנקס",
                StationAddress = " תל אביב אבן גבירול 147",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.0898,
                LocationLongitude = 34.7827
            });
            Stations.Add(new Station
            {
                StationCode = 25829,
                StationName = "אבן גבירול ארלוזורוב",
                StationAddress = " תל אביב יפו אבן גבירול 113",
                PlaceToSit = true,
                BoardBusTiming = false,
                LocationLatitude = 32.0852,
                LocationLongitude = 34.7825
            });
            Stations.Add(new Station
            {
                StationCode = 25823,
                StationName = "עיריית תא אבן גבירול",
                StationAddress = " אבן גבירול תל אביב יפו",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.0619,
                LocationLongitude = 34.7814
            });
            Stations.Add(new Station
            {
                StationCode = 21516,
                StationName = "ככר רבין אבן גבירול",
                StationAddress = " אבן גבירול 63",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.0788,
                LocationLongitude = 34.7812


            });




            Lines.Add(new BusLine
            {
                FirstLineStation = 20277,
                LastLineStation = 28596,
                BusLineIndentificator = Configuration.BusInLineIndentificator++,
                LineArea = Area.Center,
                LineNumber = 74
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 20277,
                SecondStationCode = 28596,
                DistanceBetweenStations = 0.2,
                AverageTimeBetweenStations = new TimeSpan(0, 0, 40)

            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 28596,
                SecondStationCode = 25893,
                DistanceBetweenStations = 1.42,
                AverageTimeBetweenStations = new TimeSpan(0, 0, 40)

            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 25893,
                SecondStationCode = 21128,
                DistanceBetweenStations = 0.27,
                AverageTimeBetweenStations = new TimeSpan(0, 1, 0)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 21128,
                SecondStationCode = 25852,
                DistanceBetweenStations = 0.39,
                AverageTimeBetweenStations = new TimeSpan(0, 2, 0)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 25852,
                SecondStationCode = 25829,
                DistanceBetweenStations = 0.35,
                AverageTimeBetweenStations = new TimeSpan(0, 1, 0)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 25829,
                SecondStationCode = 25823,
                DistanceBetweenStations = 0.6,
                AverageTimeBetweenStations = new TimeSpan(0, 1, 0)
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 25823,
                SecondStationCode = 21516,
                DistanceBetweenStations = 0.6,
                AverageTimeBetweenStations = new TimeSpan(0, 2, 0)
            });


            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 1,
                StationCode = 20277
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 2,
                StationCode = 28596
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 3,
                StationCode = 25893
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 4,
                StationCode = 21128
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 5,
                StationCode = 25852
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 6,
                StationCode = 25829
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 7,
                StationCode = 25823
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 8,
                StationCode = 21516
            });


            #endregion

            #region 947 3 stations
            Stations.Add(new Station
            {
                StationCode = 42658,
                StationName = "תחנה המרכזית חוף הכרמל ",
                StationAddress = "חיפה",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.7938,
                LocationLongitude = 34.959
            });

            Stations.Add(new Station
            {
                StationCode = 30136,
                StationName = "ת.מרכזית נתניה רציפים",
                StationAddress = "   שדרות בינימין תחנה מרכזית נתניה",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.3269,
                LocationLongitude = 34.8587
            });
            Stations.Add(new Station
            {
                StationCode = 37269,
                StationName = "מסוף רעננה",
                StationAddress = "רעננה",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.1751,
                LocationLongitude = 34.8901
            });
            Lines.Add(new BusLine
            {
                FirstLineStation = 42658,
                LastLineStation = 37269,
                BusLineIndentificator = Configuration.BusInLineIndentificator++,
                LineArea = Area.North,
                LineNumber = 947
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 42658,
                SecondStationCode = 30136,
                DistanceBetweenStations = 40.80,
                AverageTimeBetweenStations = new TimeSpan(0, 51, 0)

            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 30136,
                SecondStationCode = 37269,
                DistanceBetweenStations = 28.8,
                AverageTimeBetweenStations = new TimeSpan(0, 36, 0)
            });

            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 1,
                StationCode = 42658
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 2,
                StationCode = 30136
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 3,
                StationCode = 37269
            });


            #endregion

            #region 1 3 stations
            Stations.Add(new Station
            {
                StationCode = 3374,
                StationName = "ביטוח לאומי ",
                StationAddress = "שדרות שזר ירושלים",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.7845,
                LocationLongitude = 35.2008
            });

            Stations.Add(new Station
            {
                StationCode = 3632,
                StationName = "מלכי ישראל בן מתיתיהו",
                StationAddress = "מלכי ישראל 30",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.7892,
                LocationLongitude = 35.2152
            });
            Stations.Add(new Station
            {
                StationCode = 3379,
                StationName = "הנביאים הרב קוק",
                StationAddress = "הנביאים 56 ירושלים",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.7839,
                LocationLongitude = 35.2209
            });
            Lines.Add(new BusLine
            {
                FirstLineStation = 3374,
                LastLineStation = 3379,
                BusLineIndentificator = Configuration.BusInLineIndentificator++,
                LineArea = Area.Jerusalem,
                LineNumber = 1
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 3374,
                SecondStationCode = 3632,
                DistanceBetweenStations = 3,
                AverageTimeBetweenStations = new TimeSpan(0, 6, 0)

            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 3632,
                SecondStationCode = 3379,
                DistanceBetweenStations = 2,
                AverageTimeBetweenStations = new TimeSpan(0, 4, 0)
            });

            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 1,
                StationCode = 3374
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 2,
                StationCode = 3632
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 3,
                StationCode = 3379
            });



            #endregion

            #region 23  2 stations

            Stations.Add(new Station
            {
                StationCode = 26156,
                StationName = "כורזין",
                StationAddress = " גבעתיים כורזין 6",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.0599,
                LocationLongitude = 34.8186
            });

            Stations.Add(new Station
            {
                StationCode = 26158,
                StationName = "הרב הרצוג טבנקין",
                StationAddress = " גבעתיים הרב הרצוג 38",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 32.0617,
                LocationLongitude = 34.8186
            });

            Lines.Add(new BusLine
            {
                FirstLineStation = 26156,
                LastLineStation = 26158,
                BusLineIndentificator = Configuration.BusInLineIndentificator++,
                LineArea = Area.Center,
                LineNumber = 23
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 26156,
                SecondStationCode = 26158,
                DistanceBetweenStations = 42.80,
                AverageTimeBetweenStations = new TimeSpan(0, 1, 0)

            });


            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 1,
                StationCode = 26156
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 2,
                StationCode = 26158
            });


            #endregion

            #region 15 2 stations

            Stations.Add(new Station
            {
                StationCode = 19228,
                StationName = "מגדל מים",
                StationAddress = "תל שבע",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.2575,
                LocationLongitude = 34.8652
            });

            Stations.Add(new Station
            {
                StationCode = 19227,
                StationName = "שכונה 7",
                StationAddress = "תל שבע",
                PlaceToSit = true,
                BoardBusTiming = true,
                LocationLatitude = 31.3973,
                LocationLongitude = 34.761
            });

            Lines.Add(new BusLine
            {
                FirstLineStation = 19228,
                LastLineStation = 19227,
                BusLineIndentificator = Configuration.BusInLineIndentificator++,
                LineArea = Area.South,
                LineNumber = 15
            });
            TwoStations.Add(new TwoFollowingStations
            {
                FirstStationCode = 19228,
                SecondStationCode = 19227,
                DistanceBetweenStations = 0.5,
                AverageTimeBetweenStations = new TimeSpan(0, 1, 0)

            });

            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 1,
                StationCode = 19228
            });
            PointsInLinePath.Add(new PointInPathLine
            {
                PointInPathLineNumber = Configuration.BusInLineIndentificator - 1,
                NumberInPath = 2,
                StationCode = 19227
            });


            #endregion

        }
    }
}
