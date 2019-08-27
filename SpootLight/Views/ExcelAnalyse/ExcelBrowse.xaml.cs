using SpootLight.Controllers;
using System;
using System.Collections.Generic;
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

namespace SpootLight.Views.ExcelAnalyse
{
    /// <summary>
    /// Logique d'interaction pour ExcelBrowse.xaml
    /// </summary>
    public partial class ExcelBrowse : Window
    {
        private object dummyNode = null;
        public object path = "C:\\java\\COREP\\2018-12-31\\CR\\";
        public ExcelBrowse()
        {
            InitializeComponent();
            initial();
        }
        public string SelectedImagePath { get; set; }
        public void initial()
        {
            foreach (string s in Directory.GetLogicalDrives())
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = s;
                item.Tag = s;
                item.FontWeight = FontWeights.Normal;
                item.Items.Add(dummyNode);
                item.Expanded += new RoutedEventHandler(folder_Expanded);
                foldersItem.Items.Add(item);
            }


            TreeViewItem item1 = new TreeViewItem();
            item1.Header = path;
            item1.Tag = path;
            item1.FontWeight = FontWeights.Normal;
            item1.Items.Add(dummyNode);
            item1.Expanded += new RoutedEventHandler(folder_Expanded);
            foldersItem.Items.Add(item1);

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
                        //  if (!s.Contains(".xls"))
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);
                        item.Items.Add(subitem);
                    }
                    //NACHI   Directory.GetFiles
                    foreach (string s in Directory.GetFiles(item.Tag.ToString()))
                    {
                        TreeViewItem subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Items.Add(dummyNode);
                        // if (!s.Contains(".xls") )
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);
                        item.Items.Add(subitem);
                        // System.Diagnostics.Process.Start(@"C:\Users\Dell E6540\Desktop\DOT NET\WPF_Explorer_Tree\Financial Sample.xlsx");
                    }


                }
                catch (Exception) { }
            }
        }

        private void foldersItem_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
        }
        private void ExcelBtn_Click(object sender, RoutedEventArgs e)
        {
            PopUpExcel.Visibility = Visibility.Hidden;
            System.Diagnostics.Process.Start(@SelectedImagePath);
        }

        private void AppBtn_Click(object sender, RoutedEventArgs e)
        {
            PopUpExcel.Visibility = Visibility.Hidden;
            ExcelShow t = new ExcelShow();

            MessageBox.Show(SelectedImagePath);
            t.SelectedImagePath = SelectedImagePath;
            t.Sheet.Load(SelectedImagePath);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            PopUpExcel.Visibility = Visibility.Hidden;
        }

        private void Consulter_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem temp = ((TreeViewItem)foldersItem.SelectedItem);

            if (temp == null)
                return;
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
                PopUpExcel.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("Format ou fichier non supportée ! \nVeuillez sélectionner des fichier Excel (.xlsx)", "Fichier Non Supportée !" , MessageBoxButton.OK,MessageBoxImage.Stop);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonCloseBrowser_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
