using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class LocationDownload : PagesCommon<LocationDownload>
    {
        public LocationDownload(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"Download", By.XPath("//button[text()='Download']") },
            {"Spinner", By.CssSelector("[name='chopper']") },
            {"DownloadSize", By.XPath("//button[text()='Download']/preceding-sibling::*[1]") },
            {"DateCreated", By.XPath("//button[text()='Download']/preceding-sibling::*[2]") }
        };

        public LocationDownload ClickDownload()
        {
            elements["Download"].Click();
            Thread.Sleep(1000);
            return this;
        }

        public LocationDownload WaitSpinner()
        {
            WaitToDisappear(Driver, Locators["Spinner"], 180);
            return this;
        }

        public int DownloadSize
        {
            get
            {
                var s = elements["DownloadSize"].Text;
                var m = Regex.Match(s, @"File Size:\s+([0-9.]+)\s*MB");
                var x = float.Parse(m.Groups[1].Value);
                return Convert.ToInt32(x * 1024 * 1024);
            }
        }

        public DateTime DateCreated
        {
            get
            {
                var s = elements["DateCreated"].Text;
                var m = Regex.Match(s, @"Date Created:\s+([0-9/]+)");
                DateTime.TryParse(m.Groups[1].Value, out DateTime d);
                return d;
            }
        }
    }
}
