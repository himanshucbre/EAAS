using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Services
{
    public interface IEncryption
    {
        
        byte[] Encrypt(string encryptionType , byte[] plainBytes, string strPassword = "", byte[] rgbSalt =  null);
        string Encrypt(string encryptionType ,string plainText, string strPassword ="", byte[] rgbSalt = null);

    }
}
