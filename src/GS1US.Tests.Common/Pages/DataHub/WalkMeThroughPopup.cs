using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class WalkMeThroughPopup : PagesCommon<WalkMeThroughPopup>
    {
        public WalkMeThroughPopup(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            { "popup", By.CssSelector("div.wm-outer-div") },
            { "close", By.CssSelector("span.walkme-action-destroy-1") }
        };

        protected override void WaitForPage()
        {
            WaitLocator(Driver, Locators["popup"], 60);
        }

        public WalkMeThroughPopup Close()
        {
            elements["close"].Click();
            return this;
        }
    }
}
