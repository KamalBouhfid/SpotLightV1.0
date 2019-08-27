using SpootLight.Controllers;
using SpootLight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logique d'interaction pour Form_PR_ACCOUNT.xaml
    /// </summary>
    public partial class Form_PR_ACCOUNT : Window
    {
        PR_ACCOUNT_MODEL ctx = new PR_ACCOUNT_MODEL();
        FormController formcontroller = new FormController();
        public Form_PR_ACCOUNT()
        {
            InitializeComponent();
            BankCode.Text = Globals.getAnalyse()[1];
            ProcessDate.Text = Globals.getAnalyse()[3];
            Version.Text = Globals.getAnalyse()[5];
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
                if (formcontroller.CheckNotNull(account) && formcontroller.CheckNoString(Credit_Conversion_Factor) &&
                formcontroller.CheckNotNull(Description) && formcontroller.CheckNotNull(Balance_Off_Sheet) &&
                formcontroller.CheckNotNull(Sens_Code) && formcontroller.CheckNoString(Cmdr_Weighting))
                {
                    PR_ACCOUNT_MODEL ctx = new PR_ACCOUNT_MODEL();


                    PR_ACCOUNT acc = new PR_ACCOUNT();

                    acc.Bank_Code = Globals.getAnalyse()[1];
                    acc.Process_Date = Globals.getAnalyse()[3];
                    acc.Account = account.Text;
                    acc.Version = Version.Text;
                    acc.Description = Description.Text;
                    acc.Basel_In = Base_In.IsChecked.Value;
                    acc.Basel_Segment = Basel_Segment.Text;
                    string Credit = Credit_Conversion_Factor.Text;
                    Nullable<float> f;
                    if (Credit == null || Credit.Equals("")) f = null;
                    else f = (float)Convert.ToDecimal(Credit_Conversion_Factor.Text.Replace(".", ","));
                    acc.Credit_Conversion_Factor = f;
                    acc.Balance_Off_Sheet = Balance_Off_Sheet.Text;
                    acc.Sens_Code = Sens_Code.Text;
                    acc.Is_Commitment = Is_Commitment.IsChecked.Value;
                    acc.Is_Provision = Is_Provision.IsChecked.Value;
                    acc.Is_Doubtful = Is_Doubtful.IsChecked.Value;
                    acc.Is_Accrued_Interest = Is_Accrued_Interest.IsChecked.Value;
                    acc.Sign = Sign.Text;
                    acc.Exposition_Type = Exposition_Type.Text;
                    acc.Is_security = Is_security.IsChecked.Value;
                    acc.Cmdr_In = Cmdr_In.IsChecked.Value;
                    string CMDR = Cmdr_Conversion_Factor.Text;
                    Nullable<float> fCMDR;
                    if (CMDR == null || CMDR.Equals("")) fCMDR = null;
                    else fCMDR = (float)Convert.ToDecimal(Cmdr_Conversion_Factor.Text.Replace(".", ","));
                    acc.Cmdr_Conversion_Factor = fCMDR;
                    acc.Cmdr_Sens_Code = Cmdr_Sens_Code.Text;
                    string CMDRWE = Cmdr_Weighting.Text;
                    Nullable<float> fCMDRWE;
                    if (CMDRWE == null || CMDRWE.Equals("")) fCMDRWE = null;
                    else fCMDRWE = (float)Convert.ToDecimal(Cmdr_Weighting.Text.Replace(".", ","));
                    acc.Cmdr_Weighting = fCMDRWE;
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

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Credit_Conversion_Factor_TextChanged(object sender, TextChangedEventArgs e)
        {
            formcontroller.CheckNoString(Credit_Conversion_Factor);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Account_TextChanged(object sender, TextChangedEventArgs e)
        {
            formcontroller.CheckNotNull(account);
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
