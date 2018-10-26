using EAAS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EAAS.Controllers
{
    public class EncryptionController : ApiController
    {
       

        // GET: api/Encryption/
        [HttpGet]
        public string Encrypt(string text, string key, string encryptionType)
        {
           return new Encryption().Encrypt( text,  key, encryptionType);
        }

       
    }
}
