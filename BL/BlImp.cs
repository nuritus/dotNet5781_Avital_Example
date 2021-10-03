using System;
using System.Collections.Generic;

using System.Text;
using System.Linq;
using BlApi;
using DalApi;
using DO;
using BO;

namespace BL
{
    public class BlImp : IBl
    {
        #region singelton

        static readonly BlImp instance = new BlImp();
        static BlImp() { }
        BlImp() { }
        public static BlImp Instance => instance;
        #endregion

        readonly IDal dal = DalFactory.GetDal();

        //--------------------------------------BUS------------------------------------------------------
        #region Add a bus
        public void AddBus(BO.Bus busToAdd)
        {
            if (busToAdd.LicensingDate > DateTime.Now)
                throw new AddingProblemException("This date is a future date");
            if (busToAdd.LicenseNumber.Length != 8 && busToAdd.LicensingDate.Year >= 2018
                || busToAdd.LicenseNumber.Length != 7 && busToAdd.LicensingDate.Year < 2018)
                throw new AddingProblemException("The number of digits in the license number is not valid");
            int tryParse;
            if (!int.TryParse(busToAdd.LicenseNumber, out tryParse))
                throw new AddingProblemException("The License number must be only with digits");
            if (tryParse < 0)
                throw new AddingProblemException("The License number must be a positive number");
            if (busToAdd.FuelTank < 0)
                throw new AddingProblemException("The amount of kilometers left until there is no fuel must be a positive number ");
            if (busToAdd.FuelTank > 1200)
                throw new AddingProblemException("The amount of kilometers left until there is no fuel can't be over 1200 km");
            if (busToAdd.Mileage < 0)
                throw new AddingProblemException("The mileage must be a positive number");
            try
            {
                dal.AddBus((DO.Bus)busToAdd.CopyPropertiesToNew(typeof(DO.Bus)));
            }
            catch (Exception ex)
            {
                throw new AddingProblemException("Can't add this bus", ex);
            }
        }
        #endregion

        #region Delete bus

        public void DeleteBus(string licenseNumber)
        {
            try
            {
                dal.DeleteBus(licenseNumber);
            }
            catch (DoesntExistException ex)
            {
                throw new DeletedProblemException("Can't deleted this bus", ex);
            }
        }
        #endregion

        #region Get all the buses
        public IEnumerable<BO.Bus> GetAllBuses()
        {
            //create list of all the buses (with BO bus)
            var listOfBuses = from bus in dal.GetAllBuses()
                              select new BO.Bus()
                              {
                                  SelfPayment = bus.SelfPayment,
                                  Status = (BO.BusStatus)bus.Status,
                                  FuelTank = bus.FuelTank,
                                  InCity = bus.InCity,
                                  LicenseNumber = bus.LicenseNumber,
                                  LicensingDate = new DateTime(bus.LicensingDate.Year, bus.LicensingDate.Month, bus.LicensingDate.Day),
                                  Mileage = bus.Mileage
                              };
            return listOfBuses;
        }
        #endregion

        #region Get a bus details
        public BO.Bus GetBusDetails(string licenseNumber)
        {
            try
            {
                //try to bring the data about the bus from the DS
                DO.Bus bus = dal.GetABus(licenseNumber);
                return new BO.Bus()
                {
                    SelfPayment = bus.SelfPayment,
                    Status = (BO.BusStatus)bus.Status,
                    FuelTank = bus.FuelTank,
                    InCity = bus.InCity,
                    LicenseNumber = bus.LicenseNumber,
                    LicensingDate = new DateTime(bus.LicensingDate.Year, bus.LicensingDate.Month, bus.LicensingDate.Day),
                    Mileage = bus.Mileage
                };
            }
            catch (DoesntExistException ex)
            {
                throw new GetDetailsProblemException("Can't get this bus", ex);
            }
        }
        #endregion

        #region Refueling bus
        public void RefuelingBus(string licenseBusToRefuel)
        {
            try
            {
                //try to update the fuel in the bus to be 1200
                DO.Bus myBus = dal.GetABus(licenseBusToRefuel);
                myBus.FuelTank = 1200;
                myBus.Status = DO.BusStatus.InFueling;
                dal.UpdateBus(myBus);
            }
            catch (DoesntExistException ex)
            {
                throw new GetDetailsProblemException("Can't get this bus", ex);
            }
        }

        #endregion

        #region UpdateSelfPaymentInBus
        void IBl.UpdateSelfPaymentInBus(string licenseNumber, bool flag)
        {
            try
            {
                DO.Bus myBus = dal.GetABus(licenseNumber);
                myBus.SelfPayment = flag;
                dal.UpdateBus(myBus);
            }
            catch (DoesntExistException ex)
            {
                throw new UpdateProblemException("Can't update self payment in bus", ex);
            }
        }
        #endregion

