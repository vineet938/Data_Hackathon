using GS1US.Test.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.UIIS.Pages
{
    class Confirmation : PagesCommon
    {
        public Confirmation(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"syncSuccessNotice", By.CssSelector("i.fa-check-circle") },
            {"vendedPrefix", By.CssSelector("div.prefix") },
            {"vendedEntityGLN", By.CssSelector("div.gln") },
            {"accountNumber", By.CssSelector("div.account-number") },
            {"companyName", By.CssSelector("div.company-name") },
            {"companyLocation", By.CssSelector("div.company-location") },
            {"identifierType", By.CssSelector("div.prefix-type") },
            {"capacity", By.CssSelector("div.capacity") },
            {"rangeType", By.CssSelector("div.range-type") }
        };

        public bool IsSyncSucceeded(int timeoutSecond=120)
        {
            var el = WaitUtils.WaitLocator(Driver, Locators["syncSuccessNotice"], timeoutSecond);
            return el != null;
        }

        private string ReadLabel(string elementKey)
        {
            var el = WaitUtils.WaitLocator(Driver, Locators[elementKey], 15);
            return el.Text;
        }

        public string GetVendedPrefix() => ReadLabel("vendedPrefix");

        public string GetVendedEntityGLN() => ReadLabel("vendedEntityGLN");

        public string GetAccountNumber() => ReadLabel("accountNumber");

        public string GetCompanyName() => ReadLabel("companyName");

        public string GetCompanyLocation() => ReadLabel("companyLocation");

        public string GetIdentifierType() => ReadLabel("identifierType");

        public int GetCapacity() => Int32.Parse(ReadLabel("capacity").Replace(",", ""));

        public string GetRangeType() => ReadLabel("rangeType");
    }
}
