using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.IO;

namespace EAAS.Core.Factory
{
    public class DESEncryption : ICryptoProviderFactory
    {
        private static byte[] SALT = { 34, 65, 11, 12, 16, 0, 65, 128, 92, 72, 65, 23, 87, 11, 10, 8 };
        private enum CryptProc { ENCRYPT, DECRYPT };
        public string Encrypt(string plainText, string key, byte[] rgbSalt)
        {
            TripleDESCryptoServiceProvider desCryptoProvider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider();

            byte[] byteHash;
            byte[] byteBuff;

            byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key));
            desCryptoProvider.Key = byteHash;
            desCryptoProvider.Mode = CipherMode.ECB; //CBC, CFB
            byteBuff = Encoding.UTF8.GetBytes(plainText);

            string encoded =
                Convert.ToBase64String(desCryptoProvider.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            return encoded;
        }
        public string Decrypt(string cipherText, string key, byte[] rgbSalt)
        {
            TripleDESCryptoServiceProvider desCryptoProvider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider();

            byte[] byteHash;
            byte[] byteBuff;

            byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key));
            desCryptoProvider.Key = byteHash;
            desCryptoProvider.Mode = CipherMode.ECB; //CBC, CFB
            byteBuff = Convert.FromBase64String(cipherText);

            string plaintext = Encoding.UTF8.GetString(desCryptoProvider.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            return plaintext;
        }

        public byte[] Decrypt(byte[] cipherBytes, string key, byte[] rgbSalt)
        {
            return CryptBytes(cipherBytes, key, 2, CryptProc.DECRYPT, rgbSalt);
        }


        private static byte[] CryptBytes(byte[] plain, string password, int iterations, CryptProc cryptproc, byte[] rgbSalt)

        {
            //Create our key from the password provided
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, rgbSalt, "SHA512", iterations);

            //We'll be using 3DES
            TripleDES des = TripleDES.Create();
            des.Key = pdb.GetBytes(24);
            des.IV = pdb.GetBytes(8);
            MemoryStream memstream = new MemoryStream();
            ICryptoTransform cryptor = (cryptproc == CryptProc.ENCRYPT) ? des.CreateEncryptor() : des.CreateDecryptor();
            CryptoStream cryptostream = new CryptoStream(memstream, cryptor, CryptoStreamMode.Write);
            cryptostream.Write(plain, 0, plain.Length); //write finished product to our MemoryStream
            cryptostream.Close();
            return memstream.ToArray();

        }
        public byte[] Encrypt(byte[] plainBytes, string key, byte[] rgbSalt)
        {

            return CryptBytes(plainBytes, key, 2, CryptProc.ENCRYPT, rgbSalt);
           
        }
       


    }

}