        //--------------------------------------BUS-LINE-------------------------------------------------
        #region Add bus line
        public void AddBusLine(BO.BusLine lineToAdd)
        {
            foreach (BO.StationInPath station in lineToAdd.StationsInPath)
                try //check that all the stations in the path of the line- really exist in the system
                    //and that the line number is valid
                {
                    if (lineToAdd.LineNumber < 1 || lineToAdd.LineNumber > 999)
                        throw new AddingProblemException("The number of the line is invalid");
                    if (station.StationName != dal.GetAStation(station.StationCode).StationName)
                        throw new AddingProblemException("The name of one of the stations is invalid");
                }
                catch (Exception ex)
                {
                    throw new AddingProblemException("Can't add one of the stations", ex);
                }
            int count = 0;
            foreach (BO.StationInPath station in lineToAdd.StationsInPath)// count the number of stations in the path
                count++;
            for (int i = 0; i < count - 1; i++)//check that there is no two following stations with the same code
            {
                if (lineToAdd.StationsInPath.ElementAt(i).StationCode == lineToAdd.StationsInPath.ElementAt(i + 1).StationCode)
                    throw new AddingProblemException("two following station with the same code");
            }


            for (int i = 0; i < count - 1; i++)//check that there are details about every two following stations
            {
                try
                {
                    TwoFollowingStations twoStations =
                          dal.GetTwoStations(lineToAdd.StationsInPath.ElementAt(i).StationCode,
                          lineToAdd.StationsInPath.ElementAt(i + 1).StationCode);//check exery station in the path with the next station

                    //lineToAdd.StationsInPath.ElementAt(i + 1).DistanceFromPreStation = twoStations.DistanceBetweenStations;
                    //lineToAdd.StationsInPath.ElementAt(i + 1).TimeFromPreStaion = new TimeSpan(twoStations.AverageTimeBetweenStations.Ticks);
                }
                catch (InfoTwoStationsMissException ex)//throw exception if there is missing data between following stations in the path
                {
                    throw new MissDataOfTwoStationsExceptions
                        (ex.FirstStation, ex.SecondStation,
                        "missing information between two stations", ex);
                }
            }
            if (count >= 2) //if the list of the station have two stations
            {
                dal.AddBusLine((DO.BusLine)lineToAdd.CopyPropertiesToNew(typeof(DO.BusLine)));//add the bus to the system
            }
            else //if the list of the stations have less than 2 stations
                throw new AddingProblemException("line must have at least two stations");

            foreach (BO.StationInPath station in lineToAdd.StationsInPath)
            //add all the stations to the system as "point in path" 
            {
                dal.AddPointInPathLine(new PointInPathLine
                {
                    StationCode = station.StationCode,
                    PointInPathLineNumber = dal.GetTheLastBusIdentificator(),
                    NumberInPath = station.StationNumberInPath
                });
            }

        }
        #endregion

        #region Delete bus line
        public void DeleteBusLine(int lineToDeleteIndentificator)
        {

            try
            {
                dal.DeleteBusLine(lineToDeleteIndentificator);
            }
            catch (DoesntExistException ex)
            {
                throw new DeletedProblemException("Can't deleted this bus", ex);
            }
            //get all the stations of the line:
            var listToDelete = dal.GetPartOfPointInPathLine(x => x.PointInPathLineNumber == lineToDeleteIndentificator);
            int count = 0;
            foreach (PointInPathLine point in listToDelete)//count the bumber of stations in the bus path
                count++;
            //delete all the stations of the specific line from the ds:
            for (int i = count - 1; i >= 0; i--)
                dal.DeletePointInPath(listToDelete.ElementAt(i).PointInPathLineNumber, listToDelete.ElementAt(i).NumberInPath);

        }
        #endregion

        #region Get all the lines
        public IEnumerable<BO.BusLine> GetAllBusLines()
        {
            //create a list of all the lines.. sort the lines by the  line number
            var listOfLines = from bus in dal.GetAllBusLines()
                              orderby bus.LineNumber
                              select new BO.BusLine()
                              {
                                  //(get all the stations in the path of the line..
                                  StationsInPath = HelpMethods.pathOfAline(bus.BusLineIndentificator, dal),
                                  FirstLineStation = bus.FirstLineStation,
                                  LastLineStation = bus.LastLineStation,
                                  BusLineIndentificator = bus.BusLineIndentificator,
                                  LineArea = (BO.Area)bus.LineArea,
                                  LineNumber = bus.LineNumber
                              };
            return listOfLines;
        }
        #endregion

        #region Get line details
        public BO.BusLine GetBusLineDetails(int busLineIndentificator)
        {
            try
            {
                //try to bring the data about the line from the DS
                DO.BusLine line = dal.GetABusLine(busLineIndentificator);
                return new BO.BusLine()
                {
                    StationsInPath = HelpMethods.pathOfAline(busLineIndentificator, dal),
                    FirstLineStation = line.FirstLineStation,
                    LastLineStation = line.LastLineStation,
                    BusLineIndentificator = line.BusLineIndentificator,
                    LineArea = (BO.Area)line.LineArea,
                    LineNumber = line.LineNumber
                };
            }
            catch (DoesntExistException ex)
            {
                throw new GetDetailsProblemException("Can't get this line", ex);
            }
        }
        #endregion

