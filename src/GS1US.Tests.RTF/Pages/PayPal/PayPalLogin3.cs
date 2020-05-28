using GS1US.Tests.RTF.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.RTF.Pages.PayPal
{
    class PayPalLogin3 : IPayPalLogin
    {
        private PageElements elements;
        private Dictionary<string, string> xpaths = new Dictionary<string, string>()
        {
            {"email", "//*[@id='email']" },
            {"password", "//*[@id='password']" },
            {"login", "//*[@id='btnLogin']" }
        };

        public PayPalLogin3(IWebDriver driver)
        {
            //WaitUtils.WaitIdToDisappear(driver, "preloaderSpinner", 60);
            WaitUtils.WaitId(driver, "btnLogin", 60);

            elements = new PageElements(driver, xpaths);
        }

        public IPayPalLogin FillCredentials(string email, string password)
        {
            elements["email"].SetText(email);
            elements["password"].SetText(password);
            return this;
        }

        public IPayPalLogin Login()
        {
            elements["login"].Click();
            return this;
        }
    }
}
