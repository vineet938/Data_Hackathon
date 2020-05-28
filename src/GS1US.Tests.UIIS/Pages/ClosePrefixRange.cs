using GS1US.Test.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.UIIS.Pages
{
    class ClosePrefixRange : PagesCommon
    {
        public ClosePrefixRange(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"range", By.CssSelector("div.close-range-input input") },
            {"reason", By.XPath("//label[text()='Why is the prefix range being closed?']/following-sibling::input") },
            {"next", By.CssSelector("button.next.green") },

        };

        public ClosePrefixRange EnterRange(string range)
        {
            WaitUtils.WaitLocator(Driver, Locators["range"], 5);
            elements["range"].SetText(range);
            return this;
        }

        public ClosePrefixRange EnterReasonForClosing(string reason)
        {
            WaitUtils.WaitLocator(Driver, Locators["reason"], 5);
            elements["reason"].SetText(reason);
            return this;
        }

        public ClosePrefixRange ClickNextButton()
        {
            WaitUtils.WaitLocator(Driver, Locators["next"], 5);
            elements["next"].Click();
            return this;
        }
    }
}
