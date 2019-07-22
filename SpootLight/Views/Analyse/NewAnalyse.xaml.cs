using SpootLight.Models;
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

namespace SpootLight.Views.Analyse
{
    /// <summary>
    /// Logique d'interaction pour NewAnalyse.xaml
    /// </summary>
    public partial class NewAnalyse : Window
    {
        
        public NewAnalyse()
        {
            InitializeComponent();
        }
        public void initial()
        {
            //Entity.ItemsSource = analysemodel.getEntity();
        }

        private void AddAnalyse_Click(object sender, RoutedEventArgs e)
        {
            AnalyseModel ctx = new AnalyseModel();
            analyse bac = new analyse();
            Random r = new Random(12000);

            DescriptionEntity.SelectAll();
            bac.id = r.Next();
            bac.entity_id = Entity.SelectedValue.ToString();
            bac.user_id = 2;
            bac.date_analyse = DateEntity.SelectedDate;
            bac.Description = DescriptionEntity.Selection.Text;
            bac.version = VersionEntity.Text;

            ctx.Insert(bac);
        }

        private void CancelAnalyse_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
    public static class CollectionData
    {
        public static Dictionary<string, string> GetChoices()
        {

            return AnalyseModel.getEntity();


        }
    }
}
