using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EAAS.Core.Factory
{
    public class FPEAlphanumeric : ICryptoProviderFactory
    {
        public string Encrypt(string plainText, string strPassword, byte[] salt)
        {
            FPEHashCrypto fpeHashCrypto = new FPEHashCrypto(strPassword);
            var ceaserKey = System.Text.Encoding.ASCII.GetString(salt);
            var ceaserKeyDictionary = JsonConvert.DeserializeObject<Dictionary<char, int>>(ceaserKey);
            return fpeHashCrypto.Process(plainText, strPassword, Mode.Encrypt, ceaserKeyDictionary);
        }

        public string Decrypt(string cipherText, string strPassword, byte[] salt)
        {
            FPEHashCrypto fpeHashCrypto = new FPEHashCrypto(strPassword);

            var ceaserKey = System.Text.Encoding.ASCII.GetString(salt);
            var ceaserKeyDictionary = JsonConvert.DeserializeObject<Dictionary<char, int>>(ceaserKey);
            return fpeHashCrypto.Process(cipherText, strPassword, Mode.Decrypt, ceaserKeyDictionary);
        }

        public byte[] Decrypt(byte[] cipherBytes, string strPassword, byte[] salt)
        {
            throw new NotImplementedException();
        }

        public byte[] Encrypt(byte[] plainBytes, string strPassword, byte[] salt)
        {
            throw new NotImplementedException();
        }
    }
}
