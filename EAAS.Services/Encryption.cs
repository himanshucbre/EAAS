using EAAS.Services.Factory;
using System;

using System.Security.Cryptography;

namespace EAAS.Services
{
    public class Encryption : IEncryption
    {
        public string Encrypt(string plainText, string key)
        {
            ICryptoProviderFactory cryptoProviderFactory = null;

            cryptoProviderFactory = CryptoProviderFactory.CreateEncryptionFactory(EncryptionAlgo.Rijndael);
            cryptoProviderFactory.Encrypt("", "");
            throw new NotImplementedException();
        }
    }
}
