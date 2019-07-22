using SpootLight.Controllers;
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
    /// Logique d'interaction pour PR_GUARANTEE_TYPE_UserControl.xaml
    /// </summary>
    public partial class PR_GUARANTEE_TYPE_UserControl : UserControl
    {
        private int numberOfRecPerPage; //Initialize our Variable, Classes and the List
        List<string> contents = new List<string>();
        public List<string> Items = new List<string>();
        static Paging PagedTable = new Paging();

        static PR_GUARANTEE_TYPE_MODEL GuaranteeLists;
        public List<string> analyseSession = new List<string>();
        static string BankCode = Globals.getAnalyse()[1];
        static string dateArrete = Globals.getAnalyse()[3];
        private static List<PR_GUARANTEE_TYPE> Guarantees;
        private static List<PR_GUARANTEE_TYPE> myList = new List<PR_GUARANTEE_TYPE>();
        public PR_GUARANTEE_TYPE_UserControl()
        {
            InitializeComponent();
            PagedTable.type = typeof(PR_GUARANTEE_TYPE);
            BankCode = Globals.getAnalyse()[1];
            dateArrete = Globals.getAnalyse()[3];
            initial();
        }
        public void initial()
        {
            GuaranteeLists = new PR_GUARANTEE_TYPE_MODEL();
            Guarantees = GuaranteeLists.GUARANTEES.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete)).ToList<PR_GUARANTEE_TYPE>();
            myList = Guarantees;
            pagy();
        }
        private void Backwards_Click(object sender, RoutedEventArgs e)
        {
            G_GUARANTEE_TYPE.ItemsSource = PagedTable.Previous(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void First_Click(object sender, RoutedEventArgs e)
        {
            G_GUARANTEE_TYPE.ItemsSource = PagedTable.First(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void Last_Click(object sender, RoutedEventArgs e)
        {
            G_GUARANTEE_TYPE.ItemsSource = PagedTable.Last(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }

        private void Forward_Click(object sender, RoutedEventArgs e)    //For each of these you call the direction you want and pass in the List and ComboBox output
        {                                                               //and use the above function to output the Record number to the Label
            G_GUARANTEE_TYPE.ItemsSource = PagedTable.Next(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void NumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)  //I couldn't get this function to update in place (if the grid showed 20 and I selected 100 it would jump to 200)
        {                                                                                          //So instead I had it call the First function and that does an acceptable job.
            numberOfRecPerPage = Convert.ToInt32(NumberOfRecords.SelectedItem);
            G_GUARANTEE_TYPE.ItemsSource = PagedTable.First(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
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
            Form_PR_GUARANTEE_TYPE f = new Form_PR_GUARANTEE_TYPE();
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
                DataRowView row = G_GUARANTEE_TYPE.SelectedItem as DataRowView;
                if (row != null)
                {
                    Form_PR_GUARANTEE_TYPE t = new Form_PR_GUARANTEE_TYPE();
                    t.Show();
                    t.BankCode.Text = row.Row.ItemArray[0].ToString();
                    t.ProcessDate.Text = row.Row.ItemArray[1].ToString();
                    t.GuaranteeAccount.Text = row.Row.ItemArray[2].ToString();
                    t.GuaranteeTypeDescription.Text = row.Row.ItemArray[3].ToString();
                    t.GuaranteeClass.Text = row.Row.ItemArray[4].ToString();
                    t.GuaranteeWeighting.Text = row.Row.ItemArray[5].ToString();
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
            GuaranteeLists.delete(G_GUARANTEE_TYPE);
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

            PagedTable.type = typeof(PR_GUARANTEE_TYPE);

            DataTable firstTable = PagedTable.SetPaging(myList.Cast<Object>().ToList(), numberOfRecPerPage); //Fill a DataTable with the First set based on the numberOfRecPerPage

            G_GUARANTEE_TYPE.ItemsSource = firstTable.DefaultView;
        }

        private void GuaranteeAccountSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Guarantees = GuaranteeLists.GUARANTEES.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete)).ToList<PR_GUARANTEE_TYPE>();
            var filteredList = Guarantees.Where(categorie => categorie.Guarantee_Account != null && categorie.Guarantee_Account.Contains(GuaranteeAccountSearch.Text));
            myList = filteredList.Cast<PR_GUARANTEE_TYPE>().ToList();
            pagy();
            if (GuaranteeAccountSearch.Text.Equals("") || GuaranteeAccountSearch.Text.Equals(null))
            {
                initial();
            }
        }

        private void GuaranteeTypeDescriptionSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Guarantees = GuaranteeLists.GUARANTEES.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete)).ToList<PR_GUARANTEE_TYPE>();
            var filteredList = Guarantees.Where(categorie => categorie.Guarantee_Type_Description != null && categorie.Guarantee_Type_Description.Contains(GuaranteeTypeDescriptionSearch.Text));
            myList = filteredList.Cast<PR_GUARANTEE_TYPE>().ToList();
            pagy();
            if (GuaranteeTypeDescriptionSearch.Text.Equals("") || GuaranteeTypeDescriptionSearch.Text.Equals(null))
            {
                initial();
            }
        }

        private void GuaranteeClassSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Guarantees = GuaranteeLists.GUARANTEES.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete)).ToList<PR_GUARANTEE_TYPE>();
            var filteredList = Guarantees.Where(categorie => categorie.Guarantee_Class != null && categorie.Guarantee_Class.Contains(GuaranteeClassSearch.Text));
            myList = filteredList.Cast<PR_GUARANTEE_TYPE>().ToList();
            pagy();
            if (GuaranteeClassSearch.Text.Equals("") || GuaranteeClassSearch.Text.Equals(null))
            {
                initial();
            }
        }

        private void GuaranteeWeightingSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Guarantees = GuaranteeLists.GUARANTEES.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete)).ToList<PR_GUARANTEE_TYPE>();
            var filteredList = Guarantees.Where(categorie => categorie.Guarantee_Weighting != null && categorie.Guarantee_Weighting.Value.ToString().StartsWith(GuaranteeTypeDescriptionSearch.Text.Replace(".",",")));
            myList = filteredList.Cast<PR_GUARANTEE_TYPE>().ToList();
            pagy();
            if (GuaranteeWeightingSearch.Text.Equals("") || GuaranteeWeightingSearch.Text.Equals(null))
            {
                initial();
            }
        }
    }
}
