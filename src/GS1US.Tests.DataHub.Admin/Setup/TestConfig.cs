using System;
using System.IO;
using GS1US.Tests.Common.Setup;
using static GS1US.Tests.Common.Utils.RandUtils;

namespace GS1US.Tests.DataHub.Admin
{
    public class TestConfig : SimpleTestConfig
    {
        public string BaseUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ApiBaseUrl { get; set; }
        public int UmbrellaAdmin { get; set; }
        public string ApimKey { get; set; }

        public TestConfig(string tmpDir) : base(MakeTempPath())
        {
        }

        private static string MakeTempPath() =>
            Path.Combine(Path.GetTempPath(), $"GS1-{RandomString(32)}");
    }
}