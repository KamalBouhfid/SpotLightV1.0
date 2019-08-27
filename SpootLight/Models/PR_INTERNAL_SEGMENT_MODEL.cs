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
    class PR_INTERNAL_SEGMENT_MODEL
    {
        readonly SpotLightDATAEntities crudctx = new SpotLightDATAEntities();
        private List<PR_INTERNAL_SEGMENT> Internal_Segment;
        public PR_INTERNAL_SEGMENT_MODEL()
        {
            FillCategories();
        }

        private void FillCategories()
        {

            var q = (from a in crudctx.PR_INTERNAL_SEGMENT
                     select a).Distinct().ToList();
            this.Internal_Segment = q;
        }
        public List<PR_INTERNAL_SEGMENT> INTERNAL_SEGMENT
        {
            get
            {
                return Internal_Segment;
            }
            set
            {
                Internal_Segment = value;
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

        public void InsertOrUpdate(PR_INTERNAL_SEGMENT acc)
        {

            var UpdatedorInserted =
                crudctx.PR_INTERNAL_SEGMENT.Where(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Internal_Segment.Equals(acc.Internal_Segment) && c.Version.Equals(acc.Version)).FirstOrDefault();

            if (crudctx.PR_INTERNAL_SEGMENT.Any(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Internal_Segment.Equals(acc.Internal_Segment) && c.Version.Equals(acc.Version)))
            {

                UpdatedorInserted.Internal_Segment = acc.Internal_Segment;
                UpdatedorInserted.Process_Date = acc.Process_Date;
                UpdatedorInserted.Bank_Code = acc.Bank_Code;
                UpdatedorInserted.Version = acc.Version;

                UpdatedorInserted.Internal_Segment_Description = acc.Internal_Segment_Description;
                UpdatedorInserted.Customer_Market = acc.Customer_Market;
                UpdatedorInserted.Sub_Category = acc.Sub_Category;

                MessageBox.Show("Modifiée");
            }

            else
            {
                crudctx.PR_INTERNAL_SEGMENT.Add(acc);
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
                            string PK_Internal_Segment = selectedrows[3].ToString();
                            var deleteInternal_Segment = crudctx.PR_INTERNAL_SEGMENT.Where(m => m.Bank_Code.Equals(PK_CodeBank) && m.Process_Date.Equals(PK_ProcessDate) && m.Internal_Segment.Equals(PK_Internal_Segment) && m.Version.Equals(PK_Version)).Single();
                            crudctx.PR_INTERNAL_SEGMENT.Remove(deleteInternal_Segment);
                            crudctx.SaveChanges();
                        }
                        MessageBox.Show("Supprimée");
                        g.ItemsSource = crudctx.PR_INTERNAL_SEGMENT.ToList();
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
