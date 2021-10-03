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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        private MainWindow main;
        private PlGui.Pages.HomePage homePage= new PlGui.Pages.HomePage();
        private PlGui.Pages.UpdatePasswordPage updatePasswordPage;
        private BO.User connectedUser;
        private PlGui.Pages.StationsPage stationsPage =new PlGui.Pages.StationsPage();
        private PlGui.Pages.LinesPage linesPage = new PlGui.Pages.LinesPage();
        private PlGui.Pages.UsersPage usersPage;

        public MenuWindow(BO.User myUser)
        {
            InitializeComponent();
            connectedUser = myUser;
            userNameTextBlock.DataContext = connectedUser;
            updatePasswordPage = new Pages.UpdatePasswordPage(myUser);
            usersPage = new PlGui.Pages.UsersPage();
            Main.NavigationService.Navigate(homePage);
            homeWin.Background = Brushes.DarkBlue;
            homeWin.Foreground = Brushes.White;

        }

        private void Stations_Manegement(object sender, RoutedEventArgs e)
        {
            defaultColor();
            stationWin.Background = Brushes.DarkBlue;
            stationWin.Foreground = Brushes.White;
            Main.NavigationService.Navigate(stationsPage);
        }

        private void Lines_Manegement(object sender, RoutedEventArgs e)
        {
            defaultColor();
            lineWin.Background = Brushes.DarkBlue;
            lineWin.Foreground = Brushes.White;
            Main.NavigationService.Navigate(linesPage);
        }
        private void updatePassword_Click(object sender, RoutedEventArgs e)
        {
            defaultColor();
            updatePasswordWin.Background = Brushes.DarkBlue;
            updatePasswordWin.Foreground = Brushes.White;
            Main.NavigationService.Navigate(updatePasswordPage);
        }

        private void Users_Management(object sender, RoutedEventArgs e)
        {
            defaultColor();
            manageWin.Background = Brushes.DarkBlue;
            manageWin.Foreground = Brushes.White;
            Main.NavigationService.Navigate(usersPage);
        }

        private void Home_Screen(object sender, RoutedEventArgs e)
        {
            defaultColor();
            homeWin.Background = Brushes.DarkBlue;
            homeWin.Foreground = Brushes.White;
            Main.NavigationService.Navigate(homePage);        
        }

        private void Close_Progrem(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void defaultColor()
        {
            homeWin.Background = Brushes.WhiteSmoke;
            homeWin.Foreground = Brushes.Black;

            lineWin.Background = Brushes.WhiteSmoke;
            lineWin.Foreground = Brushes.Black;

            stationWin.Background = Brushes.WhiteSmoke;
            stationWin.Foreground = Brushes.Black;

            updatePasswordWin.Background = Brushes.WhiteSmoke;
            updatePasswordWin.Foreground = Brushes.Black;

            manageWin.Background = Brushes.WhiteSmoke;
            manageWin.Foreground = Brushes.Black;
        }

        private void clickToDisconnect(object sender, RoutedEventArgs e)
        {
            main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void window_MouseDown(object sender, MouseButtonEventArgs e)
        {//enable to drag the window
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void grid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {//enable to enlarge and reduse the size of the window by double click
            if (this.WindowState == WindowState.Normal && e.ChangedButton== MouseButton.Left && e.ClickCount == 2)
            {
                this.WindowState = WindowState.Maximized;
            }
            else if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                this.WindowState = WindowState.Normal;
            }

        }
    }
}
