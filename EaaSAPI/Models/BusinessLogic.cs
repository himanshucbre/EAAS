using EAAS.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EAAS.Models
{
    public class BusinessLogic
    {
        private DBConnectivity DBObj = new DBConnectivity();
        public UserRegistration UserRegistration(string EmailId, string Password, string FirstName, string LastName)
        {
            UserRegistration ObjUserReg = null;
            try
            {
                ObjUserReg = new UserRegistration();
                DataTable dt = new DataTable();
                Dictionary<string, object> Dic = new Dictionary<string, object>();
                Dic.Add("@EmailId", EmailId);
                Dic.Add("@Password", Password);
                Dic.Add("@FirstName", FirstName);
                Dic.Add("@LastName", LastName);
                dt = DBObj.GetTableData(Dic, "SP_UserRegistration");
                if (dt != null && dt.Rows.Count > 0)
                {
                    ObjUserReg.EmailId = dt.Rows[0]["EmailId"].ToString();
                    ObjUserReg.UserId = dt.Rows[0]["UserId"].ToString();
                    ObjUserReg.FirstName = dt.Rows[0]["FirstName"].ToString();
                    ObjUserReg.LastName = dt.Rows[0]["LastName"].ToString();
                    ObjUserReg.Code = dt.Rows[0]["Code"].ToString();
                }
                return ObjUserReg;
            }
            catch (Exception ex)
            {
                return ObjUserReg;
            }
        }

        public UserRegistration AuthenticateUser(string EmailId, string Password)
        {
            UserRegistration ObjUser = new UserRegistration();
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> Dic = new Dictionary<string, object>();
                Dic.Add("@EmailId", EmailId);
                Dic.Add("@Password", Password);
                dt = DBObj.GetTableData(Dic, "SP_AuthenticateUser");
                if (dt != null && dt.Rows.Count > 0)
                {
                    ObjUser.EmailId = dt.Rows[0]["EmailId"].ToString();
                    ObjUser.UserId = dt.Rows[0]["UserId"].ToString();
                    ObjUser.FirstName = dt.Rows[0]["FirstName"].ToString();
                    ObjUser.LastName = dt.Rows[0]["LastName"].ToString();
                    ObjUser.Code = dt.Rows[0]["Code"].ToString();
                    ObjUser.Password = dt.Rows[0]["Password"].ToString();
                }
                return ObjUser;
            }
            catch (Exception ex)
            {
                return ObjUser;
            }
        }

        public List<UserAppinfo> GetUserApps(string UserId, string AppId)
        {
            List<UserAppinfo> userappinfo = new List<UserAppinfo>();
            try
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> Dic = new Dictionary<string, object>();
                Dic.Add("@UserId", UserId);
                Dic.Add("@AppId", AppId);
                dt = DBObj.GetTableData(Dic, "SP_GetUserApplications");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        UserAppinfo UAI = new UserAppinfo();
                        UAI.AppName = dr["AppName"].ToString();
                        UAI.AppId = dr["AppId"].ToString();
                        UAI.UserId = dr["UserId"].ToString();
                        UAI.AppKey = dr["AppKey"].ToString();
                        UAI.AppSecret = dr["AppSecret"].ToString();
                        List<string> UrlList = dr["Urls"].ToString().Split(';').ToList<string>();
                        UAI.Urls = UrlList;
                        userappinfo.Add(UAI);
                    }

                }
                return userappinfo;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public AppRegistration AppRegistration(string UserId, string AppId, string AppName, List<EncryptionKeyValue> EncryptionList, List<string> Urls)
        {
            AppRegistration appReg = new AppRegistration();
            try
            {
                var algo = new SecurityAlgorithm();
                DataTable encryptiontable = new DataTable();
                encryptiontable.Columns.Add("AppId", typeof(int));
                encryptiontable.Columns.Add("EncryptionType", typeof(string));
                encryptiontable.Columns.Add("EncryptionKey", typeof(string));
                encryptiontable.Columns.Add("EncryptionSalt", typeof(string));
                if (string.IsNullOrEmpty(AppId))
                {
                    EncryptionList.Add(new EncryptionKeyValue { EncryptionType = "md5", EncryptionKey = Guid.NewGuid().ToString().Replace("-", string.Empty), EncryptionSalt = Guid.NewGuid().ToString().Replace("-", string.Empty) });
                    EncryptionList.Add(new EncryptionKeyValue { EncryptionType = "rijndael", EncryptionKey = Guid.NewGuid().ToString().Replace("-", string.Empty), EncryptionSalt = Guid.NewGuid().ToString().Replace("-", string.Empty) });
                    EncryptionList.Add(new EncryptionKeyValue { EncryptionType = "des", EncryptionKey = Guid.NewGuid().ToString().Replace("-", string.Empty), EncryptionSalt = Guid.NewGuid().ToString().Replace("-", string.Empty) });
                    EncryptionList.Add(new EncryptionKeyValue { EncryptionType = "tripledes", EncryptionKey = Guid.NewGuid().ToString().Replace("-", string.Empty), EncryptionSalt = Guid.NewGuid().ToString().Replace("-", string.Empty) });
                    EncryptionList.Add(new EncryptionKeyValue { EncryptionType = "aes", EncryptionKey = Guid.NewGuid().ToString().Replace("-", string.Empty), EncryptionSalt = Guid.NewGuid().ToString().Replace("-", string.Empty) });
                    EncryptionList.Add(new EncryptionKeyValue { EncryptionType = "aes256", EncryptionKey = Guid.NewGuid().ToString().Replace("-", string.Empty), EncryptionSalt = Guid.NewGuid().ToString().Replace("-", string.Empty) });
                    EncryptionList.Add(new EncryptionKeyValue { EncryptionType = "fpean", EncryptionKey = Guid.NewGuid().ToString().Replace("-", string.Empty), EncryptionSalt = JsonConvert.SerializeObject(algo.caesarAlphabet) });
                    EncryptionList.Add(new EncryptionKeyValue { EncryptionType = "fpen", EncryptionKey = Guid.NewGuid().ToString().Replace("-", string.Empty), EncryptionSalt = Guid.NewGuid().ToString().Replace("-", string.Empty) });
                }
                foreach (var li in EncryptionList)
                {
                    DataRow Dr = encryptiontable.NewRow();
                    Dr["AppId"] = 0;
                    Dr["EncryptionKey"] = li.EncryptionKey;
                    Dr["EncryptionSalt"] = li.EncryptionSalt;
                    Dr["EncryptionType"] = li.EncryptionType;
                    encryptiontable.Rows.Add(Dr);
                }

                Dictionary<string, object> Dic = new Dictionary<string, object>();
                StringBuilder sbUrl = new StringBuilder();
                foreach (string list in Urls)
                {
                    sbUrl.Append(list + ";");
                }
                if (sbUrl.Length != 0)
                {
                    sbUrl.Remove(sbUrl.Length - 1, 1);
                }
                int.TryParse(UserId, out var inputUserId);
                int.TryParse(AppId, out var inputAppId);
                var parameters = new[]
                {
                    new SqlParameter("@AppEncryptionKey", SqlDbType.Structured)
                    {
                        TypeName = "dbo.[type_EncryptionKeyValue1]",
                        Value = encryptiontable
                    },
                    new SqlParameter("@AppId", SqlDbType.Int)
                    {
                        Value = inputAppId
                    },
                     new SqlParameter("@UserId", SqlDbType.Int)
                    {
                        Value = inputUserId
                    },
                     new SqlParameter("@AppName", SqlDbType.VarChar)
                    {
                        Value = AppName
                    },
                     new SqlParameter("@Urls", SqlDbType.VarChar)
                    {
                        Value = sbUrl.ToString()
                    }
                };
                DataTable dt = new DataTable();
                dt = DBObj.GetTableData(parameters, "SP_AppRegistration");
                if (dt != null && dt.Rows.Count > 0)
                {
                    appReg.AppId = Convert.ToString(dt.Rows[0]["AppId"]);
                    appReg.UserId = Convert.ToString(dt.Rows[0]["UserId"]);
                    appReg.AppName = Convert.ToString(dt.Rows[0]["AppName"]);
                    appReg.AppKey = Convert.ToString(dt.Rows[0]["AppKey"]);
                    appReg.AppSecret = Convert.ToString(dt.Rows[0]["AppSecret"]);
                    appReg.Code = Convert.ToInt32(dt.Rows[0]["Code"]);
                    List<string> UrlList = dt.Rows[0]["Urls"].ToString().Split(';').ToList<string>();
                    appReg.Urls = UrlList;
                }
                return appReg;
            }
            catch (Exception ex)
            {
                appReg.Code = 400;
                return appReg;
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
                        Appreg.AppId = AppDetails.Rows[0]["AppId"].ToString();
                        List<EncryptionKeyValue> LIEKV = new List<EncryptionKeyValue>();
                        foreach (DataRow dr in KeyDetails.Rows)
                        {
                            EncryptionKeyValue EKV = new EncryptionKeyValue();
                            EKV.EncryptionKey = dr["EncryptionKey"].ToString();
                            EKV.EncryptionType = dr["EncryptionType"].ToString();
                            EKV.EncryptionSalt = dr["EncryptionSalt"].ToString();
                            LIEKV.Add(EKV);
                        }
                        Appreg.AppEncryptionKey = LIEKV;
                    }
                }
                return Appreg;
            }
            catch (Exception ex)
            {
                return Appreg;
            }
        }




    }
}