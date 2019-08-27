using SpootLight.Controllers;
using SpootLight.Models;
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
using System.Windows.Shapes;

namespace SpootLight.Popup
{
    /// <summary>
    /// Logique d'interaction pour ImportChoser.xaml
    /// </summary>
    public partial class ImportChoser : Window
    {
        public string JobName = "";
        public string JobNameCrush = "";
        public ImportChoser()
        {
            InitializeComponent();
        }
        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            JobTalendController.ImporterJob(FilePath, JobName);
        }
        private void Import_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(FilePath.Text.Equals("") || FilePath.Text.Equals(null))
                {
                    MessageBox.Show("Veuillez choisir un fichier");
                }
                else
                {
                    if ((bool)EcraseData.IsChecked)
                    {
                        //MessageBox.Show("ecraser ! avec " + JobNameCrush + " -- " + FilePath.Text);
                        JobTalendController.editFile(FilePath.Text, JobNameCrush);
                        JobTalendController.runJob2Finale(JobNameCrush);
                        this.Close();

                    }
                    else
                    {
                        //MessageBox.Show("non ecraser ! avec " + JobName + " -- " + FilePath.Text);
                        JobTalendController.editFile(FilePath.Text, JobName);
                        JobTalendController.runJob2Finale(JobName);
                        this.Close();
                    }
                }
            }catch(Exception ex)
            {
                //MessageBox.Show(ex.Message + ex.StackTrace);
                MessageBox.Show("Echec d'éxcution");
            }
            
        }
    }
}
