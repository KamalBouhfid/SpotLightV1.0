using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace SpootLight.Models
{
    class Globals
    {
        public static SqlConnection con = new SqlConnection("Data Source=10.173.182.158\\BAM;Initial Catalog=SpotLightDATA3;User ID=sa;Password=Azerty01");
        public static SqlConnection con2 = new SqlConnection("Data Source=10.173.182.158\\BAM;Initial Catalog=SpotLightOp;User ID=sa;Password=Azerty01");

        //public static SqlConnection con = new SqlConnection("Data Source=DESKTOP-LE7PMMN\\SQLEXPRESS;Initial Catalog=SpotLightDATA3;User ID=SA;Password=pr0jetpr0jet");
        //public static SqlConnection con = new SqlConnection("Data Source=MERCUREIT-PC2;Initial Catalog=SpotLightDATA;integrated security=True;");
        //public static SqlConnection con2 = new SqlConnection("Data Source=MERCUREIT-PC2;Initial Catalog=SpotLightOp;integrated security=True;");
        //public static SqlConnection con2 = new SqlConnection("Data Source=DESKTOP-LE7PMMN\\SQLEXPRESS;Initial Catalog=SpotLightOp;User ID=SA;Password=pr0jetpr0jet");


        public static SqlCommand cmd;
        static AnalyseModel analyseModel = new AnalyseModel();
        public static Type type;
        public static List<string> analyseSession = new List<string>();
        public static List<string> user = new List<string>();
        readonly static SpotLightDATAEntities crudctx = new SpotLightDATAEntities();

        public static void setAnalyse(List<string> analyseSession)
        {
            Globals.analyseSession = analyseSession;
            
        }
        public static List<string> getAnalyse()
        {
            return analyseSession;
        }
        public static void setUser(List<string> user)
        {
            Globals.user = user;
        }
        public static List<string> getUser()
        {
            return user;
        }
        /*public static string GetTableName(Type entityType)
        {
            var sql = crudctx.Set(entityType).ToString();
            var regex = new Regex(@"FROM \[dbo\]\.\[(?<table>.*)\] AS");
            var match = regex.Match(sql);

            return match.Groups["table"].Value;
        }*/
        public static void checkAdmin(string groupe, StackPanel pan)
        {
            if (groupe.Equals(null) || groupe.Equals(""))
            {
                MessageBox.Show(" internal error !");
            }
            else if (groupe.Equals("User"))
            {
                pan.Visibility = Visibility.Hidden;
            }
        }
        public static void checkAdminList(string groupe, System.Windows.Controls.MenuItem menu,StackPanel panel)
        {
            if (groupe.Equals(null) || groupe.Equals(""))
            {
                MessageBox.Show(" internal error !");
            }
            else if (groupe.Equals("User"))
            {
                menu.Visibility = Visibility.Hidden;
                panel.Visibility = Visibility.Hidden;
                panel.Children.Clear();
            }
        }
        public static void search(DataGrid grid, System.Windows.Controls.TextBox word,string table,string condition,string BankCode,string ProcessDate,string version)
        {

            if (word.Text.Equals(null) || word.Text.Equals("") ) {
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlDataReader dr;
                dt.Clear();
                cmd = new SqlCommand("SELECT TOP 100 * FROM " + table+ " where Bank_Code='"+ BankCode + "' and " +
                    "Process_Date='"+ ProcessDate + "' and Version='"+version+"' ", con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    grid.ItemsSource = dt.DefaultView;
                }
                con.Close();
                Console.WriteLine("search with null");
            }else
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlDataReader dr;
                dt.Clear();
                cmd = new SqlCommand("SELECT TOP 100 * FROM " + table + " where " + condition + " LIKE '%" + word.Text + "%' and Bank_Code='" + BankCode + "' and Process_Date='" + ProcessDate + "' and Version='"+version+"' ", con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    grid.ItemsSource = dt.DefaultView;
                }
                con.Close();
                Console.WriteLine("search with not null");
            }
        }

        public static void ParamCopy(string OldBankCode,string OldProcessDate, string OldVersion, string NewBankCode, string NewProcessDate,string NewVersion)
        {
            SqlCommand PR_ACC,PR_BASEL_CAT,PR_BASEL_PORT,PR_CUR,PR_ECO,PR_GUAR,PR_INTSEG,PR_SCRT;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();

                PR_ACC = new SqlCommand("INSERT INTO [SpotLightDATA3].[dbo].[PR_ACCOUNT]([Bank_Code],[Process_Date],[Version],[Account],[Description]" +
                    ",[Basel_In],[Basel_Segment],[Credit_Conversion_Factor],[Balance_Off_Sheet],[Sens_Code],[Is_Commitment],[Is_Provision]" +
                    ",[Is_Doubtful],[Is_Accrued_Interest]) SELECT '" + NewBankCode + "','" + NewProcessDate + "','" + NewVersion + "',[Account],[Description],[Basel_In],[Basel_Segment]" +
                    ",[Credit_Conversion_Factor],[Balance_Off_Sheet],[Sens_Code],[Is_Commitment],[Is_Provision],[Is_Doubtful],[Is_Accrued_Interest]" +
                    " FROM[SpotLightDATA3].[dbo].[PR_ACCOUNT] WHERE Bank_Code = '" + OldBankCode + "' AND Process_Date = '" + OldProcessDate + "' AND Version = '" + OldVersion + "' ", con);

                PR_CUR = new SqlCommand(" INSERT INTO[SpotLightDATA3].[dbo].[PR_CURRENCY]( " +
                    "[Bank_Code] ,[Process_Date],[Version],[Currency] ,[Exchange_Rate]) " +
                    "SELECT '"+ NewBankCode + "','"+ NewProcessDate + "','" + NewVersion + "',[Currency],[Exchange_Rate] FROM[SpotLightDATA3].[dbo].[PR_CURRENCY]" +
                    " WHERE Bank_Code = '"+OldBankCode+"' AND Process_Date = '"+ OldProcessDate + "' AND Version = '" + OldVersion + "' ", con);

                PR_BASEL_CAT = new SqlCommand("INSERT INTO [SpotLightDATA3].[dbo].[PR_BASEL_CATEGORY] ( [Bank_Code] ,[Process_Date] " +
                    ",[Version],[Category] ,[Category_Description] ,[Sub_Category] ,[Sub_Category_Description] ,[Sub_Portfolio]) SELECT " +
                    "'" + NewBankCode + "' ,'" + NewProcessDate + "','" + NewVersion + "' ,[Category] ,[Category_Description] ,[Sub_Category]  ,[Sub_Category_Description] " +
                    ",[Sub_Portfolio] FROM[SpotLightDATA3].[dbo].[PR_BASEL_CATEGORY] WHERE Bank_Code = '" + OldBankCode + "' " +
                    "AND Process_Date = '" + OldProcessDate + "' AND Version = '" + OldVersion + "' ", con);

                PR_BASEL_PORT = new SqlCommand("INSERT INTO [SpotLightDATA3].[dbo].[PR_BASEL_PORTFOLIO] ( [Bank_Code] ,[Process_Date] " +
                    ",[Version],[Portfolio] ,[Portfolio_Description] ,[Sub_Portfolio] ,[Sub_Portfolio_Description] ,[Weighting]) SELECT " +
                    "'" + NewBankCode + "' ,'" + NewProcessDate + "','" + NewVersion + "' ,[Portfolio] ,[Portfolio_Description] ,[Sub_Portfolio] ,[Sub_Portfolio_Description] " +
                    ",[Weighting] FROM[SpotLightDATA3].[dbo].[PR_BASEL_PORTFOLIO] WHERE Bank_Code = '" + OldBankCode + "' AND" +
                    " Process_Date = '" + OldProcessDate + "' AND Version = '" + OldVersion + "'", con);

                PR_ECO = new SqlCommand("INSERT INTO [SpotLightDATA3].[dbo].[PR_ECONOMIC_SECTOR] ( [Bank_Code],[Process_Date],[Version],[Economic_Sector],[Description]" +
                    ",[Sub_Category],[Cmdr_Weighting]) SELECT " +  "'" + NewBankCode + "' ,'" + NewProcessDate + "','" + NewVersion + "',[Economic_Sector],[Description]" +
                    ",[Sub_Category],[Cmdr_Weighting] FROM[SpotLightDATA3].[dbo].[PR_ECONOMIC_SECTOR] WHERE Bank_Code = '" + OldBankCode + "' AND" +
                    " Process_Date = '" + OldProcessDate + "' AND Version = '" + OldVersion + "'", con);

                PR_GUAR = new SqlCommand("INSERT INTO [SpotLightDATA3].[dbo].[PR_GUARANTEE_TYPE] ( [Bank_Code],[Process_Date],[Version],[Guarantee_Account]" +
                    ",[Guarantee_Type_Description],[Guarantee_Class],[Guarantee_Weighting],[In_COREP],[In_CMDR],[Guarantee_Weighting_Cmdr]) SELECT " + "'" + NewBankCode + "' ," +
                    "'" + NewProcessDate + "','" + NewVersion + "',[Guarantee_Account],[Guarantee_Type_Description],[Guarantee_Class],[Guarantee_Weighting],[In_COREP],[In_CMDR]" +
                    ",[Guarantee_Weighting_Cmdr] FROM[SpotLightDATA3].[dbo].[PR_GUARANTEE_TYPE] WHERE Bank_Code = '" + OldBankCode + "' AND" + " Process_Date = '" + OldProcessDate + "'" +
                    " AND Version = '" + OldVersion + "'", con);

                PR_INTSEG = new SqlCommand("INSERT INTO [SpotLightDATA3].[dbo].[PR_INTERNAL_SEGMENT] ( [Bank_Code],[Process_Date],[Version],[Internal_Segment]" +
                    ",[Internal_Segment_Description],[Customer_Market],[Sub_Category]) SELECT " + "'" + NewBankCode + "' ," +"'" + NewProcessDate + "','" + NewVersion + "'," +
                    "[Internal_Segment],[Internal_Segment_Description],[Customer_Market],[Sub_Category] FROM[SpotLightDATA3].[dbo].[PR_INTERNAL_SEGMENT]" +
                    " WHERE Bank_Code = '" + OldBankCode + "' AND" + " Process_Date = '" + OldProcessDate + "'" +" AND Version = '" + OldVersion + "'", con);

                PR_SCRT = new SqlCommand("INSERT INTO [SpotLightDATA3].[dbo].[PR_SECURITY_TYPE] ( [Bank_Code],[Process_Date],[Version],[Security_Type],[Sub_Portfolio]) " +
                    "SELECT " + "'" + NewBankCode + "' ," + "'" + NewProcessDate + "','" + NewVersion + "'," +"[Security_Type],[Sub_Portfolio] " +
                    "FROM[SpotLightDATA3].[dbo].[PR_SECURITY_TYPE]" + " " + "WHERE Bank_Code = '" + OldBankCode + "' AND" + " Process_Date = '" + OldProcessDate + "'" + " AND " +
                    "Version = '" + OldVersion + "'", con);

                try
                {
                    PR_ACC.ExecuteNonQuery();
                    PR_CUR.ExecuteNonQuery();
                    PR_BASEL_CAT.ExecuteNonQuery();
                    PR_BASEL_PORT.ExecuteNonQuery();
                    PR_ECO.ExecuteNonQuery();
                    PR_GUAR.ExecuteNonQuery();
                    PR_INTSEG.ExecuteNonQuery();
                    PR_SCRT.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur : \n " + ex.Message + " \n " + ex.StackTrace);
                }

            }
            con.Close();
        }
        public static void DataCopy(string OldBankCode, string OldProcessDate, string OldVersion, string NewBankCode, string NewProcessDate, string NewVersion)
        {
            SqlCommand DT_AUX_ACC, DT_CUST,DT_GUAR, DT_DEF, DT_PROV;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();

                DT_AUX_ACC = new SqlCommand("INSERT INTO [SpotLightDATA3].[dbo].[DT_AUX_ACCOUNT]([Bank_Code],[Process_Date],[Version],[Internal_Account_Number]" +
                    ",[Currency_Code],[Customer_Code],[Acounting_Branch],[Opening_Date],[Product_Code],[Accounting_Balance_in_local_currency],[Accounting_Balance_in_currency]" +
                    ",[GL_internal_Number],[GL_reporting_Number],[Doubtful_Status],[Provision_affected_amount],[Provision_amount_total_currency]" +
                    ",[Attribute_1],[Attribute_2],[Attribute_3],[Attribute_4],[Attribute_5]) SELECT '" + NewBankCode + "'," +
                    "'" + NewProcessDate + "','" + NewVersion + "',[Internal_Account_Number]" +",[Currency_Code],[Customer_Code],[Acounting_Branch],[Opening_Date],[Product_Code]," +
                    "[Accounting_Balance_in_local_currency],[Accounting_Balance_in_currency]" +",[GL_internal_Number],[GL_reporting_Number],[Doubtful_Status],[Provision_affected_amount]," +
                    "[Provision_amount_total_currency]" +",[Attribute_1],[Attribute_2],[Attribute_3],[Attribute_4],[Attribute_5]"+ " FROM[SpotLightDATA3].[dbo].[DT_AUX_ACCOUNT]" +
                    " WHERE Bank_Code = '" + OldBankCode + "' AND Process_Date = '" + OldProcessDate + "'" +"AND Version = '" + OldVersion + "'", con);

                DT_CUST = new SqlCommand("INSERT INTO [SpotLightDATA3].[dbo].[DT_CUSTOMER]([Bank_Code],[Process_Date],[Version],[Customer_Code],[Customer_First_Name]," +
                    "[Customer_Last_Name],[Company_Name],[Customer_Type],[Customer_ID_Type],[Customer_ID_Number],[Company_group],[Company_group_name],[Customer_Country_Code],[Economic_Sector],[Activity_Area],[Internal_Segment],[Internal_Notation],[External_Notation],[Business_Category]," +
                    "[Commitment_Amount_in_local_currency],[Default_Amount_in_local_currency],[Rating],[Turnover],[Attribute_1],[Attribute_2],[Attribute_3],[Attribute_4],[Attribute_5])" +
                    " SELECT '" + NewBankCode + "','" + NewProcessDate + "','" + NewVersion + "',[Customer_Code],[Customer_First_Name],[Customer_Last_Name],[Company_Name],[Customer_Type]" +
                    ",[Customer_ID_Type],[Customer_ID_Number],[Company_group],[Company_group_name],[Customer_Country_Code],[Economic_Sector],[Activity_Area],[Internal_Segment],[Internal_Notation]" +
                    ",[External_Notation],[Business_Category],[Commitment_Amount_in_local_currency],[Default_Amount_in_local_currency],[Rating],[Turnover],[Attribute_1]" +
                    ",[Attribute_2],[Attribute_3],[Attribute_4],[Attribute_5]" + " FROM[SpotLightDATA3].[dbo].[DT_CUSTOMER] " +
                    "WHERE Bank_Code = '" + OldBankCode + "' AND Process_Date = '" + OldProcessDate + "' AND Version = '" + OldVersion + "'", con);

                DT_GUAR = new SqlCommand("INSERT INTO [SpotLightDATA3].[dbo].[DT_GUARANTEE]([Bank_Code],[Process_Date],[Version],[Internal_Guarantee_Number]," +
                    "[Internal_Contract_Number],[Guarantee_type],[Guarantee_Customer],[Guarantee_currency],[GL_internal_Number],[Guarantee_assigned_amount],[Guarantee_amount],[Value_Date]," +
                    "[End_Date],[Attribute_1],[Attribute_2],[Attribute_3],[Attribute_4],[Attribute_5]) SELECT '" + NewBankCode + "','" + NewProcessDate + "','" + NewVersion+ "',[Internal_Guarantee_Number],[Internal_Contract_Number]" +
                    ",[Guarantee_type],[Guarantee_Customer],[Guarantee_currency],[GL_internal_Number],[Guarantee_assigned_amount],[Guarantee_amount],[Value_Date],[End_Date],[Attribute_1],[Attribute_2],[Attribute_3]," +
                    "[Attribute_4],[Attribute_5]"+ " FROM[SpotLightDATA3].[dbo].[DT_GUARANTEE] WHERE Bank_Code = '" + OldBankCode + "' AND Process_Date = '" + OldProcessDate + "'" +
                    "AND Version = '" + OldVersion + "'", con);

                DT_DEF = new SqlCommand("INSERT INTO [SpotLightDATA3].[dbo].[DT_DEFAULT]([Bank_Code],[Process_Date],[Version],[Customer_Code],[Rating]," +
                    "[Default_Amount_in_local_currency]) SELECT '" + NewBankCode + "', '" + NewProcessDate + "','" + NewVersion + "',[Customer_Code],[Rating]," +
                    "[Default_Amount_in_local_currency]" + " FROM[SpotLightDATA3].[dbo].[DT_DEFAULT] WHERE Bank_Code = '" + OldBankCode + "' " +
                    "AND Process_Date = '" + OldProcessDate + "' AND Version = '" + OldVersion + "'", con);

                DT_PROV = new SqlCommand("  INSERT INTO [SpotLightDATA3].[dbo].[DT_PROVISION]([Bank_Code],[Process_Date],[Version],[Internal_Provision_Number]," +
                    "[Provision_Stage],[Currency_Code],[Customer_Code],[Balance_off_balance],[Maturity_Date],[Provision_amount_in_currency]," +
                    "[Provision_amount_in_local_currency],[GL_internal_Number],[GL_reporting_Number],[Attribute_1],[Attribute_2]," +
                    "[Attribute_3],[Attribute_4],[Attribute_5]) SELECT '" + NewBankCode + "','" + NewProcessDate + "','" + NewVersion + "',[Internal_Provision_Number]," +
                    "[Provision_Stage],[Currency_Code],[Customer_Code],[Balance_off_balance],[Maturity_Date],[Provision_amount_in_currency],[Provision_amount_in_local_currency]," +
                    "[GL_internal_Number],[GL_reporting_Number],[Attribute_1],[Attribute_2],[Attribute_3],[Attribute_4],[Attribute_5]" +"" +
                    " FROM[SpotLightDATA3].[dbo].[DT_PROVISION] WHERE Bank_Code = '" + OldBankCode + "' AND Process_Date = '" + OldProcessDate + "'" +
                    "AND Version = '" + OldVersion + "'", con);

                try
                {
                    DT_AUX_ACC.ExecuteNonQuery();
                    DT_CUST.ExecuteNonQuery();
                    DT_GUAR.ExecuteNonQuery();
                    DT_DEF.ExecuteNonQuery();
                    DT_PROV.ExecuteNonQuery();
                }catch(Exception ex)
                {
                    MessageBox.Show("Error : \n " + ex.Message + " \n " + ex.StackTrace);
                }
            }
            con.Close();
        }
        public static void InsertAnalyse(int id, string entity_id, string user_id, string date_analyse, string Description, string version,string type)
        {
            if (con2.State == ConnectionState.Closed)
            {
                con2.Open();
                cmd = new SqlCommand(" INSERT INTO analyse values("+ id + ",'"+ entity_id + "','"+ user_id + "','"+ date_analyse + "','"+ Description + "'" +
                    ",'"+ version + "','" + type + "')", con2);
                cmd.ExecuteNonQuery();
            }
            con2.Close();
        }
        public static void EditAnalyse(string entity_id, string date_analyse, string Description,string type)
        {
            if (con2.State == ConnectionState.Closed)
            {
                con2.Open();
                cmd = new SqlCommand(" UPDATE analyse  set Description='"+Description+"', type='"+type+"' where entity_id='"+entity_id+"' and " +
                    "date_analyse='"+date_analyse+"' ", con2);
                cmd.ExecuteNonQuery();
            }
            con2.Close();
            MessageBox.Show("Modifiée");
        }
        public static DataSet GetDataSet(string sql)
        {
            cmd = new SqlCommand(sql, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            adp.Fill(ds);
            con.Close();

            return ds;
        }

        public static System.Data.DataTable GetDataTable(string sql)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            Console.WriteLine(" pmm"+sql);
            DataSet ds = GetDataSet(sql);
            dt = ds.Tables[0];
            if (ds.Tables.Count > 0)
            {
                Console.WriteLine(" I got tables ");
                return dt;

            } else
            {
                Console.WriteLine(" I got null ");
                return null;
            }
        }
        public static void Loading(RadBusyIndicator radBusyIndicator, bool isbusy)
        {
            radBusyIndicator.IsBusy = isbusy;
        }


        public static void data2Exel(DataGrid dataGrid)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true; //www.yazilimkodlama.com
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < dataGrid.Columns.Count; j++) //Başlıklar için
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true; //Başlığın Kalın olması için
                sheet1.Columns[j + 1].ColumnWidth = 15; //Sütun genişliği ayarı
                myRange.Value2 = dataGrid.Columns[j].Header;
            }
            for (int i = 0; i < dataGrid.Columns.Count; i++)
            { //www.yazilimkodlama.com
                for (int j = 0; j < dataGrid.Items.Count; j++)
                {
                    TextBlock b = dataGrid.Columns[i].GetCellContent(dataGrid.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    if(b!=null)
                    myRange.Value2 = b.Text;
                }
            }
        }
    }
}
