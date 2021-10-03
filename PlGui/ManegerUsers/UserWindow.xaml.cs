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
using BlApi;
using BO;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        IBl bl;
        private ObservableCollection<BO.User> allUsers = new ObservableCollection<BO.User>();
        private AddNewUserWindow addUserWindow;
        private BO.User lastSelectedUser;

        public UserWindow()
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            foreach (BO.User s in bl.GetAllUsers())
                allUsers.Add(s);
            usersListView.DataContext = allUsers;
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            addUserWindow = new AddNewUserWindow();
            addUserWindow.Owner = this;
            addUserWindow.ShowInTaskbar = false;
            addUserWindow.ShowDialog();

        }
        private void addUserToList(BO.User userToAdd)
        {
            //function to sent to the add window- add the station to the list
            allUsers.Add(userToAdd);
            allUsers.Clear();
            foreach (BO.User s in bl.GetAllUsers())
                allUsers.Add(s);
        }

        private void stationsListView_DoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
