using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAAS.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EaaSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecryptionController : ControllerBase
    {
        [HttpGet("{encryptionType}/{cipherText}/{key}")]
        public string Decryptstring(string encryptionType, string cipherText, string key)
        {
            return new Decryption().Decrypt(encryptionType, cipherText, key);
        }

        [HttpGet("{encryptionType}/{cipherBytes}/{key}")]
        public byte[] DecryptBytes(string encryptionType, byte[] cipherBytes, string key)
        {
            return new Decryption().Decrypt(encryptionType, cipherBytes, key);
        }

    }
}
