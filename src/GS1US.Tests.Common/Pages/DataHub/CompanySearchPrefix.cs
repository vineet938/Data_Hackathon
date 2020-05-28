using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class CompanySearchPrefix : CompanyTabs<CompanySearchPrefix>
    {
        public CompanySearchPrefix(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>(base.Locators)
        {
            {"Prefix", By.Id("txtPrefix") },
            {"Search", By.Id("btnApplyFilters") },
            {"FirstRow-Company", By.CssSelector("#SearchResultsTable tbody tr:first-child td:nth-child(4)") }
        };

        public CompanySearchPrefix EnterPrefix(string prefix)
        {
            elements["Prefix"].SetText(prefix);
            return this;
        }

        public CompanySearchPrefix ClickSearch()
        {
            elements["Search"].Click();
            WaitToDisappear(Driver, Locators["Processing"], 60);
            return this;
        }

        public string FirstCompanyName => elements["FirstRow-Company"].Text;
    }
}
