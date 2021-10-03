using System;
using System.Collections.Generic;
using System.Text;
using DO;

namespace DalApi
{
    public interface IDal 
    {
        // ------------------------------ All the functions Add -------------------------
        /// <summary>
        /// Function that add a bus to the system
        /// </summary>
        /// <param name="busToAdd"> is the bus that we add</param> 
        void AddBus(Bus busToAdd);
        /// <summary>
        /// Function that add a station to the system 
        /// </summary>
        /// <param name="stationToAdd"> the new station  </param>
        void AddStation(Station stationToAdd);
        /// <summary>
        /// Function that add a busLine to the system 
        /// </summary>
        /// <param name="busLineToAdd"> The bus line that we add </param>
        void AddBusLine(BusLine busLineToAdd);
        /// <summary>
        /// Function that add a point to the path
        /// </summary>
        /// <param name="pointToAdd"> The point in the path of the line that we added that we add
        void AddPointInPathLine(PointInPathLine pointToAdd);
        /// <summary>
        /// Function that adds two following stations  
        /// </summary>
        /// <param name="   twoStationsToAdd"> The two stations that we have to add
        void AddTwoFollowingStations(TwoFollowingStations twoStationsToAdd);
        /// <summary>
        /// Function that adds a user to the system
        /// </summary>
        /// <param name="userToAdd"> this is the user we have to add</param>
        void AddUser(User userToAdd);
        /// <summary>
        /// Function that adds a line-trip to the system
        /// </summary>
        /// <param name="lineTripToAdd">the line-trip we add</param>
        void AddLineTrip(LineTrip lineTripToAdd);

        //--------------------- All the functions of update -------------------
        /// <summary>
        /// Function that updates the details of the bus in the system
        /// </summary>
        /// <param name="busToUpdate"> the bus to update in the system</param>
        void UpdateBus (Bus busToUpdate);
        /// <summary>
        /// Function that updates the details of the station in the system
        /// </summary>
        /// <param name="stationToUpdate"> the station that we upadate</param>
        void UpdateStation(Station stationToUpdate);
        /// <summary>
        /// Function that updates the details of the bus line in the system
        /// </summary>
        /// <param name="busLineToUpdate"> the bus line that we update</param>
        void UpdateBusLine(BusLine busLineToUpdate);
        /// <summary>
        /// Function that updates the details of the point of a line in the system
        /// </summary>
        /// <param name="pointToUpdate"> the point that we update in the line</param>
        void UpdatePointInPathLine(PointInPathLine pointToUpdate);
        /// <summary>
        /// Function that updates the details two stations that are following one another in the system
        /// </summary>
        /// <param name="twoStationToUpdate"> the two stations that we have to update</param>
        void UpdateTwoFollowingStations(TwoFollowingStations twoStationToUpdate);
        /// <summary>
        /// Function that updates the password of the user in the system
        /// </summary>
        /// <param name="usertoUpdate"> the user that we have to update his password</param>
        void UpdateUser(User usertoUpdate);

        void UpdateLineTrip(LineTrip lineTriptoUpdate, TimeSpan OldFirstExit);

        //---------------------------- All the delete functions ------------------------------------
        /// <summary>
        /// Deletes a bus by his license number
        /// </summary>
        /// <param name="licenseNumberToDelete"> the license number of the bus we have to delete</param>
        void DeleteBus(string licenseNumberToDelete);
        /// <summary>
        /// Deletes the station by it's indentification code 
        /// </summary>
        /// <param name="stationCodeToDelete"> the identificator code of the station we delete </param>
        void DeleteStation(int stationCodeToDelete);
        /// <summary>
        /// Deletes a bus line by its idenficator code
        /// </summary>
        /// <param name="busIndentificatorToDelete">the indentificator code of the bus we want to delete</param>
        void DeleteBusLine(int busIndentificatorToDelete);
        /// <summary>
        /// Deletes a point in the path according the line identificator, and the number of this point in the path
        /// </summary>
        /// <param name="lineIdentificator"> the idetificator of the line</param>
        /// <param name="numberInPath"> the number in the path of the choosed point</param>
        void DeletePointInPath(int PointNumber, int numberInPath);
        /// <summary>
        /// Deletes two following stations according to the code of each one
        /// </summary>
        /// <param name="firstCodeStation"> code of the first of the two station</param>
        /// <param name="secondCodeStation"> code of the second of the two stations</param>
        void DeleteTwoFollowingStations(int firstCodeStation, int secondCodeStation);
        /// <summary>
        /// Deletes the user according to his name
        /// </summary>
        /// <param name="userName"> the name of the user that we have to delete</param>
        void DeleteUser(string userName);

