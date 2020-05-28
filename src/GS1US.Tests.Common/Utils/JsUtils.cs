using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.Common.Utils
{
    public static class JsUtils
    {
        public static IEnumerable<string> MakeFileObject(Stream fs, string filename)
        {
            yield return "new File([";
            var b = new byte[8192];
            var n = fs.Read(b, 0, b.Length);
            while (n > 0)
            {
                yield return "new Uint8Array([";
                var arr = b.Take(n).Select(x => x.ToString());
                yield return string.Join(",", arr);
                yield return "])";
                n = fs.Read(b, 0, b.Length);
                if (n > 0) yield return ",";
            }
            yield return $"], '{filename}')";
        }
    }
}
