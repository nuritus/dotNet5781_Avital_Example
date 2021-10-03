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

namespace PlGui.userControls
{
    /// <summary>
    /// Interaction logic for TimeTextBox.xaml
    /// </summary>
    public partial class TimeTextBox : UserControl
    {
        //the property of the TimeTextBox
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        //the propery of the "TextBox"..
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TimeTextBox));

        public TimeTextBox()
        {
            InitializeComponent();
            timeTextBox.DataContext = this;
        }

        private void timeTextBox_previewKeyDown(object sender, KeyEventArgs e)
        {
            //the user can't be able to delete the text (for be sure that he will not delete the ':' )
            if (timeTextBox.SelectionLength != 0 || e.Key == Key.Back ||e.Key==Key.Delete)
            {
                e.Handled = true;
                return;
            }
            //the user will be able to move in textBox with the keys right and left
            if (e.Key == Key.Left || e.Key == Key.Right)
            {
                return;
            }
            //the user will be able to insert only numbers
            commonEvents.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
            {
                if (e.Handled == true)
                    return;
            }
            //press on the ":" char:
            if (timeTextBox.SelectionStart == 2)
            {
                timeTextBox.SelectionStart = 3;
            }
            if (timeTextBox.SelectionStart == 5)
            {
                timeTextBox.SelectionStart = 6;
            }
            if (timeTextBox.SelectionStart == 8)
            {
                timeTextBox.SelectionStart = 0;
            }
                timeTextBox.SelectionLength = 1;

        }
    }
}
