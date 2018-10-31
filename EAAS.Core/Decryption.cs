using EAAS.Core.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Core
{
    public class Decryption : IDecryption
    {
        public string Decrypt(string encryptionType, string cipherText, string key = "", byte[] rgbSalt = null)
        {
            ICryptoProviderFactory cryptoProviderFactory = null;

            cryptoProviderFactory = CryptoProviderFactory.CreateEncryptionFactory(encryptionType);
            return cryptoProviderFactory.Decrypt(cipherText, key, rgbSalt);
        }

    

        public byte[] Decrypt(string encryptionType, byte[] cipherBytes, string key = "", byte[] rgbSalt = null)
        {
            ICryptoProviderFactory cryptoProviderFactory = null;

            cryptoProviderFactory = CryptoProviderFactory.CreateEncryptionFactory(encryptionType);
            return cryptoProviderFactory.Decrypt(cipherBytes, key, rgbSalt);
        }
    }
}
