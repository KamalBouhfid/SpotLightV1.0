using SpootLight.Controllers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpootLight.Views.Declarations.COREP.CMDR
{
    /// <summary>
    /// Logique d'interaction pour UserControl339.xaml
    /// </summary>
    public partial class UserControl339 : UserControl
    {
        public string JobDescription = "";
        public string JobExcelPath = "";
        public UserControl339()
        {
            InitializeComponent();
            getJobDescription();
        }
        public void getJobDescription()
        {
            JobDescription = JobTalendController.getJobData(Jobs.CMDR339Job.Value, "description");
            Console.WriteLine("my job :" + JobDescription);
            DescriptionJob.Content = JobDescription;
        }
        private void RunJob_Click(object sender, RoutedEventArgs e)
        {
            JobTalendController.runJob2Finale(Jobs.CMDR339Job.Value);
            ShowExcel(JobExcelPath);
        }
        public void ShowExcel(string FilePath)
        {
            FilePath = JobTalendController.getJobData(Jobs.CMDR339Job.Value, "excel_path");
            if (FilePath.Equals(null) || FilePath.Equals(""))
            {
                MessageBox.Show("Le fichier Demandé n'est pas trouvée ");
            }
            else
            {
                try
                {
                    Sheet339.Load(FilePath + Jobs.CMDR339File.Value);
                    Sheet339.Visibility = Visibility.Visible;
                    ExcelRun.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur dans la lecture de fichier : \n \n  " + FilePath + " \n \n " + ex.Message + " \n " + ex.StackTrace);
                }
            }
        }

        private void ExcelRun_Click(object sender, RoutedEventArgs e)
        {
            JobExcelPath = JobTalendController.getJobData(Jobs.CMDR339Job.Value, "excel_path");
            if (JobExcelPath.Equals(null) || JobExcelPath.Equals(""))
            {
                MessageBox.Show("Le fichier Demandé n'est pas trouvée ");
            }
            else
            {
                System.Diagnostics.Process.Start(JobExcelPath + Jobs.CMDR339File.Value);

            }
        }
    }
}
