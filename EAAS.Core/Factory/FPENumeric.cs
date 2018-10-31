using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Core.Factory
{
    public class FPENumeric:ICryptoProviderFactory
    {
        public string Encrypt(string plainText, string strPassword, byte[] rgbSalt)
        {

            return FPEProcess.Encrypt(plainText, strPassword);
        }

        public string Decrypt(string plainText, string strPassword, byte[] rgbSalt)
        {
            return FPEProcess.Decrypt(plainText, strPassword);
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
