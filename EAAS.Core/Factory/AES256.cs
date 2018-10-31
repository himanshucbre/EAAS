using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.IO;

namespace EAAS.Core.Factory
{
    public class AES256Encryption : ICryptoProviderFactory
    {          

        public byte[] Decrypt(byte[] cipherBytes, string key, byte[] salt)
        {
            byte[] decryptedBytes = null;
            
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var keyGen = new Rfc2898DeriveBytes(key, salt);
                    AES.Key = keyGen.GetBytes(AES.KeySize / 8);
                    AES.IV = keyGen.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        public string Decrypt(string cipherText, string key, byte[] salt)
        {
            // Get the bytes of the string
            byte[] bytesToBeDecrypted = Convert.FromBase64String(cipherText);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(key);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = Decrypt(bytesToBeDecrypted, key, salt);

            string result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
        }

        public byte[] Encrypt(byte[] plainBytes, string key, byte[] salt)
        {
            byte[] encryptedBytes = null;


            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var keyGen = new Rfc2898DeriveBytes(key,salt);
                    AES.Key = keyGen.GetBytes(AES.KeySize / 8);
                    AES.IV = keyGen.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plainBytes, 0, plainBytes.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        public string Encrypt(string plainText, string key, byte[] salt)
        {
            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(key);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = Encrypt(bytesToBeEncrypted, key, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }
    }

}
