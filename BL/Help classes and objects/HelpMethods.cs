using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DO;
using DalApi;

namespace BL
{
    static class HelpMethods
    {
        //static IDal dal = DalFactory.GetDal();


        #region sorted the stations in a bus path
        static public IEnumerable<PointInPathLine> helpSortedPath(int busLineIdentificator, IDal dal)
        {
            var pointsInPath = dal.GetPartOfPointInPathLine(x => x.PointInPathLineNumber == busLineIdentificator);
            var sortedPathOfLine = from item in pointsInPath
                                   orderby item.NumberInPath
                                   select item;
            return sortedPathOfLine;
        }
        #endregion

        #region return a list of  stations for a line
        static public IEnumerable<StationInPath> pathOfAline(int busLineIdentificator,IDal dal)
        {
            var pointsInPathDO = helpSortedPath(busLineIdentificator,dal);
            var linePathBO = from item in pointsInPathDO
                             select new StationInPath()
                             {
                                 StationCode = item.StationCode,
                                 StationNumberInPath = item.NumberInPath,
                                 StationName = dal.GetAStation(item.StationCode).StationName
                             };
            var list = linePathBO.ToList();
            if (linePathBO != null && linePathBO.Any()) //check if the list of stations us empty
            {
                var firstStation = linePathBO.Cast<StationInPath>().First();
                int count = 0;
                foreach (var station in linePathBO)
                    count++;
                int index = -1;
                try
                {
                    foreach (var item in list)
                    {
                        if (index == -1)
                        {
                            item.DistanceFromPreStation = 0;
                            item.TimeFromPreStaion = new TimeSpan(0, 0, 0);
                        }
                        else
                        {
                            item.DistanceFromPreStation = dal.GetTwoStations
                                (linePathBO.ElementAt(index).StationCode, item.StationCode).DistanceBetweenStations;
                            item.TimeFromPreStaion = dal.GetTwoStations
                                (linePathBO.ElementAt(index).StationCode, item.StationCode).AverageTimeBetweenStations;
                        }
                        index++;
                    }
                }
                catch (InfoTwoStationsMissException ex)
                {
                    throw new MissDataOfTwoStationsExceptions(ex.FirstStation, ex.SecondStation, "Problem in the details of two defrent stations", ex);
                }
            }
            return list;
        }
        #endregion

        #region return all the buses that stop in specific station
        static public IEnumerable<BusLineInStation> busesStopInAStation(int stationCode, IDal dal)
        {
            var busesInTheStation = dal.GetPartOfPointInPathLine(x => x.StationCode == stationCode);
            var buses = from bus in busesInTheStation
                        select new BusLineInStation()
                        {
                            BusLineIndentificator = bus.PointInPathLineNumber,
                            LineNumber = dal.GetABusLine(bus.PointInPathLineNumber).LineNumber
                        };
            return buses;
        }
        /// <summary>
        /// return an object of ScheduleAtAstation- include the exit times of a specific line
        /// and the 
        /// </summary>
        /// <param name="busLineID">the line identificator </param>
        /// <param name="stationCode">the station</param>
        /// <param name=""></param>
        /// <returns></returns>
        public static ScheduleAtAstation getScheduleForALine(int busLineID, int stationCode, IDal dal)
        {
            ScheduleAtAstation scheduleLine = new ScheduleAtAstation();
            //return the specific station in the path of the specific line:
            DO.PointInPathLine point = dal.GetPartOfPointInPathLine
                (x => (x.PointInPathLineNumber == busLineID && x.StationCode == stationCode)).FirstOrDefault();
            if (point == null)
                throw new GetDetailsProblemException("the station doesn't exist in path");

            List<TimeSpan> listExitTime = new List<TimeSpan>();
            //list of all the lineTrip objects of the specific line:
            var lineExits = dal.GetPartOfLinesTimes(x => x.BusLineIndentificator == busLineID).OrderBy(x => x.TimeFirstLineExit);

            //create Detailed schedule of the line exits:
            foreach (var exit in lineExits)
            {
                if (exit.TimeFirstLineExit < exit.TimeLastLineExit)
                {
                    //add all the exit times to the list of the line: in case that the the exits are in the same day
                    for (var time = exit.TimeFirstLineExit; time <= exit.TimeLastLineExit; time += exit.Frequency)
                    {
                        listExitTime.Add(new TimeSpan(time.Ticks));
                    }
                }
                else//in case that the exits are in different days:
                {
                    TimeSpan time;
                    for (time = exit.TimeFirstLineExit; time < new TimeSpan(24, 0, 0); time += exit.Frequency)
                    {
                        listExitTime.Add(new TimeSpan(time.Ticks));
                    }

                    for (time = time - new TimeSpan(24, 0, 0); time < exit.TimeLastLineExit; time += exit.Frequency)
                    {
                        listExitTime.Add(new TimeSpan(time.Ticks));
                    }

                }
            }
            //sort the list of the times and insert to the new object: 
            scheduleLine.DepartureLineTimes = from exitTime in listExitTime
                                              select exitTime;

            var listOfLineStations = HelpMethods.pathOfAline(busLineID, dal);
            TimeSpan timeFromFirstStation = new TimeSpan(0, 0, 0);
            //calculate the time from first station to the specific station:
            
            for (int i = 1; i < point.NumberInPath-1; i++)
            {
                timeFromFirstStation += listOfLineStations.ElementAt(i).TimeFromPreStaion;
            }
            scheduleLine.timeFromFirstStation = timeFromFirstStation;
            scheduleLine.StationToCheckTimesCode = stationCode;
            scheduleLine.BusLineIdentificator = busLineID;
            return scheduleLine;
        }






        #endregion
    }
}
