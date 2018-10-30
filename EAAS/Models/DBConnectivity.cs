using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
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
            string ConStr = System.Configuration.ConfigurationSettings.AppSettings["DBConnection"].ToString();
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
                SqlParameter param = new SqlParameter(KVP.Key, KVP.Value);
                Cmd.Parameters.Add(param);
            }
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
        public void ExecuteNonQuery(Dictionary<string, object> Dic, string Command)
        {
            OpenConnection();
            Cmd = new SqlCommand(Command, Con);
            Cmd.CommandType = CommandType.StoredProcedure;
            foreach (KeyValuePair<string, object> KVP in Dic)
            {
                SqlParameter param = new SqlParameter(KVP.Key, KVP.Value);
                Cmd.Parameters.Add(param);
            }
            Cmd.ExecuteNonQuery();
            ClosedConnection();
        }
    }
}