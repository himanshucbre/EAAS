using EAAS.Services.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Services
{
    public class Encryption : IEncryption
    {
       

        public byte[] Encrypt(byte[] dataToEncrypt, string key)
        {

            IEncryptionProviderFactory encryptionProviderFactory = null;

            encryptionProviderFactory = EncryptionProviderFactory.CreateEncryptionFactory(EncryptionAlgo.Rijndael);
            encryptionProviderFactory.Encrypt(null,null);
            throw new NotImplementedException();
        }
    }
}
