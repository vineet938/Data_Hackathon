using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class CompanyTabs<T> : PagesCommon<T> where T : CompanyTabs<T>
    {
        public CompanyTabs(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"CompanyTab", By.LinkText("Company") },
            {"PrefixTab", By.LinkText("Prefix") },
            {"GtinTab", By.LinkText("GTIN") },
            {"GlnTab", By.LinkText("GLN") },
            {"Processing", By.Id("SearchResultsTable_processing") }
        };

        private T ClickTab(string name)
        {
            WaitLocator(Driver, Locators[name], 15).Click();
            return (T)this;
        }

        public T ClickCompanyTab() => ClickTab("CompanyTab");
        public T ClickPrefixTab() => ClickTab("PrefixTab");
        public T ClickGtinTab() => ClickTab("GtinTab");
        public T ClickGlnTab() => ClickTab("GlnTab");
    }
}
