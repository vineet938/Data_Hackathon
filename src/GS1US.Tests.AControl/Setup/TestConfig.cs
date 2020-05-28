using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS1US.Tests.Common.Setup;
using static GS1US.Tests.Common.Utils.RandUtils;

namespace GS1US.Tests.AControl.Setup
{
    class TestConfig: SimpleTestConfig
    {
        public AccessControlConfig AccessControl;

        public TestConfig(): base(MakeTempPath())
        {
        }

        private static string MakeTempPath() =>
            Path.Combine(Path.GetTempPath(), $"GS1-{RandomString(32)}");
    }

    class AccessControlConfig
    {
        public string url;
        public string username;
        public string password;
    }
}
