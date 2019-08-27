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
    /// Logique d'interaction pour PR_INTERNAL_SEGMENT_UserControl.xaml
    /// </summary>
    public partial class PR_INTERNAL_SEGMENT_UserControl : UserControl
    {
        private int numberOfRecPerPage; //Initialize our Variable, Classes and the List
        public List<string> Items = new List<string>();
        static Paging PagedTable = new Paging();
        public Boolean selected = false;

        static PR_INTERNAL_SEGMENT_MODEL IntenalSegmentLists;
        public List<string> analyseSession = new List<string>();
        static string BankCode = Globals.getAnalyse()[1];
        static string dateArrete = Globals.getAnalyse()[3];
        static string version = Globals.getAnalyse()[5];
        private static List<PR_INTERNAL_SEGMENT> internalsegment;
        public static string groupe = "";
        private static List<PR_INTERNAL_SEGMENT> myList = new List<PR_INTERNAL_SEGMENT>();
        public PR_INTERNAL_SEGMENT_UserControl()
        {
            InitializeComponent();
            PagedTable.type = typeof(PR_INTERNAL_SEGMENT);
            BankCode = Globals.getAnalyse()[1];
            dateArrete = Globals.getAnalyse()[3];
            version = Globals.getAnalyse()[5];
            initial();
            Globals.checkAdmin(groupe, ActionsPan);
        }
        public void initial()
        {
            IntenalSegmentLists = new PR_INTERNAL_SEGMENT_MODEL();
            internalsegment = IntenalSegmentLists.INTERNAL_SEGMENT.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete) && c.Version.Equals(version)).ToList<PR_INTERNAL_SEGMENT>();
            myList = internalsegment;
            groupe = Globals.getUser()[4];
            pagy();
        }
        private void Backwards_Click(object sender, RoutedEventArgs e)
        {
            G_Internal_Segment.ItemsSource = PagedTable.Previous(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void First_Click(object sender, RoutedEventArgs e)
        {
            G_Internal_Segment.ItemsSource = PagedTable.First(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void Last_Click(object sender, RoutedEventArgs e)
        {
            G_Internal_Segment.ItemsSource = PagedTable.Last(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }

        private void Forward_Click(object sender, RoutedEventArgs e)    //For each of these you call the direction you want and pass in the List and ComboBox output
        {                                                               //and use the above function to output the Record number to the Label
            G_Internal_Segment.ItemsSource = PagedTable.Next(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void NumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)  //I couldn't get this function to update in place (if the grid showed 20 and I selected 100 it would jump to 200)
        {                                                                                          //So instead I had it call the First function and that does an acceptable job.
            numberOfRecPerPage = Convert.ToInt32(NumberOfRecords.SelectedItem);
            G_Internal_Segment.ItemsSource = PagedTable.First(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
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
            Form_PR_INTERNAL_SEGMENT f = new Form_PR_INTERNAL_SEGMENT();
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
                var row = G_Internal_Segment.SelectedItem as DataRowView;
                if (row != null)
                {
                    Form_PR_INTERNAL_SEGMENT t = new Form_PR_INTERNAL_SEGMENT();
                    t.Show();
                    t.BankCode.Text = row.Row.ItemArray[0].ToString();
                    t.ProcessDate.Text = row.Row.ItemArray[1].ToString();
                    t.Version.Text = row.Row.ItemArray[2].ToString();
                    t.Internal_Segment.Text = row.Row.ItemArray[3].ToString();
                    t.Internal_Segment_Description.Text = row.Row.ItemArray[4].ToString();
                    t.Customer_Market.Text = row.Row.ItemArray[5].ToString();
                    t.Sub_Category.Text = row.Row.ItemArray[6].ToString();
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
            IntenalSegmentLists.delete(G_Internal_Segment);
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

            PagedTable.type = typeof(PR_INTERNAL_SEGMENT);

            DataTable firstTable = PagedTable.SetPaging(myList.Cast<Object>().ToList(), numberOfRecPerPage); //Fill a DataTable with the First set based on the numberOfRecPerPage

            G_Internal_Segment.ItemsSource = firstTable.DefaultView;
        }

        private void InternalSegmentSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_Internal_Segment, InternalSegmentSearch, "PR_INTERNAL_SEGMENT", "Internal_Segment", BankCode, dateArrete, version);
        }

        private void CustomerMarketSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_Internal_Segment, CustomerMarketSearch, "PR_INTERNAL_SEGMENT", "Customer_Market", BankCode, dateArrete, version);
        }

        private void SubCategorySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.search(G_Internal_Segment, SubCategorySearch, "PR_INTERNAL_SEGMENT", "Sub_Category", BankCode, dateArrete, version);
        }

        private void Importer_Click(object sender, RoutedEventArgs e)
        {
            ImportChoser chooser = new ImportChoser();
            chooser.JobName = Jobs.SegmentClientInterne.Value;
            chooser.JobNameCrush = Jobs.SegmentClientInterneCrush.Value;
            chooser.Show();
            chooser.Closed += delegate
            {
                initial();
            };
        }
    }
}
