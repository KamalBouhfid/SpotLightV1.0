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
    /// Logique d'interaction pour Form_DT_PROVISION.xaml
    /// </summary>
    public partial class Form_DT_PROVISION : Window
    {
        FormController formcontroller = new FormController();
        public Form_DT_PROVISION()
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
                if (formcontroller.CheckNoString(Provision_amount_in_currency) &&
                formcontroller.CheckNoString(Provision_amount_in_local_currency)
                && formcontroller.CheckNotNull(Customer_Code)
                && formcontroller.CheckNotNull(Currency_Code) &&
                formcontroller.CheckNotNull(Provision_amount_in_local_currency) && formcontroller.CheckNotNull(Provision_amount_in_currency))
                {
                    DT_PROVISION_MODEL ctx = new DT_PROVISION_MODEL();


                    DT_PROVISION acc = new DT_PROVISION();

                    acc.Bank_Code = Globals.getAnalyse()[1];
                    acc.Process_Date = Globals.getAnalyse()[3];
                    acc.Version = Version.Text;
                    acc.Internal_Provision_Number = Internal_Provision_Number.Text;
                    acc.Currency_Code = Currency_Code.Text;
                    acc.Customer_Code = Customer_Code.Text;
                    acc.Balance_off_balance = Balance_off_balance.Text;
                    acc.Maturity_Date = Maturity_Date.SelectedDate;

                    string ProvisionAmountCurrency = Provision_amount_in_currency.Text;
                    string ProvisionAmountCurrencyLocal = Provision_amount_in_local_currency.Text;

                    Nullable<float> f;

                    if (ProvisionAmountCurrency == null || ProvisionAmountCurrency.Equals("")) f = null;
                    else f = (float)Convert.ToDecimal(Provision_amount_in_currency.Text.Replace(".", ","));
                    acc.Provision_amount_in_currency = f;


                    if (ProvisionAmountCurrencyLocal == null || ProvisionAmountCurrencyLocal.Equals("")) f = null;
                    else f = (float)Convert.ToDecimal(Provision_amount_in_local_currency.Text.Replace(".", ","));
                    acc.Provision_amount_in_local_currency = f;


                    acc.GL_internal_Number = GL_internal_Number.Text;
                    acc.GL_reporting_Number = GL_reporting_Number.Text;
                    acc.Provision_Stage = Provision_Stage.Text;

                    acc.Attribute_1 = Attribute_1.Text;
                    acc.Attribute_2 = Attribute_2.Text;
                    acc.Attribute_3 = Attribute_3.Text;
                    acc.Attribute_4 = Attribute_4.Text;
                    acc.Attribute_5 = Attribute_5.Text;
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
