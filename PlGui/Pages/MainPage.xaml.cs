using BlApi;
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

namespace PlGui.Pages
    /// </summary>
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    public partial class MainPage : Page
    {
        public event EventHandler InsertManegerClick;
        //public event EventHandler GetInfoAboutStationClick;
        public event EventHandler ExitProgramClick;
        IBl bl;
        public MainPage()
        {
            InitializeComponent();
            bl= BlFactory.GetBl();
        }

        private void InsertManeger_Click(object sender, RoutedEventArgs e)
        {
            if(InsertManegerClick!=null)
            {
                InsertManegerClick(this, EventArgs.Empty);
            }
        }

        private void TimesInStation_Click(object sender, RoutedEventArgs e)
        {
            LinesTimesInStationWindow window = new LinesTimesInStationWindow(bl);
            window.ShowDialog();
        }

        private void exitProgram_Click(object sender, RoutedEventArgs e)
        {
            if (ExitProgramClick != null)
            {
                ExitProgramClick(this, EventArgs.Empty);
            }
        }
    }
}
