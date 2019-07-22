using SpootLight.Views.PR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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

namespace SpootLight.Controllers
{
    /// <summary>
    /// Logique d'interaction pour JobTalend.xaml
    /// </summary>
    public partial class JobTalend : Window
    {

        public JobTalend()
        {
            InitializeComponent();
            JobTalendController.initial(trvMenu);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            JobTalendController.Jobs_choix(stack, sender,  ResultJob,  ResultJobErrors,
             NomJobResult,  CheminJobResult,  LancerJobBtn);

        }


        private void LancerJobBtn_Click(object sender, RoutedEventArgs e)
        {
            JobTalendController.runJob2(sender, ResultJob, ResultJobErrors, NomJobResult, CheminJobResult, LancerJobBtn);
        }
    }

    public class MenuItem
    {
        public MenuItem()
        {
            this.Items = new ObservableCollection<MenuItem>();
        }

        public string Title { get; set; }

        public ObservableCollection<MenuItem> Items { get; set; }
    }
}
