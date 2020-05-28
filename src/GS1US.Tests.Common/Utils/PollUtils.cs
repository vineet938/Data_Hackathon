using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.Common.Utils
{
    public static class PollUtils
    {
        public static T Poll<T>(
            string name,
            int timeoutSeconds,
            int intervalSeconds,
            Func<T> op,
            Func<T, bool> predicate,
            bool Debug = false)
        {
            var t0 = DateTime.Now;
            while (true)
            {
                T result;
                try
                {
                    result = op();
                    if (predicate(result))
                        return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine("-------- exception while polling ----------");
                    Console.WriteLine(e.Message);
                    if (Debug)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                }

                if ((DateTime.Now - t0).TotalSeconds < timeoutSeconds)
                    Task.Delay(intervalSeconds * 1000).Wait();
                else
                    break;
            }
            throw new TimeoutException($"Operation timed out: {name}");
        }
    }
}
