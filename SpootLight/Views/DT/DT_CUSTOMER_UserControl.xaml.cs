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

namespace SpootLight.Views.DT
{
    /// <summary>
    /// Logique d'interaction pour DT_CUSTOMER_UserControl.xaml
    /// </summary>
    public partial class DT_CUSTOMER_UserControl : UserControl
    {
        private int numberOfRecPerPage; //Initialize our Variable, Classes and the List
        List<string> contents = new List<string>();
        public List<string> Items = new List<string>();
        static Paging PagedTable = new Paging();

        static DT_CUSTOMER_MODEL CustomersLists;
        public List<string> analyseSession = new List<string>();
        static string BankCode = Globals.getAnalyse()[1];
        static string dateArrete = Globals.getAnalyse()[3];
        static string version = Globals.getAnalyse()[5];
        private static List<DT_CUSTOMER> customers;
        public static string groupe = "";
        private static List<DT_CUSTOMER> myList = new List<DT_CUSTOMER>();
        public string JobDescription = "";
        public DT_CUSTOMER_UserControl()
        {
            InitializeComponent();
            PagedTable.type = typeof(DT_CUSTOMER);
            BankCode = Globals.getAnalyse()[1];
            dateArrete = Globals.getAnalyse()[3];
            version = Globals.getAnalyse()[5];
            initial();
            Globals.checkAdmin(groupe, ActionsPan);
            getJobDescription();
        }
        public void initial()
        {
            CustomersLists = new DT_CUSTOMER_MODEL();
            myList = CustomersLists.CUSTOMERS.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete) && c.Version.Equals(version)).ToList<DT_CUSTOMER>();
            groupe = Globals.getUser()[4];
            pagy();
        }
        private void Backwards_Click(object sender, RoutedEventArgs e)
        {
            G_CUSTOMER.ItemsSource = PagedTable.Previous(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void First_Click(object sender, RoutedEventArgs e)
        {
            G_CUSTOMER.ItemsSource = PagedTable.First(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void Last_Click(object sender, RoutedEventArgs e)
        {
            G_CUSTOMER.ItemsSource = PagedTable.Last(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }

        private void Forward_Click(object sender, RoutedEventArgs e)    //For each of these you call the direction you want and pass in the List and ComboBox output
        {                                                               //and use the above function to output the Record number to the Label
            G_CUSTOMER.ItemsSource = PagedTable.Next(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void NumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)  //I couldn't get this function to update in place (if the grid showed 20 and I selected 100 it would jump to 200)
        {                                                                                          //So instead I had it call the First function and that does an acceptable job.
            numberOfRecPerPage = Convert.ToInt32(NumberOfRecords.SelectedItem);
            G_CUSTOMER.ItemsSource = PagedTable.First(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
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
            Form_DT_CUSTOMER f = new Form_DT_CUSTOMER();
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
                DataRowView row = G_CUSTOMER.SelectedItem as DataRowView;
                if (row != null)
                {
                    Form_DT_CUSTOMER t = new Form_DT_CUSTOMER();
                    t.Show();
                    t.BankCode.Text = row.Row.ItemArray[0].ToString();
                    t.ProcessDate.Text = row.Row.ItemArray[1].ToString();
                    t.Version.Text = row.Row.ItemArray[2].ToString();
                    t.Customer_Code.Text = row.Row.ItemArray[3].ToString();

                    t.Customer_First_Name.Text = row.Row.ItemArray[4].ToString();
                    t.Customer_Last_Name.Text = row.Row.ItemArray[5].ToString();
                    t.Company_Name.Text = row.Row.ItemArray[6].ToString();
                    t.Customer_Type.Text = row.Row.ItemArray[7].ToString();
                    t.Customer_ID_Type.Text = row.Row.ItemArray[8].ToString();
                    t.Customer_ID_Number.Text = row.Row.ItemArray[9].ToString();
                    t.Company_group.Text = row.Row.ItemArray[10].ToString();
                    t.Company_group_name.Text = row.Row.ItemArray[11].ToString();
                    t.Customer_Country_Code.Text = row.Row.ItemArray[12].ToString();
                    t.Economic_Sector.Text = row.Row.ItemArray[13].ToString();
                    t.Activity_Area.Text = row.Row.ItemArray[14].ToString();
                    t.Internal_Segment.Text = row.Row.ItemArray[15].ToString();

                    t.Internal_Notation.Text = row.Row.ItemArray[16].ToString();
                    t.External_Notation.Text = row.Row.ItemArray[17].ToString();
                    t.Business_Category.Text = row.Row.ItemArray[18].ToString();
                    t.Commitment_Amount_in_local_currency.Text = row.Row.ItemArray[19].ToString();
                    t.Default_Amount_in_local_currency.Text = row.Row.ItemArray[20].ToString();
                    t.Rating.Text = row.Row.ItemArray[21].ToString();
                    t.Turnover.Text = row.Row.ItemArray[22].ToString();

                    t.Attribute_1.Text = row.Row.ItemArray[23].ToString();
                    t.Attribute_2.Text = row.Row.ItemArray[24].ToString();
                    t.Attribute_3.Text = row.Row.ItemArray[25].ToString();
                    t.Attribute_4.Text = row.Row.ItemArray[26].ToString();
                    t.Attribute_5.Text = row.Row.ItemArray[27].ToString();

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
            catch (Exception ex)
            {
                MessageBox.Show("Erreur Interne : \n \n "+ex.Message+" \n "+ex.StackTrace);
            }
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            CustomersLists.delete(G_CUSTOMER);
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

            PagedTable.type = typeof(DT_CUSTOMER);


            DataTable firstTable = PagedTable.SetPaging(myList.Cast<Object>().ToList(), numberOfRecPerPage); //Fill a DataTable with the First set based on the numberOfRecPerPage

            G_CUSTOMER.ItemsSource = firstTable.DefaultView;
        }

        private void CustomerCodeSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_CUSTOMER, CustomerCodeSearch, "DT_CUSTOMER", "Customer_Code", BankCode, dateArrete, version);
        }

        private void CustomerFirstNameSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_CUSTOMER, CustomerFirstNameSearch, "DT_CUSTOMER", "Customer_First_Name", BankCode, dateArrete, version);
        }

        private void CustomerLastNameSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_CUSTOMER, CustomerLastNameSearch, "DT_CUSTOMER", "Customer_Last_Name", BankCode, dateArrete, version);
        }

        private void CompanyNameSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_CUSTOMER, CompanyNameSearch, "DT_CUSTOMER", "Company_Name", BankCode, dateArrete, version);
        }

        public void getJobDescription()
        {
            JobDescription = JobTalendController.getJobData(Jobs.Client.Value, "description");
            Console.WriteLine("my job :" + JobDescription);
            DescriptionJob.Content = JobDescription;
        }
        private void RunJob_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JobTalendController.runJob2Finale(Jobs.Client.Value);
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
