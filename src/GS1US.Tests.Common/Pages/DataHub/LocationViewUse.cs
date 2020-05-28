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
    public class LocationViewUse : PagesCommon<LocationViewUse>
    {
        public LocationViewUse(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"Search2", By.Id("searchButton1") },
            {"Industry", By.Id("industry") },
            {"Export",  By.XPath("//button[contains(text(),'Export') and @data-toggle]")},
            {"Processing", By.Id("ViewUseResultsTable_processing") },
            {"TableInfo", By.Id("ViewUseResultsTable_info") },
            {"Clear", By.XPath("(//button[text()='Clear Search Criteria'])[1]") },
            {"MyLocations", By.CssSelector("[name='chkIncludeMyLocations'] + label") },
            {"GLN", By.XPath("//input[@placeholder='GLN']") },
            {"FirstGLN", By.CssSelector("#ViewUseResultsTable tbody tr:first-child td:nth-child(2)") },
            {"FirstLink", By.CssSelector("#ViewUseResultsTable tbody tr:first-child td:nth-child(3) a") }
        };

        public LocationViewUse SelectIndustry(string industry)
        {
            elements["Industry"].Select(industry);
            return this;
        }

        public LocationViewUse ClickSearch2()
        {
            elements["Search2"].ScrollTo().Click();
            Thread.Sleep(1000);
            WaitToDisappear(Driver, Locators["Processing"], 60);
            return this;
        }

        public LocationViewUse ClickExport()
        {
            elements["Export"].ScrollTo().Click();
            return this;
        }

        public LocationViewUse ClickClear()
        {
            ScrollToTop();
            elements["Clear"].Click();
            return this;
        }

        public LocationViewUse ClickMyLocations()
        {
            ScrollToTop();
            Thread.Sleep(1000);
            elements["MyLocations"].Click();
            return this;
        }

        public LocationViewUse SearchGLN(string gln)
        {
            elements["GLN"].SetText(gln);
            return this;
        }

        public LocationViewUse ClickFirstLink()
        {
            elements["FirstLink"].Click();
            return this;
        }

        public int TableSize
        {
            get
            {
                var el = Driver.FindElement(Locators["TableInfo"]);
                var m = Regex.Match(el.Text, @".* of (\d+) entries");
                return int.Parse(m.Groups[1].Value);
            }
        }

        public bool IsMyLocationsChecked {
            get {
                var script = "return $('#chkIncludeMyLocations').scope().model.Data.IncludeMyLocation";
                return (bool)(Driver as IJavaScriptExecutor).ExecuteScript(script);
            }
        }

        public string FirstGln => elements["FirstGLN"].Text;
    }
}
