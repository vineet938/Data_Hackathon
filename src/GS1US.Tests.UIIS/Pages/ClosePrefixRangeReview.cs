using GS1US.Test.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.UIIS.Pages
{
    class ClosePrefixRangeReview : PagesCommon
    {
        public ClosePrefixRangeReview(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"closeRangeButton", By.CssSelector("button.next.red") }
        };

        public ClosePrefixRangeReview ClickCloseRange()
        {
            WaitUtils.WaitLocator(Driver, Locators["closeRangeButton"], 5);
            elements["closeRangeButton"].Click();
            return this;
        }
    }
}
