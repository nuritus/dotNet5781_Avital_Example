using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace dotNet5781_03B_0771_7072
{
    /// <summary>
    /// window that show the information about the bus 
    /// </summary>
    public partial class BusInfoWindow : Window
    {
        Bus myBus;
        //-----------------------------------------
        //constractor:
        //-----------------------------------------
        public BusInfoWindow(Bus bus)
        { 
            InitializeComponent();
            //make context between the UI to the data of the bus
            //print the data that the user chosed by the double click: 
            createDate.DataContext = bus;
            plateNumber.DataContext = bus;
            mileage.DataContext = bus;
            lastCare.DataContext = bus;
            KilometersFromlastCare.DataContext = bus;
            FuelKilometers.DataContext = bus; 
            timer.DataContext = bus;
            status.DataContext = bus;
            busType.DataContext = bus;
            care.DataContext = bus;
            fueling.DataContext = bus;
            timeLabel.DataContext = bus;
            myBus = bus;
        }
        //-------------------------------------------
        //events:
        //-------------------------------------------

        //event of click on the fueling button:
        private void fueling_Click(object sender, RoutedEventArgs e)//if the user chose to refueling the bus
        {   
            myBus.refueling();//sent the bus to refueling
        }

        //event of click on the care bustton: 
        private void care_Click(object sender, RoutedEventArgs e)//if the user chose to care the bus
        {
            myBus.busCare();//sent the bus to care
        }
    }
}
