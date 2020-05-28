using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS1US.Tests.Common.Utils;

namespace GS1US.Tests.Common.Pages.UserPortal
{
    public class LoginPage : PagesCommon<LoginPage>
    {
        protected override Dictionary<string, By> Locators
        {
            get =>
                new Dictionary<string, By>
                {
                    {"username", By.Id("signInName") },
                    {"password", By.Id("password") },
                    {"signIn", By.Id("next") }
                };
        }

        public LoginPage(IWebDriver driver) : base(driver) { }

        public LoginPage Login(string username, string password)
        {
            elements["username"].SetText(username);
            elements["password"].SetText(password);
            elements["signIn"].Click();
            return this;
        }

        protected override void WaitForPage()
        {
            WaitUtils.WaitLocator(Driver, Locators["password"], 10);
        }
    }
}
