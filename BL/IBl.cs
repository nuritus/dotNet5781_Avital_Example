using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BO;

namespace BlApi
{
    public interface IBl
    {
        /// <summary>
        /// function to add a bus to the system
        /// </summary>
        /// <param name="busToAdd">the bus we want to add with all it parameters</param>
        void AddBus(Bus busToAdd);
        /// <summary>
        /// function to delete a bus from the system
        /// </summary>
        /// <param name="licenseNumber">get the license number of the bus to delete</param>
        void DeleteBus(string licenseNumber);
        /// <summary>
        /// function to add a bus line to the system
        /// </summary>
        /// <param name="lineToAdd">get all the details of the new line- include the list of the stations</param>
        void AddBusLine(BusLine lineToAdd);
        /// <summary>
        /// function to delete a bus line from the system
        /// </summary>
        /// <param name="lineToDeleteIndentificator">get the bus line identificator to delete</param>
        void DeleteBusLine(int lineToDeleteIndentificator);

        /// <summary>
        /// function to delete a bus from the system
        /// </summary>
        /// <param name="stationToAdd">get the deltails of the station to add</param>
        void AddStation(Station stationToAdd);
        /// <summary>
        /// function to delete a station from the system
        /// </summary>
        /// <param name="StationCode">get the code of the station to delete </param>
        void DeleteStation(int StationCode);
        /// <summary>
        /// function to refuel a bus
        /// </summary>
        /// <param name="licenseBusToRefuel">get the license number of the bus to refuel</param>
        void RefuelingBus(string licenseBusToRefuel);
        /// <summary>
        /// return a list of all the buses with all their details
        /// </summary>
        /// <returns></returns>
        IEnumerable<Bus> GetAllBuses();
        /// <summary>
        /// return the details of a specific bus
        /// </summary>
        /// <param name="licenseNumber">get the license number of the bus </param>
        /// <returns></returns>
        Bus GetBusDetails(string licenseNumber);
        /// <summary>
        /// return a list of all the lines with all their details include the stations in their path
        /// </summary>
        /// <returns></returns>
        IEnumerable<BusLine> GetAllBusLines();
        /// <summary>
        /// return the details of a specific line
        /// </summary>
        /// <param name="busLineIndentificator">get the line identificator</param>
        /// <returns></returns>
        BusLine GetBusLineDetails(int busLineIndentificator);

        /// <summary>
        /// return a list of all the stationss with all their details include the list of the lines that passed in the station
        /// </summary>
        /// <returns></returns>
        IEnumerable<Station> GetAllStations();
        /// <summary>
        /// return the details of a specific station
        /// </summary>
        /// <param name="dtationCode">get the station code to sent its details</param>
        /// <returns></returns>
        Station GetStationDetails(int dtationCode);
        /// <summary>
        /// function to update or add data about two stations
        /// </summary>
        /// <param name="twoStations">get the two stations with the information about them </param>
        void UpdateTwoStations(DataAboutTwoStations twoStations);
        /// <summary>
        /// delete a specific station in a path of a line
        /// </summary>
        /// <param name="busLineIdentidicator">get the bus line identificator to delete station from his path</param>
        /// <param name="numberInPath">get the station number in path (first, second...) </param>
        void DeleteStationInPath(int busLineIdentidicator, int numberInPath);
        /// <summary>
        /// add a station in path of a specific line
        /// </summary>
        /// <param name="busLineIdentidicator">get the bus line identificator to add station to his path</param>
        /// <param name="stationToAdd">get the details of the station to add(using just the station code, the name and the number in path)</param>
        void AddStationInPath(int busLineIdentidicator, int placeToAddInPath, int stationCodeToAdd);
        /// <summary>
        /// update a station in the path.. in a specific place in path
        /// </summary>
        /// <param name="busLineIdentidicator">get the line identificator to add the station to it path</param>
        /// <param name="stationToAdd">get the station details to update (using the code name and number in path)</param>
        void UpdateStationInPath(int busLineIdentidicator, StationInPath stationToUpdate);
        
       /// <summary>
       /// update self payment in a bus- if there is or not
       /// </summary>
       /// <param name="licenseNumber">get the license number of the bus</param>
       /// <param name="flag">get the answer.. true- if there is devices for self payment, false if not</param>
        void UpdateSelfPaymentInBus(string licenseNumber, bool flag);
        /// <summary>
        /// update the number of a line 
        /// </summary>
        /// <param name="busIdentificator">get the line identificator</param>
        /// <param name="NewNumber">get the new number of the line</param>
        void UpdateNumberLine(int busIdentificator, int NewNumber);
        /// <summary>
        /// update the name of a station
        /// </summary>
        /// <param name="stationCode">get the station code</param>
        /// <param name="newName">get the new name of the station</param>
        void UpdateStationName(int stationCode, string newName);
        /// <summary>
        /// update place to sit in a station (if there is or not)
        /// </summary>
        /// <param name="stationCode">get the staion code</param>
        /// <param name="flag">get flag if there is place to sit-true, else-false </param>
        void UpdatePlaceToSit(int stationCode, bool flag);
        /// <summary>
        /// update board of bus timing in a station (if there is or not)
        /// </summary>
        /// <param name="stationCode">get the station code</param>
        /// <param name="flag">get flag if there is board of timing-true, else-false </param>
        void UpdateBoardBusTiming(int stationCode, bool flag);

