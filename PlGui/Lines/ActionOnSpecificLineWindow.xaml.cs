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
using BlApi;
using BO;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for ActionOnSpecificLineWindow.xaml
    /// </summary>
    public partial class ActionOnSpecificLineWindow : Window
    {
        public event EventHandler UpdateLineNumber;
        UpdateTwoStationsWindow twoStationsWindow;
        PO.BusLine lineDetails;
        BO.StationInPath stationInPathUpdate;
        int placeToAddStation;
        BO.LineTrip lineTripToUpdate;
        IBl bl;
        public ActionOnSpecificLineWindow(PO.BusLine line)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            lineDetails = line;
            ListStations.ItemsSource = lineDetails.StationsInPath;
            stationsListView.DataContext = bl.GetAllStations();
            timesListView.DataContext = bl.GetAllLineTripTimesOfALine(line.BusLineIndentificator);
            IdTextBox.Text = line.BusLineIndentificator.ToString();
            numberLineTextBox.Text = line.LineNumber.ToString();
            areaTextBox.Text = line.LineArea.areaToString();
        }

        #region update the number of the line
        private void updateNumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (numberLineTextBox.IsReadOnly)
            {
                cancelLineNumberButton.IsEnabled = true;
                numberLineTextBox.IsReadOnly = false;
                showTheGridToAddTimeButton.IsEnabled = false;
                timesListView.IsEnabled = false;
                ListStations.IsEnabled = false;
            }
            else
            {
                try
                {
                    bl.UpdateNumberLine(lineDetails.BusLineIndentificator, int.Parse(numberLineTextBox.Text));
                    lineDetails.LineNumber = int.Parse(numberLineTextBox.Text);
                    cancelLineNumberButton.IsEnabled = false;
                    numberLineTextBox.IsReadOnly = true;
                    showTheGridToAddTimeButton.IsEnabled = true;
                    timesListView.IsEnabled = true;
                    ListStations.IsEnabled = true;
                    if (UpdateLineNumber != null)
                    {
                        UpdateLineNumber(this, EventArgs.Empty);
                    }
                }

                catch (UpdateProblemException)
                {
                    errorNumberTextBlock.Visibility = Visibility.Visible;
                }
            }

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {//cancel the option to update the number
            cancelLineNumberButton.IsEnabled = false;
            numberLineTextBox.IsReadOnly = true;
            showTheGridToAddTimeButton.IsEnabled = true;
            timesListView.IsEnabled = true;
            ListStations.IsEnabled = true;
            numberLineTextBox.Text = lineDetails.LineNumber.ToString();
        }

        private void onlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            commonEvents.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
        }
        #endregion

        #region add station to path
        private void rightAddButton_Click(object sender, RoutedEventArgs e)
        {
            //show the user the options of stations to add:
            placeToAddStation = ((sender as Button).DataContext as BO.StationInPath).StationNumberInPath + 1;
            stationsToAddStackPanel.Visibility = Visibility.Visible;
            stationsListView.Focus();
        }
        private void leftAddButton_Click(object sender, RoutedEventArgs e)
        {
            //show the user the options of stations to add:
            placeToAddStation = ((sender as Button).DataContext as BO.StationInPath).StationNumberInPath;
            stationsToAddStackPanel.Visibility = Visibility.Visible;
            stationsListView.Focus();
        }
        void addStationToPath(StationInPath stationToUpdate, int placeToAdd)
        {
            //add the station that the user chose to the path
            try
            {
                bl.AddStationInPath(lineDetails.BusLineIndentificator, placeToAdd, stationToUpdate.StationCode);
                ListStations.ItemsSource = bl.GetBusLineDetails(lineDetails.BusLineIndentificator).StationsInPath;
                lineDetails.StationsInPath = bl.GetBusLineDetails(lineDetails.BusLineIndentificator).StationsInPath;
                stationsToAddStackPanel.Visibility = Visibility.Hidden;
            }
            catch (AddingProblemException)
            {
                var result = MessageBox.Show("אין אפשרות שבמסלול הקו תופיע אותה תחנה ברציפות", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error,
MessageBoxResult.Yes, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
            catch (GetDetailsProblemException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (MissDataOfTwoStationsExceptions ex)//if there are miss data about two stations in path after adding the new station:
            {
                if (twoStationsWindow != null)
                    twoStationsWindow.Close();
                //open new window to add the data between the two stations:
                twoStationsWindow = new UpdateTwoStationsWindow(ex.FirstStation, ex.SecondStation);
                //write a function to the event of the update window- the event happend when the user click to update the data that he insert
                twoStationsWindow.UpdateInformationClick += TwoStationsWindow_ClosingByUpdateAdd;
                twoStationsWindow.Show();
            }
        }

        private void TwoStationsWindow_ClosingByUpdateAdd(object sender, EventArgs e)
        {
            twoStationsWindow = null;
            //try to add the station to the path one more time 
            addStationToPath(stationInPathUpdate, placeToAddStation);
        }
        private void stationsListView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            //chose the station to add to the path from the list of stations by double click:
            BO.Station selectedStation = (sender as ListView).SelectedItem as BO.Station;
            stationInPathUpdate = new StationInPath { StationCode = selectedStation.StationCode };
            addStationToPath(stationInPathUpdate, placeToAddStation);
        }

        #endregion

        #region delete station from path
        private void removeStationButton_Click(object sender, RoutedEventArgs e)
        {
            stationInPathUpdate = (sender as Button).DataContext as BO.StationInPath;
            var result = MessageBox.Show("האם אתה בטוח שברצונך למחוק קו זה?", "אישור פעולה", MessageBoxButton.YesNo, MessageBoxImage.Question,
    MessageBoxResult.Yes, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            if (result == MessageBoxResult.Yes)
            {
                deleteStationInPath(stationInPathUpdate);
            }
        }

        private void deleteStationInPath(StationInPath station)
        {
            try
            {
                //try to delete the station from the path:
                bl.DeleteStationInPath(lineDetails.BusLineIndentificator, station.StationNumberInPath);
                lineDetails.StationsInPath = bl.GetBusLineDetails(lineDetails.BusLineIndentificator).StationsInPath;
                ListStations.ItemsSource = bl.GetBusLineDetails(lineDetails.BusLineIndentificator).StationsInPath;
            }
            catch (MissDataOfTwoStationsExceptions ex)
            {
                //if there are miss da
                if (twoStationsWindow != null)
                    twoStationsWindow.Close();
                twoStationsWindow = new UpdateTwoStationsWindow(ex.FirstStation, ex.SecondStation);
                twoStationsWindow.UpdateInformationClick += TwoStationsWindow_ClosingByUpdate;
                twoStationsWindow.Show();
            }
            catch (DeletedProblemException)
            {
                MessageBox.Show("קו חייב לפחות שתי תחנות במסלול", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
    MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                    MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
        }

        private void TwoStationsWindow_ClosingByUpdate(object sender, EventArgs e)
        {
            twoStationsWindow = null;
            deleteStationInPath(stationInPathUpdate);
        }

        #endregion

        #region options of stations to add to the path
        private void stationList_GotFocus(object sender, RoutedEventArgs e)
        {
            stationsToAddStackPanel.Visibility = Visibility.Visible;
        }

        private void stationList_LostFocus(object sender, RoutedEventArgs e)
        {
            stationsToAddStackPanel.Visibility = Visibility.Collapsed;
        }
        #endregion


        #region update data between stations with the pre station
        private void updateStationsInfoButton_Click(object sender, RoutedEventArgs e)
        {
            BO.StationInPath station = (sender as Button).DataContext as BO.StationInPath;
            if (station.StationNumberInPath != 1)
            {
                int first = (ListStations.Items[station.StationNumberInPath - 2] as BO.StationInPath).StationCode;
                int second = (ListStations.Items[station.StationNumberInPath - 1] as BO.StationInPath).StationCode;
                twoStationsWindow = new UpdateTwoStationsWindow(first, second);
                twoStationsWindow.UpdateInformationClick += updateInformation_Click;
                twoStationsWindow.ShowDialog();
            }
        }

        private void updateInformation_Click(object sender, EventArgs e)
        {//after the user add the station to the path- update the list on the screen
            ListStations.ItemsSource = bl.GetBusLineDetails(lineDetails.BusLineIndentificator).StationsInPath;
        }
        #endregion

        #region add and delete times
        private void deleteTimeButton_Click(object sender, RoutedEventArgs e)
        {
            //delete a time exit of the line
            BO.LineTrip lineTimes = (sender as Button).DataContext as BO.LineTrip;
            bl.DeleteLineTrip(lineTimes.BusLineIndentificator, lineTimes.TimeFirstLineExit);
            timesListView.ItemsSource = bl.GetAllLineTripTimesOfALine(lineDetails.BusLineIndentificator);
        }

        private void showTheGridToAddTime_Click(object sender, RoutedEventArgs e)
        {
            if (addTimesStackPanel.Visibility == Visibility.Collapsed)
            {
                //open the option to add time, lock all the other options:
                firstTimeTextBox.Text = "00:00:00";
                lastTimeTextBox.Text = "00:00:00";
                frequencyTextBox.Text = "00:00:00";
                addTimesStackPanel.Visibility = Visibility.Visible;
                ListStations.IsEnabled = false;
                timesListView.IsEnabled = false;
                updateLineNumberButton.IsEnabled = false;
                errorInDataLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                addTimesStackPanel.Visibility = Visibility.Collapsed;
                ListStations.IsEnabled = true;
                timesListView.IsEnabled = true;
                updateLineNumberButton.IsEnabled = true;
            }
        }

        private void addTimeToLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddLineTrip(new LineTrip
                {
                    BusLineIndentificator = lineDetails.BusLineIndentificator,
                    Frequency = TimeSpan.Parse(frequencyTextBox.Text),
                    TimeFirstLineExit = TimeSpan.Parse(firstTimeTextBox.Text),
                    TimeLastLineExit = TimeSpan.Parse(lastTimeTextBox.Text)
                });
                //update the list in the window:
                timesListView.ItemsSource = bl.GetAllLineTripTimesOfALine(lineDetails.BusLineIndentificator);
            }
            catch (AddingProblemException ex)
            {
                if (ex.InnerException == null)
                    errorInDataLabel.Content = "אחת השעות שהוכנסו אינה תקינה*";
                else
                    errorInDataLabel.Content = "ישנה התנגשות עם זמנים המוגדרים בקו*";
                errorInDataLabel.Visibility = Visibility.Visible;
            }
        }

        private void cancelTheOptionToAddTime_Click(object sender, RoutedEventArgs e)
        {
            //close the option to add time:
            addTimesStackPanel.Visibility = Visibility.Collapsed;
            ListStations.IsEnabled = true;
            updateLineNumberButton.IsEnabled = true;
            cancelLineNumberButton.IsEnabled = false;
            timesListView.IsEnabled = true;
        }

        #endregion

        #region update times of line trip:
        private void updateTimeButton_Click(object sender, RoutedEventArgs e)
        {
            lineTripToUpdate = (sender as Button).DataContext as BO.LineTrip;

            updateFrecuencyTimeTextBox.Text = lineTripToUpdate.Frequency.ToString().Substring(0, 8);

            updateLastHourTimeTextBox.Text = lineTripToUpdate.TimeLastLineExit.ToString().Substring(0, 8);

            timesListView.IsEnabled = false;
            updateTimesStackPanel.Visibility = Visibility.Visible;
            showTheGridToAddTimeButton.IsEnabled = false;
            ListStations.IsEnabled = false;
            updateLineNumberButton.IsEnabled = false;
            cancelLineNumberButton.IsEnabled = false;
            errorInLastHourLabel.Visibility = Visibility.Hidden;
        }

        private void updateLastHourButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateLastHourLineTrip(lineTripToUpdate.BusLineIndentificator, lineTripToUpdate.TimeFirstLineExit,
                    TimeSpan.Parse(updateLastHourTimeTextBox.Text));
                timesListView.ItemsSource = bl.GetAllLineTripTimesOfALine(lineDetails.BusLineIndentificator);
                errorInLastHourLabel.Visibility = Visibility.Hidden;
            }
            catch (InvalidValueException)
            {
                errorInLastHourLabel.Content = "הוכנסה שעה לא חוקית*";
                errorInLastHourLabel.Visibility = Visibility.Visible;
            }
            catch (UpdateProblemException)
            {
                errorInLastHourLabel.Content = "ישנה התנגשות עם זמנים מוגדרים בקו*";
                errorInLastHourLabel.Visibility = Visibility.Visible;
            }
        }

        private void endUpdateTimeButton_Click(object sender, RoutedEventArgs e)
        {
            updateTimesStackPanel.Visibility = Visibility.Collapsed;
            showTheGridToAddTimeButton.IsEnabled = true;
            ListStations.IsEnabled = true;
            updateLineNumberButton.IsEnabled = true;
            timesListView.IsEnabled = true;
        }

        private void updateFrequencyButton_Click(object sender, RoutedEventArgs e)
        {
            bl.UpdateFrequencyLineTrip(lineTripToUpdate.BusLineIndentificator, lineTripToUpdate.TimeFirstLineExit,
                   TimeSpan.Parse(updateFrecuencyTimeTextBox.Text));
            timesListView.ItemsSource = bl.GetAllLineTripTimesOfALine(lineDetails.BusLineIndentificator);
            errorInLastHourLabel.Visibility = Visibility.Hidden;
        }

        #endregion
    }
}
