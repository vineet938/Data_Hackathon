using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.AccessControl
{
    public class Login : PagesCommon<Login>
    {
        public Login(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"LoginButton", By.Id("auth") }
        };

        protected override void WaitForPage()
        {
            WaitLocator(Driver, Locators["LoginButton"], 60);
        }

        public Login ClickLoginButton()
        {
            elements["LoginButton"].Click();
            return this;
        }
    }
}