        /// <summary>
        /// function to add user to the system
        /// </summary>
        /// <param name="newUser">get the details of the new user</param>
        void AddUser(User newUser);
        /// <summary>
        /// delete user from the system
        /// </summary>
        /// <param name="userName">get the user name to delete</param>
        void DeleteUser(string userName);
        /// <summary>
        /// function to update the password of a user
        /// </summary>
        /// <param name="userName">get the name of the user</param>
        /// <param name="newPassword">get the new password</param>
        void UpdateUserPassword(string userName, string newPassword);
        /// <summary>
        /// update the user access to the system
        /// </summary>
        /// <param name="userName">get the user name</param>
        /// <param name="access">get the newe access- if he has managmentAccess-true else false</param>
        void UpdateUserAccess(string userName, bool access);
        /// <summary>
        /// search if a user exist in the system acccording to the password and name
        /// </summary>
        /// <param name="myUser">get the user to search</param>
        /// <returns></returns>
        bool SearchUser(string myUserName, string myUserPassword);
        /// <summary>
        /// return the details of a specific user
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        User GetUserDetails(string name);
        /// <summary>
        /// return the distance between two stations
        /// </summary>
        /// <param name="firstStationCode">the first station code</param>
        /// <param name="secondStationCode">the second station code</param>
        /// <returns></returns>
        IEnumerable<User> GetAllUsers();
        double DistanceBetweenTwoStations(int firstStationCode, int secondStationCode);
        /// <summary>
        /// return the time of traveling between two stations
        /// </summary>
        /// <param name="firstStationCode">get the first station code</param>
        /// <param name="secondStationCode">get the second station code</param>
        /// <returns></returns>
        TimeSpan TimeBetweenTwoStations(int firstStationCode, int secondStationCode);

        /// <summary>
        /// return all the lines with all their details sorted by the their area of activity
        /// </summary>
        /// <returns></returns>
        IEnumerable<IGrouping<Area, BusLine>> GetLinesByArea();
        /// <summary>
        /// check if there is data in the system about two diffrent stations
        /// </summary>
        /// <param name="firstStationCode">get the first staion code</param>
        /// <param name="secondStationCode">get the srecond station code</param>
        /// <returns></returns>
        bool checkIfDataBetweenStationsExist(int firstStationCode, int secondStationCode);

        /// <summary>
        /// add a new line trip to the system
        /// </summary>
        /// <param name="lineTripToAdd">get line trip with- time first exit, frequency, time last exit and the line id </param>
        void AddLineTrip(LineTrip lineTripToAdd);
        /// <summary>
        /// delete a line trip from the system
        /// </summary>
        /// <param name="lineID">the line id to delete</param>
        /// <param name="firstLineExit">the first exit of the line trip</param>
        void DeleteLineTrip(int lineID, TimeSpan firstLineExit);
        /// <summary>
        /// update the last exit hour of the line
        /// </summary>
        /// <param name="lineID">the line id</param>
        /// <param name="firstLineExit">the first time of exit</param>
        void UpdateLastHourLineTrip(int lineID, TimeSpan firstLineExit, TimeSpan newlastLineExit);

        /// <summary>
        /// update the frequency of an exit of a line
        /// </summary>
        /// <param name="lineID">the line id</param>
        /// <param name="firstLineExit">the first time of the line exit</param>
        void UpdateFrequencyLineTrip(int lineID, TimeSpan firstLineExit, TimeSpan newfrequencyLineExit);
        /// <summary>
        /// get all the "line trip" times for a specific line
        /// </summary>
        /// <param name="busLineId">the line id</param>
        /// <returns></returns>
        /// 
        IEnumerable<LineTrip> GetAllLineTripTimesOfALine(int busLineId);
        /// <summary>
        /// get information about the lines that are closing to a specific station according the time it get
        /// </summary>
        /// <param name="codeStaion">the code of the station to check</param>
        /// <param name="currentTime">the time we search closing lines for it</param>
        /// <returns></returns>
        IEnumerable<LineTimingInStation> GetAllLinesTimesInStation(int codeStaion, TimeSpan currentTime);

    }
}
