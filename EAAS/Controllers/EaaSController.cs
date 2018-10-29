using EAAS.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EAAS.Controllers
{
    public class EaaSController : ApiController
    {

        public  readonly byte[] Salt;
        public  readonly string InitVector;
        public  readonly int Keysize;

       public EaaSController()
        {
            Salt = new byte[] {  0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c }; // Convert.ToByte(ConfigurationManager.AppSettings["Salt"]) };
            InitVector = ConfigurationManager.AppSettings["InitVector"];
            Keysize = int.Parse(ConfigurationManager.AppSettings["Keysize"]);
            
        }

        // GET: api/Encryption/
        [HttpGet]
        //[Route("{encryptionType}/{plainText}/{strPassword}/{rgbSalt}")]
        public string Encryptstring(string encryptionType, string plainText, string strPassword , byte[] rgbSalt )
        {  
            if(rgbSalt == null)
            {
                rgbSalt = this.Salt;
            }
           return new Encryption().Encrypt(encryptionType, plainText, strPassword, rgbSalt);
        }

        [HttpGet]
        //[Route("{encryptionType}/{plainBytes}/{strPassword}/{rgbSalt}")]
        public byte[] EncryptBytes(string encryptionType, byte[] plainBytes, string strPassword , byte[] rgbSalt )
        {
            return new Encryption().Encrypt(encryptionType, plainBytes, strPassword, rgbSalt);
        }

        [HttpGet]
        //[Route("{encryptionType}/{plainText}/{strPassword}/{rgbSalt}")]
        public string Decryptstring(string encryptionType, string cipherText, string strPassword , byte[] rgbSalt )
        {
            return new Decryption().Decrypt(encryptionType, cipherText, strPassword, rgbSalt);
        }

        [HttpGet]
        //[Route("{encryptionType}/{plainText}/{strPassword}/{rgbSalt}")]
        public byte[] DecryptBytes(string encryptionType, byte[] cipherBytes, string strPassword , byte[] rgbSalt )
        {
            return new Decryption().Decrypt(encryptionType, cipherBytes, strPassword, rgbSalt);
        }
    }
}
