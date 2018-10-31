using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Core.Factory
{
    public class FPENumeric:ICryptoProviderFactory
    {
        public string Encrypt(string plainText, string strPassword, byte[] salt)
        {

            return FPEProcess.Encrypt(plainText, strPassword, System.Text.Encoding.UTF8.GetString(salt));
        }

        public string Decrypt(string plainText, string strPassword, byte[] salt)
        {
            return FPEProcess.Decrypt(plainText, strPassword, System.Text.Encoding.UTF8.GetString(salt));
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
