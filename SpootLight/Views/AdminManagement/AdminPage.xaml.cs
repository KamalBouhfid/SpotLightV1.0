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

namespace SpootLight.Views.AdminManagement
{
    /// <summary>
    /// Logique d'interaction pour AdminPage.xaml
    /// </summary>
    public partial class AdminPage : UserControl
    {
        private int numberOfRecPerPage; //Initialize our Variable, Classes and the List
        public List<string> Items = new List<string>();
        static Paging PagedTable = new Paging();

        static USER_MODEL UsersLists;
        public List<string> analyseSession = new List<string>();
        static string BankCode = Globals.getAnalyse()[1];
        static string dateArrete = Globals.getAnalyse()[3];
        private static List<UserSpot> users;
        private static List<UserSpot> myList = new List<UserSpot>();
        public static string groupe = "";

        public AdminPage()
        {
            InitializeComponent();
            PagedTable.type = typeof(UserSpot);
            BankCode = Globals.getAnalyse()[1];
            dateArrete = Globals.getAnalyse()[3];
            initial();
            Globals.checkAdmin(groupe, ActionsPan);
        }

        public void initial()
        {
            UsersLists = new USER_MODEL();
            users = UsersLists.USERS.ToList<UserSpot>();
            myList = users;
            groupe = Globals.getUser()[4];
            pagy();
        }


        private void Backwards_Click(object sender, RoutedEventArgs e)
        {
            G_USER.ItemsSource = PagedTable.Previous(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void First_Click(object sender, RoutedEventArgs e)
        {
            G_USER.ItemsSource = PagedTable.First(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void Last_Click(object sender, RoutedEventArgs e)
        {
            G_USER.ItemsSource = PagedTable.Last(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }

        private void Forward_Click(object sender, RoutedEventArgs e)    //For each of these you call the direction you want and pass in the List and ComboBox output
        {                                                               //and use the above function to output the Record number to the Label
            G_USER.ItemsSource = PagedTable.Next(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
            PageInfo.Content = PageNumberDisplay();
        }
        private void NumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)  //I couldn't get this function to update in place (if the grid showed 20 and I selected 100 it would jump to 200)
        {                                                                                          //So instead I had it call the First function and that does an acceptable job.
            numberOfRecPerPage = Convert.ToInt32(NumberOfRecords.SelectedItem);
            G_USER.ItemsSource = PagedTable.First(myList.Cast<Object>().ToList(), numberOfRecPerPage).DefaultView;
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

            PagedTable.type = typeof(UserSpot);

            DataTable firstTable = PagedTable.SetPaging(myList.Cast<Object>().ToList(), numberOfRecPerPage); //Fill a DataTable with the First set based on the numberOfRecPerPage

            G_USER.ItemsSource = firstTable.DefaultView;
        }

        private void EmailSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            users = UsersLists.USERS.ToList<UserSpot>();
            var filteredList = users.Where(user => user.email != null && user.email.Contains(EmailSearch.Text));
            myList = filteredList.Cast<UserSpot>().ToList();
            pagy();
            if (EmailSearch.Text.Equals("") || EmailSearch.Text.Equals(null))
            {
                initial();
            }
        }

        private void NomSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            users = UsersLists.USERS.ToList<UserSpot>();
            var filteredList = users.Where(user => user.lastname != null && user.lastname.Contains(NomSearch.Text));
            myList = filteredList.Cast<UserSpot>().ToList();
            pagy();
            if (NomSearch.Text.Equals("") || NomSearch.Text.Equals(null))
            {
                initial();
            }
        }

        private void PrenomSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            users = UsersLists.USERS.ToList<UserSpot>();
            var filteredList = users.Where(user => user.firstname != null && user.firstname.Contains(PrenomSearch.Text));
            myList = filteredList.Cast<UserSpot>().ToList();
            pagy();
            if (PrenomSearch.Text.Equals("") || PrenomSearch.Text.Equals(null))
            {
                initial();
            }
        }
        private void IDSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            users = UsersLists.USERS.ToList<UserSpot>();
            var filteredList = users.Where(user => user.id.ToString().StartsWith(IDSearch.Text));
            myList = filteredList.Cast<UserSpot>().ToList();
            pagy();
            if (IDSearch.Text.Equals("") || IDSearch.Text.Equals(null))
            {
                initial();
            }
        }

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView row = G_USER.SelectedItem as DataRowView;
                if (row != null)
                {
                    Form_User t = new Form_User();
                    t.Show();
                    t.IDUser.Text = row.Row.ItemArray[0].ToString();
                    t.FirstnameTxt.Text = row.Row.ItemArray[3].ToString();
                    t.LastnameTxt.Text = row.Row.ItemArray[4].ToString();
                    t.EmailTxt.Text = row.Row.ItemArray[2].ToString();
                    t.PassTxt.Password = AuthentificationController.EncryptDecrypt(row.Row.ItemArray[1].ToString(), 128);
                    t.Closed += delegate
                    {
                        initial();
                    };
                }
                else
                {
                    MessageBox.Show("Sélectionnez une ligne");
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Sélectionnez une ligne");
            }
        }

        private void Nouveau_Click(object sender, RoutedEventArgs e)
        {
            Form_User f = new Form_User();
            f.Show();
            f.Closed += delegate
            {
                initial();
            };
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            UsersLists.delete(G_USER);
            initial();
        }


    }
}
