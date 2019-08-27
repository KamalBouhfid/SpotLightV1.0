using SpootLight.Controllers;
using SpootLight.Models;
using SpootLight.Popup;
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
    /// Logique d'interaction pour PR_SECURITY_TYPE_UserControl.xaml
    /// </summary>
    public partial class PR_SECURITY_TYPE_UserControl : UserControl
    {
        private int numberOfRecPerPage; //Initialize our Variable, Classes and the List
        public List<string> Items = new List<string>();
        static Paging PagedTable = new Paging();
        public Boolean selected = false;

        static PR_SECURITY_TYPE_MODEL SecurityLists;
        public List<string> analyseSession = new List<string>();
        static string BankCode = Globals.getAnalyse()[1];
        static string dateArrete = Globals.getAnalyse()[3];
        static string version = Globals.getAnalyse()[5];
        private static List<PR_SECURITY_TYPE> security;
        public static string groupe = "";
        private static List<PR_SECURITY_TYPE> myList = new List<PR_SECURITY_TYPE>();
        public PR_SECURITY_TYPE_UserControl()
        {
            InitializeComponent();
            PagedTable.type = typeof(PR_SECURITY_TYPE);
            BankCode = Globals.getAnalyse()[1];
            dateArrete = Globals.getAnalyse()[3];
            version = Globals.getAnalyse()[5];
            initial();
            Globals.checkAdmin(groupe, ActionsPan);
        }
        public void initial()
        {
            SecurityLists = new PR_SECURITY_TYPE_MODEL();
            security = SecurityLists.SECURITY.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete) && c.Version.Equals(version)).ToList<PR_SECURITY_TYPE>();
            myList = security;
            Console.WriteLine("version :" + version);
            groupe = Globals.getUser()[4];
            pagy();
        }
        private void Backwards_Click(object sender, RoutedEventArgs e)
        {
            G_Security.ItemsSource = PagedTable.Previous(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void First_Click(object sender, RoutedEventArgs e)
        {
            G_Security.ItemsSource = PagedTable.First(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void Last_Click(object sender, RoutedEventArgs e)
        {
            G_Security.ItemsSource = PagedTable.Last(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }

        private void Forward_Click(object sender, RoutedEventArgs e)    //For each of these you call the direction you want and pass in the List and ComboBox output
        {                                                               //and use the above function to output the Record number to the Label
            G_Security.ItemsSource = PagedTable.Next(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void NumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)  //I couldn't get this function to update in place (if the grid showed 20 and I selected 100 it would jump to 200)
        {                                                                                          //So instead I had it call the First function and that does an acceptable job.
            numberOfRecPerPage = Convert.ToInt32(NumberOfRecords.SelectedItem);
            G_Security.ItemsSource = PagedTable.First(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
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
            Form_PR_SECURITY_TYPE f = new Form_PR_SECURITY_TYPE();
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
                var row = G_Security.SelectedItem as DataRowView;
                if (row != null)
                {
                    Form_PR_SECURITY_TYPE t = new Form_PR_SECURITY_TYPE();
                    t.Show();
                    t.BankCode.Text = row.Row.ItemArray[0].ToString();
                    t.ProcessDate.Text = row.Row.ItemArray[1].ToString();
                    t.Version.Text = row.Row.ItemArray[2].ToString();
                    t.Security_Type.Text = row.Row.ItemArray[3].ToString();
                    t.Sub_Portfolio.Text = row.Row.ItemArray[4].ToString();
                    t.Closed += delegate {
                        initial();
                    };
                }
                else
                {
                    MessageBox.Show("Sélectionner une ligne !");
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Sélectionner une ligne !");
            }
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            SecurityLists.delete(G_Security);
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

            NumberOfRecords.SelectedItem = 100; //Initialize the ComboBox

            numberOfRecPerPage = Convert.ToInt32(NumberOfRecords.SelectedItem); //Convert the Combox Output to type int

            // PagedTable.PageIndex = 0;

            PagedTable.type = typeof(PR_SECURITY_TYPE);

            DataTable firstTable = PagedTable.SetPaging(myList.Cast<Object>().ToList(), numberOfRecPerPage); //Fill a DataTable with the First set based on the numberOfRecPerPage

            G_Security.ItemsSource = firstTable.DefaultView;
        }

        private void Security_TypeSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_Security, Security_TypeSearch, "PR_SECURITY_TYPE", "Security_Type", BankCode, dateArrete,version);

        }

        private void Sub_PortfolioSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_Security, Sub_PortfolioSearch, "PR_SECURITY_TYPE", "Sub_Portfolio", BankCode, dateArrete,version);

        }

        private void Importer_Click(object sender, RoutedEventArgs e)
        {
            /*ImportChoser chooser = new ImportChoser();
            chooser.JobName = Jobs.TypeSecurite.Value;
            chooser.JobNameCrush = Jobs.TypeSecuriteCrush.Value;
            chooser.Show();*/
        }
    }
}
