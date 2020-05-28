using GS1US.Test.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.UIIS.Pages.SSO
{
    class MsSsoStaySignedIn : PagesCommon
    {
        public MsSsoStaySignedIn(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"no", By.Id("idBtn_Back") }
        };

        protected override void WaitForPage()
        {
            WaitUtils.WaitLocator(Driver, Locators["no"], 15);
        }

        public MsSsoStaySignedIn ClickNo()
        {
            elements["no"].Click();
            return this;
        }
    }
}
