using SpootLight.Controllers;
using SpootLight.Models;
using SpootLight.Views.PR;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpootLight.Views
{
    /// <summary>
    /// Logique d'interaction pour Crud_UserControl.xaml
    /// </summary>
    public partial class Crud_UserControl : UserControl
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
        public Crud_UserControl()
        {
            InitializeComponent();
            stackTest.Children.Clear();
            PR_ACCOUNT_UserControl u = new PR_ACCOUNT_UserControl();
            stackTest.Children.Add(u);
            SousDataCrudLixtBox.ItemsSource = contents;
            UserConnect.Content = " Bonjour M." + Globals.getUser()[1];
            TimeConnect.Content = DateTime.Now.ToString("dd-MM-yyyy' -- 'hh':'mm':'ss");
        }
        private void DataCrudLixtBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if ((DataCrudLixtBox.SelectedItem as ListBoxItem).Content.Equals("DATA PR"))
            {
                contents = PR_ACCOUNT_MODEL.getPRTables();
                SousDataCrudLixtBox.ItemsSource = contents;
                //setDataInList(contents);
                SousDataCrudLixtBox.SelectionChanged += delegate {
                    SousDataCrudLixtBox.ItemsSource = contents;
                    Console.WriteLine("Hello World !" + contents[0]);
                    if ((SousDataCrudLixtBox.SelectedItem.Equals(contents[0])))
                    {
                        stackTest.Children.Clear();
                        PR_ACCOUNT_UserControl u = new PR_ACCOUNT_UserControl();
                        stackTest.Children.Add(u);
                    }
                    else if ((SousDataCrudLixtBox.SelectedItem.Equals(contents[1])))
                    {
                        stackTest.Children.Clear();
                        PR_BASEL_CATEGORY_UserControl u = new PR_BASEL_CATEGORY_UserControl();
                        stackTest.Children.Add(u);

                    }
                    else if ((SousDataCrudLixtBox.SelectedItem.Equals(contents[2])))
                    {
                        stackTest.Children.Clear();
                        PR_BASEL_PORTFOLIO_UserControl u = new PR_BASEL_PORTFOLIO_UserControl();
                        stackTest.Children.Add(u);
                    }
                    else if ((SousDataCrudLixtBox.SelectedItem.Equals(contents[3])))
                    {
                        stackTest.Children.Clear();
                        PR_CURRENCY_UserConntrol u = new PR_CURRENCY_UserConntrol();
                        stackTest.Children.Add(u);
                    }
                    else if ((SousDataCrudLixtBox.SelectedItem.Equals(contents[4])))
                    {
                        stackTest.Children.Clear();
                        PR_ECONOMIC_SECTOR_UserControl u = new PR_ECONOMIC_SECTOR_UserControl();
                        stackTest.Children.Add(u);
                    }
                    else if ((SousDataCrudLixtBox.SelectedItem.Equals(contents[5])))
                    {
                        stackTest.Children.Clear();
                        Console.WriteLine("correct unitil here !");
                        PR_GUARANTEE_TYPE_UserControl u = new PR_GUARANTEE_TYPE_UserControl();
                        stackTest.Children.Add(u);
                    }
                };
                Console.WriteLine("all right !" + contents[0]);

            }
            Console.WriteLine("this is :" + DataCrudLixtBox.SelectedItem);
        }
        public void setAnalyse(List<string> analyseSession)
        {
            this.analyseSession = analyseSession;
        }
        public List<string> getAnalyse()
        {
            return analyseSession;
        }
    }
}
