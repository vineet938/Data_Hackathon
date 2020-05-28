using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class CompanySearchGln : CompanyTabs<CompanySearchGln>
    {
        public CompanySearchGln(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>(base.Locators)
        {
            {"GLN", By.Id("txtGLN") },
            {"Search", By.Id("btnApplyFilters") },
            {"FirstRow-GLN", By.CssSelector("#SearchResultsTable tbody tr:first-child td:nth-child(4)") }
        };

        public CompanySearchGln EnterGln(string gln)
        {
            elements["GLN"].SetText(gln);
            return this;
        }

        public CompanySearchGln ClickSearch()
        {
            elements["Search"].Click();
            WaitToDisappear(Driver, Locators["Processing"], 60);
            return this;
        }

        public string FirstGln => elements["FirstRow-GLN"].Text;
    }
}
