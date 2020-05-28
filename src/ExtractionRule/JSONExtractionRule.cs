using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.WebTesting;

namespace DataHubLoadTest
{
    [DisplayName("JSON Extraction Rule")]
    [Description("Extracts Specific JSON value from an object")]
    class JSONExtractionRule : ExtractionRule
    {
        public string Name { get; set; }
        public override void Extract(object sender, ExtractionEventArgs e)
        {
            if (e.Response.BodyString != null)
            {
                var json = e.Response.BodyString;
                var data = JObject.Parse(json);

                if (data != null)
                {
                    e.WebTest.Context.Add(ContextParameterName, data.SelectToken(Name));
                    e.Success = true;
                    return;
                }
            }
            e.Success = false;
            e.Message = String.Format(CultureInfo.CurrentCulture, "property not found: {0}", Name);
        }
    }
}