        #region UpdateNumberLine

        void IBl.UpdateNumberLine(int busIdentificator, int NewNumber)
        {
            if (NewNumber <= 0 || NewNumber > 999)
                throw new UpdateProblemException("מספר הקו לא תקין");
            try
            {
                DO.BusLine myLine = dal.GetABusLine(busIdentificator);
                myLine.LineNumber = NewNumber;
                dal.UpdateBusLine(myLine);
            }
            catch (DoesntExistException ex)
            {
                throw new UpdateProblemException("Can't update line number", ex);
            }
        }
        #endregion

        #region GetLinesByArea
        IEnumerable<IGrouping<BO.Area, BO.BusLine>> IBl.GetLinesByArea()
        {
            var listByArea = from line in GetAllBusLines()
                             group line by line.LineArea into g
                             select g;
            return listByArea;
        }
        #endregion

        //-------------------------------------STATION---------------------------------------------------
        #region Add a station
        public void AddStation(BO.Station stationToAdd)
        {
            if (stationToAdd.StationCode < 0)//check that the station code to add is not negitive
                throw new AddingProblemException("קוד תחנה לא יכול להיות מספר שלילי");
            if (stationToAdd.LocationLatitude < 31 || stationToAdd.LocationLatitude > 33.3
                || stationToAdd.LocationLongitude < 34.3 || stationToAdd.LocationLongitude > 35.5)
                throw new AddingProblemException("המיקום שנבחר לא נמצא בגבולות הארץ");
            try//try to add the station to the system
            {
                dal.AddStation((DO.Station)stationToAdd.CopyPropertiesToNew(typeof(DO.Station)));
            }
            catch (AlreadyExistException ex)
            {
                throw new AddingProblemException("קוד תחנה זהה כבר קיים במערכת", ex);
            }
        }

        #endregion

        #region Delete a station
        public void DeleteStation(int StationCode)
        {
            //before delete a station check that there is no lines that pass in the station:
            //in case there is lines that pass in station it will throw exception

            var lineInStation = dal.GetPartOfPointInPathLine(x => (x.StationCode == StationCode)).FirstOrDefault();
            if (lineInStation != null)
                throw new DeletedProblemException("There is/are line(s) that stop in this station");
            try
            {
                var listOfTwoStations = dal.GetPartOfTwoFollowingStations(x => x.FirstStationCode == StationCode || x.SecondStationCode == StationCode);
                //delete all the two following stations with those station:
                for (int i = 0; i < listOfTwoStations.Count(); i++)
                {
                    dal.DeleteTwoFollowingStations(listOfTwoStations.ElementAt(i).FirstStationCode,
                        listOfTwoStations.ElementAt(i).SecondStationCode);
                }

                dal.DeleteStation(StationCode);
            }
            catch (DoesntExistException ex)
            {
                throw new DeletedProblemException("Can't deleted this bus", ex);
            }

        }
        #endregion

        #region Get all the stations
        public IEnumerable<BO.Station> GetAllStations()
        {
            //create a list of all the stations.. sort the stations by the station code
            var listOfStations = from station in dal.GetAllStations()
                                 orderby station.StationName
                                 select new BO.Station()
                                 {
                                     StationAddress = station.StationAddress,
                                     StationCode = station.StationCode,
                                     LocationLatitude = station.LocationLatitude,
                                     LocationLongitude = station.LocationLongitude,
                                     StationName = station.StationName,
                                     PlaceToSit = station.PlaceToSit,
                                     BoardBusTiming = station.BoardBusTiming,
                                     //get the list of all the lines that stop in the station
                                     LinesInStation = HelpMethods.busesStopInAStation(station.StationCode, dal)
                                 };
            return listOfStations;
        }
        #endregion

        #region Get station details
        public BO.Station GetStationDetails(int stationCode)
        {
            try
            {
                //try to bring the data about the station from the DS
                DO.Station stationDO = dal.GetAStation(stationCode);
                return new BO.Station()
                {
                    StationAddress = stationDO.StationAddress,
                    StationCode = stationDO.StationCode,
                    LocationLatitude = stationDO.LocationLatitude,
                    LocationLongitude = stationDO.LocationLongitude,
                    StationName = stationDO.StationName,
                    PlaceToSit = stationDO.PlaceToSit,
                    BoardBusTiming = stationDO.BoardBusTiming,
                    LinesInStation = HelpMethods.busesStopInAStation(stationCode, dal)
                };
            }
            catch (DoesntExistException ex)
            {
                throw new GetDetailsProblemException("לא קיימת תחנה עם קוד זה", ex);
            }
        }
        #endregion

        #region UpdateStationName
        void IBl.UpdateStationName(int stationCode, string newName)
        {
            try
            {
                DO.Station myStation = dal.GetAStation(stationCode);
                myStation.StationName = newName;
                dal.UpdateStation(myStation);
            }
            catch (DoesntExistException ex)
            {
                throw new UpdateProblemException("Can't update the name in station", ex);
            }
        }
        #endregion

