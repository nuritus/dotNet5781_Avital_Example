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
    /// Interaction logic for AddNewUserWindow.xaml
    /// </summary>
    public partial class AddNewUserWindow : Window
    {
        IBl bl;
        Action<BO.User> addUserToMainList;
        bool canceled = false;
        User userToAdd;
        public AddNewUserWindow(IBl progremBl, Action<BO.User> addUser)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            bl = progremBl;
            addUserToMainList = addUser;

        }

        private void AddUserToSystem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                userToAdd = new BO.User
                {
                    UserName = userNameTextBox.Text,
                    UserPassword = passwordTextBox.Text,
                    UserAccessManagement = (bool)accessManagementCheckBox.IsChecked,

                };
                bl.AddUser(userToAdd);
                MessageBox.Show(" !המשמש נוסף בהצלחה");
                this.Close();
                //this.Owner.Show();
            }
            catch (AddingProblemException ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
           MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);



            }

          
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            canceled = true;
            this.Close();
        }

        private void userNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Foreground = Brushes.Gray;
                (sender as TextBox).Text = " ...שם משתמש";
            }

        }

        private void userNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Foreground = Brushes.Black;
            if ((sender as TextBox).Text == " ...שם משתמש")
                (sender as TextBox).Text = "";

        }

        private void passwordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Foreground = Brushes.Gray;
                (sender as TextBox).Text = " ...ססמא";
            }
        }

        private void passwordTextBox_TextChange(object sender, TextChangedEventArgs e)
        {
            if (passwordTextBox.Text != " ...ססמא")
            {
                if (userNameTextBox.Text != " ...שם משתמש" && passwordTextBox.Text != "")
                {
                    AddUserToSystemButton.IsEnabled = true;
                }
                else
                {
                    AddUserToSystemButton.IsEnabled = false;
                }
            }
        }

        private void userNameTextBox_TextChange(object sender, TextChangedEventArgs e)
        {
            if (userNameTextBox.Text != " ...שם משתמש")
            {
                if (userNameTextBox.Text != "" && passwordTextBox.Text != " ...ססמא" )
                {
                    AddUserToSystemButton.IsEnabled = true;
                }
                else
                {
                    AddUserToSystemButton.IsEnabled = false;
                }
            }

        }

        private void passwordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Foreground = Brushes.Black;
            if ((sender as TextBox).Text == " ...ססמא")
                (sender as TextBox).Text = "";
        }
        private void addUserWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!canceled)
                addUserToMainList(userToAdd);
        }
    }
}
