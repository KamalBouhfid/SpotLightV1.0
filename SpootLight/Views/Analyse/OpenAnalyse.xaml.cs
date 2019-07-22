using SpootLight.Models;
using SpootLight.Views.PR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Logique d'interaction pour OpenAnalyse.xaml
    /// </summary>
    public partial class OpenAnalyse : Window
    {
        static AnalyseModel AnalyseLists = new AnalyseModel();
        
        private static List<analyse> analyses = AnalyseLists.Analyses;

        private List<string> analyseSession = new List<string>();
        public List<string> user = new List<string>();
        private  string UserLogin = Globals.getUser()[1];
        private  string UserEmail = Globals.getUser()[2];
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
            Console.WriteLine("user   yyoo " + UserId);
        }

        private void Consulter_Click(object sender, RoutedEventArgs e)
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
            View_PR_ACCOUNT p = new View_PR_ACCOUNT();
            p.setAnalyse(analyseSession);
            p.Show();
            this.Close();
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
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            UserId = "";
            UserLogin = "";
            UserEmail = "";
            System.Windows.Application.Current.Shutdown();
            System.Windows.Forms.Application.Restart();
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
            UserId = "";
            UserLogin = "";
            UserEmail = "";
            System.Windows.Application.Current.Shutdown();
            System.Windows.Forms.Application.Restart();
        }
    }
}
