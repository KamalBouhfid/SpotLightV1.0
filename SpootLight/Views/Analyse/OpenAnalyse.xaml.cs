using SpootLight.Models;
using SpootLight.Views.PR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Logique d'interaction pour OpenAnalyse.xaml
    /// </summary>
    public partial class OpenAnalyse : Window
    {
        static AnalyseModel AnalyseLists = new AnalyseModel();
        
        private static List<analyse> analyses = AnalyseLists.Analyses;

        private List<string> analyseSession = new List<string>();
        public List<string> user = new List<string>();
        private  string UserLogin = Globals.getUser()[0];
        private  string UserEmail = Globals.getUser()[1];
        private static string UserId = Globals.getUser()[0];
        IList<analyse> myList;

        public OpenAnalyse()
        {
            InitializeComponent();
            initial();
        }
        public void initial()
        {
            AnalyseModel.load_analyse(G_Analyses);
        }

        private void Consulter_Click(object sender, RoutedEventArgs e)
        {
                //radBusyIndicator.IsBusy = true;
                OuvrirAnalyse();
        }
        public void setAnalyse(List<string> analyseSession)
        {
            this.analyseSession = analyseSession;
        }
        public List<string> getAnalyse()
        {
            return analyseSession;
        }

        private void NewAnalyse_Click(object sender, RoutedEventArgs e)
        {
            NewAnalyse n = new NewAnalyse();
            n.Show();
            n.Closed += delegate
            {
                initial();
            };
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public  void setUser(List<string> user)
        {
            this.user = user;
        }
        public  List<string> getUser()
        {
            return user;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
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

        private void G_Analyses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OuvrirAnalyse();
        }
        public void OuvrirAnalyse()
        {
            try
            {
                string id = (G_Analyses.SelectedItem as analyse).id.ToString();
                string entity_id = (G_Analyses.SelectedItem as analyse).entity_id.ToString();
                string user_id = (G_Analyses.SelectedItem as analyse).user_id.ToString();
                string date_analyse = (G_Analyses.SelectedItem as analyse).date_analyse.ToString();
                DateTime dt = Convert.ToDateTime(date_analyse);
                string Description = (G_Analyses.SelectedItem as analyse).Description.ToString();
                string version = (G_Analyses.SelectedItem as analyse).version.ToString();
                analyseSession.Add(id);
                analyseSession.Add(entity_id);
                analyseSession.Add(user_id);
                analyseSession.Add(dt.ToString("yyyy/MM/dd").Replace("/", "-"));
                analyseSession.Add(Description);
                analyseSession.Add(version);
                Globals.setAnalyse(analyseSession);
                Console.WriteLine("0000000000000000000000000"+dt.ToString("yyyy/MM/dd").Replace("/", "-"));
                //Globals.DataCopy(entity_id, dt.ToString("yyyy/MM/dd").Replace("/", "-"), "SABA004", "2018-12-04");
                View_PR_ACCOUNT p = new View_PR_ACCOUNT();
                //p.BankCode = entity_id;
                //p.dateArrete = dt.ToString("yyyy/MM/dd").Replace("/", "-");
                //p.version = version;
                p.setAnalyse(analyseSession);
                //View_PR_ACCOUNT.HeaderInfo.Add(entity_id);
                //View_PR_ACCOUNT.HeaderInfo.Add(date_analyse);
                //View_PR_ACCOUNT.HeaderInfo.Add(version);
                p.Show();
                this.Close();
            }
            catch (NullReferenceException )
            {
                MessageBox.Show("Veuillez Sélectionner une Analyse", " Aucun ligne sélectioné", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.StackTrace + " \n\n\n\n" +Ex.Message, " Aucun ligne sélectioné",MessageBoxButton.OK,MessageBoxImage.Stop);
            }
        }
        public void Logout()
        {
            UserId = "";
            UserLogin = "";
            UserEmail = "";
            System.Windows.Application.Current.Shutdown();
            System.Windows.Forms.Application.Restart();
        }

        private void G_Analyses_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                NewAnalyse n = new NewAnalyse();
                n.Show();
                string entity_id = (G_Analyses.SelectedItem as analyse).entity_id.ToString();
                string date_analyse = (G_Analyses.SelectedItem as analyse).date_analyse.ToString();
                string version = (G_Analyses.SelectedItem as analyse).version.ToString();
                n.Entity.Visibility = Visibility.Hidden;

                n.EntityName.Visibility = Visibility.Visible;
                n.EntityName.Content = entity_id;

                n.DateEntity.Visibility = Visibility.Hidden;
                n.ProcessDateName.Visibility = Visibility.Visible;
                n.ProcessDateName.Content = date_analyse;

                n.CopyEntity.Visibility = Visibility.Hidden;
                n.Datacopy.Visibility = Visibility.Hidden;
                n.CopieLabel.Visibility = Visibility.Hidden;
                n.CopieLabelName.Visibility = Visibility.Hidden;
                List<string> list = new List<string>();
                list = AnalyseModel.getDataEntityAnalyse(entity_id, date_analyse,version);
                Console.WriteLine("that y !" + list[0]);
                n.TypeAnalyse.SelectedValue = list[4];
                n.DescriptionEntity.Text = list[2];
                n.Height = 300;
                n.MinHeight = 300;

                n.TitleWin.Content = "Modifier Analyse";
                n.setIsedit(true);
                n.Closed += delegate {
                    initial();
                };
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Veuillez Sélectionner une ligne ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : \n \n " + ex.Message + " \n \n " + ex.StackTrace);
            }
        }
    }
}
