using GS1US.Test.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.UIIS.Pages
{
    class OpenPrefixRangeReview : PagesCommon
    {
        public OpenPrefixRangeReview(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"openRangeButton", By.CssSelector("button.next.green") },
            {"error", By.CssSelector("div.error") }
        };

        public OpenPrefixRangeReview ClickOpenRange()
        {
            WaitUtils.WaitLocator(Driver, Locators["openRangeButton"], 5);
            elements["openRangeButton"].Click();
            return this;
        }

        private string ReadLabel(string elementKey)
        {
            var el = WaitUtils.WaitLocator(Driver, Locators[elementKey], 15);
            return el.Text;
        }

        public string GetError() => ReadLabel("error");

    }
}
