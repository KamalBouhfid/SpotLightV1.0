using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;


namespace SpootLight.Controllers
{
    class JobTalendController
    {
        public static String var = "";
        public static SqlConnection con = new SqlConnection("Data Source="+ System.Environment.MachineName + "\\SQLEXPRESS;Initial Catalog=SpotLightOp;Integrated Security=True");
        public static String path = "";
        static String description_job = "";
        static String chemin_job = "";
        static  String id_job;

        public static void initial(TreeView trvMenu)
        {
            DataTable ss = load_data();

            foreach (DataRow dtr in ss.Rows)
            {
                Console.WriteLine(dtr["description"]);

            }


            DataTable ss2 = load_data2();


            foreach (DataRow dtr in ss2.Rows)
            {
                Console.WriteLine(dtr["name"]);

            }

            var map = new Dictionary<string, MenuItem>();


            foreach (DataRow dtr in ss2.Rows)
            {
                Console.WriteLine(dtr["name"]);

                MenuItem ch = new MenuItem() { Title = dtr["name"].ToString() };
                map.Add(dtr["num"].ToString(), ch);

                trvMenu.Items.Add(ch);
            }



            foreach (DataRow dtr in ss.Rows)
            {
                Console.WriteLine(dtr["description"]);

                MenuItem ch = new MenuItem() { Title = dtr["description"].ToString() };


                DataTable ss3 = load_data3(dtr["job_id"].ToString());

                foreach (DataRow dtrr in ss3.Rows)
                {
                    MenuItem chh = new MenuItem() { Title = dtrr["description"].ToString() };

                    ch.Items.Add(chh);

                }



                map[dtr["type"].ToString()].Items.Add(ch);

            }

        }

        public static void add_traitement(String id_job, String result, String exceptions)
        {
            con.Open();
            String sql = "insert into traitement (job_id, result, exceptions) values(" + id_job + ",'" + result + "','" + exceptions + "')";
            Console.WriteLine(sql);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static DataTable load_data2()
        {
            SqlCommand cmd;
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlDataReader dr;
            dt.Clear();
            cmd = new SqlCommand("SELECT * FROM type", con);
            con.Open();
            dr = cmd.ExecuteReader();
            dt.Load(dr);

            con.Close();
            return dt;

        }

        public static DataTable load_data()
        {
            SqlCommand cmd;
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlDataReader dr;

            dt.Clear();
            cmd = new SqlCommand("SELECT * FROM jobs WHERE job_parent = 0 ORDER BY ordre ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            dt.Load(dr);

            con.Close();
            return dt;

        }

        public static DataTable load_data3(String job_parent)
        {
            SqlCommand cmd;
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlDataReader dr;

            dt.Clear();

            Console.WriteLine("SELECT * FROM jobs  where job_parent = " + job_parent + " ORDER BY ordre");
            cmd = new SqlCommand("SELECT * FROM jobs  where job_parent = " + job_parent + " ORDER BY ordre", con);


            con.Open();
            dr = cmd.ExecuteReader();
            dt.Load(dr);

            con.Close();
            return dt;

        }

        public static DataTable load_job(String desciption)
        {
            SqlCommand cmd;
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlDataReader dr;

            dt.Clear();
            cmd = new SqlCommand("SELECT * FROM jobs WHERE description LIKE '" + desciption + "'", con);
            Console.WriteLine("SELECT * FROM jobs WHERE description LIKE '" + desciption + "'");

            con.Open();
            dr = cmd.ExecuteReader();
            dt.Load(dr);

            con.Close();
            return dt;

        }

        public static Dictionary<string, string> load_entities()
        {
            SqlCommand cmd;
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlDataReader dr;

            dt.Clear();
            cmd = new SqlCommand("SELECT * FROM entities", con);


            con.Open();
            dr = cmd.ExecuteReader();
            dt.Load(dr);

            con.Close();

            var map = new Dictionary<string, string>();

            foreach (DataRow dtr in dt.Rows)
            {


                map.Add(dtr["id"].ToString(), dtr["name"].ToString());


            }

            return map;

        }

        public static void constructResult(DataReceivedEventArgs args)
        {

            var += args.Data + "\n";
        }

        public static void Jobs_choix(StackPanel stack, object sender,RichTextBox ResultJob, RichTextBox ResultJobErrors,
            Label NomJobResult, Label CheminJobResult,Button LancerJobBtn)
        {
            stack.Visibility = Visibility.Visible;



            DataTable data = load_job(((TextBlock)sender).Text);
            if (data.Rows.Count > 0)
            {
                ResultJob.SelectAll();

                ResultJobErrors.SelectAll();
                ResultJobErrors.Selection.Text = "";

                description_job = data.Rows[0]["description"].ToString();
                chemin_job = data.Rows[0]["chemin_bin"].ToString();
                id_job = data.Rows[0]["job_id"].ToString();
                DataTable ss3 = load_data3(id_job);

                ResultJob.FontSize = 12; // 24 points

                NomJobResult.Content = description_job;
                CheminJobResult.Content = chemin_job;

                path = data.Rows[0]["chemin_bin"].ToString();


            }
        }

        public static void runJob2(object sender, RichTextBox ResultJob, RichTextBox ResultJobErrors,
            Label NomJobResult, Label CheminJobResult, Button LancerJobBtn)
        {
            ResultJob.SelectAll();
            ResultJob.Selection.Text = "";
            ResultJobErrors.SelectAll();
            ResultJobErrors.Selection.Text = "";
            DataTable ss3 = load_data3(id_job);


            ResultJob.FontSize = 12; // 24 points

            NomJobResult.Content = description_job;
            CheminJobResult.Content = chemin_job;


            var process = new Process();
            var startinfo = new ProcessStartInfo("cmd.exe", @"-jar /C " + path);

            try
            {
                var = "";


                startinfo.RedirectStandardOutput = true;
                startinfo.UseShellExecute = false;
                startinfo.CreateNoWindow = true;
                startinfo.RedirectStandardError = true;

                process.StartInfo = startinfo;
                process.OutputDataReceived += (sender2, args) => constructResult(args);   // do whatever processing you need to do in this handler
                process.Start();
                process.BeginOutputReadLine();


                string stderrx = process.StandardError.ReadToEnd();

                process.WaitForExit();
                // errors = process.StandardError.ReadToEnd();
                ResultJob.AppendText(var);

                ResultJobErrors.AppendText(stderrx.Replace("'", "\'"));

                add_traitement(id_job, var.Replace("'", "''"), stderrx.Replace("'", "''"));

                //process.BeginOutputReadLine();  
            }
            finally
            {

            }


        }

    }


   
}
