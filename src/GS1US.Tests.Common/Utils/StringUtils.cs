using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GS1US.Tests.Common.Utils
{
    public static class StringUtils
    {
        public static string Norm(this string s)
        {
            return Regex.Replace(s, @"\s+", " ").Trim().ToLower();
        }
    }
}
