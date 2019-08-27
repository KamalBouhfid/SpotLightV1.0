using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
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
        public static Dictionary<string, string> getType()
        {
            var map = new Dictionary<string, string>();
            map.Add("Simulation", "1. Simulation");
            map.Add("Production", "2. Production");
            return map;
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
        public static Dictionary<string, string> getAnalyse()
        {
            var map = new Dictionary<string, string>();
            string date_f;
            var q = (from a in crudctx.analyse
                     select new
                     {
                         id = a.id,
                         date = a.date_analyse,
                         entity = a.entity_id,
                         version = a.version
                     }).Distinct().ToList();

            for (int i = 0; i < q.Count; i++)
            {
                date_f = " Entité :"+ q[i].entity + " Date :"+((DateTime)q[i].date).ToString("yyyy/MM/dd").Replace("/", "-")+" Version : "+ q[i].version;
                map.Add(q[i].id.ToString(),date_f);
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
        public List<string> getEntityAndDateFromID(int key)
        {
            List<string> result = new List<string>();

            if (key == 0000)
            {
                result.Add("0000");
                result.Add("0000");
                result.Add("0000");
                return result;
            }
            else
            {
                var q = (from a in crudctx.analyse.Where(c => c.id == key)
                         select new
                         {
                             entity = a.entity_id,
                             dateanalyse = a.date_analyse,
                             version = a.version
                         }).Distinct().ToList();
                for (int i = 0; i < q.Count; i++)
                {
                    DateTime dt = Convert.ToDateTime(q[i].dateanalyse);
                    result.Add(q[i].entity);
                    result.Add(dt.ToString("yyyy/MM/dd").Replace("/", "-"));
                    result.Add(q[i].version);
                    Console.WriteLine("this is finaleee !! : " + q[i].entity + " and : " + q[i].dateanalyse.ToString()+" and :"+ q[i].version );
                }
                return result;
            }
        }

        public static List<string> getDataEntityAnalyse(string entityid,string dateanalyse,string version)
        {
            /*List<string> list = new List<string>();
            var q = (from a in crudctx.analyse
                     select new
                     {
                         id = a.id,
                         name = a.entity_id,
                         dateanalyse = a.date_analyse,
                         description = a.Description,
                         version = a.version,
                         type = a.type
                     }).Distinct().ToList();

            for (int i = 0; i < q.Count; i++)
            {
                if(q[i].name.Equals(entityid) && q[i].dateanalyse.Equals(dateanalyse)) { 
                list.Add(q[i].id.ToString());
                list.Add(q[i].name);
                list.Add(q[i].dateanalyse.ToString());
                list.Add(q[i].description);
                list.Add(q[i].version);
                list.Add(q[i].type);
                    return list;
                }else
                {
                    return null;
                }
            }
            return list;*/

            SqlCommand cmd = new SqlCommand("select * from analyse where entity_id ='"+entityid+"' and date_analyse='"+dateanalyse+ "' and version = (SELECT MAX(version) FROM analyse where entity_id='" + entityid + "' and date_analyse='" + dateanalyse + "')", Globals.con2);
            Globals.con2.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<string> list = new List<string>();
            while (reader.Read())
            {
                list.Add(reader["entity_id"].ToString());
                list.Add(reader["date_analyse"].ToString());
                list.Add(reader["Description"].ToString());
                list.Add(reader["version"].ToString());
                list.Add(reader["type"].ToString());
            }
            Globals.con2.Close();
            return list;
        }

        public int Checkversion(string bankcode, string processdate, string version)
        {
            List<string> list = new List<string>();
            list = getDataEntityAnalyse(bankcode, processdate, version);
            if(list == null || list.Count == 0)
            {
                return 1;
            }else
            {
                return Convert.ToInt32(list[3]) + 1;
            }
        }
    }
}
