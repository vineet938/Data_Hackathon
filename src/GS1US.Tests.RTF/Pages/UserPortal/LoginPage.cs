using GS1US.Tests.RTF.Steps;
using GS1US.Tests.RTF.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.RTF.Pages.UserPortal
{
    class LoginPage : PagesCommon
    {
        protected override Dictionary<string, By> Locators
        {
            get =>
                new Dictionary<string, By>
                {
                    {"username", By.Id("UserName") },
                    {"password", By.Id("Password") },
                    {"signIn", By.CssSelector("input.primaryBtn") }
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
            WaitUtils.WaitId(Driver, "UserName", 10);
        }
    }
}
