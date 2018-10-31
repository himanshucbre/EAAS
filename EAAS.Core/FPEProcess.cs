using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Core
{
    class FPEProcess
    {
        static string tweak = "IamTweak";
        static System.Numerics.BigInteger module = 10;
        public static string Encrypt(string data, string key)
        {
            module = System.Numerics.BigInteger.Pow(10, data.Length);
            return Convert.ToString(FE1.Encrypt(module, System.Numerics.BigInteger.Parse(data), System.Text.Encoding.ASCII.GetBytes(key), System.Text.Encoding.ASCII.GetBytes(tweak)));
        }

        public static string Decrypt(string data, string key)
        {
            module = System.Numerics.BigInteger.Pow(10, data.Length);
            return Convert.ToString(FE1.Decrypt(module, System.Numerics.BigInteger.Parse(data), System.Text.Encoding.ASCII.GetBytes(key), System.Text.Encoding.ASCII.GetBytes(tweak)));
        }
    }
}
