using System;
using System.Collections.Generic;
using DalApi;
using DO;
using DS;
using System.Linq;


namespace Dal
{
    sealed class DalObject : IDal
    {
        #region singelton

        class Nested
        {
            static Nested() { }
            internal static readonly IDal instance = new DalObject();
        }
        static DalObject() { }
        DalObject() { }
        public static IDal Instance
        { get { return Nested.instance; } }

        #endregion

        //--------------------------------------------BUS--------------------------------------------
        #region AddBus
        public void AddBus(Bus busToAdd)
        {
            if (DataSource.Buses.Find(x => x.LicenseNumber == busToAdd.LicenseNumber) != null)
                throw new AlreadyExistException("The bus already exist in the system");
            DataSource.Buses.Add(busToAdd.Clone());
        }
        #endregion

        #region UpdateBus

        public void UpdateBus(Bus busToUpdate)
        {
            Bus myBus = DataSource.Buses.Find(x => x.LicenseNumber == busToUpdate.LicenseNumber);
            if (myBus == null || myBus.Deleted)
                throw new DoesntExistException("This bus doesn't exist in the system");
            myBus.Mileage = busToUpdate.Mileage;
            myBus.InCity = busToUpdate.InCity;
            myBus.SelfPayment = busToUpdate.SelfPayment;
            myBus.Status = busToUpdate.Status;
            //DataSource.Buses.Remove(myBus);
            //DataSource.Buses.Add(busToUpdate.Clone());
        }
        #endregion

        #region DeleteBus
        public void DeleteBus(string licenseNumberToDelete)
        {
            Bus bus = DataSource.Buses.Find(x => x.LicenseNumber == licenseNumberToDelete);
            if (bus == null || bus.Deleted)
                throw new DoesntExistException("The bus doesn't exist in system");
            bus.Deleted = true;

        }
        #endregion

        #region GetABus
        public Bus GetABus(string licenseNumber)
        {
            Bus bus = DataSource.Buses.Find(x => x.LicenseNumber == licenseNumber);
            if (bus != null && !bus.Deleted)
                return bus.Clone();
            throw new DoesntExistException("The bus doesn't exist in system");
        }
        #endregion

        #region GetAllBuses
        public IEnumerable<Bus> GetAllBuses()
        {
            var list = from bus in DataSource.Buses
                       where (!bus.Deleted)
                       select bus.Clone();
            return list;
        }

        #endregion

        #region GetPartOfBuses
        public IEnumerable<Bus> GetPartOfBuses(Predicate<Bus> BusCondition)
        {
            var list = from bus in DataSource.Buses
                       where (!bus.Deleted && BusCondition(bus))
                       select bus.Clone();
            return list;
        }
        #endregion
        //------------------------------------------STATION-------------------------------------------

        #region AddStation 
        public void AddStation(Station stationToAdd)
        {
            if (DataSource.Stations.Find(x => x.StationCode == stationToAdd.StationCode) != null)
                throw new AlreadyExistException("The station already exist in the system");
            DataSource.Stations.Add(stationToAdd.Clone());
        }
        #endregion

        #region UpadateStation 

        public void UpdateStation(Station stationToUpdate)
        {
            Station myStation = DataSource.Stations.Find(x => x.StationCode == stationToUpdate.StationCode);
            if (myStation == null || myStation.Deleted)
                throw new DoesntExistException("This station doesn't exist in the system");
            myStation.BoardBusTiming = stationToUpdate.BoardBusTiming;
            myStation.PlaceToSit = stationToUpdate.PlaceToSit;
            myStation.LocationLatitude = stationToUpdate.LocationLatitude;
            myStation.LocationLongitude = stationToUpdate.LocationLongitude;
            myStation.StationAddress = stationToUpdate.StationAddress;
            myStation.StationName = stationToUpdate.StationName;
            //DataSource.Stations.Remove(myStation);
            //DataSource.Stations.Add(stationToUpdate.Clone());
        }
        #endregion

