using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SpootLight.Models
{
    class AnalyseModel
    {
        public static SpotLightDBEntities1 crudctx = new SpotLightDBEntities1();
        public AnalyseModel()
        {
            //  FillAuthors();
            FillAccounts();
        }

        private void FillAccounts()
        {

            var q = (from a in crudctx.analyse
                     select a).Distinct().ToList();
            this.AnalyseList = q;

        }
        public static Dictionary<string, string> getEntity()
        {
            var map = new Dictionary<string, string>();
            var q = (from a in crudctx.entities
                     select new
                     {
                         id=a.id,
                         name=a.name }).Distinct().ToList();

            for (int i = 0; i < q.Count; i++) { 
            map.Add(q[i].id, q[i].name);
            }
            return map;
        }
        private List<analyse> AnalyseList;
        public List<analyse> Analyses
        {
            get
            {
                return AnalyseList;
            }
            set
            {
                AnalyseList = value;
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
        public static void load_analyse(DataGrid g)
        {
            g.ItemsSource = crudctx.analyse.ToList(); 
        }
        public void Insert(analyse acc)
        {

            crudctx.analyse.Add(acc);
            MessageBox.Show("Bien Ajoutée !");
            crudctx.SaveChanges();
        }
    }
}
