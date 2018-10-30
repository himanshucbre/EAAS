using EAAS.Services.Factory;
using System;

using System.Security.Cryptography;

namespace EAAS.Services
{
    public class Encryption : IEncryption
    {
        public string Encrypt (string encryptionType, string plainText, string strPassword = "", byte[] rgbSalt = null)
        {
            ICryptoProviderFactory cryptoProviderFactory = null;

            cryptoProviderFactory = CryptoProviderFactory.CreateEncryptionFactory(encryptionType);
            return cryptoProviderFactory.Encrypt(plainText, strPassword, rgbSalt);
        }

        public byte[] Encrypt(string encryptionType, byte[] plainBytes, string strPassword = "", byte[] rgbSalt = null)
        {
            ICryptoProviderFactory cryptoProviderFactory = null;

            cryptoProviderFactory = CryptoProviderFactory.CreateEncryptionFactory(encryptionType);
            return cryptoProviderFactory.Encrypt(plainBytes, strPassword, rgbSalt);
        }
    }
}
