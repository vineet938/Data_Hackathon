using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class CompanyDownload : PagesCommon<CompanyDownload>
    {
        public CompanyDownload(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"DownloadActive", By.XPath("(//*[contains(@class,'gs1-card')])[2]//button") },
            {"DownloadActive-Size", By.XPath("(//*[contains(@class,'gs1-card')])[2]//p[3]") }
        };

        protected override void WaitForPage()
        {
            Wait(Driver, 30, d => !String.IsNullOrWhiteSpace(elements["DownloadActive-Size"].Text));
        }

        public CompanyDownload ClickDownloadActiveList()
        {
            WaitLocator(Driver, Locators["DownloadActive"], 15).Click();
            return this;
        }

        public (double Size, string Unit) ActiveListDownloadSize
        {
            get
            {
                var s = elements["DownloadActive-Size"].Text;
                var m = Regex.Match(s, @".*File Size:\s*([0-9.]+)\s*([A-Z]+)");
                var size = double.Parse(m.Groups[1].Value);
                var unit = m.Groups[2].Value;
                return (size, unit);
            }
        }
    }
}
