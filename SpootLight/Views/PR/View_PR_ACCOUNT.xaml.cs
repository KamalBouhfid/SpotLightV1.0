using SpootLight.Controllers;
using SpootLight.Models;
using SpootLight.Views.Analyse;
using SpootLight.Views.ExcelAnalyse;
using SpootLight.Views.Talend;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace SpootLight.Views.PR
{
    /// <summary>
    /// Logique d'interaction pour PR_ACCOUNT.xaml
    /// </summary>
    public partial class View_PR_ACCOUNT : Window
    {
        List<string> PRcontents = new List<string>();
        List<string> DTcontents = new List<string>();
        public List<string> Items = new List<string>();
        static Paging PagedTable = new Paging();

        public List<string> analyseSession = new List<string>();
        static string BankCode = Globals.getAnalyse()[1];
        static string dateArrete = Globals.getAnalyse()[3];
        private static List<PR_ACCOUNT> myList= new List<PR_ACCOUNT>();

        PR_ACCOUNT_UserControl PR_Account_U = new PR_ACCOUNT_UserControl();
        PR_BASEL_CATEGORY_UserControl PR_Category_U = new PR_BASEL_CATEGORY_UserControl();
        PR_BASEL_PORTFOLIO_UserControl PR_Portfolio_U = new PR_BASEL_PORTFOLIO_UserControl();
        PR_CURRENCY_UserConntrol PR_Currency_U = new PR_CURRENCY_UserConntrol();
        PR_ECONOMIC_SECTOR_UserControl PR_Economic_U = new PR_ECONOMIC_SECTOR_UserControl();
        PR_GUARANTEE_TYPE_UserControl PR_Guarantee_U = new PR_GUARANTEE_TYPE_UserControl();

        public View_PR_ACCOUNT()
        {
            InitializeComponent();
            initial();
            initialJobTalend();
            NavEvents();
        }
        public void initial()
        {
            ParampanelNav.Children.Clear();
            JobpanelNav.Children.Clear();
            DatapanelNav.Children.Clear();
            PRcontents = PR_ACCOUNT_MODEL.getPRTables();
            SousParamCrudLixtBox.ItemsSource = PRcontents;
            SousParamCrudLixtBox.SelectedItem = SousParamCrudLixtBox.Items[0];
            DTcontents = PR_ACCOUNT_MODEL.getDTTables();
            SousDataCrudLixtBox.ItemsSource = DTcontents;
            SousDataCrudLixtBox.SelectedItem = SousParamCrudLixtBox.Items[0];
            stackData.Children.Clear();
            stackData.Children.Add(PR_Account_U);
            UserConnect.Content = "M." + Globals.getUser()[1];
            TimeConnect.Content = DateTime.Now.ToString("dd-MM-yyyy' -- 'hh':'mm");
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            BankCode = "";
            dateArrete = "";
            OpenAnalyse o = new OpenAnalyse();
            o.Show();
            this.Close();
        }

        public void setAnalyse(List<string> analyseSession)
        {
            this.analyseSession = analyseSession;
        }
        public List<string> getAnalyse()
        {
            return analyseSession;
        }

        private void OpenAnalyse_Click(object sender, RoutedEventArgs e)
        {
            BankCode = "";
            dateArrete = "";
            OpenAnalyse o = new OpenAnalyse();
            o.Show();
            this.Close();
        }

        private void JobTalend_Click(object sender, RoutedEventArgs e)
        {
            ParampanelNav.Children.Clear();
            DatapanelNav.Children.Clear();
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxJob.Visibility = Visibility.Visible;
            try
            {
                JobpanelNav.Children.Add(trvMenu);
            }
            catch (System.ArgumentException)
            {
                JobpanelNav.Children.Clear();
                JobpanelNav.Children.Add(trvMenu);
            }
        }

        private void DataCrud_Click(object sender, RoutedEventArgs e)
        {

            GroupBoxData.Visibility = Visibility.Visible;
            GroupBoxJob.Visibility = Visibility.Hidden;
            JobpanelNav.Children.Clear();
            ParampanelNav.Children.Clear();
            try { 
            DatapanelNav.Children.Add(SousDataCrudLixtBox);
            }
            catch (System.ArgumentException)
            {
                DatapanelNav.Children.Clear();
                DatapanelNav.Children.Add(SousDataCrudLixtBox);
            }
        }

        private void ExcelBrowse_Click(object sender, RoutedEventArgs e)
        {
            ExcelBrowse Ex = new ExcelBrowse();
            Ex.Show();
        }

        private void NewAnalyse_Click(object sender, RoutedEventArgs e)
        {
            NewAnalyse analyse = new NewAnalyse();
            analyse.Show();
        }

        private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
            if(this.WindowState != WindowState.Maximized)
            this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }
        public void NavEvents()
        {
            ParamCrudNav.MouseLeave += (s, e) => { ParamCrudNav.Background = Brushes.Transparent; };
            ParamCrudNav.MouseEnter += (s, e) => { ParamCrudNav.Background = Brushes.LightBlue; };

            JobTalendNav.MouseLeave += (s, e) => { JobTalendNav.Background = Brushes.Transparent; };
            JobTalendNav.MouseEnter += (s, e) => { JobTalendNav.Background = Brushes.LightBlue; };

            CorepNav.MouseLeave += (s, e) => { CorepNav.Background = Brushes.Transparent; };
            CorepNav.MouseEnter += (s, e) => { CorepNav.Background = Brushes.LightBlue; };

            RepportingNav.MouseLeave += (s, e) => { RepportingNav.Background = Brushes.Transparent; };
            RepportingNav.MouseEnter += (s, e) => { RepportingNav.Background = Brushes.LightBlue; };
        }
        private void SousDataCrudLixtBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupBoxData.Visibility = Visibility.Visible;
            GroupBoxJob.Visibility = Visibility.Hidden;

            DTcontents = PR_ACCOUNT_MODEL.getDTTables();
            SousDataCrudLixtBox.ItemsSource = DTcontents;
            SousDataCrudLixtBox.SelectionChanged += delegate {
                    SousDataCrudLixtBox.ItemsSource = DTcontents;
                    if ((SousDataCrudLixtBox.SelectedItem.Equals(DTcontents[0])))
                    {
                        stackData.Children.Clear();
                        stackData.Children.Add(PR_Account_U);
                    }
                    else if ((SousDataCrudLixtBox.SelectedItem.Equals(DTcontents[1])))
                    {
                        stackData.Children.Clear();
                        stackData.Children.Add(PR_Category_U);
                    }
                };
        }

        ///////***************************************************************
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxJob.Visibility = Visibility.Visible;
            JobTalendController.Jobs_choix(stack, sender, ResultJob, ResultJobErrors,
             NomJobResult, CheminJobResult, LancerJobBtn);

        }
        private void LancerJobBtn_Click(object sender, RoutedEventArgs e)
        {
            JobTalendController.runJob2(sender, ResultJob, ResultJobErrors, NomJobResult, CheminJobResult, LancerJobBtn);
        }
        public void initialJobTalend()
        {
            JobTalendController.initial(trvMenu);
        }

        private void ParamCrudNav_Click(object sender, RoutedEventArgs e)
        {
            GroupBoxData.Visibility = Visibility.Visible;
            GroupBoxJob.Visibility = Visibility.Hidden;
            JobpanelNav.Children.Clear();
            DatapanelNav.Children.Clear();
            try
            {
                ParampanelNav.Children.Add(SousParamCrudLixtBox);
            }
            catch (System.ArgumentException)
            {
                ParampanelNav.Children.Clear();
                ParampanelNav.Children.Add(SousParamCrudLixtBox);
            }
        }

        private void SousParamCrudLixtBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupBoxData.Visibility = Visibility.Visible;
            GroupBoxJob.Visibility = Visibility.Hidden;

            PRcontents = PR_ACCOUNT_MODEL.getPRTables();
            SousParamCrudLixtBox.ItemsSource = PRcontents;
            SousParamCrudLixtBox.SelectionChanged += delegate {
                SousParamCrudLixtBox.ItemsSource = PRcontents;
                if ((SousParamCrudLixtBox.SelectedItem.Equals(PRcontents[0])))
                {
                    stackData.Children.Clear();
                    stackData.Children.Add(PR_Account_U);
                }
                else if ((SousParamCrudLixtBox.SelectedItem.Equals(PRcontents[1])))
                {
                    stackData.Children.Clear();
                    stackData.Children.Add(PR_Category_U);
                }
                else if ((SousParamCrudLixtBox.SelectedItem.Equals(PRcontents[2])))
                {
                    stackData.Children.Clear();
                    stackData.Children.Add(PR_Portfolio_U);
                }
                else if ((SousParamCrudLixtBox.SelectedItem.Equals(PRcontents[3])))
                {
                    stackData.Children.Clear();
                    stackData.Children.Add(PR_Currency_U);
                }
                else if ((SousParamCrudLixtBox.SelectedItem.Equals(PRcontents[4])))
                {
                    stackData.Children.Clear();
                    stackData.Children.Add(PR_Economic_U);
                }
                else if ((SousParamCrudLixtBox.SelectedItem.Equals(PRcontents[5])))
                {
                    stackData.Children.Clear();
                    Console.WriteLine("correct unitil here !");
                    stackData.Children.Add(PR_Guarantee_U);
                }
            };
        }
    }
    public class MenuItem
    {
        public MenuItem()
        {
            this.Items = new ObservableCollection<MenuItem>();
        }

        public string Title { get; set; }

        public ObservableCollection<MenuItem> Items { get; set; }
    }
}