        #region DeleteStation
        public void DeleteStation(int stationCodeToDelete)
        {
            Station station = DataSource.Stations.Find(x => x.StationCode == stationCodeToDelete);
            if (station == null || station.Deleted == true)
                throw new DoesntExistException("This station doesn't exist in system");
            station.Deleted = true;
        }
        #endregion

        #region GetAStation
        public Station GetAStation(int stationCode)
        {
            Station station = DataSource.Stations.Find(x => x.StationCode == stationCode);
            if (station != null && !station.Deleted)
                return station.Clone();
            throw new DoesntExistException("The station doesn't exist in system");
        }
        #endregion

        #region GetAllStation
        public IEnumerable<Station> GetAllStations()
        {
            var list = from station in DataSource.Stations
                       where (!station.Deleted)
                       select station.Clone();
            return list;
        }
        #endregion

        #region GetPartOfStations 
        public IEnumerable<Station> GetPartOfStations(Predicate<Station> StationCondition)
        {
            var list = from station in DataSource.Stations
                       where (!station.Deleted && StationCondition(station))
                       select station.Clone();
            return list;
        }
        #endregion
        //-----------------------------------------LINE-------------------------------------------------
        #region AddBusLine
        public void AddBusLine(BusLine busLineToAdd)
        {
            BusLine line = busLineToAdd.Clone();
            line.BusLineIndentificator = ++Configuration.BusInLineIndentificator;
            DataSource.Lines.Add(line);
        }
        #endregion

        #region UpdateBusLine
        public void UpdateBusLine(BusLine busLineToUpdate)
        {
            BusLine myLine = DataSource.Lines.Find(x => x.BusLineIndentificator == busLineToUpdate.BusLineIndentificator);
            if (myLine == null)
                throw new DoesntExistException("This bus line doesn't exist in the system");
            myLine.FirstLineStation = busLineToUpdate.FirstLineStation;
            myLine.LastLineStation = busLineToUpdate.LastLineStation;
            myLine.LineArea = busLineToUpdate.LineArea;
            myLine.LineNumber = busLineToUpdate.LineNumber;
            //DataSource.Lines.Remove(myLine);
            //DataSource.Lines.Add(busLineToUpdate.Clone());
        }
        #endregion

        #region DeleteBusLine
        public void DeleteBusLine(int busIndentificatorToDelete)
        {
            BusLine busLine = DataSource.Lines.Find(x => x.BusLineIndentificator == busIndentificatorToDelete);
            if (busLine == null)
                throw new DoesntExistException("The line to delete doesn't exist in system");
            DataSource.Lines.Remove(busLine);
        }
        #endregion

        #region GetABusLine
        public BusLine GetABusLine(int busIndetificator)
        {
            BusLine line = DataSource.Lines.Find(x => x.BusLineIndentificator == busIndetificator);
            if (line != null)
                return line.Clone();
            throw new DoesntExistException("The line doesn't exist in system");
        }
        #endregion

        #region GetAllBusLines

        public IEnumerable<BusLine> GetAllBusLines()
        {
            var list = from line in DataSource.Lines
                       select line.Clone();
            return list;
        }
        #endregion

        #region GetPartOfBusesLines
        public IEnumerable<BusLine> GetPartOfBuseLines(Predicate<BusLine> BusLineCondition)
        {
            var list = from busline in DataSource.Lines
                       where (BusLineCondition(busline))
                       select busline.Clone();
            return list;
        }
        #endregion

        //----------------------------------------POINT-IN-PATH-----------------------------------------

        #region AddPointInPathLine

        public void AddPointInPathLine(PointInPathLine pointToAdd)
        {
            if (DataSource.PointsInLinePath.Find(x => x.PointInPathLineNumber == pointToAdd.PointInPathLineNumber
              && x.NumberInPath == pointToAdd.NumberInPath) != null)
                throw new AlreadyExistException("The point already axist in the path");
            DataSource.PointsInLinePath.Add(pointToAdd.Clone());
        }
        #endregion

        #region UpdatePointInPathLine

