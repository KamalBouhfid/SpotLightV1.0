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
    /// Logique d'interaction pour Form_DT_CUSTOMER.xaml
    /// </summary>
    public partial class Form_DT_CUSTOMER : Window
    {
        FormController formcontroller = new FormController();
        public Form_DT_CUSTOMER()
        {
            InitializeComponent();
            BankCode.Text = Globals.getAnalyse()[1];
            ProcessDate.Text = Globals.getAnalyse()[3];
            Version.Text = Globals.getAnalyse()[5];
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

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (formcontroller.CheckNoString(Commitment_Amount_in_local_currency) &&
                formcontroller.CheckNoString(Default_Amount_in_local_currency) &&
                formcontroller.CheckNoString(Turnover) && formcontroller.CheckNotNull(Customer_Code))
                {
                    DT_CUSTOMER_MODEL ctx = new DT_CUSTOMER_MODEL();


                    DT_CUSTOMER acc = new DT_CUSTOMER();

                    acc.Bank_Code = Globals.getAnalyse()[1];
                    acc.Process_Date = Globals.getAnalyse()[3];
                    acc.Version = Version.Text;
                    acc.Customer_Code = Customer_Code.Text;


                    acc.Customer_First_Name = Customer_First_Name.Text;
                    acc.Customer_Last_Name = Customer_Last_Name.Text;

                    acc.Company_Name = Company_Name.Text;
                    acc.Customer_Type = Customer_Type.Text;
                    acc.Customer_ID_Type = Customer_ID_Type.Text;
                    acc.Customer_ID_Number = Customer_ID_Number.Text;
                    acc.Company_group = Company_group.Text;

                    acc.Company_group_name = Company_group_name.Text;
                    acc.Customer_Country_Code = Customer_Country_Code.Text;
                    acc.Economic_Sector = Economic_Sector.Text;
                    acc.Activity_Area = Activity_Area.Text;
                    acc.Internal_Segment = Internal_Segment.Text;
                    acc.Internal_Notation = Internal_Notation.Text;
                    acc.External_Notation = External_Notation.Text;
                    acc.Business_Category = Business_Category.Text;

                    string CommitmentAmountInLocalCurrency = Commitment_Amount_in_local_currency.Text;
                    string DefaultAmountInLocalVCurrency = Default_Amount_in_local_currency.Text;
                    string TurnoverF = Turnover.Text;
                    Nullable<float> f;

                    if (CommitmentAmountInLocalCurrency == null || CommitmentAmountInLocalCurrency.Equals("")) f = null;
                    else f = (float)Convert.ToDecimal(Commitment_Amount_in_local_currency.Text.Replace(".", ","));
                    acc.Commitment_Amount_in_local_currency = f;

                    if (DefaultAmountInLocalVCurrency == null || DefaultAmountInLocalVCurrency.Equals("")) f = null;
                    else f = (float)Convert.ToDecimal(Default_Amount_in_local_currency.Text.Replace(".", ","));
                    acc.Default_Amount_in_local_currency = f;

                    if (TurnoverF == null || TurnoverF.Equals("")) f = null;
                    else f = (float)Convert.ToDecimal(Turnover.Text.Replace(".", ","));
                    acc.Turnover = f;

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

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
