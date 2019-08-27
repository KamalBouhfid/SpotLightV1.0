using SpootLight.Controllers;
using SpootLight.Models;
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
using System.Windows.Shapes;

namespace SpootLight.Views.Analyse
{
    /// <summary>
    /// Logique d'interaction pour NewAnalyse.xaml
    /// </summary>
    public partial class NewAnalyse : Window
    {
        private bool isedit = false;
        FormController formcontroller = new FormController();
        public bool getIsdedit()
        {
            return isedit;
        }
        public void setIsedit(bool isedit)
        {
            this.isedit = isedit;
        }
        public NewAnalyse()
        {
            InitializeComponent();
            
        }
        public void initial()
        {

        }

        private void AddAnalyse_Click(object sender, RoutedEventArgs e)
        {
            if (!getIsdedit())
            {
                if (formcontroller.CheckEntityNotNull(Entity) && formcontroller.CheckDateNotNull(DateEntity) &&
                formcontroller.CheckEntityNotNull(TypeAnalyse) && formcontroller.CheckNotNull(DescriptionEntity))
                {
                    AnalyseModel ctx = new AnalyseModel();
                    List<string> entitydata = new List<string>();
                    analyse bac = new analyse();
                    Random r = new Random();
                    int index = Convert.ToInt32(CopyEntity.SelectedValue);
                    string date = ((DateTime)DateEntity.SelectedDate).ToString("yyyy/MM/dd").Replace("/", "-");
                    entitydata.Add(ctx.getEntityAndDateFromID(index)[0]);
                    entitydata.Add(ctx.getEntityAndDateFromID(index)[1]);
                    entitydata.Add(ctx.getEntityAndDateFromID(index)[2]);
                    Console.WriteLine(" yyyyyyyy o  " + entitydata[0] + " \n " + entitydata[1] + "  \n " + Entity.SelectedValue.ToString() + " \n" + DateEntity.SelectedDate.ToString());
                    string version = (ctx.Checkversion(Entity.SelectedValue.ToString(), DateEntity.SelectedDate.ToString(),"")).ToString();

                    Globals.ParamCopy(entitydata[0], entitydata[1], entitydata[2], Entity.SelectedValue.ToString(), date, version);
                    if (Datacopy.IsChecked == true)
                        Globals.DataCopy(entitydata[0], entitydata[1], entitydata[2], Entity.SelectedValue.ToString(), date,version);
                    Globals.InsertAnalyse(r.Next(10000), Entity.SelectedValue.ToString(), Globals.getUser()[0], DateEntity.SelectedDate.ToString(), DescriptionEntity.Text, version, TypeAnalyse.SelectedValue.ToString());
                    this.Close();
                    //AnalyseModel.load_analyse(G_Analyses);
                }
                else
                {
                    MessageBox.Show("Veuillez remplir les champs vides");
                }
            }
            else
            {
                try
                {
                    Globals.EditAnalyse(EntityName.Content.ToString(), ProcessDateName.Content.ToString(), DescriptionEntity.Text, TypeAnalyse.SelectedValue.ToString());
                    this.Close();
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Veuillez insérer les modifications");
                }

            }
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
        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }

        private void DateEntity_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            AnalyseModel ctx = new AnalyseModel();
            string version = (ctx.Checkversion(Entity.SelectedValue.ToString(), DateEntity.SelectedDate.ToString(), "")).ToString();
            VersionEntity.Text = version;
        }
    }
    public static class CollectionData
    {
        public static Dictionary<string, string> GetChoices()
        {
            return AnalyseModel.getEntity();
        }
        public static Dictionary<string, string> GetAnalyse()
        {
            return AnalyseModel.getAnalyse();
        }
        public static Dictionary<string, string> GetTypes()
        {
            return AnalyseModel.getType();
        }
    }
}
