using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class ProductShareAddNew : PagesCommon<ProductShareAddNew>
    {
        public ProductShareAddNew(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"CompanyName", By.Id("dtShareEligibleCompaniesName1") },
            {"CompanyList-FirstRow", By.CssSelector("table#dtShareEligibleCompanies tbody tr:first-child") },
            {"Companies", By.XPath("//table[@id='dtShareEligibleCompanies']/tbody/tr") },
            {"ContinueButton", By.Id("btnContinue") }
        };

        public ProductShareAddNew FilterByCompanyName(string name)
        {
            elements["CompanyName"].SetText(name);
            Wait(Driver, 15, d => d.FindElements(Locators["Companies"]).Count == 1);
            return this;
        }

        public ProductShareAddNew SelectFirstRow()
        {
            Apply("CompanyList-FirstRow", 15, 1, Click);
            return this;
        }

        public ProductShareAddNew ClickContinue()
        {
            elements["ContinueButton"].Click();
            return this;
        }
    }
}
