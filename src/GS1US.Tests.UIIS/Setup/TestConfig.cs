using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.UIIS.Setup.Config
{
    class TestConfig
    {
        public bool headless;
        public bool debug;
        public Uiis uiis;
        public ConnectionStrings connectionString;
        public int dbTimeout;
        public int dbPollInterval;
    }

    class Uiis
    {
        public string url;
        public string username;
        public string password;
    }

    class TestUrls
    {
        public string uiis;
    }

    class ConnectionStrings
    {
        public string imis;
        public string uiis;
    }
}
