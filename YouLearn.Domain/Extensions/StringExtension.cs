using System;
using System.Collections.Generic;
using System.Text;

namespace YouLearn.Domain.Extensions
{
    public static class StringExtension
    {
        public static string ConvertToMD5(this string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            var password = (text += "ricardo");
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(text));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }
    }
}