        public void UpdatePointInPathLine(PointInPathLine pointToUpdate)
        {
            PointInPathLine point = DataSource.PointsInLinePath.Find
             (x => x.PointInPathLineNumber == pointToUpdate.PointInPathLineNumber
              && x.NumberInPath == pointToUpdate.NumberInPath);
            if (point == null)
                throw new DoesntExistException("This point doesn't exist in the system");
            point.StationCode = pointToUpdate.StationCode;
        }
        #endregion

        #region DeletePoinInPath
        public void DeletePointInPath(int pointNumber, int numberInPath)
        {
            PointInPathLine point = DataSource.PointsInLinePath.Find(x => x.PointInPathLineNumber == pointNumber && x.NumberInPath == numberInPath);
            if (point == null)
                throw new DoesntExistException("The point in path of the line doesn't exist in system");
            DataSource.PointsInLinePath.Remove(point);
        }
        #endregion

        #region GetAPoint
        public PointInPathLine GetAPoint(int numberInPathLine, int pointInPathNumber)
        {
            PointInPathLine point = DataSource.PointsInLinePath.Find(x => x.PointInPathLineNumber == numberInPathLine
              && x.NumberInPath == pointInPathNumber);
            if (point != null)
                return point.Clone();
            throw new DoesntExistException("The point in path doesn't exist");
        }
        #endregion

        #region GetAllThePointInPathLine
        public IEnumerable<PointInPathLine> GetAllThePointInPathLine()
        {
            var list = from point in DataSource.PointsInLinePath
                       select point.Clone();
            return list;
        }
        #endregion

        #region GetPartOfPointInPathLine
        public IEnumerable<PointInPathLine> GetPartOfPointInPathLine(Predicate<PointInPathLine> PointInPathLineCondition)
        {
            var list = from pointInPath in DataSource.PointsInLinePath
                       where PointInPathLineCondition(pointInPath)
                       select pointInPath.Clone();
            return list;
        }
        #endregion

        //--------------------------------------TWO-STATIONS--------------------------------------------

        #region AddTwoFollowingStations
        public void AddTwoFollowingStations(TwoFollowingStations twoStationsToAdd)
        {

            if (DataSource.TwoStations.Find(x => x.FirstStationCode == twoStationsToAdd.FirstStationCode
              && x.SecondStationCode == twoStationsToAdd.SecondStationCode) != null)
                throw new AlreadyExistException("The two following stations already exist in the system");
            DataSource.TwoStations.Add(twoStationsToAdd.Clone());

        }
        #endregion

        #region UpdateTwoFollowingStations
        public void UpdateTwoFollowingStations(TwoFollowingStations twoStationToUpdate)
        {
            TwoFollowingStations twoStations = DataSource.TwoStations.Find(x => x.FirstStationCode == twoStationToUpdate.FirstStationCode
             && x.SecondStationCode == twoStationToUpdate.SecondStationCode);
            if (twoStations == null)
                throw new InfoTwoStationsMissException(twoStationToUpdate.FirstStationCode, twoStationToUpdate.SecondStationCode,
                    "miss information beteen to stations");

            twoStations.DistanceBetweenStations = twoStationToUpdate.DistanceBetweenStations;
            twoStations.AverageTimeBetweenStations = new TimeSpan(twoStationToUpdate.AverageTimeBetweenStations.Ticks);
            //DataSource.TwoStations.Remove(twoStations);
            //DataSource.TwoStations.Add(twoStationToUpdate.Clone());
        }
        #endregion

        #region DeleteTwoFollowingStation
        public void DeleteTwoFollowingStations(int firstCodeStation, int secondCodeStation)
        {
            TwoFollowingStations twoStations = DataSource.TwoStations.Find(x => x.FirstStationCode == firstCodeStation
            && x.SecondStationCode == secondCodeStation);
            if (twoStations == null)
                throw new InfoTwoStationsMissException(firstCodeStation, secondCodeStation, "miss information beteen to stations");
            DataSource.TwoStations.Remove(twoStations);

        }
        #endregion

