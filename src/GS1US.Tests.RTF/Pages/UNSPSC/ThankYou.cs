using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS1US.Tests.RTF.Common;
using OpenQA.Selenium;

namespace GS1US.Tests.RTF.Pages.UNSPSC
{
    class ThankYou : PagesCommon
    {
        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"welcome", By.XPath("//p[text()='Welcome to UNSPSC!']") }
        };

        public ThankYou(IWebDriver driver) : base(driver)
        {
        }

        protected override void WaitForPage()
        {
            WaitUtils.WaitLocator(Driver, Locators["welcome"], 60);
        }

        public bool IsWelcomeToUnspscDisplayed() =>
            elements["welcome"].Displayed;

    }
}
