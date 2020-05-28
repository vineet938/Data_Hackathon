using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class CompanySearchGtin : CompanyTabs<CompanySearchGtin>
    {
        public CompanySearchGtin(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>(base.Locators)
        {
            {"GTIN", By.Id("txtGTN") },
            {"Search", By.Id("btnApplyFilters") },
            {"FirstRow-GTIN", By.CssSelector("#SearchResultsTable tbody tr:first-child td:nth-child(4)") }
        };

        public CompanySearchGtin EnterGtin(string gtin)
        {
            elements["GTIN"].SetText(gtin);
            return this;
        }

        public CompanySearchGtin ClickSearch()
        {
            elements["Search"].Click();
            WaitToDisappear(Driver, Locators["Processing"], 60);
            return this;
        }

        public string FirstGtin => elements["FirstRow-GTIN"].Text;
    }
}
