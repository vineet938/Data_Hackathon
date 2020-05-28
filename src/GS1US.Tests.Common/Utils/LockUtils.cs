using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.Common.Utils
{
    public static class LockUtils
    {
        public static string PickUnusedAccount(string prefix, IEnumerable<string> items)
        {
            Console.WriteLine($"Number of candidates: {items.Count()}");
            try
            {
                using (var fs = new FileStream($"{prefix}.lock", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                {
                    string result = null;
                    foreach (var item in items)
                    {
                        var filename = $"{prefix}-{item}";
                        if (!File.Exists(filename))
                        {
                            File.Create(filename).Dispose();
                            Console.WriteLine($"Picked {prefix}: {item}");
                            result = item;
                            break;
                        }
                    }
                    fs.Close();
                    return result;
                }
            }
            catch (IOException)
            {
                return null;
            }
        }

        public static void ReleaseAccount(string prefix, string item)
        {
            File.Delete($"{prefix}-{item}");
        }
    }
}
