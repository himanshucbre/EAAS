using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAAS.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EaaSAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class EncryptionController : ControllerBase
    {
      

        [HttpGet("encryptText/{encryptionType}/{plainText}/{key}", Name = "encryptText")]
        public string Encryptstring(string encryptionType, string plainText, string key)
        {           
            return new Encryption().Encrypt(encryptionType, plainText, key);
        }


        [HttpGet("encryptByte/{encryptionType}/{plainBytes}/{key}", Name = "encryptByte")]
        public byte[] EncryptBytes(string encryptionType, byte[] plainBytes, string key )
        {
            return new Encryption().Encrypt(encryptionType, plainBytes, key);
        }

        
    }
}
