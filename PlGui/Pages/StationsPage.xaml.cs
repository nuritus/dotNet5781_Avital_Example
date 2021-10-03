using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PlGui.Pages
{
    /// <summary>
    /// Interaction logic for StationsPage.xaml
    /// </summary>
    public partial class StationsPage : Page
    {
        IBl bl;
        private ObservableCollection<BO.Station> allStations = new ObservableCollection<BO.Station>();
        private AddStationWindow addStationWindow;
        private InsertTwoStationsWindow insertTwoStationsWindow;
        private ActionOnSpecificStationWindow specificStationWindow;
        private BO.Station lastSelectedStation;
        int lastLengthOFitemsSelected = 0; //count the number of stations that were selected from the list

        public StationsPage()
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            foreach (BO.Station s in bl.GetAllStations())
                allStations.Add(s);
            stationsListView.DataContext = allStations;
        }

        private void AddStationButton_Click(object sender, RoutedEventArgs e)
        {
            //open a window to add details of a new station..
            addStationWindow = new AddStationWindow(bl, addStationToList);
            addStationWindow.ShowDialog();
        }
        private void addStationToList(BO.Station stationToAdd)
        {
            //function to sent to the add window- add the station to the list
            allStations.Add(stationToAdd);
            allStations.Clear();
            foreach (BO.Station s in bl.GetAllStations())
                allStations.Add(s);
        }
        private void deleteStationButton_Click(object sender, RoutedEventArgs e)
        {
            BO.Station stationToDelte = ((sender as Button).DataContext) as BO.Station;
            //delete the station from the list (and from the ds)
            try
            {
                //ask the user if he sure that he want to delete the station
                var result = MessageBox.Show("האם אתה בטוח שברצונך למחוק תחנה זו?", "אישור פעולה", MessageBoxButton.YesNo, MessageBoxImage.Question,
                    MessageBoxResult.Yes, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                if (result == MessageBoxResult.Yes)
                {
                    //try to delete the station:
                    bl.DeleteStation(stationToDelte.StationCode);
                    allStations.Remove(stationToDelte);
                }

            }
            //if the station doesn't succeeded to delete:
            catch (DeletedProblemException)
            {
                MessageBox.Show("אין אפשרות למחוק תחנה שעוברים בה קווים", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
        }

        private void updateTwoStationsButton_Click(object sender, RoutedEventArgs e)
        { //click on the button to update distance and time between two stations
            insertTwoStationsWindow = new InsertTwoStationsWindow(bl, (stationsListView.SelectedItems[0] as BO.Station),
                (stationsListView.SelectedItems[1] as BO.Station));
            insertTwoStationsWindow.ShowDialog();
        }

        private void stationslistView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //the user will be able to select only two stations and he will be able to click on the update two stations information just
            //if he chose exactly two stations:
            int counter = (sender as ListView).SelectedItems.Count;
            //update the last selected station (for double click) just in case that the number of the selected items
            //grows up.. if double click on already selected station- update in the second click,
            //if double click on none selected station-update in the first click
            if (counter > lastLengthOFitemsSelected)
                lastSelectedStation = (sender as ListView).SelectedItems[counter - 1] as BO.Station;

            lastLengthOFitemsSelected = (sender as ListView).SelectedItems.Count;
            if (counter > 2)
                (sender as ListView).SelectedItems.RemoveAt(2);
            else if (counter < 2)
                updateTwoStationsButton.IsEnabled = false;
            else
                updateTwoStationsButton.IsEnabled = true;

        }

        private void stationsListView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            // if (stationsListView.SelectedItems.Count == 0)
            {
                specificStationWindow = new ActionOnSpecificStationWindow(bl, lastSelectedStation);
                specificStationWindow.UpdateStationName += SpecificStationWindow_UpdateStationName;
                specificStationWindow.ShowDialog();
            }
        }

        private void SpecificStationWindow_UpdateStationName(object sender, EventArgs e)
        {//order the list after change name of a station
            stationsListView.ItemsSource = allStations.OrderBy(x => x.StationName);
        }
    }
}
