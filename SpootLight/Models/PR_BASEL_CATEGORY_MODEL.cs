using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SpootLight.Models
{
    class PR_BASEL_CATEGORY_MODEL
    {
        readonly SpotLightDATAEntities crudctx = new SpotLightDATAEntities();
        public static List<string> Tables;
        private List<PR_BASEL_CATEGORY> categories;
        public PR_BASEL_CATEGORY_MODEL()
        {
            FillCategories();
        }
        private void FillCategories()
        {

            var q = (from a in crudctx.PR_BASEL_CATEGORY
                     select a).Distinct().ToList();
            this.categories = q;

            /* foreach (PR_ACCOUNT k in q)
             {
                 Console.WriteLine(" hhhhhhhh " + k.Account + "  " + k.Bank_Code + "  " + k.Process_Date);
             } */

        }
        public List<PR_BASEL_CATEGORY> CATEGORIES
        {
            get
            {
                return categories;
            }
            set
            {
                categories = value;
                NotifyPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void InsertOrUpdate(PR_BASEL_CATEGORY acc)
        {

            var UpdatedorInserted =
                crudctx.PR_BASEL_CATEGORY.Where(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Sub_Category.Equals(acc.Sub_Category)).FirstOrDefault();

            if (crudctx.PR_BASEL_CATEGORY.Any(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Sub_Category.Equals(acc.Sub_Category)))
            {

                UpdatedorInserted.Sub_Category = acc.Sub_Category;
                UpdatedorInserted.Process_Date = acc.Process_Date;
                UpdatedorInserted.Bank_Code = acc.Bank_Code;
                UpdatedorInserted.Version = acc.Version;

                UpdatedorInserted.Category = acc.Category;
                UpdatedorInserted.Category_Description = acc.Category_Description;

                UpdatedorInserted.Sub_Category_Description = acc.Sub_Category_Description;
                UpdatedorInserted.Sub_Portfolio = acc.Sub_Portfolio;

                MessageBox.Show("Modifiée");
            }

            else
            {
                crudctx.PR_BASEL_CATEGORY.Add(acc);
                MessageBox.Show("Ajoutée");
            }

            crudctx.SaveChanges();
        }
        public void deleteTest(DataGrid g)
        {
            try
            {
            DataRowView row = g.SelectedItem as DataRowView;
                if (row != null)
                {
                    MessageBoxResult d = MessageBox.Show("Voulez-vous vraiment supprimer la(les) ligne(s) sélectionnée(s) ?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (d == MessageBoxResult.Yes)
                    {
                        string PK_CodeBank = row.Row.ItemArray[0].ToString();
                        string PK_ProcessDate = row.Row.ItemArray[1].ToString();
                        string PK_SubCategory = row.Row.ItemArray[5].ToString();
                        string PK_Version = row.Row.ItemArray[2].ToString();
                        var deleteCategory = crudctx.PR_BASEL_CATEGORY.Where(m => m.Bank_Code.Equals(PK_CodeBank) && m.Process_Date.Equals(PK_ProcessDate) && m.Sub_Category.Equals(PK_SubCategory) && m.Version.Equals(PK_Version)).Single();
                        crudctx.PR_BASEL_CATEGORY.Remove(deleteCategory);
                        crudctx.SaveChanges();
                        MessageBox.Show("Supprimée");
                        g.ItemsSource = crudctx.PR_BASEL_CATEGORY.ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Sélectionnez une ligne ");
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Sélectionnez une ligne ");
            }
        }
        public void delete(DataGrid g)
        {
            try
            {
                DataRowView row = g.SelectedItem as DataRowView;
                if (row != null)
                {
                    MessageBoxResult d = MessageBox.Show("Voulez-vous vraiment supprimer la(les) ligne(s) sélectionnée(s) ?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (d == MessageBoxResult.Yes)
                    {
                        foreach (DataRowView selectedrows in g.SelectedItems)
                        {
                            string PK_CodeBank = selectedrows[0].ToString();
                            string PK_ProcessDate = selectedrows[1].ToString();
                            string PK_Version = selectedrows[2].ToString();
                            string PK_SubCategory = selectedrows[5].ToString();
                            var deletePortfolio = crudctx.PR_BASEL_CATEGORY.Where(m => m.Bank_Code.Equals(PK_CodeBank) && m.Process_Date.Equals(PK_ProcessDate) && m.Version.Equals(PK_Version) && m.Sub_Category.Equals(PK_SubCategory)).Single();
                            crudctx.PR_BASEL_CATEGORY.Remove(deletePortfolio);
                            crudctx.SaveChanges();
                        }
                        MessageBox.Show("Supprimée");
                        g.ItemsSource = crudctx.PR_BASEL_CATEGORY.ToList();
                    }
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
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : \n \n " + ex.Message + " \n \n " + ex.StackTrace);
            }
        }
    }
}
