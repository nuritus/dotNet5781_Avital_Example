//------------------------------------------------------------
//מצורף בתיקייה של תרגיל זה קובץ וורד בשם 'תיעוד בונוסים'
//------------------------------------------------------------

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace dotNet5781_03B_0771_7072
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //the list of all the buses:
        private ObservableCollection<Bus> ourCollection = new ObservableCollection<Bus>();
       
        //-----------------------------------------------
        //constractor:
        //-----------------------------------------------
        public MainWindow()
        {
            InitializeComponent();
            Insert10Buses();
            busesList.DataContext = ourCollection;
            this.Closing += MainWindow_Closed;
            //busesList.DisplayMemberPath = "BusNumber";
        }
       
        //-----------------------------------------------
        //Methods:
        //-----------------------------------------------

        private void Insert10Buses()
        {
            DateTime dateCare = new DateTime(2018, 11, 22);//the date of the last care for a bus that has to be cared
            DateTime dateCreate = new DateTime(2015, 1, 1);//the first date that all the bus go on road
            DateTime dateCareAllBuses = new DateTime(2020, 5, 22);//the date of the last care of all the buses (accept the first one)
            // add all the buses to the list
            ourCollection.Add(new Bus("1234567", dateCreate, dateCare, true, 10000, 1000, 50000));//more than a year from last care
            ourCollection.Add(new Bus("9876543", dateCreate, dateCareAllBuses, true, 19990, 1000, 50000));//close to 20000 kilometers
            ourCollection.Add(new Bus("1597534", dateCreate, dateCareAllBuses, true, 4000, 10, 50000));//close to the end of the fuel

            //the other buses:
            ourCollection.Add(new Bus("1111111", dateCreate, dateCareAllBuses, true, 4000, 900, 50000));
            ourCollection.Add(new Bus("2222222", dateCreate, dateCareAllBuses, true, 5000, 800, 50000));
            ourCollection.Add(new Bus("3333333", dateCreate, dateCareAllBuses, true, 4000, 900, 50000));
            ourCollection.Add(new Bus("4444444", dateCreate, dateCareAllBuses, false, 4000, 800, 50000));
            ourCollection.Add(new Bus("5555555", dateCreate, dateCareAllBuses, true, 7000, 700, 50000));
            ourCollection.Add(new Bus("6666666", dateCreate, dateCareAllBuses, true, 4000, 1100, 50000));
            ourCollection.Add(new Bus("7777777", dateCreate, dateCareAllBuses, false, 4000, 500, 50000));
            ourCollection.Add(new Bus("8888888", dateCreate, dateCareAllBuses, true, 4000, 1000, 50000));


        }
        //-----------------------------------------------
        //Events: 
        //-----------------------------------------------

        //click on the button to add bus:
        private void AddBus_Click(object sender, RoutedEventArgs e)//add bus to the list
        {
            WindowBusData windowBusData = new WindowBusData(ourCollection);
            windowBusData.Show();
        }

        //sent the bus to travel:
        private void Bus_Drive(object sender, RoutedEventArgs e) //sent the bus to travel
        {
            //select the suitable item in the list for the button that was clicked:
            ListBoxItem selectedItem = (ListBoxItem)busesList.ItemContainerGenerator.
                           ContainerFromItem(((Button)sender).DataContext);
            selectedItem.IsSelected = true;

            DriveWindow driveWindow = new DriveWindow(busesList.SelectedValue as Bus);
            driveWindow.ShowDialog();
        }

        //sent the bus to refualing:
        private void Bus_refueling(object sender, RoutedEventArgs e)
        {
            //select the suitable item in the list for the button that was clicked:
            ListBoxItem selectedItem = (ListBoxItem)busesList.ItemContainerGenerator.
                                       ContainerFromItem(((Button)sender).DataContext);
            selectedItem.IsSelected = true;
            
            //refueling the bus
            (busesList.SelectedValue as Bus).refueling();
        }

        //double click on a bus-item in the list
        private void choose_Bus_Double_Click(object sender, MouseButtonEventArgs e)
        {
            BusInfoWindow infoWindow = new BusInfoWindow(busesList.SelectedValue as Bus);
            infoWindow.Show();
        }
       
        //-------------------------------------------
        //search box:
        //-------------------------------------------
        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchBox.Text == "")//if the search text box is empty
                foreach (var item in busesList.Items)
                {
                    ListBoxItem ourItem = (ListBoxItem)busesList.ItemContainerGenerator. //find the suitable listBoxItem to the bus
                                              ContainerFromItem(item);
                    ourItem.Visibility = Visibility.Visible;//the user will be able to see all the buses
                }
            else
            {
                foreach (var item in busesList.Items)
                {
                    ListBoxItem ourItem = (ListBoxItem)busesList.ItemContainerGenerator.//find the suitable listBoxItem to the bus
                                              ContainerFromItem(item);
                    int searchLength = ((string)searchBox.Text).Length;//the leangth of the text that the user insert
                    if (searchLength > ((item as Bus).BusNumber).Length)//if the leangth of the text that the user insert is more than the leangth of the busNumber
                        ourItem.Visibility = Visibility.Collapsed;//the user won't be able to see all the buses
                    else if (searchBox.Text != (item as Bus).BusNumber.Substring(0, (searchBox.Text).Length))//if the text that the user insert doesn't equal to the start of the bus number
                        ourItem.Visibility = Visibility.Collapsed;//the user won't be able to see all the buses
                    else
                        ourItem.Visibility = Visibility.Visible;//the user will be able to see all the buses
                }
            }
        }

        //avoid from the user to add "spaces" in the begining of the search box:
        private void searchBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((sender as TextBox).Text == "" && e.Key == Key.Space)
                e.Handled = true;
            return;
        }
        //--------------------------------------------------------
        //event of closing the main window close all the threads:
        //--------------------------------------------------------
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

    }

}