        #region UpdatePlaceToSit
        void IBl.UpdatePlaceToSit(int stationCode, bool flag)
        {
            try
            {
                DO.Station myStation = dal.GetAStation(stationCode);
                myStation.PlaceToSit = flag;
                dal.UpdateStation(myStation);
            }
            catch (DoesntExistException ex)
            {
                throw new UpdateProblemException("Can't update place to sit in station", ex);
            }
        }
        #endregion

        #region UpdateBoardBusTiming

        void IBl.UpdateBoardBusTiming(int stationCode, bool flag)
        {
            try
            {
                DO.Station myStation = dal.GetAStation(stationCode);
                myStation.BoardBusTiming = flag;
                dal.UpdateStation(myStation);
            }
            catch (DoesntExistException ex)
            {
                throw new UpdateProblemException("Can't update bourd timing in station", ex);
            }
        }
        #endregion

        //-------------------------------------POINT-IN-PATH---------------------------------------------
        #region Add a station in a line path
        public void AddStationInPath(int busLineIdentidicator, int placeToAddInPath, int stationCodeToAdd)
        {
            try
            {
                if (placeToAddInPath != 1)
                    //check that there are a station before the station we add..
                    dal.GetAPoint(busLineIdentidicator, placeToAddInPath - 1);
            }
            catch (DoesntExistException ex)
            {
                throw new AddingProblemException("מיקום תחנה במסלול לא תקין", ex);
            }
            try
            {//check that the station and the line are exist in the system 
                dal.GetABusLine(busLineIdentidicator);
                dal.GetAStation(stationCodeToAdd);
            }
            catch (GetDetailsProblemException ex)
            {
                throw new AddingProblemException(ex.Message, ex);
            }
            //get the list of all the stations in the path- sorted:
            var listOfAllStationInLine = HelpMethods.helpSortedPath(busLineIdentidicator, dal);

            //get list of all the stations that appear after the station we add.. we have to update the location in the path
            //of all the stations in the list:
            var listToUpdate = from point in listOfAllStationInLine
                               where (point.NumberInPath >= placeToAddInPath)
                               select point;
            int count = 0;
            foreach (PointInPathLine item in listToUpdate)//count how many stations there are after the station we want to remove+1
                count++;

            if (count == 0)//if we want to add station in the end of the path
            {
                #region check that there are details butween the stations in the path
                try
                {//check that there is details between the new station to the station before it
                    if (placeToAddInPath != 1)
                    {
                        if (stationCodeToAdd == dal.GetAPoint(busLineIdentidicator, placeToAddInPath - 1).StationCode)
                            throw new AddingProblemException("Can't add The same station");
                        dal.GetTwoStations(dal.GetAPoint(busLineIdentidicator,
                            placeToAddInPath - 1).StationCode, stationCodeToAdd);
                    }
                }
                catch (InfoTwoStationsMissException ex)
                {
                    throw new MissDataOfTwoStationsExceptions
                        (ex.FirstStation, ex.SecondStation,
                        "missing information between two stations", ex);
                }
                #endregion
                //add the station to the line path:
                dal.AddPointInPathLine(new PointInPathLine()
                {
                    PointInPathLineNumber = busLineIdentidicator,
                    StationCode = stationCodeToAdd,
                    NumberInPath = placeToAddInPath
                });
                //update the last station of the line to be the station we just add:
                DO.BusLine updateLine = dal.GetABusLine(busLineIdentidicator);
                dal.UpdateBusLine(new DO.BusLine
                {
                    BusLineIndentificator = busLineIdentidicator,
                    FirstLineStation = updateLine.FirstLineStation,
                    LineNumber = updateLine.LineNumber,
                    LineArea = updateLine.LineArea,
                    LastLineStation = stationCodeToAdd
                });

            }

            else//if the station to add is not station in the end of the path
            {
                #region check if the details of the distance of the path are update
                try
                {
                    if (placeToAddInPath != 1)
                    {
                        if (stationCodeToAdd == dal.GetAPoint(busLineIdentidicator, placeToAddInPath - 1).StationCode)
                            throw new AddingProblemException("Can't add The same station");
                        //check there is data between the new station and the station before
                        dal.GetTwoStations(dal.GetAPoint(busLineIdentidicator,
                                placeToAddInPath - 1).StationCode, stationCodeToAdd);
                    }
                    if (stationCodeToAdd == dal.GetAPoint(busLineIdentidicator, placeToAddInPath).StationCode)
                        throw new AddingProblemException("Can't add The same station");

                    //check there is data between the new station and the station after it
                    dal.GetTwoStations(stationCodeToAdd, dal.GetAPoint(busLineIdentidicator,
                            placeToAddInPath).StationCode);
                }
                catch (InfoTwoStationsMissException ex)
                {
                    throw new MissDataOfTwoStationsExceptions
                        (ex.FirstStation, ex.SecondStation,
                        "missing information between two stations", ex);
                }
                #endregion
                //add new point in path- in the end of the path- and pass to this point the details of 
                //the real last station in the path..
                dal.AddPointInPathLine(new PointInPathLine()
                {
                    //the number in path- the end of the path
                    NumberInPath = listToUpdate.ElementAt(count - 1).NumberInPath + 1,
                    //the code of the real last station 
                    StationCode = listToUpdate.ElementAt(count - 1).StationCode,
                    //insert the ine identificator
                    PointInPathLineNumber = listToUpdate.ElementAt(count - 1).PointInPathLineNumber
                });
                //update all the stations in the list of stations to update to be the station in the "next" location in path 
                for (int i = count - 2; i >= 0; i--)
                {
                    PointInPathLine point = listToUpdate.ElementAt(i);
                    //update the station to be in the next location in path:
                    dal.UpdatePointInPathLine(new PointInPathLine()
                    {
                        StationCode = point.StationCode,
                        NumberInPath = point.NumberInPath + 1,
                        PointInPathLineNumber = busLineIdentidicator
                    });
                }
                //"insert" the new station to the path:
                dal.UpdatePointInPathLine(new PointInPathLine()
                {
                    NumberInPath = placeToAddInPath,
                    PointInPathLineNumber = busLineIdentidicator,
                    StationCode = stationCodeToAdd
                });
                if (placeToAddInPath == 1)//if we add the first station
                {
                    //we update the first station of the bus line in the ds:
                    DO.BusLine lineToUpdate = dal.GetABusLine(busLineIdentidicator);
                    dal.UpdateBusLine(new DO.BusLine
                    {
                        BusLineIndentificator = busLineIdentidicator,
                        FirstLineStation = stationCodeToAdd,
                        LastLineStation = lineToUpdate.LastLineStation,
                        LineArea = lineToUpdate.LineArea,
                        LineNumber = lineToUpdate.LineNumber
                    });
                }

            }
        }
        #endregion

