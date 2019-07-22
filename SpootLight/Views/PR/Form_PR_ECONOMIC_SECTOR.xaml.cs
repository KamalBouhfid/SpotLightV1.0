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
    /// Logique d'interaction pour Form_PR_ECONOMIC_SECTOR.xaml
    /// </summary>
    public partial class Form_PR_ECONOMIC_SECTOR : Window
    {
        public Form_PR_ECONOMIC_SECTOR()
        {
            InitializeComponent();
            BankCode.Text = Globals.getAnalyse()[1];
            ProcessDate.Text = Globals.getAnalyse()[3];
        }
        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            PR_ECONOMIC_SECTOR_MODEL ctx = new PR_ECONOMIC_SECTOR_MODEL();
            PR_ECONOMIC_SECTOR bac = new PR_ECONOMIC_SECTOR();

            bac.Bank_Code = Globals.getAnalyse()[1];
            bac.Process_Date = Globals.getAnalyse()[3];
            bac.Economic_Sector = EconomicSectorTxt.Text;

            bac.Description = DescriptionTxt.Text;
            bac.Sub_Category = SubCategoryTxt.Text;

            ctx.InsertOrUpdate(bac);
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
    }
}
