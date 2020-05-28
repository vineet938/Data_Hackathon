using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.Common.Setup
{
    public interface ITestConfig
    {
        bool Headless { get; }
        bool Debug { get; }
        string DownloadFolder { get; }
    }
}
