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
    /// Interaction logic for AddStationWindow.xaml
    /// </summary>
    public partial class AddStationWindow : Window
    {
        IBl bl;
        Action<BO.Station> addStationToMainList;
        bool canceled = false;
        Station stationToAdd;
        public AddStationWindow(IBl progremBl, Action<BO.Station> addStation)
        {
            InitializeComponent();
            bl = progremBl;
            addStationToMainList = addStation;
        }

        #region all textBoxes got and lost focus
        private void stationCodeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Foreground = Brushes.Black;
            if ((sender as TextBox).Text == " ...קוד תחנה")
                (sender as TextBox).Text = "";
        }

        private void stationCodeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Foreground = Brushes.Gray;
                (sender as TextBox).Text = " ...קוד תחנה";
            }
        }

        private void latitudeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Foreground = Brushes.Black;
            if ((sender as TextBox).Text == " ...קו רוחב")
                (sender as TextBox).Text = "";
        }

        private void latitudeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Foreground = Brushes.Gray;
                (sender as TextBox).Text = " ...קו רוחב";
            }
        }

        private void longitudeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Foreground = Brushes.Black;
            if ((sender as TextBox).Text == " ...קו אורך")
                (sender as TextBox).Text = "";
        }

        private void longitudeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Foreground = Brushes.Gray;
                (sender as TextBox).Text = " ...קו אורך";
            }
        }

        private void stationNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Foreground = Brushes.Black;
            if ((sender as TextBox).Text == " ...שם תחנה")
                (sender as TextBox).Text = "";
        }

        private void stationNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Foreground = Brushes.Gray;
                (sender as TextBox).Text = " ...שם תחנה";
            }
        }

        private void stationAddressTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Foreground = Brushes.Black;
            if ((sender as TextBox).Text == " ...כתובת תחנה")
                (sender as TextBox).Text = "";
        }

        private void stationAddressTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Foreground = Brushes.Gray;
                (sender as TextBox).Text = " ...כתובת תחנה";
            }
        }
        #endregion

        #region textBoxses TextChanged- the add button will be enabled just in case the user filled them all
        private void stationCodeTextBox_TextChange(object sender, TextChangedEventArgs e)
        {
            if (stationCodeTextBox.Text!= " ...קוד תחנה")
            {
                if(stationCodeTextBox.Text != "" && latitudeTextBox.Text != " ...קו רוחב" &&
                    longitudeTextBox.Text != " ...קו אורך" && stationNameTextBox.Text != " ...שם תחנה"
                     && stationAddressTextBox.Text != " ...כתובת תחנה")
                {
                    AddStationToSystemButton.IsEnabled = true;
                }
                else
                {
                    AddStationToSystemButton.IsEnabled = false;
                }
            }
        }

        private void latitudeTextBox_TextChange(object sender, TextChangedEventArgs e)
        {
            if (latitudeTextBox.Text != " ...קו רוחב")
            {
                if (stationCodeTextBox.Text != " ...קוד תחנה" && latitudeTextBox.Text != "" &&
    longitudeTextBox.Text != " ...קו אורך" && stationNameTextBox.Text != " ...שם תחנה"
     && stationAddressTextBox.Text != " ...כתובת תחנה")
                {
                    AddStationToSystemButton.IsEnabled = true;
                }
                else
                {
                    AddStationToSystemButton.IsEnabled = false;
                }
            }
        }

        private void longitudeTextBox_TextChange(object sender, TextChangedEventArgs e)
        {
            if (longitudeTextBox.Text != " ...קו אורך")
            {
                if (stationCodeTextBox.Text != " ...קוד תחנה" && latitudeTextBox.Text != " ...קו רוחב" &&
    longitudeTextBox.Text != "" && stationNameTextBox.Text != " ...שם תחנה"
     && stationAddressTextBox.Text != " ...כתובת תחנה")
                {
                    AddStationToSystemButton.IsEnabled = true;
                }
                else
                {
                    AddStationToSystemButton.IsEnabled = false;
                }
            }
        }

        private void stationNameTextBox_TextChange(object sender, TextChangedEventArgs e)
        {
            if (stationNameTextBox.Text != " ...שם תחנה")
            {
                if (stationCodeTextBox.Text != " ...קוד תחנה" && latitudeTextBox.Text != " ...קו רוחב" &&
    longitudeTextBox.Text != " ...קו אורך" && stationNameTextBox.Text != ""
     && stationAddressTextBox.Text != " ...כתובת תחנה")
                {
                    AddStationToSystemButton.IsEnabled = true;
                }
                else
                {
                    AddStationToSystemButton.IsEnabled = false;
                }
            }
        }

        private void stationAddressTextBox_TextChange(object sender, TextChangedEventArgs e)
        {
            if (stationAddressTextBox.Text != " ...כתובת תחנה")
            {
                if (stationCodeTextBox.Text != " ...קוד תחנה" && latitudeTextBox.Text != " ...קו רוחב" &&
    longitudeTextBox.Text != " ...קו אורך" && stationNameTextBox.Text != " ...שם תחנה"
     && stationAddressTextBox.Text != "")
                {
                    AddStationToSystemButton.IsEnabled = true;
                }
                else
                {
                    AddStationToSystemButton.IsEnabled = false;
                }
            }
        }
        #endregion

        private void AddStationToSystem_Click(object sender, RoutedEventArgs e)
        {
            //try to add the station to the system:
            try
            {
               
                stationToAdd = new BO.Station
                {
                    StationCode = int.Parse(stationCodeTextBox.Text),
                    StationName = stationNameTextBox.Text,
                    StationAddress = stationAddressTextBox.Text,
                    LocationLatitude = double.Parse(latitudeTextBox.Text),
                    LocationLongitude = double.Parse(longitudeTextBox.Text),
                    PlaceToSit = (bool)placeToSitCheckBox.IsChecked,
                    BoardBusTiming = (bool)timeBoardCheckBox.IsChecked
                };
                bl.AddStation(stationToAdd);
                MessageBox.Show(" !התחנה נוספה בהצלחה");
                this.Close();
            }
           
            catch (AddingProblemException ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void onlyDouble_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            commonEvents.TextBox_OnlyDoubleNumbers_PreviewKeyDown(sender, e);
        }

        private void onlyInteger_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            commonEvents.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            canceled = true;
            this.Close();
        }

        private void addStationWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!canceled)
                addStationToMainList(stationToAdd);
        }

        private void window_MouseDown(object sender, MouseButtonEventArgs e)
        {//enable to drag the window
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
