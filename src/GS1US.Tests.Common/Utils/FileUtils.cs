using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.Common.Utils
{
    public static class FileUtils
    {
        public static IEnumerable<string> FindFiles(string directory, Func<string, bool> predicate) =>
            from f in Directory.EnumerateFiles(directory)
            where predicate(f)
            select f;
    }
}
