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
using System.Windows.Media;

namespace SpootLight.Models
{
    class DT_GARANTE_MODEL
    {
        readonly SpotLightDATAEntities crudctx = new SpotLightDATAEntities();
        public static List<string> Tables;
        public DT_GARANTE_MODEL()
        {
            FillAccounts();
        }

        private void FillAccounts()
        {

            var q = (from a in crudctx.DT_GUARANTEE
                     select a).Distinct().Take(10000).ToList();
            this.garanteie = q;

        }
        private List<DT_GUARANTEE> garanteie;
        public List<DT_GUARANTEE> GUAR
        {
            get
            {
                return garanteie;
            }
            set
            {
                garanteie = value;
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

        public void InsertOrUpdate(DT_GUARANTEE acc)
        {

            var UpdatedorInserted =
                crudctx.DT_GUARANTEE.Where(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Internal_Guarantee_Number.Equals(acc.Internal_Guarantee_Number) && c.Internal_Contract_Number.Equals(acc.Internal_Contract_Number) && c.Version.Equals(acc.Version)).FirstOrDefault();

            if (crudctx.DT_GUARANTEE.Any(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Internal_Guarantee_Number.Equals(acc.Internal_Guarantee_Number) && c.Internal_Contract_Number.Equals(acc.Internal_Contract_Number) && c.Version.Equals(acc.Version)))
            {

                UpdatedorInserted.Internal_Contract_Number = acc.Internal_Contract_Number;
                UpdatedorInserted.Internal_Guarantee_Number = acc.Internal_Guarantee_Number;
                UpdatedorInserted.Process_Date = acc.Process_Date;
                UpdatedorInserted.Bank_Code = acc.Bank_Code;
                UpdatedorInserted.Version = acc.Version;

                UpdatedorInserted.Guarantee_type = acc.Guarantee_type;
                UpdatedorInserted.Guarantee_Customer = acc.Guarantee_Customer;
                UpdatedorInserted.Guarantee_currency = acc.Guarantee_currency;
                UpdatedorInserted.GL_internal_Number = acc.GL_internal_Number;
                UpdatedorInserted.Guarantee_assigned_amount = acc.Guarantee_assigned_amount;
                UpdatedorInserted.Guarantee_amount = acc.Guarantee_amount;
                UpdatedorInserted.Value_Date = acc.Value_Date;
                UpdatedorInserted.End_Date = acc.End_Date;
                UpdatedorInserted.Attribute_1 = acc.Attribute_1;
                UpdatedorInserted.Attribute_2 = acc.Attribute_2;
                UpdatedorInserted.Attribute_3 = acc.Attribute_3;
                UpdatedorInserted.Attribute_4 = acc.Attribute_4;
                UpdatedorInserted.Attribute_5 = acc.Attribute_5;

                MessageBox.Show("Modifiée");
            }

            else
            {
                crudctx.DT_GUARANTEE.Add(acc);
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
                            string PK_Internal_Guar = selectedrows[3].ToString();
                            string PK_Internal_Contr = selectedrows[4].ToString();
                            var deleteGuar = crudctx.DT_GUARANTEE.Where(m => m.Bank_Code.Equals(PK_CodeBank) && m.Process_Date.Equals(PK_ProcessDate) && m.Internal_Guarantee_Number.Equals(PK_Internal_Guar) && m.Internal_Contract_Number.Equals(PK_Internal_Contr) && m.Version.Equals(PK_Version)).Single();
                            crudctx.DT_GUARANTEE.Remove(deleteGuar);
                            crudctx.SaveChanges();
                        }
                        MessageBox.Show("Supprimée");
                        g.ItemsSource = crudctx.DT_GUARANTEE.ToList();
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

        public void FormController(TextBox txt)
        {
            SaisieInt(txt);
        }

        static float SaisieInt(TextBox txt)
        {
            float num = 0;
            string atr;
            bool err;
            do
            {
                try
                {
                    err = false;
                    Console.WriteLine(txt.Text);
                    atr = txt.Text;
                    num = float.Parse(atr);
                }
                catch (System.FormatException)
                {
                    txt.BorderBrush = Brushes.Red;
                    MessageBoxResult M = MessageBox.Show("Le Numéro de Compte doit etre Numérique !", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                    if (M == MessageBoxResult.OK)
                    {
                        err = false;
                    }
                    else
                    {
                        err = true;
                        txt.BorderBrush = Brushes.Green;
                    }
                }
            } while (err);

            return num;

        }
    }
}
