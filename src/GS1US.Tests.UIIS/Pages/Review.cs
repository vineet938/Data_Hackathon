using GS1US.Test.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.UIIS.Pages
{
    class Review : PagesCommon
    {
        public Review(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"vendIdentifier", By.XPath("//span[text()='VEND IDENTIFIER']") },
            {"holdPrefix", By.XPath("//span[text()='HOLD PREFIX']") }
        };

        public Review ClickVendIdentifier()
        {
            WaitUtils.WaitLocator(Driver, Locators["vendIdentifier"], 5);
            elements["vendIdentifier"].Click();
            return this;
        }

        public Review ClickHoldPrefix()
        {
            WaitUtils.WaitLocator(Driver, Locators["holdPrefix"], 5);
            elements["holdPrefix"].Click();
            return this;
        }
    }
}
