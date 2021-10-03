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
    /// Interaction logic for UpdateTwoStationsWindow.xaml
    /// </summary>
    /// 

    public delegate void EventHandler(object sender, EventArgs e);
    public partial class UpdateTwoStationsWindow : Window
    {
        public event EventHandler UpdateInformationClick;
        public event EventHandler TheSameStation;
        int firstStationCode;
        int secondStationCode;
        IBl bl;  
        
        public UpdateTwoStationsWindow(int firstCode, int secondCode)
        {//constractor:
            InitializeComponent();
            bl = BlFactory.GetBl();
            firstStationCode = firstCode;
            secondStationCode = secondCode;
            if (firstCode==secondCode)
            {
                if(TheSameStation!=null)
                {
                    TheSameStation(this,EventArgs.Empty);
                    this.Close();
                }
            }
            try
            {
                code1.Text = "" + firstCode;
                code2.Text = "" + secondCode;
                name1.Text = bl.GetStationDetails(firstCode).StationName;
                name2.Text = bl.GetStationDetails(secondCode).StationName;
            }
            catch
            {
                MessageBox.Show("תקלה בשליפת מידע על אחת התחנות");
                this.Close();
            }
        }

        #region distance textBox got and lost focus
        private void distance_GotFocus(object sender, RoutedEventArgs e)
        {
            distance.Foreground = Brushes.Black;
            if (distance.Text == "...הכנס מרחק בין תחנות")
                (sender as TextBox).Text = "";
        }

        private void distance_LostFocus(object sender, RoutedEventArgs e)
        {
            if (distance.Text == "")
            {
                distance.Foreground = Brushes.Gray;
                distance.Text = "...הכנס מרחק בין תחנות";
            }
        }
        #endregion

        private void updateData_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                bl.UpdateTwoStations(new DataAboutTwoStations
                {
                    FirstStationCode = firstStationCode,
                    SecondStationCode = secondStationCode,
                    DistanceBetweenStations = double.Parse(distance.Text),
                    AverageTimeBetweenStations = TimeSpan.Parse(timeBetweenStations.Text)
                }) ;
                if (UpdateInformationClick != null)
                {
                    UpdateInformationClick(this, EventArgs.Empty);
                }
                this.Close();
            }
            catch
            {
                messege.Visibility = Visibility.Visible;
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void onlyNumberInsert_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            commonEvents.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
        }

        private void distanceTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            commonEvents.TextBox_OnlyDoubleNumbers_PreviewKeyDown(sender, e);
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
