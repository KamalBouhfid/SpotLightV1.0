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
    class PR_GUARANTEE_TYPE_MODEL
    {
        readonly SpotLightDATAEntities crudctx = new SpotLightDATAEntities();
        private List<PR_GUARANTEE_TYPE> Guarantee;
        public PR_GUARANTEE_TYPE_MODEL()
        {
            FillCategories();
        }

        private void FillCategories()
        {

            var q = (from a in crudctx.PR_GUARANTEE_TYPE
                     select a).Distinct().ToList();
            this.Guarantee = q;

        }
        public List<PR_GUARANTEE_TYPE> GUARANTEES
        {
            get
            {
                return Guarantee;
            }
            set
            {
                Guarantee = value;
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
        public void InsertOrUpdate(PR_GUARANTEE_TYPE acc)
        {

            var UpdatedorInserted =
                crudctx.PR_GUARANTEE_TYPE.Where(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Guarantee_Account.Equals(acc.Guarantee_Account)).FirstOrDefault();

            if (crudctx.PR_GUARANTEE_TYPE.Any(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Guarantee_Account.Equals(acc.Guarantee_Account)))
            {

                UpdatedorInserted.Guarantee_Account = acc.Guarantee_Account;
                UpdatedorInserted.Process_Date = acc.Process_Date;
                UpdatedorInserted.Bank_Code = acc.Bank_Code;

                UpdatedorInserted.Guarantee_Type_Description = acc.Guarantee_Type_Description;
                UpdatedorInserted.Guarantee_Class = acc.Guarantee_Class;
                UpdatedorInserted.Guarantee_Weighting = acc.Guarantee_Weighting;

                MessageBox.Show("Bien Modifiée !");
            }

            else
            {
                crudctx.PR_GUARANTEE_TYPE.Add(acc);
                MessageBox.Show("Bien Ajoutée !");
            }

            crudctx.SaveChanges();
        }
        public void delete(DataGrid g)
        {
            DataRowView row = g.SelectedItem as DataRowView;
            string PK_CodeBank = row.Row.ItemArray[0].ToString();
            string PK_ProcessDate = row.Row.ItemArray[1].ToString();
            string PK_GuaranteeAccount = row.Row.ItemArray[2].ToString();
            var deletePortfolio = crudctx.PR_GUARANTEE_TYPE.Where(m => m.Bank_Code.Equals(PK_CodeBank) && m.Process_Date.Equals(PK_ProcessDate) && m.Guarantee_Account.Equals(PK_GuaranteeAccount)).Single();
            crudctx.PR_GUARANTEE_TYPE.Remove(deletePortfolio);
            crudctx.SaveChanges();
            MessageBox.Show("Bien Supprimée !");
            g.ItemsSource = crudctx.PR_GUARANTEE_TYPE.ToList();
        }
    }
}
