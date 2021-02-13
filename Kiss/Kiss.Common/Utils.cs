using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Kiss.Common
{
    public static class Utils
    {
        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }
    }
}
