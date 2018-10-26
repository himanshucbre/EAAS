using EAAS.Services.Factory;
using System;

using System.Security.Cryptography;

namespace EAAS.Services
{
    public class Encryption : IEncryption
    {
        public string Encrypt(string text, string key, EncryptionAlgo encryptionAlgo)
        {
            ICryptoProviderFactory cryptoProviderFactory = null;

            cryptoProviderFactory = CryptoProviderFactory.CreateEncryptionFactory(encryptionAlgo);
            return cryptoProviderFactory.Encrypt(text, key);
        }
    }
}
