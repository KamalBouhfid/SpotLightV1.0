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
                crudctx.PR_CURRENCY.Where(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Currency.Equals(acc.Currency) && c.Version.Equals(acc.Version)).FirstOrDefault();

            if (crudctx.PR_CURRENCY.Any(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Currency.Equals(acc.Currency) && c.Version.Equals(acc.Version)))
            {

                UpdatedorInserted.Currency = acc.Currency;
                UpdatedorInserted.Process_Date = acc.Process_Date;
                UpdatedorInserted.Version = acc.Version;
                UpdatedorInserted.Bank_Code = acc.Bank_Code;

                UpdatedorInserted.Exchange_Rate = acc.Exchange_Rate;

                MessageBox.Show("Modifiée");
            }

            else
            {
                crudctx.PR_CURRENCY.Add(acc);
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
                        string PK_Version = row.Row.ItemArray[2].ToString();
                        string PK_Currency = row.Row.ItemArray[3].ToString();
                        var deletePortfolio = crudctx.PR_CURRENCY.Where(m => m.Bank_Code.Equals(PK_CodeBank) && m.Process_Date.Equals(PK_ProcessDate) && m.Currency.Equals(PK_Currency) && m.Version.Equals(PK_Version)).Single();
                        crudctx.PR_CURRENCY.Remove(deletePortfolio);
                        crudctx.SaveChanges();
                        MessageBox.Show("Supprimée");
                        g.ItemsSource = crudctx.PR_CURRENCY.ToList();
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
                            string PK_Currency = selectedrows[3].ToString();
                            string PK_Version = selectedrows[2].ToString();
                            var deletePortfolio = crudctx.PR_CURRENCY.Where(m => m.Bank_Code.Equals(PK_CodeBank) && m.Process_Date.Equals(PK_ProcessDate) && m.Currency.Equals(PK_Currency) && m.Version.Equals(PK_Version)).Single();
                            crudctx.PR_CURRENCY.Remove(deletePortfolio);
                            crudctx.SaveChanges();
                        }
                        MessageBox.Show("Supprimée");
                        g.ItemsSource = crudctx.PR_CURRENCY.ToList();
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
                MessageBox.Show("Erreur :"+ex.Message +"  \n "+ex.StackTrace);
            }
        }
    }
}
