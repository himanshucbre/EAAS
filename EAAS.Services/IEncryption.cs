using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Services
{
    public interface IEncryption
    {
        string Encrypt(string plainText, string key);
    }
}
