using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Core
{
    interface IDecryption
    {        
       
        byte[] Decrypt(string encryptionType , byte[] cipherBytes, string key = "", byte[] salt = null);
        string Decrypt(string encryptionType ,string cipherText, string key = "", byte[] salt = null);
    }
}
