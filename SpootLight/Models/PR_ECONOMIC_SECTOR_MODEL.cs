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
    class PR_ECONOMIC_SECTOR_MODEL
    {
        readonly SpotLightDATAEntities crudctx = new SpotLightDATAEntities();
        private List<PR_ECONOMIC_SECTOR> economic;
        public PR_ECONOMIC_SECTOR_MODEL()
        {
            FillCategories();
        }

        private void FillCategories()
        {

            var q = (from a in crudctx.PR_ECONOMIC_SECTOR
                     select a).Distinct().ToList();
            this.economic = q;

        }

        public List<PR_ECONOMIC_SECTOR> ECONOMICS
        {
            get
            {
                return economic;
            }
            set
            {
                economic = value;
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
        public void InsertOrUpdate(PR_ECONOMIC_SECTOR acc)
        {

            var UpdatedorInserted =
                crudctx.PR_ECONOMIC_SECTOR.Where(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Economic_Sector.Equals(acc.Economic_Sector)).FirstOrDefault();

            if (crudctx.PR_ECONOMIC_SECTOR.Any(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Economic_Sector.Equals(acc.Economic_Sector)))
            {

                UpdatedorInserted.Economic_Sector = acc.Economic_Sector;
                UpdatedorInserted.Process_Date = acc.Process_Date;
                UpdatedorInserted.Bank_Code = acc.Bank_Code;

                UpdatedorInserted.Description = acc.Description;
                UpdatedorInserted.Sub_Category = acc.Sub_Category;

                MessageBox.Show("Bien Modifiée !");
            }

            else
            {
                crudctx.PR_ECONOMIC_SECTOR.Add(acc);
                MessageBox.Show("Bien Ajoutée !");
            }

            crudctx.SaveChanges();
        }
        public void delete(DataGrid g)
        {
            DataRowView row = g.SelectedItem as DataRowView;
            string PK_CodeBank = row.Row.ItemArray[0].ToString();
            string PK_ProcessDate = row.Row.ItemArray[1].ToString();
            string PK_EconomicSector = row.Row.ItemArray[2].ToString();
            var deleteSector = crudctx.PR_ECONOMIC_SECTOR.Where(m => m.Bank_Code.Equals(PK_CodeBank) && m.Process_Date.Equals(PK_ProcessDate) && m.Economic_Sector.Equals(PK_EconomicSector)).Single();
            crudctx.PR_ECONOMIC_SECTOR.Remove(deleteSector);
            crudctx.SaveChanges();
            MessageBox.Show("Bien Supprimée !");
            g.ItemsSource = crudctx.PR_ECONOMIC_SECTOR.ToList();
        }
    }
}
