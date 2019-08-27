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
    /// Logique d'interaction pour Form_DT_GARANTE.xaml
    /// </summary>
    public partial class Form_DT_GARANTE : Window
    {
        FormController formcontroller = new FormController();

        public Form_DT_GARANTE()
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
                if (formcontroller.CheckNoString(Guarantee_assigned_amount) &&
                formcontroller.CheckNoString(Guarantee_amount) && formcontroller.CheckNotNull(Internal_Contract_Number)
                && formcontroller.CheckNotNull(Internal_Guarantee_Number))
                {
                    DT_GARANTE_MODEL ctx = new DT_GARANTE_MODEL();


                    DT_GUARANTEE acc = new DT_GUARANTEE();

                    acc.Bank_Code = Globals.getAnalyse()[1];
                    acc.Process_Date = Globals.getAnalyse()[3];
                    acc.Version = Version.Text;
                    acc.Internal_Guarantee_Number = Internal_Guarantee_Number.Text;
                    acc.Internal_Contract_Number = Internal_Contract_Number.Text;


                    acc.Guarantee_type = Guarantee_type.Text;
                    acc.Guarantee_Customer = Guarantee_Customer.Text;

                    acc.Guarantee_currency = Gurantee_currency.Text;
                    acc.GL_internal_Number = GL_internal_Number.Text;


                    string GuaranteeAssignedAmount = Guarantee_assigned_amount.Text;
                    string GuaranteAmount = Guarantee_amount.Text;
                    Nullable<float> f;

                    if (GuaranteeAssignedAmount == null || GuaranteeAssignedAmount.Equals("")) f = null;
                    else f = (float)Convert.ToDecimal(Guarantee_assigned_amount.Text.Replace(".", ","));
                    acc.Guarantee_assigned_amount = f;

                    if (GuaranteAmount == null || GuaranteAmount.Equals("")) f = null;
                    else f = (float)Convert.ToDecimal(Guarantee_amount.Text.Replace(".", ","));
                    acc.Guarantee_amount = f;

                    acc.Value_Date = Value_Date.SelectedDate;
                    acc.End_Date = End_Date.SelectedDate;

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
