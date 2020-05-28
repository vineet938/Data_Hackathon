using GS1US.Test.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.UIIS.Pages.SSO
{
    class MsSsoPassword : PagesCommon
    {
        public MsSsoPassword(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"password", By.XPath("//input[@name='passwd']") },
            {"next", By.Id("idSIButton9") }
        };

        protected override void WaitForPage()
        {
            WaitUtils.WaitLocator(Driver, Locators["password"], 15);
        }

        public MsSsoPassword EnterPassword(string password)
        {
            elements["password"].SetText(password);
            return this;
        }

        public MsSsoPassword ClickNext()
        {
            elements["next"].Click();
            return this;
        }
    }
}
