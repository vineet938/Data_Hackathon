using GS1US.Test.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.UIIS.Pages
{
    class OpenPrefixRangeConfirmation : PagesCommon
    {
        public OpenPrefixRangeConfirmation(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"title", By.CssSelector("div.title") }
        };

        private string ReadLabel(string elementKey)
        {
            var el = WaitUtils.WaitLocator(Driver, Locators[elementKey], 15);
            return el.Text;
        }

        public string GetTitle() => ReadLabel("title");

    }
}
