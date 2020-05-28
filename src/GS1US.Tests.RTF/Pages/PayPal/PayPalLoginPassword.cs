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
    class PayPalLoginPassword
    {
        private readonly PageElements elements;
        private readonly Dictionary<string, By> locators = new Dictionary<string, By>()
        {
            {"password", By.Id("password") },
            {"login", By.Id("btnLogin") },
        };

        public PayPalLoginPassword(IWebDriver driver)
        {
            WaitUtils.WaitToDisappear(driver, By.ClassName("spinnerWithLockIcon"), 60);
            WaitUtils.WaitId(driver, "password", 30);
            Thread.Sleep(1000);  // hack for unattached element issue

            elements = new PageElements(driver, locators);
        }

        public PayPalLoginPassword FillPassword(string password)
        {
            elements["password"].SetText(password);
            return this;
        }

        public PayPalLoginPassword Login()
        {
            elements["login"].Click();
            return this;
        }

    }
}
