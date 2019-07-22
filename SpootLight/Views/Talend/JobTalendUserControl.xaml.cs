using SpootLight.Controllers;
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

namespace SpootLight.Views.Talend
{
    /// <summary>
    /// Logique d'interaction pour JobTalendUserControl.xaml
    /// </summary>
    public partial class JobTalendUserControl : UserControl
    {
        public JobTalendUserControl()
        {
            InitializeComponent();
            initial();
        }
        public void initial()
        {
            JobTalendController.initial(trvMenu);
        }
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            JobTalendController.Jobs_choix(stack, sender, ResultJob, ResultJobErrors,
             NomJobResult, CheminJobResult, LancerJobBtn);

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
