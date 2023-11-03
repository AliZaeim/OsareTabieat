using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Core.Security
{
    public static class Passwordhelper
    {
        public static string EncodePasswordMd5(string pass) //Encrypt using MD5   
        {
            using MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(Encoding.ASCII.GetBytes(pass));
            return Encoding.ASCII.GetString(result);
        }
    }
}
