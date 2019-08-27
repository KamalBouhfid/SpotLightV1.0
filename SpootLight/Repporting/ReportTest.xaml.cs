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

namespace SpootLight.Repporting
{
    /// <summary>
    /// Logique d'interaction pour ReportTest.xaml
    /// </summary>
    public partial class ReportTest : Window
    {
        public ReportTest()
        {
            InitializeComponent();
            ReportUserControl r = new ReportUserControl();
            ReportCall.Children.Add(r);
        }
    }
}
