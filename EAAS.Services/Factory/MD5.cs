using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Services.Factory
{
    public class MD5 : IEncryptionProviderFactory
    {
        public byte[] Encrypt(byte[] dataToEncrypt, string key)
        {
            throw new NotImplementedException();
        }
    }
}
