using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace GS1US.Tests.UIIS.Setup
{
    using GS1US.Test.Common;
    using GS1US.Tests.UIIS.Setup.Config;
    using Newtonsoft.Json;
    using OpenQA.Selenium.Chrome;
    using Shouldly;
    using System.IO;
    
    [Binding]
    class TestSetup
    {
        public static TestConfig Config = JsonConvert.DeserializeObject<TestConfig>(
            File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testconfig.json")));

        public static UiisContext UiisContext = new UiisContext();
        public static Database.IMIS Imis;
        public static Database.UIIS UiisDb;

        public static IWebDriver Driver;


        [BeforeScenario(Order = 30)]
        public static void CreateDriver()
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            var opt = new ChromeOptions();
            opt.AddArgument("--ignore-certificate-errors");
            if (Config.headless && !Config.debug)
            {
                opt.AddArgument("--window-size=1280,2048");
                opt.AcceptInsecureCertificates = true;
                opt.AddArgument("--headless");
            }

            Driver = new ChromeDriver(driverService, opt, TimeSpan.FromMinutes(5));
        }

        [AfterScenario(Order = 100)]
        public static void DestroyDriver()
        {
            var error = ScenarioContext.Current.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError;
            if (!Config.debug && (Config.headless || !error))
            {
                if (Driver != null)
                {
                    Driver.Quit();
                    Driver = null;
                }
            }
        }

        [BeforeScenario(Order = 20)]
        public static void ConnectToImis()
        {
            var conn = new SqlConnection(Config.connectionString.imis);
            //ScenarioContext.Current.Add(ContextKeys.IDM, new IDM(conn));

            var delta = conn.QuerySingle<DateTime>("select getdate()") - DateTime.Now;
            Imis = new Database.IMIS(conn, delta);

        }

        [AfterScenario(Order = 20)]
        public static void DisconnectFromImis()
        {
            Imis.Close();
            //ScenarioContext.Current.Get<IDM>(ContextKeys.IDM).Close();
        }

        [BeforeScenario(Order = 21)]
        public static void ConnectToUiisDb()
        {
            var conn = new SqlConnection(Config.connectionString.uiis);
            UiisDb = new Database.UIIS(conn);
        }

        [AfterScenario(Order = 21)]
        public static void DisconnectFromUiisDb()
        {
            UiisDb.Close();
        }

        [AfterScenario(Order = 10)]
        public static void TakeScreenshot()
        {
            var prefix = ScenarioContext.Current.ScenarioInfo.Title.Replace(" ", "_");
            var date = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            var uuid = Guid.NewGuid().ToString();
            var filename = $"{prefix}-{date}-{uuid}.png";
            var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            screenshot.SaveAsFile(filename);
            var dir = Directory.GetCurrentDirectory();
            var path = Path.Combine(dir, filename);
            Console.WriteLine($"Screenshot: file:///{path}");
        }
    }
}
