using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace SpootLight.Controllers
{
    class FormController
    {
        public bool CheckNoString(TextBox text)
        {
            Regex RX = new Regex("^-?\\d*(\\.\\d+)?$");
            Match match = RX.Match(text.Text.Replace(",", "."));
            if(text.Text.Equals("") || text.Text.Equals(null))
            {
                text.BorderBrush = Brushes.Transparent;
            }
            if (match.Success)
            {
                text.BorderBrush = Brushes.Green;
                text.ToolTip = "Format valide !";
                return true;
            }
            else
            {
                text.BorderBrush = Brushes.Red;
                text.ToolTip = "Format invalide !";
                return false;
            }
        }
        public bool CheckNotNull(TextBox text)
        {
            if (text.Text != null && text.Text != "")
            {
                text.BorderBrush = Brushes.Green;
                text.ToolTip = "Format valide !";
                return true;
            }
            else
            {
                text.BorderBrush = Brushes.Red;
                text.ToolTip = "Format invalide !";
                return false;
            }
        }
        public bool CheckEntityNotNull(ComboBox text)
        {
            if (text.Text != null && text.Text != "")
            {
                text.BorderBrush = Brushes.Green;
                text.ToolTip = "Format valide !";
                return true;
            }
            else
            {
                text.BorderBrush = Brushes.Red;
                text.ToolTip = "Format invalide !";
                return false;
            }
        }
        public bool CheckDateNotNull(DatePicker text)
        {
            if (text.Text != null && text.Text != "")
            {
                text.BorderBrush = Brushes.Green;
                text.ToolTip = "Format valide !";
                return true;
            }
            else
            {
                text.BorderBrush = Brushes.Red;
                text.ToolTip = "Format invalide !";
                return false;
            }
        }
    }
}
