using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace iTunesBackupParser
{
    internal static class DatabaseHelpers
    {
        /// <summary>
        /// Reads a string from stream
        /// </summary>
        internal static string GetString(Stream stream)
        {
            var byte0 = stream.ReadByte();
            var byte1 = stream.ReadByte();

            if (byte0 == 255 && byte1 == 255)
                return "NULL";

            //Big endian: first byte is 2^8 times as important as second byte
            var stringLength = 256 * byte0 + byte1;

            var buffer = new byte[stringLength];
            stream.Read(buffer, 0, stringLength);

            var getString = Encoding.UTF8.GetString(buffer, 0, stringLength).Normalize(NormalizationForm.FormC);
            return getString;
        }

        internal static char ToHexChar(int b)
        {
            b &= 15; //Set XXXXXXXX to 0000XXXX (First 4 bytes 0ed)

            if (b < 10 && b >= 0)
                return (char)b;
            return (char)('A' + (char)(b - 10));
        }

        internal static string GetSHA1Hash(string text)
        {
            var sha1 = new SHA1CryptoServiceProvider();

            var stringData = Encoding.ASCII.GetBytes(text);
            var resultData = sha1.ComputeHash(stringData);
            var hash = "";
            foreach (var b in resultData)
            {
                var c = Convert.ToString(b, 16);
                if (c.Length == 1)
                    c = "0" + c;
                hash += c;
            }

            return hash;
        }
    }
}
