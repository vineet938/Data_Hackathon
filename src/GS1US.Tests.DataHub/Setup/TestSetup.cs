using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using GS1US.Tests.Common.Setup;
using GS1US.Tests.Common.Database;
using GS1US.Tests.Common.Utils;

namespace GS1US.Tests.DataHub.Setup
{
    [Binding]
    static class TestSetup
    {
        public static TestConfig Config = SetupUtils.ReadConfig<TestConfig>("testconfig.json");
        public static IWebDriver Driver;
        public static Imis Imis;

        [BeforeScenario]
        public static void BeforeScenario()
        {
            Driver = SetupUtils.CreateDriver(Config);
            Imis = DbUtils.ConnectToImis(Config.connectionString.imis);
        }

        [AfterScenario(Order = 20)]
        public static void AfterScenario()
        {
            if (SetupUtils.ShouldDestroyDriver(Config, ScenarioContext.Current) && Driver != null)
            {
                Driver.Quit();
                Driver = null;
            }

            Imis.Close();
        }

        [AfterScenario(Order = 10)]
        public static void TakeScreenshot()
        {
            Driver.TakeScreenshot();
        }
    }
}
