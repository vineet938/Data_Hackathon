using GS1US.Test.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.UIIS.Pages
{
    class OnHoldPrefixesConfirmation : PagesCommon
    {
        public OnHoldPrefixesConfirmation(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"vendPrefixButton", By.CssSelector("button.next.green") },
            {"Account Number", By.CssSelector("div.account-number") },
            {"Identifier Type", By.CssSelector("div.prefix-type") },
            {"Capacity", By.XPath("//div[text()='Capacity']/following-sibling::div") },
            {"Prefix", By.CssSelector("div.specific-prefix-request") },
            {"Reason", By.XPath("//div[text()='Reason']/following-sibling::div") },
            {"vendedPrefix", By.CssSelector("div.prefix") }
        };

        public OnHoldPrefixesConfirmation ClickVendPrefix()
        {
            WaitUtils.WaitLocator(Driver, Locators["vendPrefixButton"], 5);
            elements["vendPrefixButton"].Click();
            return this;
        }

        public bool HasLabel(string label) =>
            WaitUtils.WaitLocator(Driver, By.XPath($"//div[text()='{label}']"), 5) != null;

        public string ReadLabel(string elementKey)
        {
            var el = WaitUtils.WaitLocator(Driver, Locators[elementKey], 15);
            return el.Text;
        }

        public string GetAccountNumber() => ReadLabel("Account Number");

        public string GetIdentifierType() => ReadLabel("Identifier Type");

        public string GetCapacity() => ReadLabel("Capacity");

        public string GetPrefix() => ReadLabel("Prefix");

        public string GetReason() => ReadLabel("Reason");

        public string GetVendedPrefix() => ReadLabel("vendedPrefix");
    }
}
