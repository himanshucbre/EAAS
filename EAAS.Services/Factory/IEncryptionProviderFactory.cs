using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Services
{
    public interface IEncryptionProviderFactory
    {
        byte[] Encrypt(byte[] dataToEncrypt, string key);         
    }
}
