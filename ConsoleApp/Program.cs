﻿using EAAS.Services.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            RijndaelEncryption rijndaelEncryption = new RijndaelEncryption();

            rijndaelEncryption.Encrypt("","");

            Console.Read();
        }
    }
}