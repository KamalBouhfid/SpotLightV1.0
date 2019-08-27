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

namespace SpootLight.Views.PR
{
    /// <summary>
    /// Logique d'interaction pour Form_PR_INTERNAL_SEGMENT.xaml
    /// </summary>
    public partial class Form_PR_INTERNAL_SEGMENT : Window
    {
        FormController formcontroller = new FormController();
        public Form_PR_INTERNAL_SEGMENT()
        {
            InitializeComponent();
            BankCode.Text = Globals.getAnalyse()[1];
            ProcessDate.Text = Globals.getAnalyse()[3];
            Version.Text = Globals.getAnalyse()[5];
        }
        private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }
        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (formcontroller.CheckNotNull(Internal_Segment) && formcontroller.CheckNotNull(Customer_Market) &&
                                formcontroller.CheckNotNull(Sub_Category))
                {

                    PR_INTERNAL_SEGMENT_MODEL ctx = new PR_INTERNAL_SEGMENT_MODEL();
                    PR_INTERNAL_SEGMENT bac = new PR_INTERNAL_SEGMENT();

                    bac.Bank_Code = Globals.getAnalyse()[1];
                    bac.Process_Date = Globals.getAnalyse()[3];
                    bac.Version = Globals.getAnalyse()[5];
                    bac.Internal_Segment = Internal_Segment.Text;
                    bac.Internal_Segment_Description = Internal_Segment_Description.Text;
                    bac.Customer_Market = Customer_Market.Text;
                    bac.Sub_Category = Sub_Category.Text;

                    ctx.InsertOrUpdate(bac);
                    this.Close();
                }
                else
                    MessageBox.Show("Veuillez remplir les champs obligatoires", "Format Incorrecte", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : \n \n " + ex.Message + " \n \n " + ex.StackTrace);
            }
            
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
