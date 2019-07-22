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
    /// Logique d'interaction pour Form_PR_BASEL_CATTEGORY.xaml
    /// </summary>
    public partial class Form_PR_BASEL_CATTEGORY : Window
    {

        public Form_PR_BASEL_CATTEGORY()
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

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            PR_BASEL_CATEGORY_MODEL ctx = new PR_BASEL_CATEGORY_MODEL();
            PR_BASEL_CATEGORY bac = new PR_BASEL_CATEGORY();
            CategorieDescriptionTxt.SelectAll();
            SubCategorieDescriptionTxt.SelectAll();
            bac.Bank_Code = Globals.getAnalyse()[1];
            bac.Process_Date = Globals.getAnalyse()[3];
            bac.Category = CategorieTxt.Text;
            bac.Category_Description = CategorieDescriptionTxt.Text;
            bac.Sub_Category = SubCategorieTxt.Text;
            bac.Sub_Category_Description = SubCategorieDescriptionTxt.Text;
            bac.Sub_Portfolio = SubPortfolioTxt.Text;

            ctx.InsertOrUpdate(bac);
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
