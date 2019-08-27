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
    /// Logique d'interaction pour Form_PR_BASEL_PORTFOLIO.xaml
    /// </summary>
    public partial class Form_PR_BASEL_PORTFOLIO : Window
    {
        FormController formcontroller = new FormController();
        public Form_PR_BASEL_PORTFOLIO()
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
        private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (formcontroller.CheckNotNull(PortfolioTxt) && formcontroller.CheckNotNull(PortfolioDescriptionTxt) &&
            formcontroller.CheckNotNull(Sub_PortfolioTxt) && formcontroller.CheckNotNull(SubPortfolioDescriptionTxt) &&
            formcontroller.CheckNoString(WeightingTxt) && formcontroller.CheckNotNull(WeightingTxt))
                {
                    PR_BASEL_PORTFOLIO_MODEL ctx = new PR_BASEL_PORTFOLIO_MODEL();
                    PR_BASEL_PORTFOLIO bac = new PR_BASEL_PORTFOLIO();
                    PortfolioDescriptionTxt.SelectAll();
                    SubPortfolioDescriptionTxt.SelectAll();
                    bac.Bank_Code = Globals.getAnalyse()[1];
                    bac.Process_Date = Globals.getAnalyse()[3];
                    bac.Version = Globals.getAnalyse()[5];
                    bac.Portfolio = PortfolioTxt.Text;
                    bac.Portfolio_Description = PortfolioDescriptionTxt.Text;
                    bac.Sub_Portfolio_Description = SubPortfolioDescriptionTxt.Text;
                    bac.Sub_Portfolio = Sub_PortfolioTxt.Text;
                    string weight = WeightingTxt.Text;
                    Nullable<float> f;
                    if (weight == null || weight.Equals("")) f = null;
                    else f = (float)Convert.ToDecimal(WeightingTxt.Text.Replace(".", ","));
                    bac.Weighting = f;
                    bac.WeightingCode = WeightingCode.Text;
                    bac.Default_Category = Default_Category.Text;
                    ctx.InsertOrUpdate(bac);
                    this.Close();
                }
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
