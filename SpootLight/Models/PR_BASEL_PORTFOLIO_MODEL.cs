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
    class PR_BASEL_PORTFOLIO_MODEL
    {
        readonly SpotLightDATAEntities crudctx = new SpotLightDATAEntities();
        private List<PR_BASEL_PORTFOLIO> portfolio;

        public PR_BASEL_PORTFOLIO_MODEL()
        {
            FillCategories();
        }

        private void FillCategories()
        {

            var q = (from a in crudctx.PR_BASEL_PORTFOLIO
                     select a).Distinct().ToList();
            this.portfolio = q;

        }

        public List<PR_BASEL_PORTFOLIO> PORTFOLIOS
        {
            get
            {
                return portfolio;
            }
            set
            {
                portfolio = value;
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
        public void InsertOrUpdate(PR_BASEL_PORTFOLIO acc)
        {

            var UpdatedorInserted =
                crudctx.PR_BASEL_PORTFOLIO.Where(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Sub_Portfolio.Equals(acc.Sub_Portfolio)).FirstOrDefault();

            if (crudctx.PR_BASEL_PORTFOLIO.Any(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Sub_Portfolio.Equals(acc.Sub_Portfolio)))
            {

                UpdatedorInserted.Sub_Portfolio = acc.Sub_Portfolio;
                UpdatedorInserted.Process_Date = acc.Process_Date;
                UpdatedorInserted.Bank_Code = acc.Bank_Code;

                UpdatedorInserted.Portfolio = acc.Portfolio;
                UpdatedorInserted.Portfolio_Description = acc.Portfolio_Description;

                UpdatedorInserted.Sub_Portfolio_Description = acc.Sub_Portfolio_Description;
                UpdatedorInserted.Weighting = acc.Weighting;

                MessageBox.Show("Bien Modifiée !");
            }

            else
            {
                crudctx.PR_BASEL_PORTFOLIO.Add(acc);
                MessageBox.Show("Bien Ajoutée !");
            }

            crudctx.SaveChanges();
        }

        public void delete(DataGrid g)
        {
            DataRowView row = g.SelectedItem as DataRowView;
            string PK_CodeBank = row.Row.ItemArray[0].ToString();
            string PK_ProcessDate = row.Row.ItemArray[1].ToString();
            string PK_SubPortfolio = row.Row.ItemArray[4].ToString();
            var deletePortfolio = crudctx.PR_BASEL_PORTFOLIO.Where(m => m.Bank_Code.Equals(PK_CodeBank) && m.Process_Date.Equals(PK_ProcessDate) && m.Sub_Portfolio.Equals(PK_SubPortfolio)).Single();
            crudctx.PR_BASEL_PORTFOLIO.Remove(deletePortfolio);
            crudctx.SaveChanges();
            MessageBox.Show("Bien Supprimée !");
            g.ItemsSource = crudctx.PR_BASEL_PORTFOLIO.ToList();
        }
    }
}
