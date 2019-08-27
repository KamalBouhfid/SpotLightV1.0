using SpootLight.Controllers;
using SpootLight.Models;
using SpootLight.Views.PR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Logique d'interaction pour DT_AUX_ACCOUNT_UserControl.xaml
    /// </summary>
    public partial class DT_AUX_ACCOUNT_UserControl : UserControl
    {
        private int numberOfRecPerPage; //Initialize our Variable, Classes and the List
        List<string> contents = new List<string>();
        public List<string> Items = new List<string>();
        static Paging PagedTable = new Paging();

        static DT_AUX_ACCOUNT_MODEL AccountsLists;
        public List<string> analyseSession = new List<string>();
        static string BankCode = Globals.getAnalyse()[1];
        static string dateArrete = Globals.getAnalyse()[3];
        static string version = Globals.getAnalyse()[5];
        private static List<DT_AUX_ACCOUNT> accounts;
        public static string groupe = "";
        private static List<DT_AUX_ACCOUNT> myList = new List<DT_AUX_ACCOUNT>();
        public string JobDescription = "";
        public DT_AUX_ACCOUNT_UserControl()
        {
            InitializeComponent();
            PagedTable.type = typeof(DT_AUX_ACCOUNT);
            BankCode = Globals.getAnalyse()[1];
            dateArrete = Globals.getAnalyse()[3];
            version = Globals.getAnalyse()[5];
            initial();
            Globals.checkAdmin(groupe, ActionsPan);
            getJobDescription();
        }
        public void initial()
        {
            AccountsLists = new DT_AUX_ACCOUNT_MODEL();
            myList = AccountsLists.Accounts.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete) && c.Version.Equals(version)).ToList<DT_AUX_ACCOUNT>();
            groupe = Globals.getUser()[4];
            pagy();
        }
        private void Backwards_Click(object sender, RoutedEventArgs e)
        {
            G_AUX_ACCOUNT.ItemsSource = PagedTable.Previous(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void First_Click(object sender, RoutedEventArgs e)
        {
            G_AUX_ACCOUNT.ItemsSource = PagedTable.First(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void Last_Click(object sender, RoutedEventArgs e)
        {
            G_AUX_ACCOUNT.ItemsSource = PagedTable.Last(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }

        private void Forward_Click(object sender, RoutedEventArgs e)    //For each of these you call the direction you want and pass in the List and ComboBox output
        {                                                               //and use the above function to output the Record number to the Label
            G_AUX_ACCOUNT.ItemsSource = PagedTable.Next(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void NumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)  //I couldn't get this function to update in place (if the grid showed 20 and I selected 100 it would jump to 200)
        {                                                                                          //So instead I had it call the First function and that does an acceptable job.
            numberOfRecPerPage = Convert.ToInt32(NumberOfRecords.SelectedItem);
            G_AUX_ACCOUNT.ItemsSource = PagedTable.First(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
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
            Form_DT_AUX_ACCOUNT f = new Form_DT_AUX_ACCOUNT();
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
                DataRowView row = G_AUX_ACCOUNT.SelectedItem as DataRowView;
                if (row != null)
                {
                    Form_DT_AUX_ACCOUNT t = new Form_DT_AUX_ACCOUNT();
                    t.Show();
                    t.BankCode.Text = row.Row.ItemArray[0].ToString();
                    t.ProcessDate.Text = row.Row.ItemArray[1].ToString();
                    t.Version.Text = row.Row.ItemArray[2].ToString();
                    t.Internal_Account_Number.Text = row.Row.ItemArray[3].ToString();

                    t.Currency_Code.Text = row.Row.ItemArray[4].ToString();
                    t.Customer_Code.Text = row.Row.ItemArray[5].ToString();
                    t.Acounting_Branch.Text = row.Row.ItemArray[6].ToString();
                    t.Opening_Date.Text = row.Row.ItemArray[7].ToString();
                    t.Product_Code.Text = row.Row.ItemArray[8].ToString();
                    t.Accounting_Balance_in_local_currency.Text = row.Row.ItemArray[9].ToString();
                    t.Accounting_Balance_in_currency.Text = row.Row.ItemArray[10].ToString();
                    t.GL_internal_Number.Text = row.Row.ItemArray[11].ToString();
                    t.GL_reporting_Number.Text = row.Row.ItemArray[12].ToString();
                    t.Doubtful_Status.Text = row.Row.ItemArray[13].ToString();
                    t.Provision_affected_amount.Text = row.Row.ItemArray[14].ToString();
                    t.Provision_amount_total_currency.Text = row.Row.ItemArray[15].ToString();
                    t.Attribute_1.Text = row.Row.ItemArray[16].ToString();
                    t.Attribute_2.Text = row.Row.ItemArray[17].ToString();
                    t.Attribute_3.Text = row.Row.ItemArray[18].ToString();
                    t.Attribute_4.Text = row.Row.ItemArray[19].ToString();
                    t.Attribute_5.Text = row.Row.ItemArray[20].ToString();

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
            AccountsLists.delete(G_AUX_ACCOUNT);
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

            PagedTable.type = typeof(DT_AUX_ACCOUNT);



            DataTable firstTable = PagedTable.SetPaging(myList.Cast<Object>().ToList(), numberOfRecPerPage); //Fill a DataTable with the First set based on the numberOfRecPerPage

            G_AUX_ACCOUNT.ItemsSource = firstTable.DefaultView;
        }

        private void AccountInternalSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine("version num :" + version);
            Globals.search(G_AUX_ACCOUNT, AccountInternalSearch, "DT_AUX_ACCOUNT", "Internal_Account_Number", BankCode, dateArrete,version);
        }

        private void CodeCurrencySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_AUX_ACCOUNT, CodeCurrencySearch, "DT_AUX_ACCOUNT", "Currency_Code", BankCode, dateArrete, version);
        }

        private void CodeClientSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_AUX_ACCOUNT, CodeClientSearch, "DT_AUX_ACCOUNT", "Customer_Code", BankCode, dateArrete, version);
        }

        private void AcountingBranchSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_AUX_ACCOUNT, AcountingBranchSearch, "DT_AUX_ACCOUNT", "Acounting_Branch", BankCode, dateArrete, version);
        }

        private void OpeningDateSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_AUX_ACCOUNT, OpeningDateSearch, "DT_AUX_ACCOUNT", "Opening_Date", BankCode, dateArrete, version);
        }

        private void ProductCodeSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_AUX_ACCOUNT, ProductCodeSearch, "DT_AUX_ACCOUNT", "Product_Code", BankCode, dateArrete, version);
        }

        private void AccountingBalanceSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_AUX_ACCOUNT, AccountingBalanceSearch, "DT_AUX_ACCOUNT", "Accounting_Balance_in_local_currency", BankCode, dateArrete, version);
        }

        public void getJobDescription()
        {
            JobDescription = JobTalendController.getJobData(Jobs.Compte.Value, "description");
            Console.WriteLine("my job :" + JobDescription);
            DescriptionJob.Content = JobDescription;
        }
        private void RunJob_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JobTalendController.editDtFtDeclarationFile(Jobs.Compte.Value);
                JobTalendController.runJob2Finale(Jobs.Compte.Value);
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

        /*private void AccountingBalanceCurrencySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_AUX_ACCOUNT, AccountingBalanceCurrencySearch, "DT_AUX_ACCOUNT", "Accounting_Balance_in_currency", BankCode, dateArrete);
        }

        private void GLInternalNumberSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_AUX_ACCOUNT, GLInternalNumberSearch, "DT_AUX_ACCOUNT", "GL_internal_Number", BankCode, dateArrete);
        }

        private void GLReportingNumberSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_AUX_ACCOUNT, GLReportingNumberSearch, "DT_AUX_ACCOUNT", "GL_reporting_Number", BankCode, dateArrete);
        }

        private void DoubtfulStatusSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_AUX_ACCOUNT, DoubtfulStatusSearch, "DT_AUX_ACCOUNT", "Doubtful_Status", BankCode, dateArrete);
        }

        private void ProvisionAffectedAmountSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_AUX_ACCOUNT, ProvisionAffectedAmountSearch, "DT_AUX_ACCOUNT", "Provision_affected_amount", BankCode, dateArrete);
        }

        private void ProvisionAmountTotalCurrencySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_AUX_ACCOUNT, ProvisionAmountTotalCurrencySearch, "DT_AUX_ACCOUNT", "Provision_amount_total_currency", BankCode, dateArrete);
        }

        private void Attribute1Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_AUX_ACCOUNT, Attribute1Search, "DT_AUX_ACCOUNT", "Attribute_1", BankCode, dateArrete);
        }

        private void Attribute2Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_AUX_ACCOUNT, Attribute2Search, "DT_AUX_ACCOUNT", "Attribute_2", BankCode, dateArrete);
        }

        private void Attribute3Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_AUX_ACCOUNT, Attribute3Search, "DT_AUX_ACCOUNT", "Attribute_3", BankCode, dateArrete);
        }

        private void Attribute4Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_AUX_ACCOUNT, Attribute4Search, "DT_AUX_ACCOUNT", "Attribute_4", BankCode, dateArrete);
        }

        private void Attribute5Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_AUX_ACCOUNT, Attribute5Search, "DT_AUX_ACCOUNT", "Attribute_5", BankCode, dateArrete);
        }*/
    }
}
