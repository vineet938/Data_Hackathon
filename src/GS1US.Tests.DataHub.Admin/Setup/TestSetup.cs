using TechTalk.SpecFlow;
using GS1US.Tests.Common.Setup;
using OpenQA.Selenium;
using GS1US.Tests.Common.Utils;
using BoDi;
using GS1US.Tests.Common.APIs.DataHub.Umbrella.Api;
using GS1US.Tests.Common.APIs.DataHub.Umbrella.Client;

namespace GS1US.Tests.DataHub.Admin
{
    [Binding]
    class TestSetup
    {
        private readonly IObjectContainer objectContainer;

        public TestSetup(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }
        
        [BeforeScenario]
        public void BeforeScenario()
        {
            var config = SetupUtils.ReadConfig<TestConfig>("testconfig.json");
            objectContainer.RegisterInstanceAs<TestConfig>(config);
            objectContainer.RegisterInstanceAs<IWebDriver>(SetupUtils.CreateDriver(config));

            var apiConfig = new Configuration();
            apiConfig.BasePath = config.ApiBaseUrl;
            apiConfig.AddApiKey("Ocp-Apim-Subscription-Key", config.ApimKey);
            objectContainer.RegisterInstanceAs<DefaultApi>(new DefaultApi(apiConfig));
        }

        [AfterScenario(Order = 20)]
        public void AfterScenario()
        {
            var driver = objectContainer.Resolve<IWebDriver>();
            var config = objectContainer.Resolve<TestConfig>();
            var context = objectContainer.Resolve<ScenarioContext>();
            if (SetupUtils.ShouldDestroyDriver(config, context) && driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }

        [AfterScenario(Order = 10)]
        public void TakeScreenshot()
        {
            var driver = objectContainer.Resolve<IWebDriver>();
            driver.TakeScreenshot();
        }
    }
}