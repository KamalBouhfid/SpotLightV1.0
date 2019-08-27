using Microsoft.Win32;
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
                Tables.Add("  PR INTRENAL SEGMENT");
                Tables.Add("  PR SECURITY TYPE");
            return Tables;
        }
        public static List<string> getDTTables()
        {
            Tables = new List<string>();
            Tables.Add("  DT AUX ACCOUNT");
            Tables.Add("  DT AUX ACCOUNT");
            return Tables;
        }

        public static List<string> getAdminList()
        {
            Tables = new List<string>();
            Tables.Add("  GESTION DES UTILISATEURS");
            Tables.Add("  PARAMETRAGE D'APPLICATION");
            return Tables;
        }
        public static List<string> getReportingTables()
        {
            Tables = new List<string>();
            Tables.Add(" COREP CA ");
            Tables.Add(" COREP CR ");
            Tables.Add(" ACTIF ");
            Tables.Add(" TECHNIQUES ATTENUATIONS RISQUE  ");
            Tables.Add(" ENGAGEMENT CLIENT  ");
            Tables.Add(" CLIENT ELIGIBLE CMDR  ");
            return Tables;
        }

        public void InsertOrUpdate(PR_ACCOUNT acc)
        {

            var UpdatedorInserted =
                crudctx.PR_ACCOUNT.Where(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Version.Equals(acc.Version) && c.Account.Equals(acc.Account)).FirstOrDefault();

            if (crudctx.PR_ACCOUNT.Any(c => c.Bank_Code.Equals(acc.Bank_Code) && c.Process_Date.Equals(acc.Process_Date) && c.Version.Equals(acc.Version) && c.Account.Equals(acc.Account)))
            {

                UpdatedorInserted.Account = acc.Account;
                UpdatedorInserted.Process_Date = acc.Process_Date;
                UpdatedorInserted.Bank_Code = acc.Bank_Code;
                UpdatedorInserted.Version = acc.Version;

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
                UpdatedorInserted.Sign = acc.Sign;
                UpdatedorInserted.Exposition_Type = acc.Exposition_Type;
                UpdatedorInserted.Is_security = acc.Is_security;
                UpdatedorInserted.Cmdr_In = acc.Cmdr_In;
                UpdatedorInserted.Cmdr_Conversion_Factor = acc.Cmdr_Conversion_Factor;
                UpdatedorInserted.Cmdr_Sens_Code = acc.Cmdr_Sens_Code;
                UpdatedorInserted.Cmdr_Weighting = acc.Cmdr_Weighting;
                MessageBox.Show("Modifiée");
            }

            else
            {
                crudctx.PR_ACCOUNT.Add(acc);
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
                        string PK_Account = row.Row.ItemArray[3].ToString();
                        string PK_Version = row.Row.ItemArray[2].ToString();
                        Console.WriteLine("okkey c bon " + PK_Account + " " + PK_Version);
                        var deleteAccount = crudctx.PR_ACCOUNT.Where(m => m.Bank_Code.Equals(PK_CodeBank) && m.Process_Date.Equals(PK_ProcessDate) && m.Account.Equals(PK_Account)).Single();
                        crudctx.PR_ACCOUNT.Remove(deleteAccount);
                        crudctx.SaveChanges();
                        MessageBox.Show("Supprimée");
                        g.ItemsSource = crudctx.PR_ACCOUNT.ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Sélectionnez une ligne");
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Sélectionnez une ligne ");
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
                            string PK_Account = selectedrows[2].ToString();
                            string PK_Version = selectedrows[20].ToString();
                            Console.WriteLine("okkey c bon " + PK_CodeBank+" "+" "+ PK_ProcessDate+" "+ PK_Account + " " + PK_Version);
                            var deleteAccount = crudctx.PR_ACCOUNT.Where(m => m.Bank_Code.Equals(PK_CodeBank) && m.Process_Date.Equals(PK_ProcessDate) && m.Account.Equals(PK_Account) && m.Version.Equals(PK_Version)).Single();
                            crudctx.PR_ACCOUNT.Remove(deleteAccount);
                            crudctx.SaveChanges();
                        }
                        MessageBox.Show("Supprimée");
                        g.ItemsSource = crudctx.PR_ACCOUNT.ToList();
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
            /*catch (Exception ex)
            {
                MessageBox.Show("Erreur :"+ex.Message+" \n \n"+ex.StackTrace);
            }*/
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
