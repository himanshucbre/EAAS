using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Core
{
    public interface ICryptoProviderFactory
    {
        byte[] Decrypt(byte[] cipherBytes, string key, byte[] rgbSalt = null);
        string Decrypt(string cipherText, string key, byte[] rgbSalt = null);
        byte[] Encrypt(byte[] plainBytes, string key, byte[] rgbSalt = null);
        string Encrypt(string plainText, string key, byte[] rgbSalt = null);
    }
}
