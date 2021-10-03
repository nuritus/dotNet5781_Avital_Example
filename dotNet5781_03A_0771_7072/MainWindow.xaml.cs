using System;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dotNet5781_03A_0771_7072
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        dotNet5781_02_0771_7072.BusLinesCollection buses = new dotNet5781_02_0771_7072.BusLinesCollection();// all the buses
        static Random rand = new Random(DateTime.Now.Millisecond);
        private dotNet5781_02_0771_7072.BusLine currentDisplayBusLine;//the current bus we chosed
        public void CreateBus()
        {
            for (int i = 0; i < 40; i++)
            {//40 stations in the program
                new dotNet5781_02_0771_7072.BusStation();
            }

            for (int i = 0; i < 10; i++) // this loop adds buses to the collection
            {
                int[] stations = new int[10];
                for (int j = 0; j < 10; j++) // this loop adds stations to each bus of the collection
                {
                    stations[j] = rand.Next(1, 40);
                    for (int k = j - 1; k >= 0; k--) // checks if the station already exists in the path
                    {
                        if (stations[j] == stations[k]) // if yes, doesn't add
                        {
                            j--;
                            break;
                        }
                    }
                }
                int busNumber = rand.Next(1, 999);
                bool flag = true;
                while (flag)
                {
                    flag = false;
                    foreach (dotNet5781_02_0771_7072.BusLine line in buses)// checks if there are not 2 buses that are the same
                        if (line.BusNumber == busNumber)
                        {
                            busNumber = rand.Next(1, 999);
                            flag = true;
                        }
                }

                buses.addLine(busNumber, stations);
            }
        }

        public MainWindow() // Constructor
        {
            InitializeComponent();
            CreateBus();
            cbBusLines.ItemsSource = buses;
            cbBusLines.DisplayMemberPath = "BusNumber";
            cbBusLines.SelectedIndex = 0;
            ShowBusLine((cbBusLines.SelectedItem as dotNet5781_02_0771_7072.BusLine).BusNumber);


            ShowBusLine((cbBusLines.SelectedValue as dotNet5781_02_0771_7072.BusLine).BusNumber);
        }

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)// the event of changing our choice
        {
                ShowBusLine((cbBusLines.SelectedValue as dotNet5781_02_0771_7072.BusLine).BusNumber);
        }

        void ShowBusLine(int number)
        {
            currentDisplayBusLine = buses[number];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.path;
        }
    }
}
