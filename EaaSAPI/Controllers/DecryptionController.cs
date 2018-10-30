using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAAS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EaaSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecryptionController : ControllerBase
    {
        [HttpGet("{encryptionType}/{cipherText}/{strPassword}/{rgbSalt}")]
        public string Decryptstring(string encryptionType, string cipherText, string strPassword, byte[] rgbSalt)
        {
            return new Decryption().Decrypt(encryptionType, cipherText, strPassword, rgbSalt);
        }

        [HttpGet("{encryptionType}/{cipherBytes}/{strPassword}/{rgbSalt}")]
        public byte[] DecryptBytes(string encryptionType, byte[] cipherBytes, string strPassword, byte[] rgbSalt)
        {
            return new Decryption().Decrypt(encryptionType, cipherBytes, strPassword, rgbSalt);
        }

    }
}
