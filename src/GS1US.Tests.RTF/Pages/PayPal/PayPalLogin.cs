using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS1US.Tests.RTF.Common;
using OpenQA.Selenium;

namespace GS1US.Tests.RTF.Pages.PayPal
{
    class PayPalLogin : IPayPalLogin
    {
        private PageElements elements;
        private Dictionary<string, string> xpaths = new Dictionary<string, string>()
        {
            {"email", "//*[@id='login_email']" },
            {"password", "//*[@id='login_password']" },
            {"login", "//*[@id='submitLogin']" }
        };

        public PayPalLogin(IWebDriver driver)
        {
            WaitUtils.WaitId(driver, "login_email", 10);

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
