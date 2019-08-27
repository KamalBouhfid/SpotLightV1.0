using SpootLight.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
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
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;

namespace SpootLight.Views.Declarations
{
    /// <summary>
    /// Logique d'interaction pour SA_UserControl.xaml
    /// </summary>
    public partial class SA_UserControl : UserControl
    {
        public string JobDescription = "";
        public string JobExcelPath = "";
        public  XlsxFormatProvider formatProvider = new XlsxFormatProvider();
        public Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf.PdfFormatProvider formatProvider2 = new Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf.PdfFormatProvider();
        public SA_UserControl()
        {
            InitializeComponent();
        }

        public void getJobDescription()
        {
            JobDescription = JobTalendController.getJobData(Jobs.Client.Value, "description");
            Console.WriteLine("my job :" + JobDescription);
            DescriptionJob.Content = JobDescription;
        }
        private void RunJob_Click(object sender, RoutedEventArgs e)
        {
            JobTalendController.runJob2Finale(Jobs.Client.Value);
            ShowExcel(JobExcelPath);
        }
        public void ShowExcel(string FilePath)
        {
            FilePath = JobTalendController.getJobData(Jobs.Client.Value, "excel_path");
            if(FilePath.Equals(null) || FilePath.Equals(""))
            {
                MessageBox.Show("Le fichier Demandé n'est pas trouvée ");
            }else {
                try
                {
                    using (Stream input = new FileStream(FilePath, FileMode.Open))
                    {
                        this.radSpreadsheet.Workbook = formatProvider.Import(input);
                    }
                    radSpreadsheet.Visibility = Visibility.Visible;
                    FormulaBarR.Visibility = Visibility.Visible;
                    ribbonView.Visibility = Visibility.Visible;
                    ExcelRun.Visibility = Visibility.Visible;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Erreur dans la lecture de fichier : \n \n  "+FilePath +" \n \n "+ ex.Message + " \n " + ex.StackTrace);
                }
                

            }
        }

        private void ExcelRun_Click(object sender, RoutedEventArgs e)
        {
            JobExcelPath = JobTalendController.getJobData(Jobs.Client.Value, "excel_path");
            if (JobExcelPath.Equals(null) || JobExcelPath.Equals(""))
            {
                MessageBox.Show("Le fichier Demandé n'est pas trouvée ");
            }
            else
            {
                System.Diagnostics.Process.Start(JobExcelPath);
            }
        }
    }
}
