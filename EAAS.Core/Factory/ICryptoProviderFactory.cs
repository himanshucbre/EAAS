using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Core
{
    public interface ICryptoProviderFactory
    {
        byte[] Decrypt(byte[] cipherBytes, string key, byte[] salt = null);
        string Decrypt(string cipherText, string key, byte[] salt = null);
        byte[] Encrypt(byte[] plainBytes, string key, byte[] salt = null);
        string Encrypt(string plainText, string key, byte[] salt = null);
    }
}
