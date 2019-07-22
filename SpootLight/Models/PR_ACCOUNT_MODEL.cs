using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SpootLight.Models
{
    class PR_ACCOUNT_MODEL
    {

        readonly SpotLightDATAEntities crudctx = new SpotLightDATAEntities();
        public static List<string> Tables;
        public PR_ACCOUNT_MODEL()
        {
            FillAccounts();
        }

        private void FillAccounts()
        {

            var q = (from a in crudctx.PR_ACCOUNT
                     select a).Distinct().ToList();
            this._accounts = q;

        }
        private List<PR_ACCOUNT> _accounts;
        public List<PR_ACCOUNT> Accounts
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

        public static List<string> getPRTables()
        {
            Tables = new List<string>();
                Tables.Add("  PR ACCOUNT");
                Tables.Add("  PR BASEL  CATEGORY");
                Tables.Add("  PR BASEL PORTFOLIO");
                Tables.Add("  PR CURRENCY");
                Tables.Add("  PR ECONOMIC SECTOR");
                Tables.Add("  PR GUARANTEE TYPE");
            return Tables;
        }
        public static List<string> getDTTables()
        {
            Tables = new List<string>();
            Tables.Add("  DT CONTRACT");
            Tables.Add("  DT CUSTOMER");
            return Tables;
        }

        public void InsertOrUpdate(PR_ACCOUNT acc)
        {

            var UpdatedorInserted =
                crudctx.PR_ACCOUNT.Where(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Account.Equals(acc.Account)).FirstOrDefault();

            if (crudctx.PR_ACCOUNT.Any(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Account.Equals(acc.Account)))
            {

                UpdatedorInserted.Account = acc.Account;
                UpdatedorInserted.Process_Date = acc.Process_Date;
                UpdatedorInserted.Bank_Code = acc.Bank_Code;

                UpdatedorInserted.Description = acc.Description;
                UpdatedorInserted.Basel_In = acc.Basel_In;

                UpdatedorInserted.Basel_Segment = acc.Basel_Segment;
                UpdatedorInserted.Credit_Conversion_Factor = acc.Credit_Conversion_Factor;
                UpdatedorInserted.Balance_Off_Sheet = acc.Balance_Off_Sheet;
                UpdatedorInserted.Sens_Code = acc.Sens_Code;
                UpdatedorInserted.Is_Commitment = acc.Is_Commitment;

                UpdatedorInserted.Is_Provision = acc.Is_Provision;
                UpdatedorInserted.Is_Doubtful = acc.Is_Doubtful;
                UpdatedorInserted.Is_Accrued_Interest = acc.Is_Accrued_Interest;
                MessageBox.Show("Bien Modifiée !");
            }

            else
            {
                crudctx.PR_ACCOUNT.Add(acc);
                MessageBox.Show("Bien Ajoutée !");
            }

            crudctx.SaveChanges();
        }

        public void delete(DataGrid g)
        {
            DataRowView row = g.SelectedItem as DataRowView;
            string PK_CodeBank = row.Row.ItemArray[0].ToString();
            string PK_ProcessDate = row.Row.ItemArray[1].ToString();
            string PK_Account = row.Row.ItemArray[2].ToString();
            var deleteAccount = crudctx.PR_ACCOUNT.Where(m => m.Bank_Code.Equals(PK_CodeBank) && m.Process_Date.Equals(PK_ProcessDate) && m.Account.Equals(PK_Account)).Single();
            crudctx.PR_ACCOUNT.Remove(deleteAccount);
            crudctx.SaveChanges();
            MessageBox.Show("Bien Supprimée !");
            g.ItemsSource = crudctx.PR_ACCOUNT.ToList();
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
                    txt.BorderBrush= Brushes.Red;
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
