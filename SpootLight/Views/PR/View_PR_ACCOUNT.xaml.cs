using SpootLight.Controllers;
using SpootLight.Models;
using SpootLight.Repporting;
using SpootLight.Views.AdminManagement;
using SpootLight.Views.Analyse;
using SpootLight.Views.Declarations.COREP.CR.CRM;
using SpootLight.Views.Declarations.COREP.CR.SA;
using SpootLight.Views.Declarations.COREP.CMDR;
using SpootLight.Views.DT;
using SpootLight.Views.ExcelAnalyse;
using SpootLight.Views.Talend;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
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
        public List<string>  HeaderInfo = new List<string>();
        List<string> PRcontents = new List<string>();
        List<string> DTcontents = new List<string>();
        List<string> Usercontents = new List<string>();
        List<string> Reportingcontents = new List<string>();
        public List<string> Items = new List<string>();
        static Paging PagedTable = new Paging();

        public List<string> analyseSession = new List<string>();
        static string BankCode = Globals.getAnalyse()[1];
        static string dateArrete = Globals.getAnalyse()[3];
        static string version = Globals.getAnalyse()[5];
        private static List<PR_ACCOUNT> myList= new List<PR_ACCOUNT>();
        public static string groupe = "";

        // ********************** Paramétrage tables *******************************

        PR_ACCOUNT_UserControl PR_Account_U = new PR_ACCOUNT_UserControl();
        PR_BASEL_CATEGORY_UserControl PR_Category_U = new PR_BASEL_CATEGORY_UserControl();
        PR_BASEL_PORTFOLIO_UserControl PR_Portfolio_U = new PR_BASEL_PORTFOLIO_UserControl();
        PR_CURRENCY_UserConntrol PR_Currency_U = new PR_CURRENCY_UserConntrol();
        PR_ECONOMIC_SECTOR_UserControl PR_Economic_U = new PR_ECONOMIC_SECTOR_UserControl();
        PR_GUARANTEE_TYPE_UserControl PR_Guarantee_U = new PR_GUARANTEE_TYPE_UserControl(); 
        PR_INTERNAL_SEGMENT_UserControl PR_Internal_Segment_U = new PR_INTERNAL_SEGMENT_UserControl();
        PR_SECURITY_TYPE_UserControl PR_SECURITY_TYPE_U = new PR_SECURITY_TYPE_UserControl();

        // ********************** Data tables *******************************

        DT_AUX_ACCOUNT_UserControl DT_Aux_Account_U = new DT_AUX_ACCOUNT_UserControl();
        DT_PROVISION_UserControl DT_Provision_U = new DT_PROVISION_UserControl();
        DT_DEFAULT_UserControl DT_Default_U = new DT_DEFAULT_UserControl();
        DT_CUSTOMER_UserControl DT_Customer_U = new DT_CUSTOMER_UserControl();
        DT_GUARANTIE_UserControl DT_Guarantie_U = new DT_GUARANTIE_UserControl();

        // ********************** Repporting tables *******************************
        ReportUserControl FT_COREP_CR = new ReportUserControl();
        FT_COREP_CA_User FT_COREP_CA = new FT_COREP_CA_User();
        FT_ASSET_User FT_ASSET = new FT_ASSET_User();
        FT_ARC_User FT_ARC = new FT_ARC_User();
        FT_CUSOMER_COMMITMENT_User FT_CUSOMER_COMMITMENT = new FT_CUSOMER_COMMITMENT_User();
        SpootLight.Repporting.FT_CUSOMER_CMDR fT_CUSOMER_CMDR = new SpootLight.Repporting.FT_CUSOMER_CMDR();

        // ********************** Declarations *******************************

        SA_UserControl SA_U = new SA_UserControl();
        CRM_UserControl CRM_U = new CRM_UserControl();
        UserControl335 CMDR335_U = new UserControl335();
        UserControl337 CMDR337_U = new UserControl337();
        UserControl339 CMDR339_U = new UserControl339();

        // ************************************************  

        ExcelShow excelfile = new ExcelShow();
        AdminPage AP = new AdminPage();


        // **************************************************
        private object dummyNode = null;
        public object path = @"D:\COREP";
        public string SelectedImagePath { get; set; }
        // ************************************************
        public void initialExcel()
        {
            /*foreach (string s in Directory.GetLogicalDrives())
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = s;
                item.Tag = s;
                item.FontWeight = FontWeights.Normal;
                item.Items.Add(dummyNode);
                item.Expanded += new RoutedEventHandler(folder_Expanded);
                foldersItem.Items.Add(item);
            }*/

            foreach (string s in Directory.GetLogicalDrives())
            {
                if (s.Contains("D:\\"))
                {
                    TreeViewItem item = new TreeViewItem();
                    item.Header = s;
                    item.Tag = s;
                    item.FontWeight = FontWeights.Normal;
                    item.Items.Add(dummyNode);
                    item.Expanded += new RoutedEventHandler(folder_Expanded);
                    foldersItem.Items.Add(item);
                }
            }

            /*TreeViewItem item1 = new TreeViewItem();
            item1.Header = " Fichier COREP ";
            item1.Tag = path;
            item1.FontWeight = FontWeights.Normal;
            item1.Items.Add(dummyNode);
            item1.Expanded += new RoutedEventHandler(folder_Expanded);
            foldersItem.Items.Add(item1);*/

        }
        public void resizing(GroupBox groupebox, double newWindowHeight,double newWindowWidth)
        {
            double Grid_Height= newWindowHeight - 220;
            double Grid_With = newWindowWidth - 272;
            groupebox.Width = newWindowWidth -260;
            groupebox.Height = newWindowHeight -80;

            PR_Account_U.G_ACCOUNT.Height = Grid_Height;
            PR_Account_U.G_ACCOUNT.Width = Grid_With;

            PR_Category_U.G_BASEL_CATEGORY.Height = Grid_Height;
            PR_Category_U.G_BASEL_CATEGORY.Width =  Grid_With;

            PR_Portfolio_U.G_BASEL_PORTFOLIO.Height = Grid_Height;
            PR_Portfolio_U.G_BASEL_PORTFOLIO.Width = Grid_With;

            PR_Currency_U.G_CURRENCY.Height = Grid_Height;
            PR_Currency_U.G_CURRENCY.Width =  Grid_With;

            PR_Economic_U.G_ECONOMIC_SECTOR.Height = Grid_Height;
            PR_Economic_U.G_ECONOMIC_SECTOR.Width =  Grid_With;

            PR_Guarantee_U.G_GUARANTEE_TYPE.Height = Grid_Height;
            PR_Guarantee_U.G_GUARANTEE_TYPE.Width =  Grid_With;

            PR_Internal_Segment_U.G_Internal_Segment.Height = Grid_Height;
            PR_Internal_Segment_U.G_Internal_Segment.Width =  Grid_With;

            PR_SECURITY_TYPE_U.G_Security.Height = Grid_Height;
            PR_SECURITY_TYPE_U.G_Security.Width =  Grid_With;

            DT_Aux_Account_U.G_AUX_ACCOUNT.Height = Grid_Height - 125;
            DT_Aux_Account_U.G_AUX_ACCOUNT.Width = Grid_With ;

            DT_Provision_U.G_PROVISION.Height = Grid_Height - 125;
            DT_Provision_U.G_PROVISION.Width = Grid_With;

            DT_Default_U.G_DEFAULT.Height = Grid_Height - 125;
            DT_Default_U.G_DEFAULT.Width = Grid_With;

            DT_Customer_U.G_CUSTOMER.Height = Grid_Height - 125;
            DT_Customer_U.G_CUSTOMER.Width = Grid_With;

            DT_Guarantie_U.G_GARANTIE.Height = Grid_Height - 125;
            DT_Guarantie_U.G_GARANTIE.Width = Grid_With;

            FT_COREP_CR.G_Reporting.Height = Grid_Height + 10;
            FT_COREP_CR.G_Reporting.Width = Grid_With;

            FT_COREP_CA.G_Reporting.Height = Grid_Height + 10;
            FT_COREP_CA.G_Reporting.Width = Grid_With;

            FT_ASSET.G_Reporting.Height = Grid_Height + 10;
            FT_ASSET.G_Reporting.Width = Grid_With;

            FT_ARC.G_Reporting.Height = Grid_Height + 10;
            FT_ARC.G_Reporting.Width = Grid_With;

            FT_CUSOMER_COMMITMENT.G_Reporting.Height = Grid_Height + 10;
            FT_CUSOMER_COMMITMENT.G_Reporting.Width = Grid_With;

            fT_CUSOMER_CMDR.G_Reporting.Height = Grid_Height + 10;
            fT_CUSOMER_CMDR.G_Reporting.Width = Grid_With;

            excelfile.Sheet.Height = Grid_Height + 70;
            excelfile.Sheet.Width = Grid_With;

            //SA_U.radSpreadsheet.Height = Grid_Height - 135;
            //SA_U.radSpreadsheet.Width = Grid_With -40;

            CRM_U.SheetSV.Height = Grid_Height -10;
            CRM_U.SheetSV.Width = Grid_With - 10;
            CRM_U.SheetEC.Height = Grid_Height - 10;
            CRM_U.SheetEC.Width = Grid_With - 10;
            CRM_U.SheetCL.Height = Grid_Height - 10;
            CRM_U.SheetCL.Width = Grid_With - 10;
            CRM_U.SheetEN.Height = Grid_Height - 10;
            CRM_U.SheetEN.Width = Grid_With - 10;

            SA_U.SheetSV.Height = Grid_Height - 10;
            SA_U.SheetSV.Width = Grid_With - 10;
            SA_U.SheetEC.Height = Grid_Height - 10;
            SA_U.SheetEC.Width = Grid_With - 10;
            SA_U.SheetCL.Height = Grid_Height - 10;
            SA_U.SheetCL.Width = Grid_With - 10;
            SA_U.SheetEN.Height = Grid_Height - 10;
            SA_U.SheetEN.Width = Grid_With - 10;

            CMDR335_U.Sheet335.Height = Grid_Height - 10;
            CMDR335_U.Sheet335.Width = Grid_With - 10;

            CMDR337_U.Sheet337.Height = Grid_Height - 10;
            CMDR337_U.Sheet337.Width = Grid_With - 10;

            CMDR339_U.Sheet339.Height = Grid_Height - 10;
            CMDR339_U.Sheet339.Width = Grid_With - 10;

            AP.G_USER.Height = Grid_Height -10;
            AP.G_USER.Width = Grid_With;

            radDocking.Height = Grid_Height +155;

        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            /*double SizeOnDeletedWindowBar = SystemParameters.WorkArea.Height -20;
            Console.WriteLine("my screen :" + SizeOnDeletedWindowBar);
            MainViewSpotLight.Height  = SizeOnDeletedWindowBar;*/
            double newWindowHeight = e.NewSize.Height -20;
            double newWindowWidth = e.NewSize.Width;
            resizing(GroupBoxData, newWindowHeight, newWindowWidth);
            resizing(GroupBoxDataDT, newWindowHeight, newWindowWidth);
            resizing(GroupBoxCorep, newWindowHeight, newWindowWidth);
            resizing(GroupBoxJob, newWindowHeight, newWindowWidth);
            resizing(GroupBoxReporting, newWindowHeight, newWindowWidth);
            resizing(GroupBoxUser, newWindowHeight, newWindowWidth);
        }
        void folder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {
                item.Items.Clear();
                try
                {
                    foreach (string s in Directory.GetDirectories(item.Tag.ToString()))
                    {
                        TreeViewItem subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Items.Add(dummyNode);
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);
                        item.Items.Add(subitem);
                    }
                    foreach (string s in Directory.GetFiles(item.Tag.ToString()))
                    {
                        TreeViewItem subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Items.Add(dummyNode);
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);
                        item.Items.Add(subitem);
                    }
                }
                catch (Exception) { }
            }
        }
        private void FoldersItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            stackCorep.Children.Clear();
            stackCorep.Children.Add(excelfile);
            excelfile.SelectedImagePath = ShowExcelMethod();
            try {
                excelfile.Sheet.Load(SelectedImagePath);
            }
            catch(NullReferenceException Nullex)
            {
                MessageBox.Show("Sélectionnez un fichier Excel (.xlsx)", "Sélectionnez un fichier", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch (NotSupportedException Nullex)
            {
                MessageBox.Show("Sélectionnez un fichier Excel (.xlsx)", "Sélectionnez un fichier", MessageBoxButton.OK, MessageBoxImage.Stop);
            }

        }
        private void OpenCorepInExcel_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@ShowExcelMethod());
        }
        public string ShowExcelMethod()
        {
            TreeViewItem temp = ((TreeViewItem)foldersItem.SelectedItem);

            if (temp == null)
                return "";
            SelectedImagePath = "";
            string temp1 = "";
            string temp2 = "";
            while (true)
            {
                temp1 = temp.Header.ToString();
                if (temp1.Contains(@"\"))
                {
                    temp2 = "";
                }
                SelectedImagePath = temp1 + temp2 + SelectedImagePath;
                if (temp.Parent.GetType().Equals(typeof(TreeView)))
                {
                    break;
                }
                temp = ((TreeViewItem)temp.Parent);
                temp2 = @"\";
            }
            //show user selected path
            if (SelectedImagePath.EndsWith(".xlsx"))
            {
                return SelectedImagePath;
            }
            else
            {
                MessageBox.Show("Format ou fichier non supportée ! \nSélectionnez des fichier Excel (.xlsx)", "Fichier Non Supportée !", MessageBoxButton.OK, MessageBoxImage.Stop);
                return null;
            }

        }
        public View_PR_ACCOUNT()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            initial();
            initialExcel();
            initialJobTalend();
            NavEvents();
            WindowTitle.Content = "< "+ BankCode +" , "+ dateArrete+ " , V " +version+ " >";
            radDocking.Width = 258;
            //radDocking.MaxWidth = 520;
            radDocking.Height = 750;
        }
        public void initial()
        {
            stackUser.Children.Add(AP);
            stackData.Children.Add(PR_Account_U);
            ParampanelNav.Children.Clear();
            JobpanelNav.Children.Clear();
            DatapanelNav.Children.Clear();
            CoreppanelNav.Children.Clear();
            AdminpanelNav.Children.Clear();
            ReportingNav.Children.Clear();

            PRcontents = PR_ACCOUNT_MODEL.getPRTables();
            SousParamCrudLixtBox.ItemsSource = PRcontents;
            SousParamCrudLixtBox.SelectedItem = SousParamCrudLixtBox.Items[0];

            DTcontents = PR_ACCOUNT_MODEL.getDTTables();
            SousDataCrudLixtBox.ItemsSource = DTcontents;
            SousDataCrudLixtBox.SelectedItem = SousParamCrudLixtBox.Items[0];

            Usercontents = PR_ACCOUNT_MODEL.getAdminList();
            AdminListBox.ItemsSource = Usercontents;
            AdminListBox.SelectedItem = AdminListBox.Items[0];

            Reportingcontents = PR_ACCOUNT_MODEL.getReportingTables();
            ReportingListBox.ItemsSource = Reportingcontents;
            //ReportingListBox.SelectedItem = ReportingListBox.Items[0];

            /*stackData.Children.Clear();
            stackData.Children.Add(PR_Account_U);*/
            UserConnect.Content = Globals.getUser()[1]+" "+ Globals.getUser()[2]+" ( "+ Globals.getUser()[4]+" )";
            TimeConnect.Content = DateTime.Now.ToString("dd-MM-yyyy' -- 'hh':'mm");
            groupe = Globals.getUser()[4];
            Globals.checkAdminList(groupe, AdminNav, AdminpanelNav);
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        public void logout()
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
            OpenAnalyse o = new OpenAnalyse();
            o.Show();
            this.Close();
        }

        private void JobTalend_Click(object sender, RoutedEventArgs e)
        {
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Hidden;
            GroupBoxReporting.Visibility = Visibility.Visible;
            stackReporting.Children.Clear();
            stackReporting.Children.Add(FT_ASSET);
        }

        private void DataCrud_Click(object sender, RoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Visible;
            stackDataDT.Children.Clear();
            stackDataDT.Children.Add(DT_Customer_U);
        }

        private void ExcelBrowse_Click(object sender, RoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Visible;
            stackDataDT.Children.Clear();
            stackDataDT.Children.Add(CRM_U);
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
            //GroupBoxData.Visibility = Visibility.Visible;
            GroupBoxJob.Visibility = Visibility.Hidden;
            GroupBoxReporting.Visibility = Visibility.Hidden;

            DTcontents = PR_ACCOUNT_MODEL.getDTTables();
            SousDataCrudLixtBox.ItemsSource = DTcontents;
            SousDataCrudLixtBox.SelectionChanged += delegate {
                    SousDataCrudLixtBox.ItemsSource = DTcontents;
                GroupBoxData.Visibility = Visibility.Visible;
                if ((SousDataCrudLixtBox.SelectedItem.Equals(DTcontents[0])))
                    {
                        stackData.Children.Clear();
                        stackData.Children.Add(DT_Aux_Account_U);
                    }
                else if ((SousDataCrudLixtBox.SelectedItem.Equals(DTcontents[1])))
                {
                    stackData.Children.Clear();
                    stackData.Children.Add(DT_Aux_Account_U);
                }
            };
        }

        ///////***************************************************************
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxJob.Visibility = Visibility.Visible;
            GroupBoxReporting.Visibility = Visibility.Hidden;
            JobTalendController.Jobs_choix(stack, sender, ResultJob, ResultJobErrors,
             NomJobResult, CheminJobResult, LancerJobBtn);

        }
        private void LancerJobBtn_Click(object sender, RoutedEventArgs e)
        {
            JobTalendController.runJob2(sender, ResultJob, ResultJobErrors, NomJobResult, CheminJobResult, LancerJobBtn);
        }
        public void initialJobTalend()
        {
            //JobTalendController.initial(trvMenu);
        }

        private void ParamCrudNav_Click(object sender, RoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Visible;
            stackData.Children.Clear();
            stackData.Children.Add(PR_Account_U);
        }

        private void SousParamCrudLixtBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //GroupBoxData.Visibility = Visibility.Visible;
            GroupBoxJob.Visibility = Visibility.Hidden;
            GroupBoxReporting.Visibility = Visibility.Hidden;

            PRcontents = PR_ACCOUNT_MODEL.getPRTables();
            SousParamCrudLixtBox.ItemsSource = PRcontents;
            SousParamCrudLixtBox.SelectionChanged += delegate {
                SousParamCrudLixtBox.ItemsSource = PRcontents;
                GroupBoxData.Visibility = Visibility.Visible;
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
                    stackData.Children.Add(PR_Guarantee_U);
                }
                else if ((SousParamCrudLixtBox.SelectedItem.Equals(PRcontents[6])))
                {
                    stackData.Children.Clear();
                    stackData.Children.Add(PR_Internal_Segment_U);
                }
                else if ((SousParamCrudLixtBox.SelectedItem.Equals(PRcontents[7])))
                {
                    stackData.Children.Clear();
                    stackData.Children.Add(PR_SECURITY_TYPE_U);
                }
            }; 
        }

        private void AdminNav_Click(object sender, RoutedEventArgs e)
        {
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxJob.Visibility = Visibility.Hidden;
            GroupBoxCorep.Visibility = Visibility.Hidden;
            GroupBoxUser.Visibility = Visibility.Visible;
            GroupBoxReporting.Visibility = Visibility.Hidden;
            JobpanelNav.Children.Clear();
            ParampanelNav.Children.Clear();
            CoreppanelNav.Children.Clear();
            DatapanelNav.Children.Clear();
            ReportingNav.Children.Clear();
            AdminpanelNav.Children.Clear();
            try
            {
                AdminpanelNav.Children.Add(AdminListBox);
            }
            catch (System.ArgumentException)
            {
                AdminpanelNav.Children.Clear();
                AdminpanelNav.Children.Add(AdminpanelNav);
            }
        }

        private void RepportingNav_Click(object sender, RoutedEventArgs e)
        {
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxJob.Visibility = Visibility.Hidden;
            GroupBoxCorep.Visibility = Visibility.Hidden;
            GroupBoxUser.Visibility = Visibility.Hidden;
            GroupBoxReporting.Visibility = Visibility.Visible;
            JobpanelNav.Children.Clear();
            DatapanelNav.Children.Clear();
            CoreppanelNav.Children.Clear();
            AdminpanelNav.Children.Clear();
            ReportingNav.Children.Clear();
            ReportingListBox.SelectedItem = ReportingListBox.Items[0];
            stackReporting.Children.Clear();
            stackReporting.Children.Add(FT_COREP_CA);
            try
            {
                ReportingNav.Children.Add(ReportingListBox);
            }
            catch (System.ArgumentException)
            {
                ReportingNav.Children.Clear();
                ReportingNav.Children.Add(ReportingListBox);
            }
        }

        private void AdminListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                if (this.WindowState != WindowState.Maximized)
                    this.WindowState = WindowState.Maximized;
                else
                    this.WindowState = WindowState.Normal;
            }
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ReportingListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxJob.Visibility = Visibility.Hidden;
            //GroupBoxReporting.Visibility = Visibility.Visible;

            Reportingcontents = PR_ACCOUNT_MODEL.getReportingTables();
            ReportingListBox.ItemsSource = Reportingcontents;
            ReportingListBox.SelectionChanged += delegate {
                ReportingListBox.ItemsSource = Reportingcontents;
                if ((ReportingListBox.SelectedItem.Equals(Reportingcontents[0])))
                {
                    stackReporting.Children.Clear();
                    stackReporting.Children.Add(FT_COREP_CA);
                }
                else if ((ReportingListBox.SelectedItem.Equals(Reportingcontents[1])))
                {
                    stackReporting.Children.Clear();
                    stackReporting.Children.Add(FT_COREP_CR);
                }
                else if ((ReportingListBox.SelectedItem.Equals(Reportingcontents[2])))
                {
                    stackReporting.Children.Clear();
                    stackReporting.Children.Add(FT_ASSET);
                }
                else if ((ReportingListBox.SelectedItem.Equals(Reportingcontents[3])))
                {
                    stackReporting.Children.Clear();
                    stackReporting.Children.Add(FT_ARC);
                }
                else if ((ReportingListBox.SelectedItem.Equals(Reportingcontents[4])))
                {
                    stackReporting.Children.Clear();
                    stackReporting.Children.Add(FT_CUSOMER_COMMITMENT);
                }
                else if ((ReportingListBox.SelectedItem.Equals(Reportingcontents[5])))
                {
                    stackReporting.Children.Clear();
                    stackReporting.Children.Add(fT_CUSOMER_CMDR);
                }
            };
        }

        private void Quitter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditAnalyse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                    NewAnalyse n = new NewAnalyse(); 
                    n.Show();
                    n.Entity.Visibility = Visibility.Hidden;

                    n.EntityName.Visibility = Visibility.Visible;
                    n.EntityName.Content = BankCode;
                    
                    n.DateEntity.Visibility = Visibility.Hidden;
                    n.ProcessDateName.Visibility = Visibility.Visible;
                    n.ProcessDateName.Content = dateArrete;

                    n.CopyEntity.Visibility =Visibility.Hidden;
                    n.Datacopy.Visibility = Visibility.Hidden;
                    n.CopieLabel.Visibility = Visibility.Hidden; 
                    n.CopieLabelName.Visibility = Visibility.Hidden;
                    List<string> list = new List<string>();
                    list = AnalyseModel.getDataEntityAnalyse(BankCode, dateArrete,version);
                    Console.WriteLine("that y !" + list[0]);
                    n.TypeAnalyse.SelectedValue = list[4];
                    n.DescriptionEntity.Text = list[2];
                    n.Height = 300;
                    n.MinHeight = 300;

                    n.TitleWin.Content = "Modifier Analyse";
                    n.setIsedit(true);
                    /*n.Closed += delegate {
                        initial();
                    };*/
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Veuillez Sélectionner une ligne ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : \n \n " + ex.Message + " \n \n " + ex.StackTrace);
            }
        }

        private void RadDocking_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
        }

        private void PR_ACCOUNT_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Visible;
            stackData.Children.Clear();
            stackData.Children.Add(PR_Account_U);
        }

        private void PR_BASEL_PORT_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Visible;
            stackData.Children.Clear();
            stackData.Children.Add(PR_Portfolio_U);
        }

        private void PR_BASEL_CATEGO_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Visible;
            stackData.Children.Clear();
            stackData.Children.Add(PR_Category_U);
        }

        private void PR_CURR_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Visible;
            stackData.Children.Clear();
            stackData.Children.Add(PR_Currency_U);
        }

        private void PR_ECO_SECT_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Visible;
            stackData.Children.Clear();
            stackData.Children.Add(PR_Economic_U);
        }

        private void PR_GUARR_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Visible;
            stackData.Children.Clear();
            stackData.Children.Add(PR_Guarantee_U);
        }

        private void PR_INTER_SEG_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Visible;
            stackData.Children.Clear();
            stackData.Children.Add(PR_Internal_Segment_U);
        }

        private void PR_SECURITY_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Visible;
            stackData.Children.Clear();
            stackData.Children.Add(PR_SECURITY_TYPE_U);
        }

        private void FT_ASSET_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Hidden;
            GroupBoxReporting.Visibility = Visibility.Visible;
            stackReporting.Children.Clear();
            stackReporting.Children.Add(FT_ASSET);

        }

        private void FT_ARC_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Hidden;
            GroupBoxReporting.Visibility = Visibility.Visible;
            stackReporting.Children.Clear();
            stackReporting.Children.Add(FT_ARC);
        }

        private void FT_COST_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Hidden;
            GroupBoxReporting.Visibility = Visibility.Visible;
            stackReporting.Children.Clear();
            stackReporting.Children.Add(fT_CUSOMER_CMDR);
        }

        private void FT_COST_COM_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Hidden;
            GroupBoxReporting.Visibility = Visibility.Visible;
            stackReporting.Children.Clear();
            stackReporting.Children.Add(FT_CUSOMER_COMMITMENT);
        }

        private void FT_COREP_CA_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Hidden;
            GroupBoxReporting.Visibility = Visibility.Visible;
            stackReporting.Children.Clear();
            stackReporting.Children.Add(FT_COREP_CA);
        }

        private void FT_COREP_CR_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Hidden;
            GroupBoxReporting.Visibility = Visibility.Visible;
            stackReporting.Children.Clear();
            stackReporting.Children.Add(FT_COREP_CR);
        }

        private void DT_Client_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Visible;
            stackDataDT.Children.Clear();
            stackDataDT.Children.Add(DT_Customer_U);
        }

        private void DT_Compte_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Visible;
            stackDataDT.Children.Clear();
            stackDataDT.Children.Add(DT_Aux_Account_U);
        }

        private void DT_Guarantie_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Visible;
            stackDataDT.Children.Clear();
            stackDataDT.Children.Add(DT_Guarantie_U);
        }

        private void DT_DEFAULT_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Visible;
            stackDataDT.Children.Clear();
            stackDataDT.Children.Add(DT_Default_U);
        }

        private void DT_PROVISION_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Visible;
            stackDataDT.Children.Clear();
            stackDataDT.Children.Add(DT_Provision_U);
        }

        private void SA_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Visible;
            stackDataDT.Children.Clear();
            stackDataDT.Children.Add(SA_U);
        }
        private void CRM_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Visible;
            stackDataDT.Children.Clear();
            stackDataDT.Children.Add(CRM_U);
        }

        private void CMDR335_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Visible;
            stackDataDT.Children.Clear();
            stackDataDT.Children.Add(CMDR335_U);
        }

        private void CMDR337_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Visible;
            stackDataDT.Children.Clear();
            stackDataDT.Children.Add(CMDR337_U);
        }

        private void CMDR339_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Visible;
            stackDataDT.Children.Clear();
            stackDataDT.Children.Add(CMDR339_U);
        }

        private void CMDR_Click(object sender, RoutedEventArgs e)
        {
            GroupBoxReporting.Visibility = Visibility.Hidden;
            GroupBoxData.Visibility = Visibility.Hidden;
            GroupBoxDataDT.Visibility = Visibility.Visible;
            stackDataDT.Children.Clear();
            stackDataDT.Children.Add(CMDR335_U);
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
