using Microsoft.VisualStudio.TestTools.WebTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractionRules
{
    class GUIDPlugIn : WebTestPlugin
    {
        [System.ComponentModel.DisplayName("Target Context Parameter Name")]
        [System.ComponentModel.Description("Name of the context parameter that will receive the generated value.")]
        public string ContextParamTarget { get; set; }

        [System.ComponentModel.DisplayName("Output Format")]
        [System.ComponentModel.Description("Format for guid (optional). Ex: N")]
        public string OutputFormat { get; set; }

        [System.ComponentModel.DisplayName("Guid Length")]
        [System.ComponentModel.Description("Length of the generated guid.")]
        public int GuidLength { get; set; }

        public override void PreWebTest(object sender, PreWebTestEventArgs e)
        {
            // Generate new guid with specified output format
            var newGuid = System.Guid.NewGuid().ToString(OutputFormat);

            // Create substring of specified length
            if (GuidLength > 0 && GuidLength < newGuid.Length)
            {
                newGuid = newGuid.Substring(0, GuidLength - 1);
            }

            // Set the context paramaeter with generated guid
            e.WebTest.Context[ContextParamTarget] = newGuid;

            base.PreWebTest(sender, e);
        }
    }
}
