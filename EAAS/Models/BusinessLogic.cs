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

        public string UserRegistration(string EmailId, string Password, string FirstName, string LastName)
        {
            string Result = "";
            try
            {
                DataTable dt = new DataTable();                
                Dictionary<string, object> Dic = new Dictionary<string, object>();
                Dic.Add("@EmailId", EmailId);
                Dic.Add("@Password", Password);
                Dic.Add("@FirstName", FirstName);
                Dic.Add("@LastName", LastName);
                dt = DBObj.GetTableData(Dic, "SP_UserRegistration");
                if (dt != null && dt.Rows.Count > 0)
                {
                    Result = dt.Rows[0]["ErrorMessage"].ToString(); ;
                }
                return Result;
            }
            catch (Exception ex)
            {
                return Result;
            }
        }

        public string AppRegistration(string UserId,string AppId,string AppName, Dictionary<string,object> AppEncryptionKey, List<string> Urls)
        {
            string Result = "";
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> Dic = new Dictionary<string, object>();
                string AppKey = AppName + "_" + AppName.Substring(0, 2) + "_007";
                StringBuilder sbUrl = new StringBuilder();
                foreach (string list in Urls)
                {
                    sbUrl.Append(list + ";");
                }
                if (sbUrl.Length != 0)
                {
                    sbUrl.Remove(sbUrl.Length - 1, 1);
                }
                StringBuilder XMLencryptionKey = new StringBuilder();
                XMLencryptionKey.Append("<Keys>");
                foreach (KeyValuePair<string ,Object> KP in AppEncryptionKey)
                {
                    XMLencryptionKey.Append("<Key>");
                    XMLencryptionKey.Append("<EncryptionKey>"+KP.Key+"</EncryptionKey>");
                    XMLencryptionKey.Append("<EncryptionValue>" + KP.Value + "</EncryptionValue>");
                }
                XMLencryptionKey.Append("</Keys>");
                Dic.Add("@AppId", AppId);
                Dic.Add("@AppName", AppName);
                Dic.Add("@UserId", UserId);
                Dic.Add("@Urls", sbUrl);
                Dic.Add("@AppKey", AppKey);
                Dic.Add("@AppEncryptionKey", XMLencryptionKey);
                dt = DBObj.GetTableData(Dic, "SP_AppRegistration");
                if (dt != null && dt.Rows.Count > 0)
                {
                    Result = dt.Rows[0]["ErrorMessage"].ToString(); ;
                }
                return Result;
            }
            catch (Exception ex)
            {
                return Result;
            }
        }

        public AppRegistration GetAppDetails(string AppKey, string AppSecret)
        {
            AppRegistration Appreg = null;
            try
            {
                Appreg = new AppRegistration();
                DataSet ds = new DataSet();
                Dictionary<string, object> Dic = new Dictionary<string, object>();
                Dic.Add("@AppKey", AppKey);
                Dic.Add("@AppSecret", AppSecret);
                ds = DBObj.GetDataSet(Dic, "SP_GetAppDetails");
                if (ds != null)
                {
                    DataTable AppDetails = new DataTable();
                    DataTable KeyDetails = new DataTable();
                    AppDetails = ds.Tables[0];
                    KeyDetails = ds.Tables[1];
                    if (AppDetails != null && AppDetails.Rows.Count > 0)
                    {
                        Appreg.AppName = AppDetails.Rows[0]["AppName"].ToString();
                        Appreg.UserId = AppDetails.Rows[0]["UserId"].ToString();
                        List<string> UrlList = AppDetails.Rows[0]["Urls"].ToString().Split(';').ToList<string>();
                        Appreg.Urls = UrlList;
                        Dictionary<string, object> EncryptKey = new Dictionary<string, object>();
                        foreach (DataRow dr in KeyDetails.Rows)
                        {
                            EncryptKey.Add(dr["AppEncryptionKey"].ToString(), dr["AppValue"].ToString());
                        }
                        Appreg.AppEncryptionKey = EncryptKey;
                    }
                }
                return Appreg;
            }
            catch (Exception ex)
            {
                return Appreg;
            }
        }
        
        public AppDetails GetUserApps(string UserId)
        {
            AppDetails appdtls = null;
            UserAppinfo appinfo = null;
            try
            {
                appdtls = new AppDetails();
                appinfo = new UserAppinfo();
                DataTable dt = new DataTable();
                Dictionary<string, object> Dic = new Dictionary<string, object>();
                Dic.Add("@UserId", UserId);               
                dt = DBObj.GetTableData(Dic, "SP_GetUserApps");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        appinfo.AppName = dr["AppName"].ToString();
                        appinfo.UserId = dr["UserId"].ToString();
                        appinfo.AppKey = dr["AppKey"].ToString();
                        appinfo.AppSecret = dr["AppSecret"].ToString();
                        appdtls.AppInfo.Add(appinfo);
                    }
                }
                return appdtls;
            }
            catch (Exception ex)
            {
                return appdtls;
            }
        }

        public string AuthenticateUser(string EmailId, string Password)
        {
            string Result = "";
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> Dic = new Dictionary<string, object>();
                Dic.Add("@EmailId", EmailId);
                Dic.Add("@Password", Password);
                dt = DBObj.GetTableData(Dic, "SP_AuthenticateUser");
                if (dt != null && dt.Rows.Count > 0)
                {
                    Result = dt.Rows[0]["ErrorMessage"].ToString(); ;
                }
                return Result;
            }
            catch (Exception ex)
            {
                return Result;
            }
        }
    }
}