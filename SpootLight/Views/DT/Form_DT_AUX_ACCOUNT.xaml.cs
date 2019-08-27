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
    /// Logique d'interaction pour Form_DT_AUX_ACCOUNT.xaml
    /// </summary>
    public partial class Form_DT_AUX_ACCOUNT : Window
    {
        FormController formcontroller = new FormController();
        public Form_DT_AUX_ACCOUNT()
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
                if (formcontroller.CheckNoString(Accounting_Balance_in_local_currency) && formcontroller.CheckNoString(Accounting_Balance_in_currency) &&
                                formcontroller.CheckNoString(Doubtful_Status) && formcontroller.CheckNoString(Provision_affected_amount) &&
                                formcontroller.CheckNoString(Provision_amount_total_currency) && formcontroller.CheckNotNull(Internal_Account_Number)
                                && formcontroller.CheckNotNull(Currency_Code) && formcontroller.CheckNotNull(Customer_Code) &&
                                formcontroller.CheckNotNull(Product_Code) && formcontroller.CheckNotNull(Accounting_Balance_in_local_currency) &&
                                formcontroller.CheckNotNull(Accounting_Balance_in_currency) && formcontroller.CheckNotNull(GL_internal_Number) &&
                                formcontroller.CheckNotNull(GL_reporting_Number) && formcontroller.CheckNotNull(Doubtful_Status))
                {
                    DT_AUX_ACCOUNT_MODEL ctx = new DT_AUX_ACCOUNT_MODEL();


                    DT_AUX_ACCOUNT acc = new DT_AUX_ACCOUNT();

                    acc.Bank_Code = Globals.getAnalyse()[1];
                    acc.Process_Date = Globals.getAnalyse()[3];
                    acc.Version = Version.Text;
                    acc.Internal_Account_Number = Internal_Account_Number.Text;
                    acc.Currency_Code = Currency_Code.Text;
                    acc.Customer_Code = Customer_Code.Text;
                    acc.Acounting_Branch = Acounting_Branch.Text;
                    acc.Opening_Date = Opening_Date.SelectedDate;
                    acc.Product_Code = Product_Code.Text;

                    string AccountingBalanceLocal = Accounting_Balance_in_local_currency.Text;
                    string AccountingBalance = Accounting_Balance_in_currency.Text;
                    string Status = Doubtful_Status.Text;
                    string ProvisionAmount = Provision_affected_amount.Text;
                    string ProvisionAmountTotal = Provision_amount_total_currency.Text;
                    Nullable<float> f;

                    if (AccountingBalanceLocal == null || AccountingBalanceLocal.Equals("")) f = null;
                    else f = (float)Convert.ToDecimal(Accounting_Balance_in_local_currency.Text.Replace(".", ","));
                    acc.Accounting_Balance_in_local_currency = f;


                    if (AccountingBalance == null || AccountingBalance.Equals("")) f = null;
                    else f = (float)Convert.ToDecimal(Accounting_Balance_in_currency.Text.Replace(".", ","));
                    acc.Accounting_Balance_in_currency = f;


                    acc.GL_internal_Number = GL_internal_Number.Text;
                    acc.GL_reporting_Number = GL_reporting_Number.Text;

                    if (Status == null || Status.Equals("")) f = null;
                    else f = (float)Convert.ToDecimal(Doubtful_Status.Text.Replace(".", ","));
                    acc.Doubtful_Status = Convert.ToInt32(f);

                    if (ProvisionAmount == null || ProvisionAmount.Equals("")) f = null;
                    else f = (float)Convert.ToDecimal(Provision_affected_amount.Text.Replace(".", ","));
                    acc.Provision_affected_amount = f;

                    if (ProvisionAmountTotal == null || ProvisionAmountTotal.Equals("")) f = null;
                    else f = (float)Convert.ToDecimal(Provision_amount_total_currency.Text.Replace(".", ","));
                    acc.Provision_amount_total_currency = f;

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

        private void Account_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
