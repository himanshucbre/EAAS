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
        public string Decrypt(string text, string key, EncryptionAlgo encryptionAlgo)
        {
            ICryptoProviderFactory cryptoProviderFactory = null;

            cryptoProviderFactory = CryptoProviderFactory.CreateEncryptionFactory(encryptionAlgo);
            return cryptoProviderFactory.Decrypt(text, key);
        }
    }
}