        #region GetTwoStations
        public TwoFollowingStations GetTwoStations(int firstCodeStation, int secondCodeStation)
        {
            TwoFollowingStations twoStations = DataSource.TwoStations.Find(x => x.FirstStationCode == firstCodeStation
                && x.SecondStationCode == secondCodeStation);
            if (twoStations != null)
                return twoStations.Clone();
            throw new InfoTwoStationsMissException(firstCodeStation, secondCodeStation, "miss information beteen to stations");

        }
        #endregion

        #region GetAllFollowingStations

        public IEnumerable<TwoFollowingStations> GetAllFollowingStations()
        {
            var list = from twoStations in DataSource.TwoStations
                       select twoStations.Clone();
            return list;
        }
        #endregion

        #region GetPartOfTwoFollowingStations
        public IEnumerable<TwoFollowingStations> GetPartOfTwoFollowingStations(Predicate<TwoFollowingStations> TwoFollowingStationsCondition)
        {
            var list = from twoStations in DataSource.TwoStations
                       where (TwoFollowingStationsCondition(twoStations))
                       select twoStations.Clone();
            return list;
        }
        #endregion region 

        //------------------------------------------USER------------------------------------------------
        #region AddUser

        public void AddUser(User userToAdd)
        {
            if (DataSource.Users.Find(x => x.UserName == userToAdd.UserName) != null)
                throw new AlreadyExistException("This user already exist");
            DataSource.Users.Add(userToAdd.Clone());

        }
        #endregion

        #region UpdateUser
        public void UpdateUser(User usertoUpdate)
        {
            User myUser = DataSource.Users.Find(x => x.UserName == usertoUpdate.UserName);
            if (myUser == null)
                throw new DoesntExistException("This user doesn't exist in the system");
            myUser.UserAccessManagement = usertoUpdate.UserAccessManagement;
            myUser.UserPassword = usertoUpdate.UserPassword;
        }
        #endregion

        #region DeleteUser 
        public void DeleteUser(string userName)
        {
            User user = DataSource.Users.Find(x => x.UserName == userName);
            if (user == null)
                throw new DoesntExistException("the user dosn't exist in system");
            DataSource.Users.Remove(user);
        }
        #endregion

        #region GetAUser
        public User GetAUser(string userName)
        {
            User myUser = DataSource.Users.Find(x => x.UserName == userName);
            if (myUser != null)
                return myUser.Clone();
            throw new DoesntExistException("the user doesn't exists in system");
        }
        #endregion

        #region GetAllUsers

        public IEnumerable<User> GetAllUsers()
        {
            var list = from user in DataSource.Users
                       select user.Clone();
            return list;
        }
        #endregion

        #region GetPartOfUsers
        public IEnumerable<User> GetPartOfUsers(Predicate<User> UserCondition)
        {
            var list = from user in DataSource.Users
                       where (UserCondition(user))
                       select user.Clone();
            return list;
        }
        #endregion
        //----------------------------------------LINE-TRIP----------------------------------------------

