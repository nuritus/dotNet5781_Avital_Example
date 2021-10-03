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
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace dotNet5781_03B_0771_7072
{

    /// <summary>
    /// window for insert the data of a new bus
    /// </summary>
    public partial class WindowBusData : Window
    {
        ObservableCollection<Bus> buses;// the list of all the buses we got it from the constractor
        private Bus myBus;// field to the bus we want to add
        public bool validBus = false;//check if the details of the bus are valid
    //----------------------------------------------------
    //constractor:
    //----------------------------------------------------
    /// <summary>
    /// the constractor get the list of buses from the main window
    /// this enable the window to update the list...
    /// </summary>
    /// <param name="list"></param>
        public WindowBusData(ObservableCollection<Bus> list)
        {
            buses = list;
            InitializeComponent();
            
            ChooseDate(createYear,createMonth);//create the options to the user to chose the day the year and the month
            ChooseDate(careYear, careMonth);//create the options to the user to chose the day the year and the month
        }

    //----------------------------------------------------
    //Methods
    //----------------------------------------------------
        
        // create the options to the user to chose the day the year and the month
        void ChooseDate(ComboBox year, ComboBox month)
        {
            for (int i = 2020; i >= 1960; i--)//the options to the year
            {
                year.Items.Add(i);
            }
            for (int i = 1; i <= 12; i++)//the options to the month
            {
                month.Items.Add(i);
            }
            //the options to the day will be according to the month..
        }

        
        // create the days in the month- according to the month..
        void dayAccordingToMonth(int month,ComboBox day)
        {
            if (day != null)//clear the list before add all the days
                day.Items.Clear();
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)//months with 31 days
            {
                for (int i = 1; i <= 31; i++)
                {
                    day.Items.Add(i);
                }
            }
            if(month == 4|| month == 6|| month == 9|| month == 11)////months with 30 days
            {
                for (int i = 1; i <= 30; i++)
                {
                    day.Items.Add(i);
                }
            }
            if(month == 2)////months with 28 days
            {
                for (int i = 1; i <= 28; i++)
                {
                    day.Items.Add(i);
                }
            }

        }

        
        // check if all the field that the user must fill is mandatory
        bool ableToClickButton() 
        {
            bool date = false;
            bool busNumber = false;
            bool mileageSum = false;
            if (createYear.SelectedItem != null && createMonth != null && createDay != null)// check if the create date updated
                date = true;
            if (plateNumber.Text!="")//check that the user insert the plate number 
                busNumber = true;
            if (mileage.Text != "")//check that the user insert the mileage of the bus
                mileageSum = true;
            return (date && busNumber && mileageSum);
        }


        //-----------------------------------------------------
        //main events:
        //-----------------------------------------------------

        /// <summary>
        /// Happening when the user insert the month of the creating date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (createMonth.SelectedItem != null)//after the user chose the month he will be able to chose the day
                createDay.IsEnabled = true;
            dayAccordingToMonth((int)(createMonth.SelectedItem),createDay);//the days according to the month..
            if (ableToClickButton())
                finishButton.IsEnabled = true;
        }
        

        /// <summary>
        /// Happening when the user insert the month of the care date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void careMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)//after the user chose the month he will be able to chose the day
                careDay.IsEnabled = true;
            dayAccordingToMonth((int)createMonth.SelectedItem, careDay);//the days according to the month..
        }


        /// <summary>
        /// Happening when the user click on the button to create the bus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBus_Click(object sender, RoutedEventArgs e)
        {
            bool closeWindow = true;//the window will be close automatic just if all the values will be valid
            validBus = closeWindow; //if the window will be close the bus will be valid
            try
            {
                //create date:
                DateTime createDate = new DateTime((int)createYear.SelectedItem, (int)createMonth.SelectedItem, (int)createDay.SelectedItem);
                string busNumber = plateNumber.Text;
                int mileageSum = int.Parse(mileage.Text);
                DateTime lastCare = DateTime.Now;
                //create new bus with the create date and the bus line number that the user insert
                //auther data with default values:
                if (betweenCities.IsChecked==true)
                    myBus = new Bus(busNumber, createDate, lastCare,false);
                else
                    myBus = new Bus(busNumber, createDate, lastCare);
                myBus.SumOfKilometers = mileageSum;//update the mileage that the user insert
                foreach (Bus bus in buses)
                    if (myBus.BusNumber == bus.BusNumber)
                        throw new invalidInputException("מספר רישוי זהה כבר קיים במערכת");
                //if the user insert last care date and the mileage from the last care update the suitable field in the bus: 
                if (careDay.SelectedItem != null && careMonth.SelectedItem != null && careYear.SelectedItem != null)
                {
                    //if the user insert the mileage from last care, update the suitable fieldin the bus: 
                    if (mileageFromCare.Text != "")
                    {
                        lastCare = new DateTime((int)careYear.SelectedItem, (int)careMonth.SelectedItem, (int)careDay.SelectedItem);
                        myBus.LastCareDate = lastCare;
                        myBus.KilloFromLastCare = int.Parse(mileageFromCare.Text);
                    }
                    else
                    {
                        MessageBox.Show("לא הוכנס קילומטראז' מהטיפול האחרון, האוטובוס יעבור טיפול חדש היום","מידע", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                //if the user insert the fual, update the suitable fieldin the bus: 
                if (fuel.Text != "")
                {
                    myBus.KilometersWithFuel = int.Parse(fuel.Text);
                }
            }
            catch (invalidInputException ex)
            {
                MessageBox.Show(ex.Message,"שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                closeWindow = false;
            }
            catch (FormatException)
            {
                MessageBox.Show("הכנסת ערך שאינו מספרי","שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (closeWindow)
            {
                buses.Add(myBus);
                this.Close();//close the window
            }
        }

        //---------------------------------------------------------
        //events for make sure that the user will be able to click on the button just if he fill all the fields that necessary
        //---------------------------------------------------------        
        private void createDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ableToClickButton())
                finishButton.IsEnabled = true;
            else
                finishButton.IsEnabled = false;
        }

        private void createYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ableToClickButton())
                finishButton.IsEnabled = true;
            else
                finishButton.IsEnabled = false;
            if ((int)createYear.SelectedItem >= 2018)
                plateNumber.MaxLength = 8;
            else
                plateNumber.MaxLength = 7;
        }

        private void mileage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ableToClickButton())
                finishButton.IsEnabled = true;
            else
                finishButton.IsEnabled = false;
        }

        private void plateNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (createYear.SelectedItem!=null && (int)createYear.SelectedItem>=2018)
            //{
            //    if (plateNumber.Text.Length == 3 || plateNumber.Text.Length == 6)
            //        plateNumber.Text = plateNumber.Text + '-';
            //}
            //else
            //{
            //    if (plateNumber.Text.Length == 2 || plateNumber.Text.Length == 6)
            //        plateNumber.Text += '-';
            //}
            if (ableToClickButton())
                finishButton.IsEnabled = true;
            else
                finishButton.IsEnabled = false;
        }

       //------------------------------------------------------
       // to make sure that the input is only numbers:
       //------------------------------------------------------
        private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            if (e.Key < Key.NumPad0 || e.Key > Key.NumPad9)
            {
                //allow get out of the text box
                if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                    return;

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

    }
}
