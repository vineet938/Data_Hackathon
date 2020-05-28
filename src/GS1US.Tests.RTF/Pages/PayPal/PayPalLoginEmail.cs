using GS1US.Tests.RTF.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GS1US.Tests.RTF.Pages.PayPal
{
    class PayPalLoginEmail : PagesCommon
    {
        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"email", By.Id("email") },
            {"next", By.Id("btnNext") },
        };

        public PayPalLoginEmail(IWebDriver driver): base(driver)
        {
        }

        protected override void WaitForPage()
        {
            WaitUtils.WaitToDisappear(Driver, By.ClassName("spinnerWithLockIcon"), 60);
            WaitUtils.WaitId(Driver, "email", 30);
            Thread.Sleep(1000);  // hack for unattached element issue
        }


        public PayPalLoginEmail FillEmail(string email)
        {
            elements["email"].SetText(email);
            return this;
        }

        public PayPalLoginEmail Next()
        {
            elements["next"].Click();
            return this;
        }

    }
}
