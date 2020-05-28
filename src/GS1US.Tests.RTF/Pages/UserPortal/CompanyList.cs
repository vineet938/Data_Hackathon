using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS1US.Tests.RTF.Common;
using OpenQA.Selenium;

namespace GS1US.Tests.RTF.Pages.UserPortal
{
    class CompanyList : PagesCommon
    {
        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"dropdown", By.Id("SelectedCompanyId") },
            {"signIn",  By.CssSelector("input.primaryBtn") }
        };

        public CompanyList(IWebDriver driver) : base(driver)
        {
        }

        public CompanyList SelectCompany(string name)
        {
            elements["dropdown"].Select(name);
            return this;
        }

        public CompanyList SignIn()
        {
            elements["signIn"].Click();
            return this;
        }

        protected override void WaitForPage()
        {
            WaitUtils.WaitId(Driver, "SelectedCompanyId", 20);
        }
    }
}
