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
    class USER_MODEL
    {
        readonly SpotLightDBEntities1 crudctx = new SpotLightDBEntities1();
        private List<UserSpot> userspot;
        public USER_MODEL()
        {
            FillCategories();
        }

        private void FillCategories()
        {

            var q = (from a in crudctx.UserSpot
                     select a).Distinct().ToList();
            this.userspot = q;

        }
        public List<UserSpot> USERS
        {
            get
            {
                return userspot;
            }
            set
            {
                userspot = value;
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
        public void InsertOrUpdate(UserSpot acc)
        {

            var UpdatedorInserted =
                crudctx.UserSpot.Where(c => c.id == (acc.id)).FirstOrDefault();

            if (crudctx.UserSpot.Any(c => c.id == (acc.id)))
            {

                UpdatedorInserted.pass = acc.pass;
                UpdatedorInserted.email = acc.email;
                UpdatedorInserted.firstname = acc.firstname;
                UpdatedorInserted.lastname = acc.lastname;

                MessageBox.Show("Bien Modifiée !");
            }

            else
            {
                crudctx.UserSpot.Add(acc);
                MessageBox.Show("Bien Ajoutée !");
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
                    MessageBoxResult d = MessageBox.Show("Etes-vous sûr de supprimer cette ligne?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (d == MessageBoxResult.Yes)
                    {
                        int PK_ID = Convert.ToInt32(row.Row.ItemArray[0]);
                        Console.WriteLine(" ///////////// " + PK_ID);
                        var deletePortfolio = crudctx.UserSpot.Where(m => m.id == PK_ID).Single();
                        crudctx.UserSpot.Remove(deletePortfolio);
                        crudctx.SaveChanges();
                        MessageBox.Show("Bien Supprimée !");
                        g.ItemsSource = crudctx.UserSpot.ToList();
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
                            int PK_ID = Convert.ToInt32(selectedrows[0].ToString());
                            Console.WriteLine(" ///////////// " + PK_ID);
                            var deletePortfolio = crudctx.UserSpot.Where(m => m.id == PK_ID).Single();
                            crudctx.UserSpot.Remove(deletePortfolio);
                            crudctx.SaveChanges();
                        }
                        MessageBox.Show("Supprimée");
                        g.ItemsSource = crudctx.UserSpot.ToList();
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
    }
}
