using EAAS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EAAS.Controllers
{
    public class DecryptionController : ApiController
    {


        public string Decrypt(string text, string key, EncryptionAlgo encryptionAlgo)
        {
            return new Decryption().Decrypt(text, key, encryptionAlgo);
        }


    }
}
