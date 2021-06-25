using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Common
{
    public static class StringHelper
    {
        public static string GetMD5(this string input)
        {
            MD5 md5 = MD5.Create();

            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var item in bytes)
            {
                stringBuilder.Append(item.ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
