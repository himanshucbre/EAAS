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
    public class EncryptionController : ControllerBase
    {
        [HttpGet("{encryptionType}/{plainText}/{key}")]
        public string Encryptstring(string encryptionType, string plainText, string key)
        {           
            return new Encryption().Encrypt(encryptionType, plainText, key);
        }


        [HttpGet("{encryptionType}/{plainBytes}/{key}")]
        public byte[] EncryptBytes(string encryptionType, byte[] plainBytes, string key )
        {
            return new Encryption().Encrypt(encryptionType, plainBytes, key);
        }

        //// GET: api/Eaas
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Eaas/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Eaas
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Eaas/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
