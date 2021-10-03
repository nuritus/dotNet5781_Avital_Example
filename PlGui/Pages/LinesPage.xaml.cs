using BlApi;
using BO;
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

namespace PlGui.Pages
{
    /// <summary>
    /// Interaction logic for LinesPage.xaml
    /// </summary>
    public partial class LinesPage : Page
    {

        private ObservableCollection<PO.BusLine> allLines = new ObservableCollection<PO.BusLine>();
        private ObservableCollection<PO.BusLine> JeruLines = new ObservableCollection<PO.BusLine>();
        private ObservableCollection<PO.BusLine> cenLines = new ObservableCollection<PO.BusLine>();
        private ObservableCollection<PO.BusLine> norLines = new ObservableCollection<PO.BusLine>();
        private ObservableCollection<PO.BusLine> souLines = new ObservableCollection<PO.BusLine>();
        private ObservableCollection<PO.BusLine> generLines = new ObservableCollection<PO.BusLine>();
        AddLineWindow addLineWindow;
        ActionOnSpecificLineWindow actionOnSpecificLineWindow;
        IBl bl;
        public LinesPage()
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            BusLines.Visibility = Visibility.Visible;
            displyComboBox.SelectedIndex = 0;
            foreach (BO.BusLine line in bl.GetAllBusLines())
                allLines.Add(line.CopyLine());
            BusLines.DataContext = allLines;
        }

        private void ActionOnSpecificLineWindow_UpdateLineNumber(object sender, EventArgs e)
        {//sort the list after update a line number
            BusLines.ItemsSource = allLines.OrderBy(x => x.LineNumber);
            updateListsByArea();
        }

        private void ShowLines_Click(object sender, RoutedEventArgs e)
        {
            linesByArea.Visibility = Visibility.Hidden;
            BusLines.Visibility = Visibility.Visible;
            allLines.Clear();
            foreach (BO.BusLine line in bl.GetAllBusLines())
                allLines.Add(line.CopyLine());
            BusLines.DataContext = allLines;
        }

        private void getTheListsByAreas()
        {
            BusLines.Visibility = Visibility.Hidden;
            linesByArea.Visibility = Visibility.Visible;
            updateListsByArea();
        }

        private void updateListsByArea()
        {
            JeruLines.Clear();
            cenLines.Clear();
            souLines.Clear();
            norLines.Clear();
            generLines.Clear();
            foreach (var group in bl.GetLinesByArea())
            {
                if (group.Key == BO.Area.Jerusalem)
                    foreach (var line in group)
                        JeruLines.Add(line.CopyLine());
                if (group.Key == BO.Area.Center)
                    foreach (var line in group)
                        cenLines.Add(line.CopyLine());
                if (group.Key == BO.Area.North)
                    foreach (var line in group)
                        norLines.Add(line.CopyLine());
                if (group.Key == BO.Area.South)
                    foreach (var line in group)
                        souLines.Add(line.CopyLine());
                if (group.Key == BO.Area.General)
                    foreach (var line in group)
                        generLines.Add(line.CopyLine());
            }
            JerusalemLines.DataContext = JeruLines;
            centerLines.DataContext = cenLines;
            northLines.DataContext = norLines;
            southLines.DataContext = souLines;
            generalLines.DataContext = generLines;
        }

        private void allLines_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            actionOnSpecificLineWindow = new ActionOnSpecificLineWindow((sender as ListView).SelectedItem as PO.BusLine);
            actionOnSpecificLineWindow.UpdateLineNumber += ActionOnSpecificLineWindow_UpdateLineNumber;
            actionOnSpecificLineWindow.ShowDialog();
        }

        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            //open a window to add details of a new line..
            addLineWindow = new AddLineWindow(bl, addLineToList);
            addLineWindow.ShowDialog();
        }

        private void addLineToList()
        {
            //function to sent to the add window- add the station to the list
            allLines.Clear();
            foreach (BO.BusLine line in bl.GetAllBusLines())
                allLines.Add(line.CopyLine());
            updateListsByArea();
        }

        private void displayLines_SlectedChanged(object sender, SelectionChangedEventArgs e)
        {
            //select the display of the lists according the choice of the user..
            var comboBoxItem = (sender as ComboBox).Items[(sender as ComboBox).SelectedIndex] as ComboBoxItem;
            if (comboBoxItem.Content.ToString() == "רשימת קווים מלאה")
            {
                BusLines.Visibility = Visibility.Visible;
                linesByArea.Visibility = Visibility.Collapsed;
            }
            else
            {
                BusLines.Visibility = Visibility.Collapsed;
                linesByArea.Visibility = Visibility.Visible;
                getTheListsByAreas();
            }
        }

        private void deleteLineButton_Click(object sender, RoutedEventArgs e)
        {
            PO.BusLine lineToDelete = ((sender as Button).DataContext) as PO.BusLine;
            //delete the line from the list (and from the ds)

            //ask the user if he sure that he want to delete the station
            var result = MessageBox.Show("האם אתה בטוח שברצונך למחוק קו זה?", "אישור פעולה", MessageBoxButton.YesNo, MessageBoxImage.Question,
                MessageBoxResult.Yes, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            if (result == MessageBoxResult.Yes)
            {
                //try to delete the line:
                bl.DeleteBusLine(lineToDelete.BusLineIndentificator);
                allLines.Remove(lineToDelete);
            }
        }
    }
}