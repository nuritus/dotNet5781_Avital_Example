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
using System.Text.RegularExpressions;

namespace dotNet5781_03B_0771_7072
{
    /// <summary>
    /// window to get distance to travel from the user
    /// </summary>
    public partial class DriveWindow : Window
    {
        Bus busToDrive;//the bus that selected to drive

        //-------------------------------------------
        //constractor:
        //-------------------------------------------
        public DriveWindow(Bus busSend)// get the bus that chose
        {
            InitializeComponent();
            busToDrive = busSend;
            driveDistance.Focus();
        }

        //---------------------------------------------
        //events:
        //---------------------------------------------
        
        //to make sure that the textBox get only numbers and enter
        private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            //allow get out of the text box
            if (e.Key == Key.Tab)
                    return;
            if (e.Key < Key.NumPad0 || e.Key > Key.NumPad9)//enable to chose the numbers in the right of keyboard
            {
                //allow list of system keys (add other key here if you want to allow)
                if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
             || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
                    return;

                char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

                //allow control system keys
                if (Char.IsControl(c)) return;

                //allow digits (without Shift or Alt)
                if (Char.IsDigit(c))
                    if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                        return; //let this key be written inside the textbox

                //forbid letters and signs (#,$, %, ...)
                e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
                return;
            }
        }

        //check if the user press "enter"
        private void checkIfEntetr(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                try //try to insert the distance from the user into the bus 
                {
                    busToDrive.travel(int.Parse(driveDistance.Text));
                    this.Close();
                }
                catch (invalidInputException ex)
                {
                    MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
