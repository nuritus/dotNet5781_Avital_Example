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
using BO;
using BlApi;

namespace PlGui
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBl bl;
        private MenuWindow menuWindow;
        private PlGui.Pages.InsertManegerPage insertManegerPage= new Pages.InsertManegerPage();
        private PlGui.Pages.MainPage mainPage = new Pages.MainPage();
        public MainWindow()
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            mainFrame.NavigationService.Navigate(mainPage);
            insertManegerPage.InsertButtonClick += InsertManegerPage_InsertButtonClick;
            mainPage.InsertManegerClick += MainPage_InsertManegerClick;
            mainPage.ExitProgramClick += Close_Progrem;
            insertManegerPage.ExitProgramClick += Close_Progrem;
            insertManegerPage.BackToMainPageClick += InsertManegerPage_BackToMainPageClick;
        }

        private void InsertManegerPage_BackToMainPageClick(object sender, EventArgs e)
        {
            mainFrame.Navigate(mainPage);
        }

        private void MainPage_InsertManegerClick(object sender, EventArgs e)
        {
            mainFrame.Navigate(insertManegerPage);
        }

        private void InsertManegerPage_InsertButtonClick(object sender, EventArgs e)
        {
            menuWindow = new MenuWindow(insertManegerPage.insertUser);
            menuWindow.Show();
            this.Close();
        }

        private void Close_Progrem(object sender, EventArgs e)
        {
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
