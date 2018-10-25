using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Services
{
    interface IDecryption
    {
        byte[] Decrypt(byte[] dataToEncrypt, string key);
    }
}
