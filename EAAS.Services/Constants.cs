using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace EAAS.Services
{
   public static class Constants
    {

        public static readonly byte[] Salt = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };
        // Convert.ToByte(ConfigurationManager.AppSettings.Get("Salt"))};

        //0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };

        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        public static readonly string InitVector = "pemgail9uzpgzl88"; // ConfigurationManager.AppSettings.Get("InitVector"); //"pemgail9uzpgzl88";
        // This constant is used to determine the keysize of the encryption algorithm.
        public static readonly int Keysize = 256;// int.Parse(ConfigurationManager.AppSettings.Get("Keysize")) ;//256;
    }
}
