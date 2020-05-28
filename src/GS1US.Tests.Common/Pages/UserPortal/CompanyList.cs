using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using GS1US.Tests.Common.Utils;

namespace GS1US.Tests.Common.Pages.UserPortal
{
    public class CompanyList : PagesCommon<CompanyList>
    {
        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"dropdown", By.Id("companies") },
            {"signIn",  By.Id("continue") }
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
            WaitUtils.WaitLocator(Driver, Locators["dropdown"], 20);
        }
    }
}
