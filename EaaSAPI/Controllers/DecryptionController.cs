using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EAAS.Core;
using EAAS.Models;
using EaaSAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EaaSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecryptionController : BaseController
    {
        [HttpGet("text/{encryptionType}/{cipherText}/{key}")]
        public IActionResult Decryptstring(string encryptionType, string cipherText, string key)
        {
            if (string.IsNullOrEmpty(this.appKey) || string.IsNullOrEmpty(this.appSecret))
            {
                return BadRequest();
            }

            var Appreg = new BusinessLogic().GetAppDetails(this.appKey, this.appSecret);
            var reqApp = Appreg?.AppEncryptionKey?.Where(x => x.EncryptionType.Equals(encryptionType)).FirstOrDefault();
            var salt = Encoding.ASCII.GetBytes(reqApp.EncryptionSalt);
            key = string.IsNullOrEmpty(key) ? reqApp?.EncryptionKey : key;

            return Ok( new Decryption().Decrypt(encryptionType, cipherText, key,salt));
        }

        [HttpGet("bytes/{encryptionType}/{cipherBytes}/{key}")]
        public IActionResult DecryptBytes(string encryptionType, byte[] cipherBytes, string key)
        {

            if (string.IsNullOrEmpty(this.appKey) || string.IsNullOrEmpty(this.appSecret))
            {
                return BadRequest();
            }

            var Appreg = new BusinessLogic().GetAppDetails(this.appKey, this.appSecret);
            var reqApp = Appreg?.AppEncryptionKey?.Where(x => x.EncryptionType.Equals(encryptionType)).FirstOrDefault();
            var salt = Encoding.ASCII.GetBytes(reqApp.EncryptionSalt);
            key = string.IsNullOrEmpty(key) ? reqApp?.EncryptionKey : key;
            return Ok(new Decryption().Decrypt(encryptionType, cipherBytes, key,salt));
        }

        [HttpPost]
        public IActionResult Decrypt([FromBody] List<RequestModel> model)
        {
            var result = new List<string>();

            if (string.IsNullOrEmpty(this.appKey) || string.IsNullOrEmpty(this.appSecret) || model == null)
            {
                return BadRequest();
            }

            var Appreg = new BusinessLogic().GetAppDetails(this.appKey, this.appSecret);            
            

            foreach (var m in model)
            {
                var reqApp = Appreg?.AppEncryptionKey?.Where(x => x.EncryptionType.Equals(m.type)).FirstOrDefault();
                var key = string.IsNullOrEmpty(m.key) ? reqApp?.EncryptionKey : m.key;
                var salt = Encoding.ASCII.GetBytes(reqApp.EncryptionSalt);
                result.Add(new Decryption().Decrypt(m.type, m.text, key, Encoding.ASCII.GetBytes(reqApp.EncryptionSalt)));
            }
            return Ok(result);
        }

    }
}
