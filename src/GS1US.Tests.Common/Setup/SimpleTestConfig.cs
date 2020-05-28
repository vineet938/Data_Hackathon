using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.Common.Setup
{
    public abstract class SimpleTestConfig : ITestConfig
    {
        private readonly string tmpDir;

        public bool Headless { get; set; }
        public bool Debug { get; set; }

        public virtual string DownloadFolder => tmpDir;

        protected SimpleTestConfig(string tmpDir)
        {
            Directory.CreateDirectory(tmpDir);
            this.tmpDir = tmpDir;
        }
    }
}