        #region Delete Station in path
        public void DeleteStationInPath(int busLineIdentidicator, int numberInPath)
        {
            try//check that the point is really exist in system
            {
                dal.GetAPoint(busLineIdentidicator, numberInPath);
            }
            catch (DoesntExistException ex)
            {
                throw new DeletedProblemException("Can't deleted this station", ex);
            }
            //get the list of all the stations in the line path
            var listOfAllStationInLine = HelpMethods.helpSortedPath(busLineIdentidicator, dal);
            //create a list of all the stations that will have to be update after the deleted include the station we delete 
            //(all the stations in the path after the station we want to delete)
            if (listOfAllStationInLine.Count() <= 2)
                throw new DeletedProblemException("line can't have less than two stations");
            var listToUpdate = from point in listOfAllStationInLine
                               where (point.NumberInPath >= numberInPath)
                               select point;
            int count = 0;
            foreach (PointInPathLine item in listToUpdate)//count how many stations there are after the station we want to remove+1
                count++;
            #region check if there is data about all the two following stations after deleted
            try
            {
                if (numberInPath != 1 && count != 1)//if the station is not the first station and not the last station
                                                    //check that there is data between the station before the one we delete and the station 
                                                    //after th one we delete:
                    dal.GetTwoStations(dal.GetAPoint(busLineIdentidicator, numberInPath - 1).StationCode,
                        dal.GetAPoint(busLineIdentidicator, numberInPath + 1).StationCode);
            }
            catch (InfoTwoStationsMissException ex)
            {
                throw new MissDataOfTwoStationsExceptions
                    (ex.FirstStation, ex.SecondStation, "miss information about two stations", ex);
            }
            #endregion
            int index = 1;//index to the station we want to promote it location in 1..
            foreach (PointInPathLine item in listToUpdate)
            {
                if (index != count)
                {
                    PointInPathLine pointUp = listToUpdate.ElementAt(index);//get the station we want to update it's location
                    pointUp.NumberInPath = item.NumberInPath;//update the location to 1 before..(for example- 2 to 1)
                    dal.UpdatePointInPathLine(pointUp);//update in the DS
                    index++;//promote the index to the next station
                }
            }
            //delete the last station in the path from the DS...
            dal.DeletePointInPath(busLineIdentidicator, listToUpdate.ElementAt(index - 1).NumberInPath);
            if (numberInPath == 1)//if we deleted the first station in the path we have to update this in the bus line..
            {
                DO.BusLine updateLine = dal.GetABusLine(busLineIdentidicator);
                dal.UpdateBusLine(new DO.BusLine
                {
                    BusLineIndentificator = updateLine.BusLineIndentificator,
                    FirstLineStation = listToUpdate.ElementAt(1).StationCode,
                    LastLineStation = updateLine.LastLineStation,
                    LineArea = updateLine.LineArea,
                    LineNumber = updateLine.LineNumber
                });
            }
            else if (count == 1)//if we deleted the last station in the path we have to update this in the bus line..
            {
                DO.BusLine updateLine = dal.GetABusLine(busLineIdentidicator);
                dal.UpdateBusLine(new DO.BusLine
                {
                    BusLineIndentificator = updateLine.BusLineIndentificator,
                    FirstLineStation = updateLine.FirstLineStation,
                    LastLineStation = listOfAllStationInLine.Where(x => x.NumberInPath + 1 == numberInPath).FirstOrDefault().StationCode,
                    LineArea = updateLine.LineArea,
                    LineNumber = updateLine.LineNumber
                });
            }


        }
        //---------------------------------------------------------------------------------