        #region AddLineTrip
        public void AddLineTrip(LineTrip lineTripToAdd)
        {
            if (lineTripToAdd.TimeFirstLineExit >= new TimeSpan(24, 0, 0) || lineTripToAdd.TimeLastLineExit >= new TimeSpan(24, 0, 0))
                throw new InvalidInputException("the hour is invalid");

            LineTrip lineTimes = lineTripToAdd.Clone();
            //check if there is a bus exit that collision with the new times:
            foreach (var r in DataSource.linesTimes)
            {
                if (lineTimes.BusLineIndentificator == r.BusLineIndentificator)
                {
                    //must be collision.. both drive in 00:00:00..
                    if (lineTimes.TimeFirstLineExit > lineTimes.TimeLastLineExit && r.TimeFirstLineExit > r.TimeLastLineExit)
                        throw new AlreadyExistException("The times of the line that has chose already have defination");
                    //both drive in time 00:00:00- 23:59:59
                    if (lineTimes.TimeFirstLineExit < lineTimes.TimeLastLineExit && r.TimeFirstLineExit < r.TimeLastLineExit)
                    {
                        if (lineTimes.TimeFirstLineExit >= r.TimeFirstLineExit && lineTimes.TimeFirstLineExit < r.TimeLastLineExit)
                            throw new AlreadyExistException("The times of the line that has chose already have defination");
                        if (lineTimes.TimeLastLineExit > r.TimeFirstLineExit && lineTimes.TimeLastLineExit < r.TimeLastLineExit)
                            throw new AlreadyExistException("The times of the line that has chose already have defination");
                        if (lineTimes.TimeFirstLineExit < r.TimeFirstLineExit && lineTimes.TimeLastLineExit > r.TimeLastLineExit)
                            throw new AlreadyExistException("The times of the line that has chose already have defination");
                    }
                    //r drive from "day" to other "day" and lineTimes in one day..
                    else if (r.TimeFirstLineExit > r.TimeLastLineExit && lineTimes.TimeFirstLineExit < lineTimes.TimeLastLineExit)
                    {
                        if (r.TimeLastLineExit > lineTimes.TimeFirstLineExit)
                            throw new AlreadyExistException("The times of the line that has chose already have defination");
                        if (r.TimeFirstLineExit < lineTimes.TimeLastLineExit)
                            throw new AlreadyExistException("The times of the line that has chose already have defination");

                        //if (r.TimeFirstLineExit < lineTriptoUpdate.TimeFirstLineExit || r.TimeLastLineExit > lineTriptoUpdate.TimeLastLineExit)
                        //    throw new AlreadyExistException("The times of the line that has chose already have defination");
                    }
                    // lineTimes drive from "day" to other "day" and r in one day..
                    else if (r.TimeFirstLineExit < r.TimeLastLineExit && lineTimes.TimeFirstLineExit > lineTimes.TimeLastLineExit)
                    {
                        if (r.TimeLastLineExit < lineTimes.TimeFirstLineExit)
                            throw new AlreadyExistException("The times of the line that has chose already have defination");
                        if (r.TimeFirstLineExit > lineTimes.TimeLastLineExit)
                            throw new AlreadyExistException("The times of the line that has chose already have defination");
                    }

                }
            }

            DataSource.linesTimes.Add(lineTripToAdd.Clone());

        }
        #endregion

