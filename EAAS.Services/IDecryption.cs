using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Services
{
    interface IDecryption
    {        
       
        byte[] Decrypt(string encryptionType , byte[] cipherBytes, string strPassword = "", byte[] rgbSalt = null);
        string Decrypt(string encryptionType ,string cipherText, string strPassword = "", byte[] rgbSalt = null);
    }
}
