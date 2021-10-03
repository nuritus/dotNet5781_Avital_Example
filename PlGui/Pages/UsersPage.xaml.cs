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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using BO;

namespace PlGui.Pages
{
    /// <summary>
    /// Interaction logic for UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        IBl bl;
        private ObservableCollection<BO.User> allUsers= new ObservableCollection<BO.User>();
        private AddNewUserWindow addUserWindow;
        public UsersPage()
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            foreach (BO.User s in bl.GetAllUsers())
                allUsers.Add(s);
            usersListView.DataContext = allUsers;
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            addUserWindow = new AddNewUserWindow(bl, addUserToList);
            addUserWindow.ShowInTaskbar = false;
            addUserWindow.ShowDialog();

        }
        private void addUserToList(BO.User userToAdd)
        {
           
            allUsers.Add(userToAdd);
            allUsers.Clear();
            foreach (BO.User s in bl.GetAllUsers())
                allUsers.Add(s);
        }

       



    }

    }
