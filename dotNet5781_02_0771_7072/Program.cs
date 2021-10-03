using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0771_7072
{
   public class Program
    {
        enum Options { ADDBUS, ADDSTATION, DELETEBUS, DELETESTATION, SEARCHSTATION, SEARCHPATH, PRINTBUSES, PRINTSTATIONS, EXIT };
        static void initialization(ref BusLinesCollection BusCollection)
        {
            for (int i = 0; i < 40; i++)//40 stations in the program
                new BusStation();
            BusCollection.addLine(1, new int[] { 1, 2, 3, 4, 5, 10 });
            BusCollection.addLine(2, new int[] { 6, 7, 8, 1, 9, 10 });
            BusCollection.addLine(3, new int[] { 11, 12, 13, 14, 15, 40 });
            BusCollection.addLine(4, new int[] { 16, 17, 1, 18, 10, 19, 20 });
            BusCollection.addLine(5, new int[] { 21, 22, 23, 24, 25, 5 });
            BusCollection.addLine(6, new int[] { 26, 27, 28, 29, 30, 18 });
            BusCollection.addLine(7, new int[] { 31, 32, 33, 34, 35 });
            BusCollection.addLine(8, new int[] { 36, 37, 38, 39, 40 });
            BusCollection.addLine(9, new int[] { 1, 6, 11, 16, 21, 26, 31, 36 });
            BusCollection.addLine(10, new int[] { 2, 7, 12, 17, 22, 27, 32 });
        }
        /// <summary>
        /// function to print the options to the user
        /// </summary>
        /// <returns></returns>
        static Options printMenu()
        {
            Console.WriteLine
(@"Enter your choice:
0: adding a new bus line
1: adding a station to a bus path
2: remove a bus line
3: remove a station from a bus path
4: search which lines pass in a station
5: options to travel between two stations
6: print all bus lines in the system
7: print all stations and the line that pass there
8: exit");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
                Console.WriteLine("ERROR. try to press your choice again");
            return (Options)choice;
        }
    static void Main(string[] args)
    {
        Options choice;
        BusLinesCollection myBusCollection = new BusLinesCollection();// create collection of buses
        initialization(ref myBusCollection);//initialize the collection
        choice = printMenu();//get the choice from the user
        while (choice != Options.EXIT)
        {
            try
            {
                switch (choice)
                {
                    case Options.ADDBUS: //add new bus to the collection
                        Console.WriteLine("please enter the number of the line you want to add:");
                        int busNumber = int.Parse(Console.ReadLine());//get the number of the bus
                        Console.WriteLine("please enter the number of station in the line path:");
                        int stationNum;
                        int.TryParse(Console.ReadLine(), out stationNum);//get how much stations will be in the bus path
                        if (stationNum <= 1)//must be more than 1 station
                            throw new ArgumentException("ERROR: must be more than 1 station");
                        int[] stations = new int[stationNum];
                        Console.WriteLine("please enter the code number of the stations: (in our program : 1 - 40)");
                        for (int i = 0; i < stationNum; i++)// get the path of the bus from the user
                            stations[i] = int.Parse(Console.ReadLine());
                        myBusCollection.addLine(busNumber, stations);//add the bus to the collection
                        break;
                    case Options.ADDSTATION://add station to path of specific bus
                        Console.WriteLine("please enter the line number:");
                        int busNum = int.Parse(Console.ReadLine());
                        Console.WriteLine("please enter the station code number to add to the path:");
                        int newStation = int.Parse(Console.ReadLine());
                        Console.WriteLine("please enter the code number of the station (in the path) will be after the station you add\npress -1 to add the last station");
                        int preStation = int.Parse(Console.ReadLine());
                        myBusCollection[busNum].addStation(newStation, preStation);//add the station
                        break;
                    case Options.DELETEBUS://delete a bus from the collection
                        Console.WriteLine("please enter the line number:");
                        busNum = int.Parse(Console.ReadLine());
                        myBusCollection.removeLine(busNum);
                        break;
                    case Options.DELETESTATION://delete a station from a path of a bus
                        Console.WriteLine("please enter the line number:");
                        busNum = int.Parse(Console.ReadLine());
                        Console.WriteLine("please enter the station code number to delete from the path:");
                        int delStation = int.Parse(Console.ReadLine());
                        myBusCollection[busNum].removeStation(delStation);//delete the station
                        break;
                    case Options.SEARCHSTATION://print all the lines that pass in specific station
                        Console.WriteLine("please enter the number of the station:");
                        stationNum = int.Parse(Console.ReadLine());
                        foreach (BusLine bus in myBusCollection)//check every bus if it passes in the station
                            if (bus.checkStation(stationNum)) Console.WriteLine("line: {0}", bus.BusNumber);
                        break;
                    case Options.SEARCHPATH:// search the options to travel between two stations and sort them according to the time
                        Console.WriteLine("please enter the number of your departure station:");
                        int startStation = int.Parse(Console.ReadLine());
                        Console.WriteLine("please enter the number of your destination station:");
                        int endStation = int.Parse(Console.ReadLine());
                        List<BusLine> subList = new List<BusLine>();// list of pathes between the two stations
                        foreach (BusLine bus in myBusCollection)
                        {
                            //check if both station are exists in the bus path and also check that the departure station is before the destination station
                            if (bus.checkStation(startStation) && bus.checkStation(endStation) && bus.indexStation(startStation) < bus.indexStation(endStation))
                                subList.Add(bus.subPath(startStation, endStation));//add the bus to the list
                        }
                        subList.Sort();//sort the list according to the time
                        foreach (BusLine bus in subList)// print the sorted list
                        {
                            Console.WriteLine("Line: {0} Time: {1}", bus.BusNumber, bus.timeBetweenTwoStations(startStation, endStation));
                        }
                        break;
                    case Options.PRINTBUSES://print all the details about the buses in the system
                        foreach (BusLine bus in myBusCollection)
                            Console.WriteLine(bus);
                        break;
                    case Options.PRINTSTATIONS://print a list of all the stations with the lines that pass there
                        foreach (BusStation station in AllBusStations.listOfStations)
                        {
                            Console.Write("station {0}{1} ", station.CodeStation, ":");
                            foreach (BusLine bus in myBusCollection)
                                if (bus.checkStation(station.CodeStation)) Console.Write("{0}  ", bus.BusNumber);
                            Console.Write("\n");
                        }
                        break;
                    case Options.EXIT:
                        break;
                    default:
                        break;
                }
            }
            catch (FormatException) { Console.WriteLine("ERROR"); }
            catch (ArgumentException ex) { Console.WriteLine(ex.Message); }
            choice = printMenu();//get the next choice from the user
        }
    }
}
}