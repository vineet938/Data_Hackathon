using GS1US.Test.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.UIIS.Pages.SSO
{
    class MsSsoUserName : PagesCommon
    {
        public MsSsoUserName(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"username", By.XPath("//input[@name='loginfmt']") },
            {"next", By.Id("idSIButton9") }
        };

        protected override void WaitForPage()
        {
            WaitUtils.WaitLocator(Driver, Locators["username"], 15);
        }

        public MsSsoUserName EnterUserName(string username)
        {
            elements["username"].SetText(username);
            return this;
        }

        public MsSsoUserName ClickNext()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            elements["next"].Click();
            return this;
        }
    }
}
