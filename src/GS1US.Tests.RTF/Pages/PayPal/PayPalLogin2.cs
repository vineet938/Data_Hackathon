using GS1US.Tests.RTF.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.RTF.Pages.PayPal
{
    class PayPalLogin2 : IPayPalLogin
    {
        private PageElements elements;
        private Dictionary<string, string> xpaths = new Dictionary<string, string>()
        {
            {"email", "//*[@id='login_email']" },
            {"password", "//*[@id='password']" },
            {"login", "//*[@id='btnLogin']" }
        };

        public PayPalLogin2(IWebDriver driver)
        {
            WaitUtils.WaitId(driver, "btnLogin", 10);

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