        #endregion

        #region Update station in path
        public void UpdateStationInPath(int busLineIdentidicator, StationInPath stationToUpdate)
        {
            try
            {
                #region check if there is details between all the stations in the path
                //check if there is a line and if there is a point in the path to update:
                dal.GetABusLine(busLineIdentidicator);
                dal.GetAPoint(stationToUpdate.StationNumberInPath, busLineIdentidicator);
                //get all the stations in the path:
                IEnumerable<StationInPath> stationsList = HelpMethods.pathOfAline(busLineIdentidicator, dal);
                //if the station to update is not the first station- check there is details between the updating
                //station and the station before
                if (stationToUpdate.StationNumberInPath != 1)
                    dal.GetTwoStations(stationsList.ElementAt(stationToUpdate.StationNumberInPath - 2).StationCode
                        , stationToUpdate.StationCode);

                int count = 0;
                foreach (StationInPath station in stationsList)//count the number of stations in the path
                    count++;
                //if the station to update is not the last station- check there is details between the updating
                //station and the station after
                if (stationToUpdate.StationNumberInPath != stationsList.ElementAt(count - 1).StationNumberInPath)
                    dal.GetTwoStations(stationToUpdate.StationCode,
                        stationsList.ElementAt(stationToUpdate.StationNumberInPath).StationCode);
                #endregion

                //update the details of the station..
                dal.UpdatePointInPathLine(new PointInPathLine()
                {
                    StationCode = stationToUpdate.StationCode,
                    NumberInPath = stationToUpdate.StationNumberInPath,
                    PointInPathLineNumber = busLineIdentidicator
                });
                //if the station is the first station:
                if (stationToUpdate.StationNumberInPath == 1)
                {
                    DO.BusLine line = dal.GetABusLine(busLineIdentidicator);
                    dal.UpdateBusLine(new DO.BusLine
                    {
                        BusLineIndentificator = line.BusLineIndentificator,
                        LineArea = line.LineArea,
                        LastLineStation = line.LastLineStation,
                        LineNumber = line.LineNumber,
                        FirstLineStation = stationToUpdate.StationCode
                    });
                }
                //if the station is the last station:
                else if (stationToUpdate.StationNumberInPath == stationsList.ElementAt(count - 1).StationNumberInPath)
                {
                    DO.BusLine line = dal.GetABusLine(busLineIdentidicator);
                    dal.UpdateBusLine(new DO.BusLine
                    {
                        BusLineIndentificator = line.BusLineIndentificator,
                        LineArea = line.LineArea,
                        LastLineStation = stationToUpdate.StationCode,
                        LineNumber = line.LineNumber,
                        FirstLineStation = line.FirstLineStation
                    });
                }
            }
            catch (DoesntExistException ex)
            {
                throw new UpdateProblemException(ex.Message, ex);
            }
            catch (InfoTwoStationsMissException ex)
            {
                throw new MissDataOfTwoStationsExceptions(ex.FirstStation, ex.SecondStation, "miss data between stations", ex);
            }
        }
        #endregion

        //-----------------------------------TWO-STATIONS-------------------------------------------------

        #region update two stations
        void IBl.UpdateTwoStations(DataAboutTwoStations twoStations)
        {
            try
            {
                if (twoStations.FirstStationCode == twoStations.SecondStationCode)
                    throw new AddingProblemException("can't update info between a station to itself");
                dal.GetAStation(twoStations.FirstStationCode);
                dal.GetAStation(twoStations.SecondStationCode);
                dal.UpdateTwoFollowingStations((DO.TwoFollowingStations)twoStations.CopyPropertiesToNew(typeof(DO.TwoFollowingStations)));
            }
            catch (InfoTwoStationsMissException)
            {
                dal.AddTwoFollowingStations((DO.TwoFollowingStations)twoStations.CopyPropertiesToNew(typeof(DO.TwoFollowingStations)));
            }
            catch (DoesntExistException ex)
            {
                throw new UpdateProblemException("Can't update the data between stations", ex);
            }
        }
        #endregion

        #region return the distance and time between two stations- 2 functions
        public double DistanceBetweenTwoStations(int firstStationCode, int secondStationCode)
        {
            try
            {
                return dal.GetTwoStations(firstStationCode, secondStationCode).DistanceBetweenStations;
            }
            catch (InfoTwoStationsMissException ex)
            {
                throw new MissDataOfTwoStationsExceptions(firstStationCode, secondStationCode, ex.Message, ex);
            }
        }
        public TimeSpan TimeBetweenTwoStations(int firstStationCode, int secondStationCode)
        {
            try
            {
                return dal.GetTwoStations(firstStationCode, secondStationCode).AverageTimeBetweenStations;
            }
            catch (InfoTwoStationsMissException ex)
            {
                throw new MissDataOfTwoStationsExceptions(firstStationCode, secondStationCode, ex.Message, ex);
            }
        }
        #endregion

