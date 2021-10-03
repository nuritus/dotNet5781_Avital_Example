using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PlGui
{
    static public class commonEvents
    {
        internal static void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
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

        internal static void TextBox_OnlyDoubleNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int result;
            //enable to add not more then 1 dot in suitable place..
            if (int.TryParse((sender as TextBox).Text, out result))
            {
                //check the languege of the keybourd and according the languege enable to insert "."
                string language = InputLanguageManager.Current.CurrentInputLanguage.Name;
                if (language == "he-IL")
                {
                    if (e.Key != Key.Decimal && e.Key != Key.OemQuestion)
                        commonEvents.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
                }
                if (language == "en-US")
                {
                    if (e.Key != Key.Decimal && e.Key != Key.OemPeriod)
                        commonEvents.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
                }
                
            }
            else
                commonEvents.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
        }
    }
}
