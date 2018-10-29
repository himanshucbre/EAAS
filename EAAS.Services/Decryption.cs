using EAAS.Services.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Services
{
    public class Decryption : IDecryption
    {
        public string Decrypt(string encryptionType, string cipherText, string strPassword = "", byte[] rgbSalt = null)
        {
            ICryptoProviderFactory cryptoProviderFactory = null;

            cryptoProviderFactory = CryptoProviderFactory.CreateEncryptionFactory(encryptionType);
            return cryptoProviderFactory.Decrypt(cipherText, strPassword, rgbSalt);
        }

    

        public byte[] Decrypt(string encryptionType, byte[] cipherBytes, string strPassword = "", byte[] rgbSalt = null)
        {
            ICryptoProviderFactory cryptoProviderFactory = null;

            cryptoProviderFactory = CryptoProviderFactory.CreateEncryptionFactory(encryptionType);
            return cryptoProviderFactory.Decrypt(cipherBytes, strPassword, rgbSalt);
        }
    }
}
