using GS1US.Test.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.UIIS.Pages
{
    class Main : PagesCommon
    {
        public Main(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"dashboard", By.LinkText("Dashboard") },
            {"vendOrHold", By.LinkText("Vend or Hold an Identifier") },
            {"openPrefixRange", By.LinkText("Open a Prefix Range") },
            {"closePrefixRange", By.LinkText("Close a Prefix Range") },
            {"onHoldPrefixes", By.LinkText("On Hold Prefixes") }
        };

        protected override void WaitForPage()
        {
            var loc = By.CssSelector(".overlay");
            if (Driver.FindElements(loc).Count() > 0)
                WaitUtils.WaitToDisappear(Driver, loc, 15);
        }

        public Main SelectVendOrHold()
        {
            WaitUtils.WaitLocator(Driver, Locators["vendOrHold"], 5);
            elements["vendOrHold"].Click();
            return this;
        }

        public Main SelectOpenPrefixRange()
        {
            WaitUtils.WaitLocator(Driver, Locators["openPrefixRange"], 5);
            elements["openPrefixRange"].Click();
            return this;
        }

        public Main SelectClosePrefixRange()
        {
            WaitUtils.WaitLocator(Driver, Locators["closePrefixRange"], 5);
            elements["closePrefixRange"].Click();
            return this;
        }

        public Main SelectOnHoldPrefixes()
        {
            WaitUtils.WaitLocator(Driver, Locators["onHoldPrefixes"], 5);
            elements["onHoldPrefixes"].Click();
            return this;
        }
    }
}