        #region check if there is data between two stations
        public bool checkIfDataBetweenStationsExist(int firstStationCode, int secondStationCode)
        {
            try
            {
                //check that both two stations exist in the system:
                dal.GetAStation(firstStationCode);
                dal.GetAStation(secondStationCode);
                //check if there is data about the two stations
                dal.GetTwoStations(firstStationCode, secondStationCode);
                return true;
            }
            catch (DoesntExistException)
            {
                throw new GetDetailsProblemException("אחת התחנות לא קיימת במערכת");
            }
            catch (InfoTwoStationsMissException)
            {
                return false;
            }
        }
        #endregion

        //----------------------------------------USER----------------------------------------------------

        #region AddUser
        public void AddUser(BO.User newUser)
        {
            try
            {
                dal.AddUser((DO.User)newUser.CopyPropertiesToNew(typeof(DO.User)));
            }
            catch (AlreadyExistException ex)
            {
                throw new AddingProblemException("Can't add this user", ex);
            }
        }
        #endregion

        #region DeleteUser
        public void DeleteUser(string userName)
        {
            try
            {
                dal.DeleteUser(userName);
            }
            catch (DoesntExistException ex)
            {
                throw new AddingProblemException("Can't delete this user", ex);
            }
        }
        #endregion

        #region UpdateUserPassword
        public void UpdateUserPassword(string userName, string newPassword)
        {
            try
            {
                dal.UpdateUser(new DO.User()
                {
                    UserName = userName,
                    UserAccessManagement = dal.GetAUser(userName).UserAccessManagement,
                    UserPassword = newPassword
                });
            }
            catch (DoesntExistException ex)
            {
                throw new AddingProblemException("Can't update this user", ex);
            }
        }
        #endregion

        #region UpdateUserAccess
        public void UpdateUserAccess(string userName, bool access)
        {
            try
            {
                dal.UpdateUser(new DO.User()
                {
                    UserName = userName,
                    UserAccessManagement = access,
                    UserPassword = dal.GetAUser(userName).UserPassword
                });
            }
            catch (DoesntExistException ex)
            {
                throw new AddingProblemException("Can't update this user", ex);
            }
        }
        #endregion

