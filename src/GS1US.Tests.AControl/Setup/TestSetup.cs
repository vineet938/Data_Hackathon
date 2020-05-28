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

namespace GS1US.Tests.AControl.Setup
{
    [Binding]
    static class TestSetup
    {
        public static TestConfig Config = SetupUtils.ReadConfig<TestConfig>("testconfig.json");
        public static IWebDriver Driver;

        [BeforeScenario]
        public static void BeforeScenario()
        {
            Driver = SetupUtils.CreateDriver(Config);
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            if (SetupUtils.ShouldDestroyDriver(Config, ScenarioContext.Current) && Driver != null)
            {
                Driver.Quit();
                Driver = null;
            }
        }
    }
}
