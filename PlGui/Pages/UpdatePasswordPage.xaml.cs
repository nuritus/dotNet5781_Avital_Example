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
{
    /// <summary>
    /// Interaction logic for UpdatePasswordPage.xaml
    /// </summary>
    public partial class UpdatePasswordPage : Page
    {
        BO.User userToUpdate;
        IBl bl;
        public UpdatePasswordPage(BO.User myUser)
        {
            InitializeComponent();
            userToUpdate = myUser;
            bl = BlFactory.GetBl();
        }
        private void updatePassword_Click(object sender, RoutedEventArgs e)
        {
            if (hidenOldPassword.Password != userToUpdate.UserPassword)
            {
                errorInUpdate.Text = "סיסמה ישנה לא תקינה";
                errorInUpdate.Visibility = Visibility.Visible;
            }
            else if (hidenNewPassword.Password != hidenNewPassword.Password)
            {
                errorInUpdate.Text = "אימות הסיסמה לא תקין";
                errorInUpdate.Visibility = Visibility.Visible;
            }
            else if (hidenNewPassword.Password.Length < 4)
            {
                errorInUpdate.Text = "סיסמה צריכה להכיל לפחות 4 תווים";
                errorInUpdate.Visibility = Visibility.Visible;
            }
            else
            {
                bl.UpdateUserPassword(userToUpdate.UserName, hidenNewPassword.Password);
                userToUpdate.UserPassword = hidenNewPassword.Password;
                MessageBox.Show("הסיסמה עודכנה בהצלחה");
            }
        }
    }
}