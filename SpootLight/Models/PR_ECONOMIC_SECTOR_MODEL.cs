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
                crudctx.PR_ECONOMIC_SECTOR.Where(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Economic_Sector.Equals(acc.Economic_Sector) && c.Version.Equals(acc.Version)).FirstOrDefault();

            if (crudctx.PR_ECONOMIC_SECTOR.Any(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Economic_Sector.Equals(acc.Economic_Sector) && c.Version.Equals(acc.Version)))
            {

                UpdatedorInserted.Economic_Sector = acc.Economic_Sector;
                UpdatedorInserted.Process_Date = acc.Process_Date;
                UpdatedorInserted.Version = acc.Version;
                UpdatedorInserted.Bank_Code = acc.Bank_Code;

                UpdatedorInserted.Description = acc.Description;
                UpdatedorInserted.Sub_Category = acc.Sub_Category;
                UpdatedorInserted.Cmdr_Weighting = acc.Cmdr_Weighting;

                MessageBox.Show("Modifiée");
            }

            else
            {
                crudctx.PR_ECONOMIC_SECTOR.Add(acc);
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
                        string PK_EconomicSector = row.Row.ItemArray[3].ToString();
                        var deleteSector = crudctx.PR_ECONOMIC_SECTOR.Where(m => m.Bank_Code.Equals(PK_CodeBank) && m.Process_Date.Equals(PK_ProcessDate) && m.Economic_Sector.Equals(PK_EconomicSector) && m.Version.Equals(PK_Version)).Single();
                        crudctx.PR_ECONOMIC_SECTOR.Remove(deleteSector);
                        crudctx.SaveChanges();
                        MessageBox.Show("Supprimée");
                        g.ItemsSource = crudctx.PR_ECONOMIC_SECTOR.ToList();
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
                            string PK_Version = selectedrows[2].ToString();
                            string PK_EconomicSector = selectedrows[3].ToString();
                            var deleteSector = crudctx.PR_ECONOMIC_SECTOR.Where(m => m.Bank_Code.Equals(PK_CodeBank) && m.Process_Date.Equals(PK_ProcessDate) && m.Economic_Sector.Equals(PK_EconomicSector) && m.Version.Equals(PK_Version)).Single();
                            crudctx.PR_ECONOMIC_SECTOR.Remove(deleteSector);
                            crudctx.SaveChanges();
                        }
                        MessageBox.Show("Supprimée");
                        g.ItemsSource = crudctx.PR_ECONOMIC_SECTOR.ToList();
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
