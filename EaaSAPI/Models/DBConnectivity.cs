using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
namespace EAAS.Models
{
    public class DBConnectivity
    {
        SqlConnection Con;
        SqlDataAdapter da;
        SqlCommand Cmd;
        DataTable Dt;
        DataSet Ds;
        public void OpenConnection()
        {
            string ConStr = "Data Source=eaas.database.windows.net,1433;User id=eaascbre;Initial catalog=EAAS;password=Cbre@123;";// System.Configuration.ConfigurationSettings.AppSettings["DBConnection"].ToString();
            Con = new SqlConnection(ConStr);
            if (Con.State == ConnectionState.Closed)
            {
                Con.Open();
            }
        }
        public void ClosedConnection()
        {
            Con.Close();
            Con.Dispose();
        }
        public DataTable GetTableData(Dictionary<string, object> Dic, string Command)
        {
            Dt = new DataTable();
            OpenConnection();
            Cmd = new SqlCommand(Command, Con);
            Cmd.CommandType = CommandType.StoredProcedure;
            foreach (KeyValuePair<string, object> KVP in Dic)
            {
                Cmd.Parameters.AddWithValue(KVP.Key, KVP.Value.ToString());
            }
            da = new SqlDataAdapter(Cmd);
            da.Fill(Dt);
            ClosedConnection();
            return Dt;
        }

        public DataTable GetTableData(SqlParameter [] parameters, string Command)
        {
            Dt = new DataTable();
            OpenConnection();
            Cmd = new SqlCommand(Command, Con);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddRange(parameters);
            da = new SqlDataAdapter(Cmd);
            da.Fill(Dt);
            ClosedConnection();
            return Dt;
        }




        public DataSet GetDataSet(Dictionary<string, object> Dic, string Command)
        {
            Ds = new DataSet();
            OpenConnection();
            Cmd = new SqlCommand(Command, Con);
            Cmd.CommandType = CommandType.StoredProcedure;
            foreach (KeyValuePair<string, object> KVP in Dic)
            {
                SqlParameter param = new SqlParameter(KVP.Key, KVP.Value);
                Cmd.Parameters.Add(param);
            }
            da = new SqlDataAdapter(Cmd);
            da.Fill(Ds);
            ClosedConnection();
            return Ds;
        }
        
    }
}