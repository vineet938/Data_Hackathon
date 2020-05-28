using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class LocationManage : PagesCommon<LocationImport>
    {
        public LocationManage(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"Processing", By.Id("dtLocationList_processing") },
            {"TopLevelGLN", By.XPath("//*[text()='Top Level GLN:']/following-sibling::h4/a") },
            {"SearchGLN", By.Id("dtLocationListGLN2") },
            {"SearchResult-FirstLink", By.CssSelector("#dtLocationList tbody tr:first-child td:nth-child(3) a") }
        };

        protected override void WaitForPage()
        {
            WaitLocator(Driver, Locators["TopLevelGLN"], 15);
        }

        public string TopLevelGln => elements["TopLevelGLN"].Text;

        public LocationManage SearchGLn(string gln)
        {
            elements["SearchGLN"].SetText(gln);
            WaitToDisappear(Driver, Locators["Processing"], 60);
            Thread.Sleep(1000);
            return this;
        }

        public LocationManage ClickFirstLocation()
        {
            elements["SearchResult-FirstLink"].Click();
            return this;
        }
    }
}
