using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS1US.Tests.Common.Setup;
using static GS1US.Tests.Common.Utils.RandUtils;

namespace GS1US.Tests.DataHub.Setup
{
    class TestConfig: SimpleTestConfig
    {
        public DataHubConfig datahub { get; set; }
        public DataHubUmbrellaApiConfig datahubUmbrellaApi { get; set; }
        public ConnectionStrings connectionString { get; set; }

        public TestConfig(): base(MakeTempPath())
        {
        }

        private static string MakeTempPath() =>
            Path.Combine(Path.GetTempPath(), $"GS1-{RandomString(32)}");
    }

    class DataHubConfig
    {
        public string url { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }

    class DataHubUmbrellaApiConfig
    {
        public string baseurl { get; set; }
    }

    class ConnectionStrings
    {
        public string imis { get; set; }
    }
}
