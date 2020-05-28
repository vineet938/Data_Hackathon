using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Test.Common
{
    class RandUtils
    {
        private static readonly RNGCryptoServiceProvider Rng = new RNGCryptoServiceProvider();

        public static int RandInt(int min, int max)
        {
            byte[] bb = new byte[4];
            Rng.GetBytes(bb);
            var i = BitConverter.ToUInt32(bb, 0);
            return (int)(min + (max - min) * (i / (double)uint.MaxValue));
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[RandInt(0, s.Length)]).ToArray());
        }

        public static string RandomName(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[RandInt(0, s.Length)]).ToArray());
        }

        public static string RandomNumber(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[RandInt(0, s.Length)]).ToArray());
        }
    }
}
