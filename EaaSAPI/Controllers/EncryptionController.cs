using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EAAS.Core;
using EAAS.Models;
using EaaSAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EaaSAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class EncryptionController : BaseController
    {
        [HttpGet("Encryptstring/{encryptionType}/{plainText}/{key}", Name = "encryptText")]
        public IActionResult Encryptstring(string encryptionType, string plainText, string key)
        {
           
                if (string.IsNullOrEmpty(this.appKey) || string.IsNullOrEmpty(this.appSecret))
                {
                    return BadRequest();
                }

            var Appreg = new BusinessLogic().GetAppDetails(this.appKey, this.appSecret);
            var reqApp = Appreg?.AppEncryptionKey?.Where(x => x.EncryptionType.Equals(encryptionType)).FirstOrDefault();
            var salt = Encoding.ASCII.GetBytes(reqApp.EncryptionSalt);
            key = string.IsNullOrEmpty(key) ? reqApp?.EncryptionKey : key;
            return Ok(new Encryption().Encrypt(encryptionType, plainText, key, salt));
        }

        [HttpGet("encryptBytes/{encryptionType}/{plainBytes}/{key}", Name = "encryptByte")]
        public IActionResult EncryptBytes(string encryptionType, byte[] plainBytes, string key )
        {
            if (string.IsNullOrEmpty(this.appKey) || string.IsNullOrEmpty(this.appSecret))
            {
                return BadRequest();
            }

            var Appreg = new BusinessLogic().GetAppDetails(this.appKey, this.appSecret);
            var reqApp = Appreg?.AppEncryptionKey?.Where(x => x.EncryptionType.Equals(encryptionType)).FirstOrDefault();
            var salt = Encoding.ASCII.GetBytes(reqApp.EncryptionSalt);
            key = string.IsNullOrEmpty(key) ? reqApp?.EncryptionKey : key;

            return Ok(new Encryption().Encrypt(encryptionType, plainBytes, key, salt));
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult EncryptText([FromBody] List<RequestModel> model)
        {
            var result =new List<string>();

            if (string.IsNullOrEmpty(this.appKey) || string.IsNullOrEmpty(this.appSecret) || model == null)
            {
                return BadRequest();
            }

            var Appreg = new BusinessLogic().GetAppDetails(this.appKey, this.appSecret);

                foreach(var m in model)
            {
                var reqApp = Appreg?.AppEncryptionKey?.Where(x => x.EncryptionType.Equals(m.type)).FirstOrDefault();
                var key = string.IsNullOrEmpty(m.key) ? reqApp?.EncryptionKey: m.key;                
                var salt = Encoding.ASCII.GetBytes(reqApp.EncryptionSalt);

                result.Add(new Encryption().Encrypt(m.type, m.text, key, salt ));
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult EncryptByte([FromBody] List<RequestModel> model)
        {
            var result = new List<byte[]>();

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

                result.Add(new Encryption().Encrypt(m.type, m.bytes, key, salt));
            }
            return Ok(result);
        }



    }




}
