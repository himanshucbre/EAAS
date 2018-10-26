using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Services
{
    interface IDecryption
    {
        
        string Decrypt(string plainText, string key, EncryptionAlgo encryptionAlgo);
    }
}