        #region SearchUser
        public bool SearchUser(string myUserName, string myUserPassword)
        {
            try
            {
                DO.User user = dal.GetAUser(myUserName);
                if (myUserPassword == user.UserPassword)
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
        #endregion

        #region get user details
        public BO.User GetUserDetails(string name)
        {
            try
            {
                DO.User myUser = dal.GetAUser(name);
                return (BO.User)myUser.CopyPropertiesToNew(typeof(BO.User));
            }
            catch (DoesntExistException ex)
            {
                throw new GetDetailsProblemException(ex.Message, ex);
            }
        }
        #endregion

        #region get all users
        public IEnumerable<BO.User> GetAllUsers()
        {
            //create list of all the buses (with BO bus)
            var listOfUsers = from user in dal.GetAllUsers()
                              select (BO.User)user.CopyPropertiesToNew(typeof(BO.User));
            return listOfUsers;
        }
        #endregion


        //---------------------------------------LINE-TRIP----------------------------------------------------
        #region update the last hour on an exit
        void IBl.UpdateLastHourLineTrip(int lineID, TimeSpan firstLineExit, TimeSpan newlastLineExit)
        {
            if(newlastLineExit>=new TimeSpan(24,0,0))
            {
                throw new InvalidValueException("there are only 24 hours in day");
            }
            try
            {
                dal.UpdateLineTrip(new DO.LineTrip
                {
                    BusLineIndentificator = lineID,
                    Frequency=dal.GetALineTimes(lineID,firstLineExit).Frequency,
                    TimeFirstLineExit=firstLineExit,
                    TimeLastLineExit=newlastLineExit
                },firstLineExit);
            }
            catch(DoesntExistException ex)
            {
                throw new UpdateProblemException("The time doesn't exist in system",ex);
            }
            catch (AlreadyExistException ex)
            {
                throw new UpdateProblemException("there are collisions with other times", ex);
            }
        }
        #endregion

        #region update the frequenct of an exit
        void IBl.UpdateFrequencyLineTrip(int lineID, TimeSpan firstLineExit, TimeSpan newfrequencyLineExit)
        {
            try
            {
                dal.UpdateLineTrip(new DO.LineTrip
                {
                    BusLineIndentificator = lineID,
                    Frequency = newfrequencyLineExit,
                    TimeFirstLineExit = firstLineExit,
                    TimeLastLineExit = dal.GetALineTimes(lineID, firstLineExit).TimeLastLineExit
                }, firstLineExit);
            }
            catch (DoesntExistException ex)
            {
                throw new UpdateProblemException("The time doesn't exist in system", ex);
            }

        }
        #endregion

        #region add line trip
        public void AddLineTrip(BO.LineTrip lineTripToAdd)
        {
            if (lineTripToAdd.TimeFirstLineExit >= new TimeSpan(24, 0, 0) || lineTripToAdd.TimeLastLineExit >= new TimeSpan(24, 0, 0))
                throw new AddingProblemException("the time is invalid");
            try
            {
                dal.AddLineTrip((DO.LineTrip)lineTripToAdd.CopyPropertiesToNew(typeof(DO.LineTrip)));
            }
            catch (Exception ex)
            {
                throw new AddingProblemException("can't add this data", ex);
            }
        }
        #endregion
     
        #region delete line trip
        public void DeleteLineTrip(int lineID, TimeSpan firstLineExit)
        {
            try
            {
                dal.DeleteLineTrip(lineID, firstLineExit);
            }
            catch (Exception ex)
            {
                throw new DeletedProblemException("can't delete this data", ex);
            }
        }
        #endregion

        #region get all the line trips for a specific line
        IEnumerable<BO.LineTrip> IBl.GetAllLineTripTimesOfALine(int busLineId)
        {
            return from time in dal.GetPartOfLinesTimes(x => x.BusLineIndentificator == busLineId)
                   orderby time.TimeFirstLineExit
                   select (BO.LineTrip)time.CopyPropertiesToNew(typeof(BO.LineTrip));
        }

        #endregion

        //-------------------------------------GET-ALL-LINES-IN-A-STATION--------------------------------------

        #region get all closing lines to  a specific station according to the time
        public IEnumerable<LineTimingInStation> GetAllLinesTimesInStation(int codeStaion, TimeSpan currentTime)
        {
            //create list of all the lines ID that passed in the station:
            var linesInStation = from point in dal.GetPartOfPointInPathLine(x => x.StationCode == codeStaion)
                                 select point.PointInPathLineNumber;
            //the list of the lines:
            linesInStation = linesInStation.Distinct();

            //list of lines arrives when the exit time and the current time are in the same day..:
            var listOfSameDay = from line in linesInStation
                                let scheduleLine = HelpMethods.getScheduleForALine(line, codeStaion, dal) //the schedule of the line
                                from timeExit in scheduleLine.DepartureLineTimes //pass on all the exit times of the line..
                                                                                 //if there is a bus that already exit and doesn't arrive yet to the station:
                                where (timeExit <= currentTime && timeExit >= currentTime - scheduleLine.timeFromFirstStation)
                                select new LineTimingInStation
                                {
                                    BusLineIdentificator = line,
                                    TimingLeft = scheduleLine.timeFromFirstStation - (currentTime - timeExit),
                                    LineNumber = dal.GetABusLine(line).LineNumber,
                                    LastStationCode = dal.GetABusLine(line).LastLineStation,
                                    ExitTimeFromFirstStation=timeExit
                                };
            //list of lines arrives when the exit time and the current time are in different days..:
            var listOfTwoDiffrentDays = from line in linesInStation
                                        let scheduleLine = HelpMethods.getScheduleForALine(line, codeStaion, dal) //the schedule of the line
                                        from timeExit in scheduleLine.DepartureLineTimes //pass on all the exit times of the line..
                                                                                         //if there is a bus that already exit and doesn't arrive yet to the station:
                                        where (timeExit + scheduleLine.timeFromFirstStation >= new TimeSpan(24, 0, 0) &&
                                         new TimeSpan(24, 0, 0) - timeExit + currentTime <= scheduleLine.timeFromFirstStation)
                                        select new LineTimingInStation
                                        {
                                            BusLineIdentificator = line,
                                            TimingLeft = scheduleLine.timeFromFirstStation - (new TimeSpan(24, 0, 0) - timeExit + currentTime),
                                            LineNumber = dal.GetABusLine(line).LineNumber,
                                            LastStationCode = dal.GetABusLine(line).LastLineStation,
                                            ExitTimeFromFirstStation=timeExit
                                        };

            var listFirstStation = from line in linesInStation
                                   let scheduleLine = HelpMethods.getScheduleForALine(line, codeStaion, dal)
                                   where scheduleLine.timeFromFirstStation == new TimeSpan(0, 0, 0)
                                   let timeExit=scheduleLine.DepartureLineTimes.Where(x=> x>currentTime).FirstOrDefault()
                                   select new LineTimingInStation
                                   {
                                       BusLineIdentificator = line,
                                       TimingLeft = scheduleLine.timeFromFirstStation - (currentTime - timeExit),
                                       LineNumber = dal.GetABusLine(line).LineNumber,
                                       LastStationCode = dal.GetABusLine(line).LastLineStation,
                                       ExitTimeFromFirstStation = timeExit
                                   };

            var onlyPositiveTimes = from line in listFirstStation
                                    where line.TimingLeft > new TimeSpan(0, 0, 0)
                                    select line;


            var newList = listOfSameDay.Union(listOfTwoDiffrentDays);
            newList = newList.Union(onlyPositiveTimes);
            return newList.OrderBy(x=>x.TimingLeft);
        }
        #endregion

    }
}
