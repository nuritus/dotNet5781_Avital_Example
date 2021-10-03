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
using System.Windows.Shapes;
using BO;
using BlApi;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddLineWindow.xaml
    /// </summary>
    public partial class AddLineWindow : Window
    {
        IBl bl;
        bool canceled = false;
        Action addLineToMainList;
        UpdateTwoStationsWindow twoStationsWindow;
        ObservableCollection<BO.StationInPath> allStationsInPath;
        int indexOfList = 0;
        public AddLineWindow(IBl progremBl, Action addLine)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            listOfAllStations.DataContext = bl.GetAllStations();
            allStationsInPath = new ObservableCollection<BO.StationInPath>();
            listOfPathStationsListView.DataContext = allStationsInPath;
            addLineToMainList = addLine;
        }

        #region add new station to the path button click
        private void AddStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Station myStation = bl.GetStationDetails((listOfAllStations.SelectedValue as BO.Station).StationCode);
                if (indexOfList == 0)
                {
                    allStationsInPath.Add(new StationInPath
                    {
                        StationCode = myStation.StationCode,
                        StationName = myStation.StationName,
                        StationNumberInPath = indexOfList + 1
                    });
                    indexOfList++;
                }
                else
                {
                    allStationsInPath.Add(new StationInPath
                    {
                        StationCode = myStation.StationCode,
                        StationName = myStation.StationName,
                        StationNumberInPath = indexOfList + 1
                    });
                    indexOfList++;
                }
                listOfPathStationsListView.Items.Refresh();
                addLineButtonToEnabled();
                addStationButton.IsEnabled = false;

            }
            catch (GetDetailsProblemException ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
                   MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
        }
        
        private void selectedStationFromList(object sender, SelectionChangedEventArgs e)
        {
            //check that the user will not add two next stations in path that are the same station
            if (indexOfList == 0 ||
                ((sender as ComboBox).SelectedItem as BO.Station).StationCode != allStationsInPath[indexOfList - 1].StationCode)
                addStationButton.IsEnabled = true;
            else
                addStationButton.IsEnabled = false;
        }

        #endregion

        #region click on the finally button to add line
        private void AddLineToSystem_Click(object sender, RoutedEventArgs e)
        {
            //copy all the stations to IEnumerable:
            var stations = from s in allStationsInPath
                           select new StationInPath
                           {
                               StationCode = s.StationCode,
                               StationName = s.StationName,
                               StationNumberInPath = s.StationNumberInPath
                           };
            try
            {
                string selectedcmb = "";
                var comboBoxItem = listOfArea.Items[listOfArea.SelectedIndex] as ComboBoxItem;
                if (comboBoxItem != null)
                {
                    selectedcmb = comboBoxItem.Content.ToString();
                }
                //add the Line to the system
                bl.AddBusLine(new BusLine
                {
                    FirstLineStation = allStationsInPath.ElementAt(0).StationCode,
                    LastLineStation = allStationsInPath.ElementAt(indexOfList - 1).StationCode,
                    LineNumber = int.Parse(lineNumberTextBox.Text),
                    LineArea = (BO.Area)Tools.stringToArea(selectedcmb),
                    StationsInPath = stations
                });
                MessageBox.Show("!הקו נוסף בהצלחה");
                
                this.Close();
            }
            catch (AddingProblemException ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }

            catch (MissDataOfTwoStationsExceptions ex)
            {
                if (twoStationsWindow != null)
                {
                    twoStationsWindow.Close();
                }
                twoStationsWindow = new UpdateTwoStationsWindow(ex.FirstStation, ex.SecondStation);
                twoStationsWindow.UpdateInformationClick += TwoStationsWindow_UpdateInformationClick;
                twoStationsWindow.Show();
            }
            catch
            {
                MessageBox.Show("תקלה בהוספת הקו", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
   MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
        }
        private void TwoStationsWindow_UpdateInformationClick(object sender, EventArgs e)
        {
            AddLineToSystem_Click(AddLineToSystemButton, null);
        }

        #endregion

        #region got and lost focus of the line number textBox
        private void lineNumberTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Foreground = Brushes.Black;
            if ((sender as TextBox).Text == " ...מספר הקו")
                (sender as TextBox).Text = "";
        }

        private void lineNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Foreground = Brushes.Gray;
                (sender as TextBox).Text = " ...מספר הקו";
            }
        }
        #endregion

        #region check if the add line button can be enabled- requires at list two stations and fill all the details
        private void addLineButtonToEnabled()
        {
            if ((lineNumberTextBox.Text == " ...מספר הקו" || lineNumberTextBox.Text == "")
                || listOfArea.SelectedItem == null || allStationsInPath.Count < 2)
                AddLineToSystemButton.IsEnabled = false;
            else
                AddLineToSystemButton.IsEnabled = true;
        }

        private void lineNumberTextBox_TextChange(object sender, TextChangedEventArgs e)
        {
            addLineButtonToEnabled();
        }

        private void lineOfArea_SelectedChange(object sender, SelectionChangedEventArgs e)
        {
            addLineButtonToEnabled();
        }

        #endregion

        #region enable to insert only digits in the textBox
        private void OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            commonEvents.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
        }
        #endregion

        private void deleteStationFromList_Click(object sender, RoutedEventArgs e)
        {//delete station from the list:
            BO.StationInPath station = ((sender as Button).DataContext) as BO.StationInPath;
            allStationsInPath.Remove(station);

            //update all the number in path of the stations in the list:
            for (int i = 0; i < allStationsInPath.Count; i++)
            {
                allStationsInPath[i].StationNumberInPath = i + 1;
            }
            listOfPathStationsListView.Items.Refresh();
            indexOfList--;
            if (allStationsInPath.Count < 2)
                AddLineToSystemButton.IsEnabled = false;
        }
        private void addLineWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!canceled)
                addLineToMainList();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            canceled = true;
            this.Close();
        }

        private void window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //enable to drag the window
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}


