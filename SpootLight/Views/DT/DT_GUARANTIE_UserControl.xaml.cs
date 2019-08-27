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

namespace SpootLight.Views.DT
{
    /// <summary>
    /// Logique d'interaction pour DT_GUARANTIE_UserControl.xaml
    /// </summary>
    public partial class DT_GUARANTIE_UserControl : UserControl
    {
        private int numberOfRecPerPage; //Initialize our Variable, Classes and the List
        List<string> contents = new List<string>();
        public List<string> Items = new List<string>();
        static Paging PagedTable = new Paging();

        static DT_GARANTE_MODEL GuarsLists;
        public List<string> analyseSession = new List<string>();
        static string BankCode = Globals.getAnalyse()[1];
        static string dateArrete = Globals.getAnalyse()[3];
        static string version = Globals.getAnalyse()[5];
        private static List<DT_GUARANTEE> guars;
        public static string groupe = "";
        private static List<DT_GUARANTEE> myList = new List<DT_GUARANTEE>();
        public string JobDescription = "";

        public DT_GUARANTIE_UserControl()
        {
            InitializeComponent();
            PagedTable.type = typeof(DT_GUARANTEE);
            BankCode = Globals.getAnalyse()[1];
            dateArrete = Globals.getAnalyse()[3];
            version = Globals.getAnalyse()[5];
            initial();
            Globals.checkAdmin(groupe, ActionsPan);
            getJobDescription();
        }
        public void initial()
        {
            GuarsLists = new DT_GARANTE_MODEL();
            myList = GuarsLists.GUAR.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete) && c.Version.Equals(version)).ToList<DT_GUARANTEE>();
            groupe = Globals.getUser()[4];
            pagy();
        }
        private void Backwards_Click(object sender, RoutedEventArgs e)
        {
            G_GARANTIE.ItemsSource = PagedTable.Previous(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void First_Click(object sender, RoutedEventArgs e)
        {
            G_GARANTIE.ItemsSource = PagedTable.First(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void Last_Click(object sender, RoutedEventArgs e)
        {
            G_GARANTIE.ItemsSource = PagedTable.Last(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }

        private void Forward_Click(object sender, RoutedEventArgs e)    //For each of these you call the direction you want and pass in the List and ComboBox output
        {                                                               //and use the above function to output the Record number to the Label
            G_GARANTIE.ItemsSource = PagedTable.Next(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void NumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)  //I couldn't get this function to update in place (if the grid showed 20 and I selected 100 it would jump to 200)
        {                                                                                          //So instead I had it call the First function and that does an acceptable job.
            numberOfRecPerPage = Convert.ToInt32(NumberOfRecords.SelectedItem);
            G_GARANTIE.ItemsSource = PagedTable.First(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
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
            Form_DT_GARANTE f = new Form_DT_GARANTE();
            f.Show();
            f.Closed += delegate
            {
                initial();
            };
        }

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView row = G_GARANTIE.SelectedItem as DataRowView;
                if (row != null)
                {
                    Form_DT_GARANTE t = new Form_DT_GARANTE();
                    t.Show();
                    t.BankCode.Text = row.Row.ItemArray[0].ToString();
                    t.ProcessDate.Text = row.Row.ItemArray[1].ToString();
                    t.Version.Text = row.Row.ItemArray[2].ToString();

                    t.Internal_Guarantee_Number.Text = row.Row.ItemArray[3].ToString();
                    t.Internal_Contract_Number.Text = row.Row.ItemArray[4].ToString();

                    t.Guarantee_type.Text = row.Row.ItemArray[5].ToString();
                    t.Guarantee_Customer.Text = row.Row.ItemArray[6].ToString();
                    t.Gurantee_currency.Text = row.Row.ItemArray[7].ToString();
                    t.GL_internal_Number.Text = row.Row.ItemArray[8].ToString();
                    t.Guarantee_assigned_amount.Text = row.Row.ItemArray[9].ToString();
                    t.Guarantee_amount.Text = row.Row.ItemArray[10].ToString();
                    t.Value_Date.Text = row.Row.ItemArray[11].ToString();
                    t.End_Date.Text = row.Row.ItemArray[12].ToString();

                    t.Attribute_1.Text = row.Row.ItemArray[13].ToString();
                    t.Attribute_2.Text = row.Row.ItemArray[14].ToString();
                    t.Attribute_3.Text = row.Row.ItemArray[15].ToString();
                    t.Attribute_4.Text = row.Row.ItemArray[16].ToString();
                    t.Attribute_5.Text = row.Row.ItemArray[17].ToString();

                    t.Closed += delegate
                    {
                        initial();
                    };
                }
                else
                {
                    MessageBox.Show("Veuillez Sélectionner une ligne !");
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Veuillez Sélectionner une ligne !");
            }
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            GuarsLists.delete(G_GARANTIE);
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

            PagedTable.type = typeof(DT_GUARANTEE);



            DataTable firstTable = PagedTable.SetPaging(myList.Cast<Object>().ToList(), numberOfRecPerPage); //Fill a DataTable with the First set based on the numberOfRecPerPage

            G_GARANTIE.ItemsSource = firstTable.DefaultView;
        }

        private void InternalGuaranteeNumberSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_GARANTIE, InternalGuaranteeNumberSearch, "DT_GUARANTEE", "Internal_Guarantee_Number", BankCode, dateArrete, version);
        }

        private void InternalContractNumberSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_GARANTIE, InternalContractNumberSearch, "DT_GUARANTEE", "Internal_Contract_Number", BankCode, dateArrete, version);
        }

        private void GuaranteeTypeSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_GARANTIE, GuaranteeTypeSearch, "DT_GUARANTEE", "Guarantee_type", BankCode, dateArrete, version);
        }

        private void GuaranteeCurrencySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_GARANTIE, GuaranteeCurrencySearch, "DT_GUARANTEE", "Guarantee_currency", BankCode, dateArrete, version);
        }

        private void GuaranteeCustomerSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_GARANTIE, GuaranteeCustomerSearch, "DT_GUARANTEE", "Guarantee_Customer", BankCode, dateArrete, version);

        }

        public void getJobDescription()
        {
            JobDescription = JobTalendController.getJobData(Jobs.Garantie.Value, "description");
            Console.WriteLine("my job :" + JobDescription);
            DescriptionJob.Content = JobDescription;
        }
        private void RunJob_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JobTalendController.runJob2Finale(Jobs.Garantie.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : \n \n " + ex.Message + " \n \n " + ex.StackTrace);
            }
            initial();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newWindowWidth = e.NewSize.Width;
            LoadedLines.Width = (newWindowWidth - 50) / 2;
            CancledLines.Width = (newWindowWidth - 50) / 2;
        }
    }
}