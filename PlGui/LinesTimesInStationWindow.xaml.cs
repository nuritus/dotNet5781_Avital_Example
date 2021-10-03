using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.ComponentModel;
using System.Threading;
using BlApi;
using BO;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for LinesTimesInStationWindow.xaml
    /// </summary>
    public partial class LinesTimesInStationWindow : Window 
    {

        BackgroundWorker clockWorker = new BackgroundWorker();
        //Stopwatch stopWatch;
        TimeSpan currentTime;
        Station currentStation;
        int speed;
        //int refreshTime;
        IBl bl;

        bool isTimerRun;
        public LinesTimesInStationWindow(IBl progremBl)
        {
            InitializeComponent();
            bl = progremBl;
            //currentStation = bl.GetStationDetails(codeStation);
            //updatePlate();
            //stopWatch = new Stopwatch();
            currentTime = new TimeSpan(DateTime.Now.Hour,DateTime.Now.Minute,DateTime.Now.Second);
            listOfAllStations.DataContext = bl.GetAllStations();

            clockWorker.DoWork += ClockWorker_DoWork;
            clockWorker.ProgressChanged += ClockWorker_ProgressChanged;
            clockWorker.WorkerReportsProgress = true;
            speed = 1;
            isTimerRun = true;        
           // stopWatch.Start();
            clockWorker.RunWorkerAsync();
        }

        void updatePlate()
        {
            int counterListItems = linesInStationListBox.Items.Count;
            for (int i= linesInStationListBox.Items.Count-1; i> 0; i--)
            {
                linesInStationListBox.Items.RemoveAt(i);
            }
            stationNameTextBlock.DataContext = currentStation;
            stationCodeTextBlock.DataContext = currentStation;
            foreach (var line in currentStation.LinesInStation)
            {
                ListBoxItem item = new ListBoxItem();
                item.Background = Brushes.Yellow;
                Label lab = new Label();
                lab.Content = line.LineNumber.ToString();
                lab.FontSize = 15;
                lab.Background = Brushes.Yellow;
                item.Content = lab;
                linesInStationListBox.Items.Add(item);
                
            }

        }
        private void ClockWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {

                clockWorker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }

        private void ClockWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string clockVisible = (currentTime += new TimeSpan(0,0,speed)).ToString();
            if (currentTime >= new TimeSpan(24, 0, 0))
            {
                currentTime = currentTime - new TimeSpan(24, 0, 0);
                clockVisible = currentTime.ToString();
            }
            clockTextBlock.Text = clockVisible.Substring(0, 8);
            if (currentStation != null)
            {
                timesInStationListView.ItemsSource = bl.GetAllLinesTimesInStation(currentStation.StationCode, currentTime);
            }
            
        }

        private void closing_Window(object sender, CancelEventArgs e)
        {
            isTimerRun=false;
        }

        private void checkIfEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                speed = int.Parse((sender as TextBox).Text);
            }
        }

        private void onlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            commonEvents.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
        }

        private void selectedStationFromList(object sender, SelectionChangedEventArgs e)
        {
            currentStation = listOfAllStations.SelectedItem as BO.Station;
            updatePlate();
        }


        private void window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //enable to drag the window
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void exitWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
