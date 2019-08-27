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

namespace SpootLight.Views.Declarations.COREP.CR.SA
{
    /// <summary>
    /// Logique d'interaction pour SA_UserControl.xaml
    /// </summary>
    public partial class SA_UserControl : UserControl
    {
        public string JobDescription = "";
        public string JobExcelPath = "";
        public SA_UserControl()
        {
            InitializeComponent();
            getJobDescription();
        }
        public void getJobDescription()
        {
            JobDescription = JobTalendController.getJobData(Jobs.CR_SAJob.Value, "description");
            Console.WriteLine("my job :" + JobDescription);
            DescriptionJob.Content = JobDescription;
        }
        private void RunJob_Click(object sender, RoutedEventArgs e)
        {
            JobTalendController.runJob2Finale(Jobs.CR_SAJob.Value);
            ShowExcel(JobExcelPath);
        }
        public void ShowExcel(string FilePath)
        {
            FilePath = JobTalendController.getJobData(Jobs.CR_SAJob.Value, "excel_path");
            if (FilePath.Equals(null) || FilePath.Equals(""))
            {
                MessageBox.Show("Le fichier Demandé n'est pas trouvée ");
            }
            else
            {
                try
                {
                    SheetSV.Load(FilePath + Jobs.CR_SASVFile.Value);
                    SheetEC.Load(FilePath + Jobs.CR_SAECFile.Value);
                    SheetEN.Load(FilePath + Jobs.CR_SAENFile.Value);
                    SheetCL.Load(FilePath + Jobs.CR_SACLFile.Value);

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
            JobExcelPath = JobTalendController.getJobData(Jobs.CR_SAJob.Value, "excel_path");
            if (JobExcelPath.Equals(null) || JobExcelPath.Equals(""))
            {
                MessageBox.Show("Le fichier Demandé n'est pas trouvée ");
            }
            else
            {
                if (SouverainTab.IsSelected)
                    System.Diagnostics.Process.Start(JobExcelPath + Jobs.CR_SASVFile.Value);
                else if (EtablissementTab.IsSelected)
                    System.Diagnostics.Process.Start(JobExcelPath + Jobs.CR_SAECFile.Value);
                else if (EntrepriseTab.IsSelected)
                    System.Diagnostics.Process.Start(JobExcelPath + Jobs.CR_SAENFile.Value);
                else if (ClienteleTab.IsSelected)
                    System.Diagnostics.Process.Start(JobExcelPath + Jobs.CR_SACLFile.Value);
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newWindowWidth = e.NewSize.Width;
            SouverainTab.Width = (newWindowWidth - 50) / 4;
            EtablissementTab.Width = (newWindowWidth - 50) / 4;
            EntrepriseTab.Width = (newWindowWidth - 50) / 4;
            ClienteleTab.Width = (newWindowWidth - 50) / 4;
        }
    }
}
