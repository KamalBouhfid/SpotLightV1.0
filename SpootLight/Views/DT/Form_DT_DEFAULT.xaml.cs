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

namespace SpootLight.Views.DT
{
    /// <summary>
    /// Logique d'interaction pour Form_DT_DEFAULT.xaml
    /// </summary>
    public partial class Form_DT_DEFAULT : Window
    {
        FormController formcontroller = new FormController();
        public Form_DT_DEFAULT()
        {
            InitializeComponent();
            BankCode.Text = Globals.getAnalyse()[1];
            ProcessDate.Text = Globals.getAnalyse()[3];
            Version.Text = Globals.getAnalyse()[5];
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (formcontroller.CheckNoString(Default_Amount_in_local_currency)
                && formcontroller.CheckNotNull(Customer_Code))
                {
                    DT_DEFAULT_MODEL ctx = new DT_DEFAULT_MODEL();


                    DT_DEFAULT acc = new DT_DEFAULT();

                    acc.Bank_Code = Globals.getAnalyse()[1];
                    acc.Process_Date = Globals.getAnalyse()[3];
                    acc.Version = Version.Text;
                    acc.Customer_Code = Customer_Code.Text;
                    acc.Rating = Rating.Text;

                    string DefaultAmountInLocalCurrency = Default_Amount_in_local_currency.Text;

                    Nullable<float> f;

                    if (DefaultAmountInLocalCurrency == null || DefaultAmountInLocalCurrency.Equals("")) f = null;
                    else f = (float)Convert.ToDecimal(Default_Amount_in_local_currency.Text.Replace(".", ","));
                    acc.Default_Amount_in_local_currency = f;

                    ctx.InsertOrUpdate(acc);
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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }

        private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
