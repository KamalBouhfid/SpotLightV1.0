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
    class DT_CUSTOMER_MODEL
    {
        readonly SpotLightDATAEntities crudctx = new SpotLightDATAEntities();
        public static List<string> Tables;
        public DT_CUSTOMER_MODEL()
        {
            FillAccounts();
        }

        private void FillAccounts()
        {

            var q = (from a in crudctx.DT_CUSTOMER
                     select a).Distinct().Take(5000).ToList();
            this.customer = q;

        }
        private List<DT_CUSTOMER> customer;
        public List<DT_CUSTOMER> CUSTOMERS
        {
            get
            {
                return customer;
            }
            set
            {
                customer = value;
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

        public void InsertOrUpdate(DT_CUSTOMER acc)
        {

            var UpdatedorInserted =
                crudctx.DT_CUSTOMER.Where(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Customer_Code.Equals(acc.Customer_Code) && c.Version.Equals(acc.Version)).FirstOrDefault();

            if (crudctx.DT_CUSTOMER.Any(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Customer_Code.Equals(acc.Customer_Code) && c.Version.Equals(acc.Version)))
            {

                UpdatedorInserted.Customer_Code = acc.Customer_Code;
                UpdatedorInserted.Process_Date = acc.Process_Date;
                UpdatedorInserted.Bank_Code = acc.Bank_Code;
                UpdatedorInserted.Version = acc.Version;


                UpdatedorInserted.Customer_First_Name = acc.Customer_First_Name;
                UpdatedorInserted.Customer_Last_Name = acc.Customer_Last_Name;

                UpdatedorInserted.Company_Name = acc.Company_Name;
                UpdatedorInserted.Customer_Type = acc.Customer_Type;
                UpdatedorInserted.Customer_ID_Type = acc.Customer_ID_Type;
                UpdatedorInserted.Customer_ID_Number = acc.Customer_ID_Number;
                UpdatedorInserted.Company_group = acc.Company_group;

                UpdatedorInserted.Company_group_name = acc.Company_group_name;
                UpdatedorInserted.Customer_Country_Code = acc.Customer_Country_Code;
                UpdatedorInserted.Economic_Sector = acc.Economic_Sector;
                UpdatedorInserted.Activity_Area = acc.Activity_Area;
                UpdatedorInserted.Internal_Segment = acc.Internal_Segment;
                UpdatedorInserted.Internal_Notation = acc.Internal_Notation;
                UpdatedorInserted.External_Notation = acc.External_Notation;
                UpdatedorInserted.Business_Category = acc.Business_Category;
                UpdatedorInserted.Commitment_Amount_in_local_currency = acc.Commitment_Amount_in_local_currency;
                UpdatedorInserted.Default_Amount_in_local_currency = acc.Default_Amount_in_local_currency;
                UpdatedorInserted.Rating = acc.Rating;
                UpdatedorInserted.Turnover = acc.Turnover;

                UpdatedorInserted.Attribute_1 = acc.Attribute_1;
                UpdatedorInserted.Attribute_2 = acc.Attribute_2;
                UpdatedorInserted.Attribute_3 = acc.Attribute_3;
                UpdatedorInserted.Attribute_4 = acc.Attribute_4;
                UpdatedorInserted.Attribute_5 = acc.Attribute_5;

                MessageBox.Show("Modifiée");
            }

            else
            {
                crudctx.DT_CUSTOMER.Add(acc);
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
                            string PK_Customer_Code = selectedrows[3].ToString();
                            var deleteCustomer = crudctx.DT_CUSTOMER.Where(m => m.Bank_Code.Equals(PK_CodeBank) && m.Process_Date.Equals(PK_ProcessDate) && m.Customer_Code.Equals(PK_Customer_Code) && m.Version.Equals(PK_Version)).Single();
                            crudctx.DT_CUSTOMER.Remove(deleteCustomer);
                            crudctx.SaveChanges();
                        }
                        MessageBox.Show("Supprimée");
                        g.ItemsSource = crudctx.DT_CUSTOMER.ToList();
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
