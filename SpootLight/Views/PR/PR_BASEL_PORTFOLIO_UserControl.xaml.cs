﻿using SpootLight.Controllers;
using SpootLight.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpootLight.Views.PR
{
    /// <summary>
    /// Logique d'interaction pour PR_BASEL_PORTFOLIO_UserControl.xaml
    /// </summary>
    public partial class PR_BASEL_PORTFOLIO_UserControl : UserControl
    {
        private int numberOfRecPerPage; //Initialize our Variable, Classes and the List
        public List<string> Items = new List<string>();
        static Paging PagedTable = new Paging();

        static PR_BASEL_PORTFOLIO_MODEL PortfolioLists;
        public List<string> analyseSession = new List<string>();
        static string BankCode = Globals.getAnalyse()[1];
        static string dateArrete = Globals.getAnalyse()[3];
        private static List<PR_BASEL_PORTFOLIO> portfolios;
        private static List<PR_BASEL_PORTFOLIO> myList = new List<PR_BASEL_PORTFOLIO>();
        public PR_BASEL_PORTFOLIO_UserControl()
        {
            InitializeComponent();
            PagedTable.type = typeof(PR_BASEL_PORTFOLIO);
            BankCode = Globals.getAnalyse()[1];
            dateArrete = Globals.getAnalyse()[3];
            initial();
        }
        public void initial()
        {
            PortfolioLists = new PR_BASEL_PORTFOLIO_MODEL();
            portfolios = PortfolioLists.PORTFOLIOS.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete)).ToList<PR_BASEL_PORTFOLIO>();
            Console.WriteLine("1 er :" + BankCode + " - 2 eme :" + dateArrete + " done !");
            myList = portfolios;

            pagy();
        }
        private void Backwards_Click(object sender, RoutedEventArgs e)
        {
            G_BASEL_PORTFOLIO.ItemsSource = PagedTable.Previous(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void First_Click(object sender, RoutedEventArgs e)
        {
            G_BASEL_PORTFOLIO.ItemsSource = PagedTable.First(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void Last_Click(object sender, RoutedEventArgs e)
        {
            G_BASEL_PORTFOLIO.ItemsSource = PagedTable.Last(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }

        private void Forward_Click(object sender, RoutedEventArgs e)    //For each of these you call the direction you want and pass in the List and ComboBox output
        {                                                               //and use the above function to output the Record number to the Label
            G_BASEL_PORTFOLIO.ItemsSource = PagedTable.Next(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void NumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)  //I couldn't get this function to update in place (if the grid showed 20 and I selected 100 it would jump to 200)
        {                                                                                          //So instead I had it call the First function and that does an acceptable job.
            numberOfRecPerPage = Convert.ToInt32(NumberOfRecords.SelectedItem);
            G_BASEL_PORTFOLIO.ItemsSource = PagedTable.First(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        public string PageNumberDisplay()
        {
            int PagedNumber = numberOfRecPerPage * (PagedTable.PageIndex + 1);
            if (PagedNumber > myList.Count)
            {
                PagedNumber = myList.Count;
            }
            return "Montrant " + PagedNumber + " de " + myList.Count; //This dramatically reduced the number of times I had to write this string statement
        }
        private void Nouveau_Click(object sender, RoutedEventArgs e)
        {
            Form_PR_BASEL_PORTFOLIO f = new Form_PR_BASEL_PORTFOLIO();
            f.Show();
            f.Closed += delegate
            {
                initial();
            };
        }

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            try {
            DataRowView row = G_BASEL_PORTFOLIO.SelectedItem as DataRowView;
            if (row != null){
            Form_PR_BASEL_PORTFOLIO t = new Form_PR_BASEL_PORTFOLIO();
            t.Show();
            t.BankCode.Text = row.Row.ItemArray[0].ToString();
            t.ProcessDate.Text = row.Row.ItemArray[1].ToString();
            t.Sub_PortfolioTxt.Text = row.Row.ItemArray[4].ToString();

            t.PortfolioTxt.Text = row.Row.ItemArray[2].ToString();
            t.PortfolioDescriptionTxt.Text = row.Row.ItemArray[3].ToString();

            t.SubPortfolioDescriptionTxt.Text = row.Row.ItemArray[5].ToString();
            t.WeightingTxt.Text = row.Row.ItemArray[6].ToString();

            t.Closed += delegate
            {
                initial();
            };
                }
                else
                {
                    MessageBox.Show("Merci de Sélectionner une ligne !");
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Merci de Sélectionner une ligne !");
            }
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            PortfolioLists.delete(G_BASEL_PORTFOLIO);
            initial();
        }
        private void pagy()
        {

            PagedTable.PageIndex = 1; //Sets the Initial Index to a default value

            int[] RecordsToShow = { 10, 20, 30, 50, 100 }; //This Array can be any number groups
            NumberOfRecords.Items.Clear();

            foreach (int RecordGroup in RecordsToShow)
            {
                NumberOfRecords.Items.Add(RecordGroup); //Fill the ComboBox with the Array
            }

            NumberOfRecords.SelectedItem = 10; //Initialize the ComboBox

            numberOfRecPerPage = Convert.ToInt32(NumberOfRecords.SelectedItem); //Convert the Combox Output to type int

            // PagedTable.PageIndex = 0;

            PagedTable.type = typeof(PR_BASEL_PORTFOLIO);

            DataTable firstTable = PagedTable.SetPaging(myList.Cast<Object>().ToList(), numberOfRecPerPage); //Fill a DataTable with the First set based on the numberOfRecPerPage

            G_BASEL_PORTFOLIO.ItemsSource = firstTable.DefaultView;
        }

        private void PortfolioSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            portfolios = PortfolioLists.PORTFOLIOS.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete)).ToList<PR_BASEL_PORTFOLIO>();
            var filteredList = portfolios.Where(categorie => categorie.Portfolio != null && categorie.Portfolio.Contains(PortfolioSearch.Text));
            myList = filteredList.Cast<PR_BASEL_PORTFOLIO>().ToList();
            pagy();
            if (PortfolioSearch.Text.Equals("") || PortfolioSearch.Text.Equals(null))
            {
                initial();
            }
        }

        private void PortfolioDescriptionSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            portfolios = PortfolioLists.PORTFOLIOS.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete)).ToList<PR_BASEL_PORTFOLIO>();
            var filteredList = portfolios.Where(categorie => categorie.Portfolio_Description != null && categorie.Portfolio_Description.Contains(PortfolioDescriptionSearch.Text));
            myList = filteredList.Cast<PR_BASEL_PORTFOLIO>().ToList();
            pagy();
            if (PortfolioDescriptionSearch.Text.Equals("") || PortfolioDescriptionSearch.Text.Equals(null))
            {
                initial();
            }
        }

        private void SousPortfolioSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            portfolios = PortfolioLists.PORTFOLIOS.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete)).ToList<PR_BASEL_PORTFOLIO>();
            var filteredList = portfolios.Where(categorie => categorie.Sub_Portfolio != null && categorie.Sub_Portfolio.Contains(SousPortfolioSearch.Text));
            myList = filteredList.Cast<PR_BASEL_PORTFOLIO>().ToList();
            pagy();
            if (SousPortfolioSearch.Text.Equals("") || SousPortfolioSearch.Text.Equals(null))
            {
                initial();
            }
        }

        private void SousPortfolioDescriptionSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            portfolios = PortfolioLists.PORTFOLIOS.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete)).ToList<PR_BASEL_PORTFOLIO>();
            var filteredList = portfolios.Where(categorie => categorie.Sub_Portfolio_Description != null && categorie.Sub_Portfolio_Description.Contains(SousPortfolioDescriptionSearch.Text));
            myList = filteredList.Cast<PR_BASEL_PORTFOLIO>().ToList();
            pagy();
            if (SousPortfolioDescriptionSearch.Text.Equals("") || SousPortfolioDescriptionSearch.Text.Equals(null))
            {
                initial();
            }
        }

        private void WeightingSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            portfolios = PortfolioLists.PORTFOLIOS.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete)).ToList<PR_BASEL_PORTFOLIO>();
            var filteredList = portfolios.Where(categorie => categorie.Weighting != null && categorie.Weighting.Value.ToString().StartsWith(WeightingSearch.Text.Replace(".", ",")));
            myList = filteredList.Cast<PR_BASEL_PORTFOLIO>().ToList();
            pagy();
            if (WeightingSearch.Text.Equals("") || WeightingSearch.Text.Equals(null))
            {
                initial();
            }
        }
    }
}
