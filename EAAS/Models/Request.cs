using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAAS.Models
{

    public class FetchUserDetails
    {
        public string SecretKey { get; set; }
        public string RegistartionID { get; set; }
    }
    public class InsertRegistrationInfo
    {
        public List<string> UrlList { get; set; }
        public string AppName { get; set; }
        public string EncryptionKey { get; set; }
    }

   
    public class UpdateRegistrationInfo
    {
        public List<string> UrlList { get; set; }
        public string AppName { get; set; }
        public string EncryptionKey { get; set; }
        public string SecretKey { get; set; }
        public string RegistartionID { get; set; }
    }

}