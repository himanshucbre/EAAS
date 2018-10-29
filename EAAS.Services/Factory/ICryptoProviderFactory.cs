using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Services
{
    public interface ICryptoProviderFactory
    {
        byte[] Decrypt(byte[] cipherBytes, string strPassword, byte[] rgbSalt = null);
        string Decrypt(string cipherText, string strPassword, byte[] rgbSalt = null);
        byte[] Encrypt(byte[] plainBytes, string strPassword, byte[] rgbSalt = null);
        string Encrypt(string plainText, string strPassword, byte[] rgbSalt = null);
    }
}
