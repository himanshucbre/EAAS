using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAAS.Models
{
    public class RegistrationResponse
    {
        public string SecretKey { get; set; }
        public string RegistrationID { get; set; }
        public string MessageDetails { get; set; }
    }

    public class FetchUserInfoResponse
    {
        public List<string> UrlList { get; set; }
        public string AppName { get; set; }
        public string EncryptionKey { get; set; }
        public string SecretKey { get; set; }
        public string RegistrationID { get; set; }
    }

    public class OutPutResponse<T>
    {
        public T payload { get; set; }
        public string ErrorMessgae {get;set;}
        public string ControllerName { get; set; }
    }
}