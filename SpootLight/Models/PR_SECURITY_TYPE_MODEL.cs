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
    class PR_SECURITY_TYPE_MODEL
    {
        readonly SpotLightDATAEntities crudctx = new SpotLightDATAEntities();
        private List<PR_SECURITY_TYPE> Security;
        public PR_SECURITY_TYPE_MODEL()
        {
            FillCategories();
        }

        private void FillCategories()
        {

            var q = (from a in crudctx.PR_SECURITY_TYPE
                     select a).Distinct().ToList();
            this.Security = q;
        }
        public List<PR_SECURITY_TYPE> SECURITY
        {
            get
            {
                return Security;
            }
            set
            {
                Security = value;
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
        public void InsertOrUpdate(PR_SECURITY_TYPE acc)
        {

            var UpdatedorInserted =
                crudctx.PR_SECURITY_TYPE.Where(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Security_Type.Equals(acc.Security_Type) && c.Version.Equals(acc.Version)).FirstOrDefault();

            if (crudctx.PR_SECURITY_TYPE.Any(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Security_Type.Equals(acc.Security_Type) && c.Version.Equals(acc.Version)))
            {

                UpdatedorInserted.Security_Type = acc.Security_Type;
                UpdatedorInserted.Process_Date = acc.Process_Date;
                UpdatedorInserted.Version = acc.Version;
                UpdatedorInserted.Bank_Code = acc.Bank_Code;

                UpdatedorInserted.Sub_Portfolio = acc.Sub_Portfolio;

                MessageBox.Show("Modifiée");
            }

            else
            {
                crudctx.PR_SECURITY_TYPE.Add(acc);
                MessageBox.Show("Ajoutée");
            }

            crudctx.SaveChanges();
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
                            string PK_Security = selectedrows[3].ToString();
                            var deleteSecurityType = crudctx.PR_SECURITY_TYPE.Where(m => m.Bank_Code.Equals(PK_CodeBank) && m.Process_Date.Equals(PK_ProcessDate) && m.Security_Type.Equals(PK_Security) && m.Version.Equals(PK_Version)).Single();
                            crudctx.PR_SECURITY_TYPE.Remove(deleteSecurityType);
                            crudctx.SaveChanges();
                        }
                        MessageBox.Show("Supprimée");
                        g.ItemsSource = crudctx.PR_SECURITY_TYPE.ToList();
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
