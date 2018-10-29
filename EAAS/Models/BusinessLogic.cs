using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
namespace EAAS.Models
{
    public class BusinessLogic
    {
        DBConnectivity DBObj = new DBConnectivity();
        public FetchUserInfoResponse FetchUserInfo(string SecretKey, string RegistrationID)
        {
            FetchUserInfoResponse ObjUserResponse = null;
            try
            {
                ObjUserResponse = new FetchUserInfoResponse();
                DataTable dt = new DataTable();
                Dictionary<string, object> Dic = new Dictionary<string, object>();
                Dic.Add("@SecretKey", "123");
                Dic.Add("@RegistrationID", "DCBD5653-487F-4030-93FB-72A104656F3B");
                dt = DBObj.GetTableData(Dic, "SP_FetchUserDetails");
                if (dt != null && dt.Rows.Count > 0)
                {
                    ObjUserResponse.AppName = dt.Rows[0]["AppName"].ToString();
                    List<string> UrlList = dt.Rows[0]["UrlList"].ToString().Split(';').ToList<string>();
                    ObjUserResponse.UrlList = UrlList;
                    ObjUserResponse.EncryptionKey = dt.Rows[0]["EncryptionKey"].ToString();

                    ObjUserResponse.SecretKey = dt.Rows[0]["SecretKey"].ToString();
                    ObjUserResponse.RegistrationID = dt.Rows[0]["RegistrationID"].ToString();
                }
                return ObjUserResponse;
            }
            catch (Exception ex)
            {
                return ObjUserResponse;
            }
        }
        public RegistrationResponse InsertUserInfo(List<string> UrlList, string AppName, string Encryptionkey)
        {
            RegistrationResponse RegResObj = null;
            try
            {
                RegResObj = new RegistrationResponse();
                string SecretKey = AppName + "_" + AppName.Substring(0, 1) + "0" + Encryptionkey.Substring(0, 1) + "-";
                StringBuilder sb = new StringBuilder();
                foreach (string list in UrlList)
                {
                    sb.Append(list + ";");
                }
                if (sb.Length != 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                DataTable dt = new DataTable();
                Dictionary<string, object> Dic = new Dictionary<string, object>();
                Dic.Add("@UrlList", "a;b;c");
                Dic.Add("@AppName", "iTime");
                Dic.Add("@Encryptionkey", "abc");
                Dic.Add("@SecretKey", "123");
                dt = DBObj.GetTableData(Dic, "SP_InsertUserDetails");
                if(dt!=null && dt.Rows.Count>0)
                {
                    RegResObj.MessageDetails = dt.Rows[0]["MessageDetails"].ToString();
                    RegResObj.RegistrationID = dt.Rows[0]["RegistrationID"].ToString();
                    RegResObj.SecretKey = dt.Rows[0]["SecretKey"].ToString();
                }
                return RegResObj;

            }
            catch (Exception ex)
            {
                return RegResObj;
            }
        }
        public RegistrationResponse UpdateUserInfo(List<string> UrlList, string AppName, string Encryptionkey, string SecretKey, string RegistrationID)
        {
            RegistrationResponse RegResObj = null;
            try
            {
                RegResObj= new RegistrationResponse();
                StringBuilder sb = new StringBuilder();
                foreach (string list in UrlList)
                {
                    sb.Append(list + ";");
                }
                if (sb.Length != 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                DataTable dt = new DataTable();
                Dictionary<string, object> Dic = new Dictionary<string, object>();
                Dic.Add("@UrlList", "a;b;d");
                Dic.Add("@AppName", "iTime");
                Dic.Add("@Encryptionkey", "abc");
                Dic.Add("@SecretKey", "123");
                Dic.Add("@RegistrationID", "DCBD5653-487F-4030-93FB-72A104656F3B");
                dt = DBObj.GetTableData(Dic, "SP_UpdateUserDetails");
                if (dt != null && dt.Rows.Count > 0)
                {
                    RegResObj.MessageDetails = dt.Rows[0]["MessageDetails"].ToString();
                    RegResObj.RegistrationID = dt.Rows[0]["RegistrationID"].ToString();
                    RegResObj.SecretKey = dt.Rows[0]["SecretKey"].ToString();
                }
                return RegResObj;
            }
            catch (Exception ex)
            {
                return RegResObj;
            }
        }


    }
}