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
    /// Interaction logic for InsertTwoStationsWindow.xaml
    /// </summary>
    public partial class InsertTwoStationsWindow : Window
    {
        IBl bl;
        BO.Station firstStation, secondStation;
        public InsertTwoStationsWindow(IBl progremBl, BO.Station fStation, BO.Station sStation)
        {
            InitializeComponent();
            bl = progremBl;
            firstStation = fStation;
            secondStation = sStation;
            //write the data into the suitable textBoxes:
            code1.Text = firstStation.StationCode.ToString();
            code2.Text = secondStation.StationCode.ToString();
            name1.Text = firstStation.StationName;
            name2.Text = secondStation.StationName;
            showDataBetweenStationsIfExists();
        }

        private void showDataBetweenStationsIfExists()
        {
            if (bl.checkIfDataBetweenStationsExist(firstStation.StationCode, secondStation.StationCode))
            {
                distanceDataTextBlock.Text = bl.DistanceBetweenTwoStations(firstStation.StationCode, secondStation.StationCode).ToString();
                timeDataTextBlock.Text = bl.TimeBetweenTwoStations(firstStation.StationCode, secondStation.StationCode).ToString();
                existDataStackPanel.Visibility = Visibility.Visible;
            }
            else
                existDataStackPanel.Visibility = Visibility.Collapsed;
        }


        #region text boxes got and lost focus (erase the text..)
        private void distanceTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Foreground = Brushes.Black;
            if ((sender as TextBox).Text == "...הכנס מרחק בין תחנות")
                (sender as TextBox).Text = "";
        }

        private void distanceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Foreground = Brushes.Gray;
                (sender as TextBox).Text = "...הכנס מרחק בין תחנות";
            }
        }

        private void hoursTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Foreground = Brushes.Black;
            if ((sender as TextBox).Text == "...שעות")
                (sender as TextBox).Text = "";
        }

        private void hoursTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Foreground = Brushes.Gray;
                (sender as TextBox).Text = "...שעות";
            }
        }

        private void minutesTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Foreground = Brushes.Black;
            if ((sender as TextBox).Text == "...דקות")
                (sender as TextBox).Text = "";
        }

        private void minutesTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Foreground = Brushes.Gray;
                (sender as TextBox).Text = "...דקות";
            }
        }

        private void secondsTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Foreground = Brushes.Black;
            if ((sender as TextBox).Text == "...שניות")
                (sender as TextBox).Text = "";
        }

        private void secondsTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Foreground = Brushes.Gray;
                (sender as TextBox).Text = "...שניות";
            }
        }



        #endregion

        #region textChanged events for update information- check that the user fill all the nesseccery fields 
        private void distanceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (distanceTextBox.Text != "...הכנס מרחק בין תחנות")
            {
                if (distanceTextBox.Text!="")
                    updateInfoButton.IsEnabled = true;
                else
                    updateInfoButton.IsEnabled = false;
            }
        }
        #endregion

        #region click on the button to update data between two station
        private void updateInfoButton_Click(object sender, RoutedEventArgs e)
        {
            //the two codes of the stations:
            int firstStationCode = firstStation.StationCode;
            int secondStationCode = secondStation.StationCode;
            try
            {
                bool stationsAlreadyExists = bl.checkIfDataBetweenStationsExist(firstStationCode, secondStationCode);
                MessageBoxResult answer=MessageBoxResult.No;
                //if the details already exist in system- ask the user if he sure that he want to update them 
                if (stationsAlreadyExists)
                {
                    answer = MessageBox.Show("?האם אתה בטוח שברצונך לעדכן מידע זה", "הודעה", MessageBoxButton.YesNo, MessageBoxImage.Question,
                        MessageBoxResult.Yes, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                }
                //if the user want to update the detaiols or the details doesn't exist yet in the system:
                if (!stationsAlreadyExists || answer == MessageBoxResult.Yes)
                {
                    //update the details that get from the user
                    bl.UpdateTwoStations(new BO.DataAboutTwoStations
                    {
                        FirstStationCode = firstStationCode,
                        SecondStationCode = secondStationCode,
                        DistanceBetweenStations = double.Parse(distanceTextBox.Text),
                        AverageTimeBetweenStations = TimeSpan.Parse(timeTextBox.Text)
                    }) ;
                    MessageBox.Show("!המידע עודכן בהצלחה");
                    this.Close();
                }
            }
            //in case that the user insert charecter instead digits:
            catch (FormatException)
            {
                MessageBox.Show("יש להכניס ספרות בלבד", "שגיאה", MessageBoxButton.OK, 
                MessageBoxImage.Error, MessageBoxResult.OK,MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
            //in case that one of the codes station dosn't exist in system
            catch(GetDetailsProblemException)
            {
                MessageBox.Show("אחת התחנות לא קיימת במערכת", "שגיאה", MessageBoxButton.OK,
                MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
            catch (AddingProblemException ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK,
                MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
        }
        #endregion

        #region enable the user to insert just numbers in text boxes
        private void integerNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            commonEvents.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
        }

        private void doubleNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            commonEvents.TextBox_OnlyDoubleNumbers_PreviewKeyDown(sender, e);
        }

        #endregion

        private void replaceBetweenStationsButton_Click(object sender, RoutedEventArgs e)
        {
            BO.Station tempStation = firstStation;
            firstStation = secondStation;
            secondStation = tempStation;
            //return on the actions of the ctor after replace the stations:
            code1.Text = firstStation.StationCode.ToString();
            code2.Text = secondStation.StationCode.ToString();
            name1.Text = firstStation.StationName;
            name2.Text = secondStation.StationName;
            showDataBetweenStationsIfExists();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
