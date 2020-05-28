using GS1US.Test.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.UIIS.Pages
{
    class OpenPrefixRange : PagesCommon
    {
        public OpenPrefixRange(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"upcRangeButton", By.CssSelector("label[for=UPC]") },
            {"eanRangeButton", By.CssSelector("label[for=EAN]") },
            {"autoVendingCheckbox", By.CssSelector("label.custom-checkbox span.checkbox") },
            {"reason", By.XPath("//span[text()='Why is the range being opened?']/following-sibling::input") },
            {"next", By.CssSelector("button.next.green") },

        };

        public OpenPrefixRange ClickUpcRangeButton()
        {
            WaitUtils.WaitLocator(Driver, Locators["upcRangeButton"], 5);
            elements["upcRangeButton"].Click();
            return this;
        }

        public OpenPrefixRange ClickEanRangeButton()
        {
            WaitUtils.WaitLocator(Driver, Locators["eanRangeButton"], 5);
            elements["eanRangeButton"].Click();
            return this;
        }

        public OpenPrefixRange EnterSpeficRange(string range)
        {
            WaitUtils.Wait(Driver, 15, d => d
                .FindElements(By.CssSelector("div.input-group input"))
                .Tail()
                .All(el => el.Enabled && el.Displayed));
            var elements = Driver.FindElements(By.CssSelector("div.input-group input"));
            range.Tail().Zip(elements.Tail(), (c, el) => el.SetText(c.ToString())).Last();
            return this;
        }

        public OpenPrefixRange ClickEnableAutomaticVending()
        {
            WaitUtils.WaitLocator(Driver, Locators["autoVendingCheckbox"], 5);
            elements["autoVendingCheckbox"].Click();
            return this;
        }

        public OpenPrefixRange EnterReasonForOpening(string reason)
        {
            WaitUtils.WaitLocator(Driver, Locators["reason"], 5);
            elements["reason"].SetText(reason);
            return this;
        }

        public OpenPrefixRange ClickNextButton()
        {
            WaitUtils.WaitLocator(Driver, Locators["next"], 5);
            elements["next"].Click();
            return this;
        }
    }
}
