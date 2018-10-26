using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Services
{
    public interface ICryptoProviderFactory
    {
        string Encrypt(string text, string key);
        string Decrypt(string cipher, string key);
    }
}
