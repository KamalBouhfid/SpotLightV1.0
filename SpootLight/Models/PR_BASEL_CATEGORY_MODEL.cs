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

                UpdatedorInserted.Category = acc.Category;
                UpdatedorInserted.Category_Description = acc.Category_Description;

                UpdatedorInserted.Sub_Category_Description = acc.Sub_Category_Description;
                UpdatedorInserted.Sub_Portfolio = acc.Sub_Portfolio;

                MessageBox.Show("Bien Modifiée !");
            }

            else
            {
                crudctx.PR_BASEL_CATEGORY.Add(acc);
                MessageBox.Show("Bien Ajoutée !");
            }

            crudctx.SaveChanges();
        }
        public void delete(DataGrid g)
        {
            DataRowView row = g.SelectedItem as DataRowView;
            string PK_CodeBank = row.Row.ItemArray[0].ToString();
            string PK_ProcessDate = row.Row.ItemArray[1].ToString();
            string PK_SubCategory = row.Row.ItemArray[4].ToString();
            var deleteCategory = crudctx.PR_BASEL_CATEGORY.Where(m => m.Bank_Code.Equals(PK_CodeBank) && m.Process_Date.Equals(PK_ProcessDate) && m.Sub_Category.Equals(PK_SubCategory)).Single();
            crudctx.PR_BASEL_CATEGORY.Remove(deleteCategory);
            crudctx.SaveChanges();
            MessageBox.Show("Bien Supprimée !");
            g.ItemsSource = crudctx.PR_BASEL_CATEGORY.ToList();
        }
    }
}
