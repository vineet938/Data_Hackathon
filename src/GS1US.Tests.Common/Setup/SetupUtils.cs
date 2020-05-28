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

namespace GS1US.Tests.Common.Setup
{
    public static class SetupUtils
    {
        public static T ReadConfig<T>(string filename) where T: ITestConfig =>
            JsonConvert.DeserializeObject<T>(
                File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename)));

        public static IWebDriver CreateDriver(ITestConfig config)
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            var opt = new ChromeOptions();
            opt.AddArgument("--ignore-certificate-errors");
            if (config.Headless && !config.Debug)
            {
                opt.AddArgument("--window-size=1280,2048");
                opt.AcceptInsecureCertificates = true;
                opt.AddArgument("--headless");
            }
            opt.AddUserProfilePreference("download.default_directory", config.DownloadFolder);

            var driver = new ChromeDriver(driverService, opt, TimeSpan.FromMinutes(5));
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(5);

            return driver;
        }

        public static bool ShouldDestroyDriver(ITestConfig config, ScenarioContext context)
        {
            var error = context.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError;
            return !config.Debug && (config.Headless || !error);
        }
    }
}
