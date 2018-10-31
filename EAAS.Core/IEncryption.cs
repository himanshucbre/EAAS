using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Core
{
    public interface IEncryption
    {
        
        byte[] Encrypt(string encryptionType , byte[] plainBytes, string key = "", byte[] salt =  null);
        string Encrypt(string encryptionType ,string plainText, string key ="", byte[] salt = null);

    }
}
