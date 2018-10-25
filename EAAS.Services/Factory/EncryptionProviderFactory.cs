using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Services.Factory
{
    
    public class EncryptionProviderFactory
    {
        public static IEncryptionProviderFactory CreateEncryptionFactory(EncryptionAlgo algo)
        {           

            switch (algo)
            {
                case EncryptionAlgo.Aes:
                    return new MD5();
                case EncryptionAlgo.Rijndael:
                    return new Rijndael();
                default:                    
                    return null;
            }         
                
        }
    }
}
