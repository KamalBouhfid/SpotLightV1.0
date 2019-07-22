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
    /// Logique d'interaction pour PR_ACCOUNT_UserControl.xaml
    /// </summary>
    public partial class PR_ACCOUNT_UserControl : UserControl
    {
        private int numberOfRecPerPage; //Initialize our Variable, Classes and the List
        List<string> contents = new List<string>();
        public List<string> Items = new List<string>();
        static Paging PagedTable = new Paging();

        static PR_ACCOUNT_MODEL AccountsLists;
        public List<string> analyseSession = new List<string>();
        // IList<StudentModel.Student> myList = StudentList.GetData();
        static string BankCode = Globals.getAnalyse()[1];
        static string dateArrete = Globals.getAnalyse()[3];
        private static List<PR_ACCOUNT> accounts;
        //private static List<PR_ACCOUNT> filtered_accounts;
        private static List<PR_ACCOUNT> myList = new List<PR_ACCOUNT>();
        public PR_ACCOUNT_UserControl()
        {
            InitializeComponent();
            PagedTable.type = typeof(PR_ACCOUNT);
            BankCode = Globals.getAnalyse()[1];
            dateArrete = Globals.getAnalyse()[3];
            initial();
        }
        public void initial()
        {
            AccountsLists = new PR_ACCOUNT_MODEL();
            myList = AccountsLists.Accounts.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete)).ToList<PR_ACCOUNT>();
            Console.WriteLine("1 er :" + BankCode + " - 2 eme :" + dateArrete + " done !");
            //myList = accounts;

            pagy();
            //Fill the dataGrid with the DataTable created previously

        }
        private void Backwards_Click(object sender, RoutedEventArgs e)
        {
            G_ACCOUNT.ItemsSource = PagedTable.Previous(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void First_Click(object sender, RoutedEventArgs e)
        {
            G_ACCOUNT.ItemsSource = PagedTable.First(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void Last_Click(object sender, RoutedEventArgs e)
        {
            G_ACCOUNT.ItemsSource = PagedTable.Last(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }

        private void Forward_Click(object sender, RoutedEventArgs e)    //For each of these you call the direction you want and pass in the List and ComboBox output
        {                                                               //and use the above function to output the Record number to the Label
            G_ACCOUNT.ItemsSource = PagedTable.Next(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void NumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)  //I couldn't get this function to update in place (if the grid showed 20 and I selected 100 it would jump to 200)
        {                                                                                          //So instead I had it call the First function and that does an acceptable job.
            numberOfRecPerPage = Convert.ToInt32(NumberOfRecords.SelectedItem);
            G_ACCOUNT.ItemsSource = PagedTable.First(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
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
            Form_PR_ACCOUNT f = new Form_PR_ACCOUNT();
            f.Show();
            f.Closed += delegate
            {
                initial();
            };
        }

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            try {
            DataRowView row = G_ACCOUNT.SelectedItem as DataRowView;
            if (row != null)
            {
            Form_PR_ACCOUNT t = new Form_PR_ACCOUNT();
            t.Show();
            t.BankCode.Text = row.Row.ItemArray[0].ToString();
            t.ProcessDate.Text = row.Row.ItemArray[1].ToString();
            t.account.Text = row.Row.ItemArray[2].ToString();

            t.Description.Text = row.Row.ItemArray[3].ToString();

            if (row.Row.ItemArray[4] == null || row.Row.ItemArray[4].ToString() == "" || row.Row.ItemArray[4].ToString().StartsWith("F"))
                t.Base_In.IsChecked = false;
            else t.Base_In.IsChecked = true;


            t.Basel_Segment.Text = row.Row.ItemArray[5].ToString();
            t.Credit_Conversion_Factor.Text = row.Row.ItemArray[6].ToString();
            t.Balance_Off_Sheet.Text = row.Row.ItemArray[7].ToString();
            t.Sens_Code.Text = row.Row.ItemArray[8].ToString();


            if (row.Row.ItemArray[9] == null || row.Row.ItemArray[9].ToString() == "" || row.Row.ItemArray[9].ToString().StartsWith("F"))
                t.Is_Commitment.IsChecked = false;
            else t.Is_Commitment.IsChecked = true;

            if (row.Row.ItemArray[10] == null || row.Row.ItemArray[10].ToString() == "" || row.Row.ItemArray[10].ToString().StartsWith("F"))
                t.Is_Provision.IsChecked = false;
            else t.Is_Provision.IsChecked = true;
            if (row.Row.ItemArray[11] == null || row.Row.ItemArray[11].ToString() == "" || row.Row.ItemArray[11].ToString().StartsWith("F"))
                t.Is_Doubtful.IsChecked = false;
            else t.Is_Doubtful.IsChecked = true;
            if (row.Row.ItemArray[12] == null || row.Row.ItemArray[12].ToString() == "" || row.Row.ItemArray[12].ToString().StartsWith("F"))
                t.Is_Accrued_Interest.IsChecked = false;
            else t.Is_Accrued_Interest.IsChecked = true;

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
            AccountsLists.delete(G_ACCOUNT);
            initial();
        }
        public void setAnalyse(List<string> analyseSession)
        {
            this.analyseSession = analyseSession;
        }
        public List<string> getAnalyse()
        {
            return analyseSession;
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

            PagedTable.type = typeof(PR_ACCOUNT);



            DataTable firstTable = PagedTable.SetPaging(myList.Cast<Object>().ToList(), numberOfRecPerPage); //Fill a DataTable with the First set based on the numberOfRecPerPage

            G_ACCOUNT.ItemsSource = firstTable.DefaultView;
        }

        private void AccountSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            accounts = AccountsLists.Accounts.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete)).ToList<PR_ACCOUNT>();
            var filteredList = accounts.Where(account => account.Account != null && account.Account.Contains(AccountSearch.Text));
            myList = filteredList.Cast<PR_ACCOUNT>().ToList();
            pagy();
            if (AccountSearch.Text.Equals("") || AccountSearch.Text.Equals(null))
            {
                initial();
            }
        }

        private void DescriptionSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            accounts = AccountsLists.Accounts.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete)).ToList<PR_ACCOUNT>();
            var filteredList = accounts.Where(account => account.Description != null && account.Description.Contains(DescriptionSearch.Text));
            myList = filteredList.Cast<PR_ACCOUNT>().ToList();
            pagy();
            if (DescriptionSearch.Text.Equals("") || DescriptionSearch.Text.Equals(null))
            {
                initial();
            }
        }

        private void BaselSegmentSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            accounts = AccountsLists.Accounts.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete)).ToList<PR_ACCOUNT>();

             var filteredList = accounts.Where(account => account.Basel_Segment != null && account.Basel_Segment.Contains(BaselSegmentSearch.Text));
             myList = filteredList.Cast<PR_ACCOUNT>().ToList();
             pagy();
            if(BaselSegmentSearch.Text.Equals("") || BaselSegmentSearch.Text.Equals(null))
            {
                initial();
            }
        }

        private void CreditFactorSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            accounts = AccountsLists.Accounts.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete) && c.Credit_Conversion_Factor.HasValue).ToList<PR_ACCOUNT>();
            var filteredList = accounts.Where(account => account.Credit_Conversion_Factor.Value.ToString().StartsWith(CreditFactorSearch.Text.Replace(".", ",")));
            myList = filteredList.Cast<PR_ACCOUNT>().ToList();
            pagy();
            if (CreditFactorSearch.Text.Equals("") || CreditFactorSearch.Text.Equals(null))
            {
                initial();
            }
        }

        private void BalanceSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            accounts = AccountsLists.Accounts.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete)).ToList<PR_ACCOUNT>();
            var filteredList = accounts.Where(account => account.Balance_Off_Sheet !=null && account.Balance_Off_Sheet.Contains(BalanceSearch.Text));
            myList = filteredList.Cast<PR_ACCOUNT>().ToList();
            pagy();
            if (BalanceSearch.Text.Equals("") || BalanceSearch.Text.Equals(null))
            {
                initial();
            }
        }

        private void SensCodeSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            accounts = AccountsLists.Accounts.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete)).ToList<PR_ACCOUNT>();
            var filteredList = accounts.Where(account => account.Sens_Code !=null && account.Sens_Code.Contains(SensCodeSearch.Text));
            myList = filteredList.Cast<PR_ACCOUNT>().ToList();
            pagy();
            if (SensCodeSearch.Text.Equals("") || SensCodeSearch.Text.Equals(null))
            {
                initial();
            }
        }
    }
}
