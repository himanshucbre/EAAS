using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Core.Factory
{
    public class FPEAlphanumeric : ICryptoProviderFactory
    {
        public string Encrypt(string plainText, string strPassword, byte[] rgbSalt)
        {
            FPEHashCrypto fpeHashCrypto = new FPEHashCrypto(strPassword);
            return fpeHashCrypto.Process(plainText, strPassword, Mode.Encrypt);
        }

        public string Decrypt(string cipherText, string strPassword, byte[] rgbSalt)
        {
            FPEHashCrypto fpeHashCrypto = new FPEHashCrypto(strPassword);
            return fpeHashCrypto.Process(cipherText, strPassword, Mode.Decrypt);
        }

        public byte[] Decrypt(byte[] cipherBytes, string strPassword, byte[] rgbSalt)
        {
            throw new NotImplementedException();
        }

        public byte[] Encrypt(byte[] plainBytes, string strPassword, byte[] rgbSalt)
        {
            throw new NotImplementedException();
        }
    }
}
