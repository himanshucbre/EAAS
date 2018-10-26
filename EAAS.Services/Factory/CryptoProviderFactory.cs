using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Services.Factory
{
    
    public class CryptoProviderFactory
    {
        public static ICryptoProviderFactory CreateEncryptionFactory(string encryptionType)
        {           

            switch (encryptionType.ToLower())
            {
                case "md5":
                    return new MD5Encryption();
                case "rijndael":
                    return new RijndaelEncryption();
                case "des":
                    return new DESEncryption();
                case "tripledes":
                    return new TripleDESEncryption();
                case "aes":
                    return new AESEncryption();
                case "aes256":
                    return new AES256Encryption();
                default:                    
                    return null;
            }         
                
        }
    }
}
