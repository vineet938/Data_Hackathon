using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using GS1US.Tests.RTF.Database;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace GS1US.Tests.RTF.Setup
{
    using GS1US.Tests.RTF.Common;
    using OpenQA.Selenium.Chrome;
    using Shouldly;
    using System.IO;
    using static ConfigKeys;

    [Binding]
    class TestSetup
    {
        private static readonly bool headless = ConfigurationManager.AppSettings[HEADLESS] == "Yes";
        private static readonly string driverDir = ConfigurationManager.AppSettings[DRIVER_LOC];
        private static readonly string imisConnStr = ConfigurationManager.AppSettings[IMIS_CONN_STRING];
        private static readonly bool debug = ConfigurationManager.AppSettings[DEBUG] == "Yes";

        public static string AppUrl = ConfigurationManager.AppSettings[ConfigKeys.CSA_URL];
        public static string PayPalUsername = ConfigurationManager.AppSettings[ConfigKeys.PAYPAL_USER];
        public static string PayPalPassword = ConfigurationManager.AppSettings[ConfigKeys.PAYPAL_PASSWORD];
        public static string MembershipAppUrl = ConfigurationManager.AppSettings[ConfigKeys.MEMBER_APP_URL];
        public static string DatahubUrl = ConfigurationManager.AppSettings[ConfigKeys.DATAHUB_URL];
        public static string UnspscUrl = ConfigurationManager.AppSettings[ConfigKeys.UNSPSC_URL];
        public static string TestAccountEmail = ConfigurationManager.AppSettings[ConfigKeys.TESTACCOUNT_EMAIL];
        public static string TestAccountEmailPassword = ConfigurationManager.AppSettings[ConfigKeys.TESTACCOUNT_EMAIL_PASSWORD];
        public static string TestAccountPortalPassword = ConfigurationManager.AppSettings[ConfigKeys.TESTACCOUNT_PORTAL_PASSWORD];
        public static string TestAccountCompanyName = ConfigurationManager.AppSettings[ConfigKeys.TESTACCOUNT_COMPANY_NAME];
        public static string TestAccountImapHost = ConfigurationManager.AppSettings[ConfigKeys.TESTACCOUNT_IMAP_HOST];
        public static int TestAccountImapPort = Int32.Parse(ConfigurationManager.AppSettings[ConfigKeys.TESTACCOUNT_IMAP_PORT]);
        public static int DbTimeout = Int32.Parse(ConfigurationManager.AppSettings[ConfigKeys.DB_TIMEOUT]);
        public static int DbPollInterval = Int32.Parse(ConfigurationManager.AppSettings[ConfigKeys.DB_POLL_INTERVAL]);
        public static int LogicAppWait = Int32.Parse(ConfigurationManager.AppSettings[ConfigKeys.LOGIC_APP_WAIT]);

        public static IWebDriver Driver;


        [BeforeScenario(Order = 30)]
        public static void CreateDriver()
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            var opt = new ChromeOptions();
            opt.AddArgument("--ignore-certificate-errors");
            if (headless && !debug)
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
            if (!(debug && error))
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
            var conn = new SqlConnection(imisConnStr);
            ScenarioContext.Current.Add(ContextKeys.IMIS, new IMIS(conn));
            ScenarioContext.Current.Add(ContextKeys.IDM, new IDM(conn));

            var delta = conn.QuerySingle<DateTime>("select getdate()") - DateTime.Now;
            ScenarioContext.Current.Add(ContextKeys.IMIS_DTIMEDIFF, delta);
        }

        [AfterScenario(Order = 20)]
        public static void DisconnectFromImis()
        {
            ScenarioContext.Current.Get<IMIS>(ContextKeys.IMIS).Close();
            ScenarioContext.Current.Get<IDM>(ContextKeys.IDM).Close();
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

        [AfterScenario(Order = 10)]
        public static void ReleaseCompany()
        {
            var ctx = ScenarioContext.Current.Get<CsaContext>(ContextKeys.POM_CONTEXT);
            if (ctx.OriginalAccount != null)
                LockUtils.ReleaseAccount("coid", ctx.OriginalAccount["CM"].First().ID);
        }
    }
}
