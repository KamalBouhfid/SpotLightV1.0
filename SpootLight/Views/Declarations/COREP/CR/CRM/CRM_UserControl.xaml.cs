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

namespace SpootLight.Views.Declarations.COREP.CR.CRM
{
    /// <summary>
    /// Logique d'interaction pour CRM_UserControl.xaml
    /// </summary>
    public partial class CRM_UserControl : UserControl
    {
        public string JobDescription = "";
        public string JobExcelPath = "";
        static JobTalendController Job = new JobTalendController();
        public CRM_UserControl()
        {
            InitializeComponent();
            getJobDescription();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newWindowWidth = e.NewSize.Width;
            SouverainTab.Width = (newWindowWidth - 50) / 4;
            EtablissementTab.Width = (newWindowWidth - 50) / 4;
            EntrepriseTab.Width = (newWindowWidth - 50) / 4;
            ClienteleTab.Width = (newWindowWidth - 50) / 4;
        }

        public void getJobDescription()
        {
            JobDescription = JobTalendController.getJobData(Jobs.CRMJob.Value, "description");
            Console.WriteLine("my job :" + JobDescription);
            DescriptionJob.Content = JobDescription;
        }
        private void RunJob_Click(object sender, RoutedEventArgs e)
        {
            JobTalendController.runJob2Finale(Jobs.CRMJob.Value);
            ShowExcel(JobExcelPath);
        }
        public void ShowExcel(string FilePath)
        {
            FilePath = JobTalendController.getJobData(Jobs.CRMJob.Value, "excel_path");
            if (FilePath.Equals(null) || FilePath.Equals(""))
            {
                MessageBox.Show("Le fichier Demandé n'est pas trouvée ");
            }
            else
            {
                try
                {
                    SheetSV.Load(FilePath+Jobs.CRMSVFile.Value);
                    SheetEC.Load(FilePath + Jobs.CRMECFile.Value);
                    SheetEN.Load(FilePath + Jobs.CRMENFile.Value);
                    SheetCL.Load(FilePath + Jobs.CRMCLFile.Value);

                    SheetSV.Visibility = Visibility.Visible;
                    SheetEC.Visibility = Visibility.Visible;
                    SheetEN.Visibility = Visibility.Visible;
                    SheetCL.Visibility = Visibility.Visible;
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
            JobExcelPath = JobTalendController.getJobData(Jobs.CRMJob.Value, "excel_path");
            if (JobExcelPath.Equals(null) || JobExcelPath.Equals(""))
            {
                MessageBox.Show("Le fichier Demandé n'est pas trouvée ");
            }
            else
            {
                if(SouverainTab.IsSelected)
                    System.Diagnostics.Process.Start(JobExcelPath+Jobs.CRMSVFile.Value);
                else if (EtablissementTab.IsSelected)
                    System.Diagnostics.Process.Start(JobExcelPath + Jobs.CRMECFile.Value);
                else if (EntrepriseTab.IsSelected)
                    System.Diagnostics.Process.Start(JobExcelPath + Jobs.CRMENFile.Value);
                else if (ClienteleTab.IsSelected)
                    System.Diagnostics.Process.Start(JobExcelPath + Jobs.CRMCLFile.Value);
            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
