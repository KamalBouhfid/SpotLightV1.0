using Microsoft.Win32;
using SpootLight.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace SpootLight.Controllers
{
    class JobTalendController
    {
        public static String var = "";
        public static SqlConnection con = Globals.con2;
        public static String path = "";
        static String description_job = "";
        static String nom_job = "";
        static String chemin_job = "";
        static String chemin_config = "";
        static  String id_job;
        private  Boolean IsError = false;
        static JobTalendController Job = new JobTalendController();

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

                MenuItem ch = new MenuItem() { Title = dtr["job_name"].ToString() };


                DataTable ss3 = load_data3(dtr["job_id"].ToString());

                foreach (DataRow dtrr in ss3.Rows)
                {
                    MenuItem chh = new MenuItem() { Title = dtrr["job_name"].ToString() };

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

        public static DataTable load_job(String JobName)
        {
            SqlCommand cmd;
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlDataReader dr;

            dt.Clear();
            cmd = new SqlCommand("SELECT * FROM jobs WHERE job_name LIKE '" + JobName + "'", con);

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
                nom_job = data.Rows[0]["job_name"].ToString();
                chemin_job = data.Rows[0]["chemin_bin"].ToString();
                id_job = data.Rows[0]["job_id"].ToString();
                DataTable ss3 = load_data3(id_job);

                ResultJob.FontSize = 12; // 24 points

                NomJobResult.Content = nom_job;
                CheminJobResult.Content = chemin_job;
                ResultJob.SelectAll();
                ResultJob.Selection.Text = "";
                ResultJob.AppendText(description_job);

                path = data.Rows[0]["chemin_bin"].ToString();


            }
        }

        public static void runJob2(object sender, RichTextBox ResultJob, RichTextBox ResultJobErrors,
            Label NomJobResult, Label CheminJobResult, Button LancerJobBtn)
        {
            //ResultJob.SelectAll();
            //ResultJob.Selection.Text = "";
            ResultJobErrors.SelectAll();
            ResultJobErrors.Selection.Text = "";
            DataTable ss3 = load_data3(id_job);


            ResultJob.FontSize = 12; // 24 points

            NomJobResult.Content = description_job;
            CheminJobResult.Content = chemin_job;


            var process = new Process();
            var startinfo = new ProcessStartInfo("cmd.exe", @"-jar /C " + path);
            Console.WriteLine("it is :" + startinfo.ToString()+" "+path);
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
                //ResultJob.AppendText(var);

                ResultJobErrors.AppendText(stderrx.Replace("'", "\'"));
                string err = ResultJobErrors.Selection.Text;
                add_traitement(id_job, var.Replace("'", "''"), stderrx.Replace("'", "''"));
                Console.WriteLine("D"+err+"F");
                //process.BeginOutputReadLine();  
                MessageBox.Show("Exécution Términée", "Exécution Résultat", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            finally
            {

            }


        }

        public static string getJobData(string JobName,string label)
        {
            DataTable data = load_job(JobName);
            string datafinal = "";
            if (data.Rows.Count > 0)
            {
                datafinal = data.Rows[0][label].ToString();
                return datafinal;
            }else
            {
                return null;
            }
        }
        public static void runJob2Finale(string JobName)
        {
            chemin_job = getJobData(JobName, "chemin_bin");
            id_job = getJobData(JobName, "job_id");
            /* DataTable data = load_job(JobName);
             if (data.Rows.Count > 0)
             {
                 description_job = data.Rows[0]["description"].ToString();
                 nom_job = data.Rows[0]["job_name"].ToString();
                 chemin_job = data.Rows[0]["chemin_bin"].ToString();
                 id_job = data.Rows[0]["job_id"].ToString();
                 chemin_config = data.Rows[0]["chemin_config"].ToString();
             }*/
             if(chemin_job.Equals("") || chemin_job.Equals(null))
            {
                MessageBox.Show("Echec d'execution \n \n Fichier .bat introuvable ", "Exécution Résultat", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var process = new Process();
                var startinfo = new ProcessStartInfo("cmd.exe", @"-jar /C " + chemin_job);
                Console.WriteLine("it is :" + startinfo.ToString() + " " + chemin_job);
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
                    add_traitement(id_job, var.Replace("'", "''"), stderrx.Replace("'", "''"));
                    if(stderrx.Replace("'", "''").Equals("")  || stderrx.Replace("'", "''").Equals(null) || stderrx.Replace("'", "''").Equals(" "))
                    {
                        MessageBox.Show("Exécution Terminée \n \n ", "Exécution Résultat", MessageBoxButton.OK, MessageBoxImage.Information);
                        
                    }else
                    {
                        MessageBox.Show("Echec de l'execution \n \n " + stderrx.Replace("'", "''"), "Exécution Résultat", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                finally
                {

                }
            }
        }

        public static void ImporterJob(TextBox TextPath,string jobname)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            string FilePath = "";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                TextPath.Text = FilePath;
                Console.WriteLine("this is our file " + FilePath);
                //editFile(FilePath, jobname);
            }
        }

        public static void editFile(string template,string jobname)
        {
            chemin_config = getJobData(jobname, "chemin_config");
            StringBuilder strtemplate = new StringBuilder(template.Replace("\\", "\\\\"));
            Console.WriteLine(" **** " + strtemplate);
            strtemplate[1] = '\\';
            strtemplate[2] = ':';
            strtemplate[3] = '/';
            Console.WriteLine(" **** " + strtemplate+" ----"+ chemin_config);
            string text = File.ReadAllText(chemin_config);
            lineChanger("SrcFile="+strtemplate.ToString(), chemin_config, 6);
            lineChanger("Version="+Globals.getAnalyse()[5], chemin_config, 7);
            //text = text.Replace(@"D\:/COREP/2018-12-31/PAR/PARAM.xlsx", strtemplate.ToString());
            //File.WriteAllText(chemin_config, text);
        }
        public static void editDtFtDeclarationFile(string jobname)
        {
            chemin_config = getJobData(jobname, "chemin_config");
            lineChanger("Version=" + Globals.getAnalyse()[5], chemin_config, 6);
        }
        public static void lineChanger(string newText, string fileName, int line_to_edit)
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[line_to_edit - 1] = newText;
            File.WriteAllLines(fileName, arrLine);
        }
        public Boolean getIserror()
        {
            return IsError;
        }
        public void setIsError(Boolean IsError)
        {
            this.IsError = IsError;
        }
    }  
}
