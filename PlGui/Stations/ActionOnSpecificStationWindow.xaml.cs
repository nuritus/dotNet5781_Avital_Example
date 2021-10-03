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
    /// Interaction logic for ActionOnSpecificStationWindow.xaml
    /// </summary>
    public partial class ActionOnSpecificStationWindow : Window
    {
        public event EventHandler UpdateStationName;
        BO.Station stationDetails;
        IBl bl;
        public ActionOnSpecificStationWindow(IBl progremBl ,BO.Station s)
        {
            InitializeComponent();
            bl = progremBl;
            stationDetails = s;
            insertFirstDetailsToTextBox();
        }

        private void insertFirstDetailsToTextBox()
        {
            codeTextBox.Text = stationDetails.StationCode.ToString();
            latitudeTextBox.Text = stationDetails.LocationLatitude.ToString();
            longitudeTextBox.Text = stationDetails.LocationLongitude.ToString();
            addressTextBox.Text = stationDetails.StationAddress;
            nameTextBox.Text = stationDetails.StationName;
            placeToSitCheckBox.IsChecked = stationDetails.PlaceToSit;
            timerBoardCheckBox.IsChecked = stationDetails.BoardBusTiming;

            stationNameTextBlock.DataContext = stationDetails;
            stationCodeTextBlock.DataContext = stationDetails;
            foreach (var line in stationDetails.LinesInStation)
            {
                ListBoxItem item = new ListBoxItem();
                item.Background = Brushes.Yellow;
                Label lab = new Label();
                lab.Content = line.LineNumber.ToString();
                lab.FontSize = 15;
                lab.Background = Brushes.Yellow;
                item.Content = lab;
                //item.FontSize = 15;;
                linesInStationListBox.Items.Add(item);
            }
        }



        #region buttons for update the basic information about the station
        private void updateNameButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.IsReadOnly)
            {
                nameTextBox.IsReadOnly = false;
                cancelNameButton.IsEnabled = true;
                updateSitsButton.IsEnabled = false;
                updateTimesBoardButton.IsEnabled = false;
            }
            else
            {
                if (nameTextBox.Text != "")
                {
                    bl.UpdateStationName(stationDetails.StationCode, nameTextBox.Text);
                    stationDetails.StationName = nameTextBox.Text;
                    stationDetails.StationName = nameTextBox.Text;
                    stationDetails.StationName = nameTextBox.Text;
                    nameTextBox.IsReadOnly = true;
                    cancelNameButton.IsEnabled = false;
                    updateSitsButton.IsEnabled = true;
                    updateTimesBoardButton.IsEnabled = true;
                    if(UpdateStationName!=null)
                    {
                        UpdateStationName(this, EventArgs.Empty);
                    }
                }
            }
        }

        private void updateSitsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!placeToSitCheckBox.IsEnabled)
            {
                placeToSitCheckBox.IsEnabled = true;
                updateNameButton.IsEnabled = false;
                cancelSitsButton.IsEnabled = true;
                updateTimesBoardButton.IsEnabled = false;
            }
            else
            {
                bl.UpdatePlaceToSit(stationDetails.StationCode, (bool)placeToSitCheckBox.IsChecked);
                stationDetails.PlaceToSit = (bool)placeToSitCheckBox.IsChecked;
                placeToSitCheckBox.IsEnabled = false;
                updateNameButton.IsEnabled = true;
                cancelSitsButton.IsEnabled = false;
                updateTimesBoardButton.IsEnabled = true;
            }
        }

        private void updateTimesBoardButton_Click(object sender, RoutedEventArgs e)
        {
            if (!timerBoardCheckBox.IsEnabled)
            {
                timerBoardCheckBox.IsEnabled = true;
                updateNameButton.IsEnabled = false;
                cancelTimesBoardButton.IsEnabled = true;
                updateSitsButton.IsEnabled = false;
            }
            else
            {
                bl.UpdateBoardBusTiming(stationDetails.StationCode, (bool)timerBoardCheckBox.IsChecked);
                stationDetails.BoardBusTiming = (bool)timerBoardCheckBox.IsChecked;
                timerBoardCheckBox.IsEnabled = false;
                updateNameButton.IsEnabled = true;
                cancelTimesBoardButton.IsEnabled = false;
                updateSitsButton.IsEnabled = true;
            }
        }


        private void cancelNameButton_Click(object sender, RoutedEventArgs e)
        {
            nameTextBox.Text = stationDetails.StationName;
            nameTextBox.IsReadOnly = true;
            cancelNameButton.IsEnabled = false;
            updateSitsButton.IsEnabled = true;
            updateTimesBoardButton.IsEnabled = true;
        }

        private void cancelSitsButton_Click(object sender, RoutedEventArgs e)
        {
            placeToSitCheckBox.IsChecked = stationDetails.PlaceToSit;
            placeToSitCheckBox.IsEnabled = false;
            updateNameButton.IsEnabled = true;
            cancelSitsButton.IsEnabled = false;
            updateTimesBoardButton.IsEnabled = true;
        }

        private void cancelTimesBoardButton_Click(object sender, RoutedEventArgs e)
        {
            timerBoardCheckBox.IsChecked = stationDetails.BoardBusTiming;
            timerBoardCheckBox.IsEnabled = false;
            updateNameButton.IsEnabled = true;
            cancelTimesBoardButton.IsEnabled = false;
            updateSitsButton.IsEnabled = true;
        }

        #endregion

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
