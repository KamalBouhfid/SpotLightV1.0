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
    class PR_CURRENCY_MODEL
    {
        readonly SpotLightDATAEntities crudctx = new SpotLightDATAEntities();
        private List<PR_CURRENCY> Currency;
        public PR_CURRENCY_MODEL()
        {
            FillCategories();
        }

        private void FillCategories()
        {

            var q = (from a in crudctx.PR_CURRENCY
                     select a).Distinct().ToList();
            this.Currency = q;

        }
        public List<PR_CURRENCY> CURENCICES
        {
            get
            {
                return Currency;
            }
            set
            {
                Currency = value;
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
        public void InsertOrUpdate(PR_CURRENCY acc)
        {

            var UpdatedorInserted =
                crudctx.PR_CURRENCY.Where(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Currency.Equals(acc.Currency)).FirstOrDefault();

            if (crudctx.PR_CURRENCY.Any(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Currency.Equals(acc.Currency)))
            {

                UpdatedorInserted.Currency = acc.Currency;
                UpdatedorInserted.Process_Date = acc.Process_Date;
                UpdatedorInserted.Bank_Code = acc.Bank_Code;

                UpdatedorInserted.Exchange_Rate = acc.Exchange_Rate;

                MessageBox.Show("Bien Modifiée !");
            }

            else
            {
                crudctx.PR_CURRENCY.Add(acc);
                MessageBox.Show("Bien Ajoutée !");
            }

            crudctx.SaveChanges();
        }
        public void delete(DataGrid g)
        {
            DataRowView row = g.SelectedItem as DataRowView;
            string PK_CodeBank = row.Row.ItemArray[0].ToString();
            string PK_ProcessDate = row.Row.ItemArray[1].ToString();
            string PK_Currency = row.Row.ItemArray[2].ToString();
            var deletePortfolio = crudctx.PR_CURRENCY.Where(m => m.Bank_Code.Equals(PK_CodeBank) && m.Process_Date.Equals(PK_ProcessDate) && m.Currency.Equals(PK_Currency)).Single();
            crudctx.PR_CURRENCY.Remove(deletePortfolio);
            crudctx.SaveChanges();
            MessageBox.Show("Bien Supprimée !");
            g.ItemsSource = crudctx.PR_CURRENCY.ToList();
        }
    }
}
