using Microsoft.Reporting.WinForms;
using SpootLight.Controllers;
using SpootLight.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace SpootLight.Repporting
{
    /// <summary>
    /// Logique d'interaction pour ReportUserControl.xaml
    /// </summary>
    public partial class ReportUserControl : UserControl
    {
        readonly SpotLightDATAEntities crudctx = new SpotLightDATAEntities();
        private List<FT_COREP_CA> FT;
        private static List<FT_COREP_CA> listFT;
        public Type type;
        public static SqlConnection con = Globals.con;
        public static SqlCommand cmd;
        public string JobDescription;
        public ReportUserControl()
        {
            InitializeComponent();
            initial();
        }
        public void initial()
        {
            getJobDescription();
            load_data("FT_COREP_CR", G_Reporting);
        }
        public void getJobDescription()
        {
            JobDescription = JobTalendController.getJobData(Jobs.COREPCRSA.Value, "description");
            Console.WriteLine("my job :" + JobDescription);
            DescriptionJob.Content = JobDescription;
        }
        private void RunJob_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JobTalendController.runJob2Finale(Jobs.COREPCRSA.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : \n \n " + ex.Message + " \n \n " + ex.StackTrace);
            }
            initial();
        }
        public static void load_data(string table, RadGridView g)
        {
            DataTable dt = new DataTable();
            SqlDataReader dr;
            dt.Clear();
            cmd = new SqlCommand("SELECT * FROM " + table, con);
            con.Open();
            dr = cmd.ExecuteReader();
            dt.Load(dr);
            g.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newWindowWidth = e.NewSize.Width;
            LoadedLines.Width = (newWindowWidth - 50) / 2;
            CancledLines.Width = (newWindowWidth - 50) / 2;
        }
    }




}
