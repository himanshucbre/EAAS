using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace EAAS.Services.Factory
{
    
    public class TripleDESEncryption : ICryptoProviderFactory
    {

        public string Encrypt(string PlainText, string key)
        {

            TripleDES des = CreateDES(key);

            ICryptoTransform ct = des.CreateEncryptor();

            byte[] input = Encoding.Unicode.GetBytes(PlainText);

            byte[] buffer = ct.TransformFinalBlock(input, 0, input.Length);
            return Convert.ToBase64String(buffer);

        }

        public string Decrypt(string CypherText, string key)

        {

            byte[] b = Convert.FromBase64String(CypherText);

            TripleDES des = CreateDES(key);
            ICryptoTransform ct = des.CreateDecryptor();
            byte[] output = ct.TransformFinalBlock(b, 0, b.Length);
            return Encoding.Unicode.GetString(output);

        }

        static TripleDES CreateDES(string key)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            TripleDES des = new TripleDESCryptoServiceProvider();
            des.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
            des.IV = new byte[des.BlockSize / 8];
            return des;
        }

    }

}
