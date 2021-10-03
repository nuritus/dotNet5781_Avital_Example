using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0771_7072
{
    public class BusLinesCollection : IEnumerable
    {
        //------------------------------------------
        //field
        //------------------------------------------
        List<BusLine> listBuses;
        /// <summary>
        /// return the same iterator of the bus in the list
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return listBuses.GetEnumerator();
        }

        /// <summary>
        /// add new line to the system
        /// </summary>
        /// <param name="busNum"></param>
        /// <param name="stations"></param>
        public void addLine(int busNum,int[] stations)
        {
            foreach (BusLine bus in listBuses)//check that the bus doesn't exist yet
                if (bus.BusNumber == busNum)
                    throw new ArgumentException("ERROR: the bus number is already exists in system");
            BusLine myBus = new BusLine(busNum);//create the new bus
            for (int i = 0; i < stations.Length; i++)
            {
                myBus.addStation(stations[i], -1);//add the station to the end of path
            }
            listBuses.Add(myBus);// add the bus to the collection
        }
     
        /// <summary>
        ///  remove a line from the collection
        /// </summary>
        /// <param name="numLineBus"></param>
        public void removeLine(int numLineBus)
        {
            for (int i = 0; i < listBuses.Count; i++)//search the line in the collection
            {
                if (listBuses[i].BusNumber == numLineBus)
                {
                    listBuses.RemoveAt(i);//remove from the list
                    break;
                }
                if (i == listBuses.Count - 1)
                    throw new ArgumentException("ERROR: the bus doesn't exist in system");
            }
        }
        /// <summary>
        /// return list of buses that pass in specific station
        /// </summary>
        /// <param name="stationNum"></param>
        /// <returns></returns>
        public List<int> busesInStation(int stationNum)
        {
            List<int> buses = new List<int>();
            foreach (BusLine bus in listBuses) //check all the buses 
                if (bus.checkStation(stationNum)) buses.Add(bus.BusNumber);
            if (buses.Count == 0)// if no bus is passes in this station
                throw new ArgumentException("ERROR: no buses pass in this station");
            return buses;//return the list of the buses
        }
        
        /// <summary>
        /// sort the bus list according to time of path
        /// </summary>
        /// <returns></returns>
        public List<BusLine> sortedList()
        {
            listBuses.Sort();//using the sort of the class list
            return listBuses;
        }

        public BusLine this[int busNum] //indexer
        {
            get 
            {
                foreach (BusLine bus in listBuses)//search the bus in the list of the buses
                    if (bus.BusNumber == busNum) return bus;
                //if the bus doesn't exists inthe system:
                throw new AggregateException("ERROR: The bus isn't exists in the system");
            }
        }

        //--------------------------------------------
        //constractor:
        //--------------------------------------------
        public BusLinesCollection()
        {
            listBuses = new List<BusLine>();
        }

    }

    }