        void DeleteLineTrip(int lineIdentificator, TimeSpan firstLineExit);
        //---------------------------------- Asking functions -------------------------------------
        // ---------- Searching an object ----------------
        /// <summary>
        /// Return a bus according to its license number 
        /// </summary>
        /// <param name="licenseNumber"> the license number of the bus we return</param>
        /// <returns></returns>
        Bus GetABus(string licenseNumber);
        /// <summary>
        /// Return a station according to the code of the station 
        /// </summary>
        /// <param name="stationCode"> the code of the station we want to return</param>
        /// <returns></returns>
        Station GetAStation(int stationCode);
        /// <summary>
        /// Return a bus line according to the identificator of the bus
        /// </summary>
        /// <param name="busIndetificator"> the indentificator </param>
        /// <returns></returns>
        BusLine GetABusLine(int busIndetificator);
        /// <summary>
        /// Return a point in the path of a bus line according to the identificator of the line it belongs to and the number of his rank in path
        /// </summary>
        /// <param name="lineIdentificator"> the identificator of the line of the point</param>
        /// <param name="numberInPathLine"> the rank of the point</param>
        /// <returns></returns>
        PointInPathLine GetAPoint(int numberInPathLine, int pointInPathNumber);
        /// <summary>
        /// Returns two following stations according to the code of the first station and of the second one
        /// </summary>
        /// <param name="firstCodeStation"> code of the 1st station</param>
        /// <param name="secondCodeStation"> code of the 2nd station</param>
        /// <returns></returns>
        TwoFollowingStations GetTwoStations(int firstCodeStation, int secondCodeStation);
        /// <summary>
        /// Return the User that we want according to his user name
        /// </summary>
        /// <param name="userName"> the user name</param>
        /// <returns></returns>
        User GetAUser(string userName);
        LineTrip GetALineTimes(int lineIdentificator, TimeSpan firstLineExit);
        // ---------- Searching a colletion ----------------
        /// <summary>
        /// Return the collection of buses 
        /// </summary>
        IEnumerable<Bus> GetAllBuses();
        /// <summary>
        /// Return the collection of stations
        /// </summary>
        IEnumerable<Station> GetAllStations();
        /// <summary>
        /// Return the collection of all the bus lines
        /// </summary>
       IEnumerable<BusLine> GetAllBusLines();
        /// <summary>
        /// Return the collection of all the points in a line  path 
        /// </summary>
        IEnumerable<PointInPathLine> GetAllThePointInPathLine();
        /// <summary>
        /// Returns a collection of all the following stations
        /// </summary>
        IEnumerable<TwoFollowingStations> GetAllFollowingStations();
        /// <summary>
        /// Return a collection of all the users
        /// </summary>
        IEnumerable<User> GetAllUsers();

        IEnumerable<LineTrip> GetAllLinesTimes();

        // ---------- Searching a colletion according to conditions ----------------
        /// <summary>
        /// Return the buses that meet the conditions
        /// </summary>
        /// <param name="BusCondition"> the condition to choose especially this bus</param>
        /// <returns></returns>
        IEnumerable<Bus> GetPartOfBuses(Predicate<Bus> BusCondition);
        /// <summary>
        /// Return the stations that meet the condition
        /// </summary>
        /// <param name="StationCondition"> the condition to choose the station</param>
        /// <returns></returns>
        IEnumerable<Station> GetPartOfStations(Predicate<Station> StationCondition);
        /// <summary>
        ///  Returns the bus lines that meet the condition
        /// </summary>
        /// <param name="BusLineCondition"> the condition for choosing the bus line </param>
        /// <returns></returns>
        IEnumerable<BusLine> GetPartOfBuseLines(Predicate<BusLine> BusLineCondition);
        /// <summary>
        /// Return the Points in the Path Line that meet the condition
        /// </summary>
        /// <param name="PointInPathLineCondition"> the condition for choosing a point</param>
        /// <returns></returns>
        IEnumerable<PointInPathLine> GetPartOfPointInPathLine(Predicate<PointInPathLine> PointInPathLineCondition);
        /// <summary>
        /// Return two following stations if they meet the condition
        /// </summary>
        /// <param name="TwoFollowingStationsCondition"> the condition</param>
        /// <returns></returns>
        IEnumerable<TwoFollowingStations> GetPartOfTwoFollowingStations(Predicate<TwoFollowingStations> TwoFollowingStationsCondition);
        /// <summary>
        /// Return the users if they meet the condition
        /// </summary>
        /// <param name="UserCondition">the condition for choosing the user</param>
        /// <returns></returns>
        IEnumerable<User> GetPartOfUsers(Predicate<User> UserCondition);
        IEnumerable<LineTrip> GetPartOfLinesTimes(Predicate<LineTrip> lineTimesCondition);


        //-------------------------------------------------------------------------------
        int GetTheLastBusIdentificator();
    }

}

