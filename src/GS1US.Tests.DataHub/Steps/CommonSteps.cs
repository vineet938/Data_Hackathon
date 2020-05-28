using GS1US.Tests.DataHub.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace GS1US.Tests.DataHub.Steps
{
    [Binding]
    public class CommonSteps
    {
        [When(@"page is refreshed")]
        public void WhenPageIsRefreshed()
        {
            TestSetup.Driver.Navigate().Refresh();
        }
    }
}
