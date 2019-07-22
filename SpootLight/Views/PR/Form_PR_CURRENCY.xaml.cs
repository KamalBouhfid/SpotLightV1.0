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
    /// Logique d'interaction pour Form_PR_CURRENCY.xaml
    /// </summary>
    public partial class Form_PR_CURRENCY : Window
    {
        public Form_PR_CURRENCY()
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
        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            PR_CURRENCY_MODEL ctx = new PR_CURRENCY_MODEL();
            PR_CURRENCY bac = new PR_CURRENCY();


            bac.Bank_Code = Globals.getAnalyse()[1];
            bac.Process_Date = Globals.getAnalyse()[3];
            bac.Currency = CurrencyTxt.Text;

            string ExchangeRate = ExchangeRateTxt.Text;
            Nullable<float> f;
            if (ExchangeRate == null || ExchangeRate.Equals("")) f = null;
            else f = (float)Convert.ToDecimal(ExchangeRateTxt.Text.Replace(".", ","));
            bac.Exchange_Rate = f;


            ctx.InsertOrUpdate(bac);
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