        #region UpdateLineTrip      
        public void UpdateLineTrip(LineTrip lineTriptoUpdate, TimeSpan OldFirstExit)
        {
            LineTrip mylineTimes = DataSource.linesTimes.Find(x => x.BusLineIndentificator == lineTriptoUpdate.BusLineIndentificator
            && x.TimeFirstLineExit == OldFirstExit);

            if (mylineTimes == null)
                throw new DoesntExistException("This line doesn't exist in the system");

            var listOfOtherTimesOfLine = from x in DataSource.linesTimes
                                         where x.BusLineIndentificator == lineTriptoUpdate.BusLineIndentificator
                                         && x.TimeFirstLineExit != OldFirstExit
                                         select x.Clone();

            //check if there is a bus exit that collision with the new times:
            foreach (var r in listOfOtherTimesOfLine)
            {
                //must be collision.. both drive in 00:00:00..
                if (lineTriptoUpdate.TimeFirstLineExit > lineTriptoUpdate.TimeLastLineExit && r.TimeFirstLineExit > r.TimeLastLineExit)
                    throw new AlreadyExistException("The times of the line that has chose already have defination");

                //both drive in time 00:00:00- 23:59:59
                if (lineTriptoUpdate.TimeFirstLineExit < lineTriptoUpdate.TimeLastLineExit && r.TimeFirstLineExit < r.TimeLastLineExit)
                {
                    if (lineTriptoUpdate.TimeFirstLineExit >= r.TimeFirstLineExit && lineTriptoUpdate.TimeFirstLineExit < r.TimeLastLineExit)
                        throw new AlreadyExistException("The times of the line that has chose already have defination");
                    if (lineTriptoUpdate.TimeLastLineExit > r.TimeFirstLineExit && lineTriptoUpdate.TimeLastLineExit < r.TimeLastLineExit)
                        throw new AlreadyExistException("The times of the line that has chose already have defination");
                    if (lineTriptoUpdate.TimeFirstLineExit < r.TimeFirstLineExit && lineTriptoUpdate.TimeLastLineExit > r.TimeLastLineExit)
                        throw new AlreadyExistException("The times of the line that has chose already have defination");
                }
                //r drive from "day" to other "day" and lineTimes in one day..
                else if (r.TimeFirstLineExit > r.TimeLastLineExit && lineTriptoUpdate.TimeFirstLineExit < lineTriptoUpdate.TimeLastLineExit)
                {
                    if (r.TimeLastLineExit > lineTriptoUpdate.TimeFirstLineExit)
                        throw new AlreadyExistException("The times of the line that has chose already have defination");
                    if (r.TimeFirstLineExit < lineTriptoUpdate.TimeLastLineExit)
                        throw new AlreadyExistException("The times of the line that has chose already have defination");

                    //if (r.TimeFirstLineExit < lineTriptoUpdate.TimeFirstLineExit || r.TimeLastLineExit > lineTriptoUpdate.TimeLastLineExit)
                    //    throw new AlreadyExistException("The times of the line that has chose already have defination");
                }
                // lineTimes drive from "day" to other "day" and r in one day..
                else if (r.TimeFirstLineExit < r.TimeLastLineExit && lineTriptoUpdate.TimeFirstLineExit > lineTriptoUpdate.TimeLastLineExit)
                {
                    if (r.TimeLastLineExit < lineTriptoUpdate.TimeFirstLineExit)
                        throw new AlreadyExistException("The times of the line that has chose already have defination");
                    if (r.TimeFirstLineExit > lineTriptoUpdate.TimeLastLineExit)
                        throw new AlreadyExistException("The times of the line that has chose already have defination");
                }
            }

            mylineTimes.TimeFirstLineExit = new TimeSpan(lineTriptoUpdate.TimeFirstLineExit.Ticks);
            mylineTimes.TimeLastLineExit = new TimeSpan(lineTriptoUpdate.TimeLastLineExit.Ticks);
            mylineTimes.Frequency = new TimeSpan(lineTriptoUpdate.Frequency.Ticks);
        }

        #endregion

        #region DeleteLineTrip
        public void DeleteLineTrip(int lineIdentificator, TimeSpan firstLineExit)
        {
            LineTrip myLineTimes = DataSource.linesTimes.Find(x => x.BusLineIndentificator == lineIdentificator &&
            firstLineExit == x.TimeFirstLineExit);
            if (myLineTimes == null)
                throw new DoesntExistException("the data about line doesn't exist in system");
            DataSource.linesTimes.Remove(myLineTimes);
        }

        #endregion

        #region GetALineTrip
        public LineTrip GetALineTimes(int lineIdentificator, TimeSpan firstLineExit)
        {
            LineTrip myLineTimes = DataSource.linesTimes.Find(x => x.BusLineIndentificator == lineIdentificator
            && x.TimeFirstLineExit == firstLineExit);
            if (myLineTimes != null)
                return myLineTimes.Clone();
            throw new DoesntExistException("the data about line doesn't exists in system");
        }
        #endregion

        #region GetAllLineTrip 
        public IEnumerable<LineTrip> GetAllLinesTimes()
        {
            var list = from lineTimes in DataSource.linesTimes
                       select lineTimes.Clone();
            return list;
        }

        #endregion

        #region GetPartOfLineTrip
        public IEnumerable<LineTrip> GetPartOfLinesTimes(Predicate<LineTrip> lineTimesCondition)
        {
            var list = from lineTimes in DataSource.linesTimes
                       where (lineTimesCondition(lineTimes))
                       select lineTimes.Clone();
            return list;
        }
        #endregion


        public int GetTheLastBusIdentificator()
        {
            return Configuration.BusInLineIndentificator;
        }











    }
}
