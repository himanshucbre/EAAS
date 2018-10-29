using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EAAS.Services.Factory
{
    public class RijndaelEncryption : ICryptoProviderFactory
    {

    
        public string Encrypt(string plainText, string strPassword, byte[] rgbSalt)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(Constants.InitVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(strPassword, rgbSalt);
            byte[] keyBytes = password.GetBytes(Constants.Keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }

        public byte[] Encrypt(byte[] plainBytes, string strPassword, byte[] rgbSalt)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(Constants.InitVector);
           
            PasswordDeriveBytes password = new PasswordDeriveBytes(strPassword, rgbSalt);
            byte[] keyBytes = password.GetBytes(Constants.Keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return cipherTextBytes;
        }
       
        public string Decrypt(string cipherText, string strPassword, byte[] rgbSalt)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(Constants.InitVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(strPassword, rgbSalt);
            byte[] keyBytes = password.GetBytes(Constants.Keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        public byte[] Decrypt(byte[] cipherBytes, string strPassword, byte[] rgbSalt)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(Constants.InitVector);
            PasswordDeriveBytes password = new PasswordDeriveBytes(strPassword, rgbSalt);
            byte[] keyBytes = password.GetBytes(Constants.Keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return plainTextBytes;
        }

        public void EncryptFile(string inputFile, string outputFile)
        {

            try
            {
                string password = @"myKey123"; // Your Key Here
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);


                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch
            {
                throw;
            }
        }
        public void DecryptFile(string inputFile, string outputFile)
        {

            {
                string password = @"myKey123"; // Your Key Here

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                fsOut.Close();
                cs.Close();
                fsCrypt.Close();

            }
        }

        private  byte[] EncryptStringToBytes(string plainText )
        {
            byte[] encrypted;
            using (Rijndael myRijndael = Rijndael.Create())
            {
                
                byte[] Key = myRijndael.Key;
                byte[] IV = myRijndael.IV;
            

                // Check arguments.
             if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
           
            // Create an Rijndael object
            // with the specified key and IV.
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

       private  string DecryptStringFromBytes(byte[] cipherText)
        {
            string plaintext = null;
            using (Rijndael myRijndael = Rijndael.Create())
            {

                byte[] Key = myRijndael.Key;
                byte[] IV = myRijndael.IV;

                // Check arguments.
                if (cipherText == null || cipherText.Length <= 0)
                    throw new ArgumentNullException("cipherText");
                if (Key == null || Key.Length <= 0)
                    throw new ArgumentNullException("Key");
                if (IV == null || IV.Length <= 0)
                    throw new ArgumentNullException("IV");

                // Declare the string used to hold
                // the decrypted text.
               

                // Create an Rijndael object
                // with the specified key and IV.
                using (Rijndael rijAlg = Rijndael.Create())
                {
                    rijAlg.Key = Key;
                    rijAlg.IV = IV;

                    // Create a decryptor to perform the stream transform.
                    ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                    // Create the streams used for decryption.
                    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {

                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                }
            }
            return plaintext;

        }


        //public static byte[] EncryptByte(byte[] plain, string password)
        //{
        //    MemoryStream memoryStream;
        //    CryptoStream cryptoStream;
        //    Rijndael rijndael = Rijndael.Create();
        //    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, SALT);
        //    rijndael.Key = pdb.GetBytes(32);
        //    rijndael.IV = pdb.GetBytes(16);
        //    memoryStream = new MemoryStream();
        //    cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
        //    cryptoStream.Write(plain, 0, plain.Length);
        //    cryptoStream.Close();
        //    return memoryStream.ToArray();
        //}
        //public static byte[] DecryptByte(byte[] cipher, string password)
        //{

        //    MemoryStream memoryStream;
        //    CryptoStream cryptoStream;
        //    Rijndael rijndael = Rijndael.Create();
        //    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, SALT);
        //    rijndael.Key = pdb.GetBytes(32);
        //    rijndael.IV = pdb.GetBytes(16);
        //    memoryStream = new MemoryStream();
        //    cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
        //    cryptoStream.Write(cipher, 0, cipher.Length);
        //    cryptoStream.Close();
        //    return memoryStream.ToArray();
        //}
    }
}
