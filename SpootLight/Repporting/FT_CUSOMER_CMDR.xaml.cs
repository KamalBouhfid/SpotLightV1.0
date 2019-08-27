using SpootLight.Controllers;
using SpootLight.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace SpootLight.Repporting
{
    /// <summary>
    /// Logique d'interaction pour FT_CUSOMER_CMDR.xaml
    /// </summary>
    public partial class FT_CUSOMER_CMDR : UserControl
    {
        public static SqlConnection con = Globals.con;
        public static SqlCommand cmd;
        public string JobDescription;
        public FT_CUSOMER_CMDR()
        {
            InitializeComponent();
            initial();
        }
        public void initial()
        {
            getJobDescription();
            load_data("FT_CUSOMER_CMDR", G_Reporting);
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
        public void getJobDescription()
        {
            JobDescription = JobTalendController.getJobData(Jobs.ClientElgCMDR.Value, "description");
            Console.WriteLine("my job :" + JobDescription);
            DescriptionJob.Content = JobDescription;
        }
        private void RunJob_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JobTalendController.runJob2Finale(Jobs.ClientElgCMDR.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : \n \n " + ex.Message + " \n \n " + ex.StackTrace);
            }
            initial();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newWindowWidth = e.NewSize.Width;
            LoadedLines.Width = (newWindowWidth - 50) / 2;
            CancledLines.Width = (newWindowWidth - 50) / 2;
        }
    }
}
