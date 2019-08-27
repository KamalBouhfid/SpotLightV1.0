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
    class DT_AUX_ACCOUNT_MODEL
    {
        readonly SpotLightDATAEntities crudctx = new SpotLightDATAEntities();
        public static List<string> Tables;
        public DT_AUX_ACCOUNT_MODEL()
        {
            FillAccounts();
        }

        private void FillAccounts()
        {

            var q = (from a in crudctx.DT_AUX_ACCOUNT
                     select a).Distinct().Take(10000).ToList();
            this._accounts = q;

        }
        private List<DT_AUX_ACCOUNT> _accounts;
        public List<DT_AUX_ACCOUNT> Accounts
        {
            get
            {
                return _accounts;
            }
            set
            {
                _accounts = value;
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

        public void InsertOrUpdate(DT_AUX_ACCOUNT acc)
        {

            var UpdatedorInserted =
                crudctx.DT_AUX_ACCOUNT.Where(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Internal_Account_Number.Equals(acc.Internal_Account_Number) && c.Version.Equals(acc.Version)).FirstOrDefault();

            if (crudctx.DT_AUX_ACCOUNT.Any(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Internal_Account_Number.Equals(acc.Internal_Account_Number) && c.Version.Equals(acc.Version)))
            {

                UpdatedorInserted.Internal_Account_Number = acc.Internal_Account_Number;
                UpdatedorInserted.Process_Date = acc.Process_Date;
                UpdatedorInserted.Bank_Code = acc.Bank_Code;
                UpdatedorInserted.Version = acc.Version;


                UpdatedorInserted.Currency_Code = acc.Currency_Code;
                UpdatedorInserted.Customer_Code = acc.Customer_Code;

                UpdatedorInserted.Acounting_Branch = acc.Acounting_Branch;
                UpdatedorInserted.Opening_Date = acc.Opening_Date;
                UpdatedorInserted.Product_Code = acc.Product_Code;
                UpdatedorInserted.Accounting_Balance_in_local_currency = acc.Accounting_Balance_in_local_currency;
                UpdatedorInserted.Accounting_Balance_in_currency = acc.Accounting_Balance_in_currency;

                UpdatedorInserted.GL_internal_Number = acc.GL_internal_Number;
                UpdatedorInserted.GL_reporting_Number = acc.GL_reporting_Number;
                UpdatedorInserted.Doubtful_Status = acc.Doubtful_Status;
                UpdatedorInserted.Provision_affected_amount = acc.Provision_affected_amount;
                UpdatedorInserted.Provision_amount_total_currency = acc.Provision_amount_total_currency;
                UpdatedorInserted.Attribute_1 = acc.Attribute_1;
                UpdatedorInserted.Attribute_2 = acc.Attribute_2;
                UpdatedorInserted.Attribute_3 = acc.Attribute_3;
                UpdatedorInserted.Attribute_4 = acc.Attribute_4;
                UpdatedorInserted.Attribute_5 = acc.Attribute_5;

                MessageBox.Show("Modifiée");
            }

            else
            {
                crudctx.DT_AUX_ACCOUNT.Add(acc);
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
                            string PK_Internal_Account = selectedrows[3].ToString();
                            var deleteAccount = crudctx.DT_AUX_ACCOUNT.Where(m => m.Bank_Code.Equals(PK_CodeBank) && m.Process_Date.Equals(PK_ProcessDate) && m.Internal_Account_Number.Equals(PK_Internal_Account) && m.Version.Equals(PK_Version)).Single();
                            crudctx.DT_AUX_ACCOUNT.Remove(deleteAccount);
                            crudctx.SaveChanges();
                        }
                        MessageBox.Show("Supprimée");
                        g.ItemsSource = crudctx.DT_AUX_ACCOUNT.ToList();
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

        public void search(List<PR_ACCOUNT> accounts, PR_ACCOUNT_MODEL AccountsLists, string BankCode, string dateArrete, TextBox Search)
        {
            List<PR_ACCOUNT> myList = new List<PR_ACCOUNT>();
            accounts = AccountsLists.Accounts.Where(c => c.Bank_Code.Equals(BankCode) && c.Process_Date.Equals(dateArrete)).ToList<PR_ACCOUNT>();
            var filteredList = accounts.Where(account => account.Account.Contains(Search.Text));
            myList = filteredList.Cast<PR_ACCOUNT>().ToList();
        }
    }
}
