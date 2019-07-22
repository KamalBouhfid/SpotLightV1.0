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
    /// Logique d'interaction pour Form_PR_GUARANTEE_TYPE.xaml
    /// </summary>
    public partial class Form_PR_GUARANTEE_TYPE : Window
    {
        public Form_PR_GUARANTEE_TYPE()
        {
            InitializeComponent();
            BankCode.Text = Globals.getAnalyse()[1];
            ProcessDate.Text = Globals.getAnalyse()[3];
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
        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            PR_GUARANTEE_TYPE_MODEL ctx = new PR_GUARANTEE_TYPE_MODEL();
            PR_GUARANTEE_TYPE bac = new PR_GUARANTEE_TYPE();


            bac.Bank_Code = Globals.getAnalyse()[1];
            bac.Process_Date = Globals.getAnalyse()[3];
            bac.Guarantee_Account = GuaranteeAccount.Text;

            bac.Guarantee_Type_Description = GuaranteeTypeDescription.Text;
            bac.Guarantee_Class = GuaranteeClass.Text;

            string GuaranteWeighting = GuaranteeWeighting.Text;
            Nullable<float> f;
            if (GuaranteWeighting == null || GuaranteWeighting.Equals("")) f = null;
            else f = (float)Convert.ToDecimal(GuaranteeWeighting.Text.Replace(".",","));
            bac.Guarantee_Weighting = f;

            ctx.InsertOrUpdate(bac);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
