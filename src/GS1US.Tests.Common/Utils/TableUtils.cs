using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace GS1US.Tests.Common.Utils
{
    public static class TableUtils
    {
        // Retrieve a value from feature-value table
        public static string Get(this Table table, string key) =>
            table.Rows.First(x => x["key"] == key)["value"];
    }
}
