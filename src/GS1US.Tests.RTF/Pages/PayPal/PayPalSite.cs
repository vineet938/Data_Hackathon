using GS1US.Tests.RTF.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.RTF.Pages.PayPal
{
    using static WaitUtils;

    class PayPalSite
    {
        private int signature;
        private IWebDriver driver;

        public PayPalSite(IWebDriver driver)
        {
            this.driver = driver;

            Task.Delay(1000).Wait();
            if (driver.FindElements(By.Id("preloaderSpinner")).Count > 0)
                WaitIdToDisappear(driver, "preloaderSpinner", 120);

            var r = WaitUtils.WaitLocators(driver, 60,
                By.Id("confirmButtonTop"),
                By.XPath("//b[contains(text(), 'Have a PayPal account?')]"),
                By.XPath("//h1[contains(text(), 'Pay with PayPal')]"),
                By.Id("miniCart"),
                By.Id("btnLogin")
            );

            signature = r.Item1 ?? -1;

            if (signature == 2)
            {
                var a = driver.FindElements(By.Id("email"));
                if (a != null && a.Count > 0)
                    signature = 11;
            }
            Console.WriteLine($"PAYPAL SITE SIGNATURE {signature}");
        }

        public void Pay(string username, string password)
        {
            switch (signature)
            {
                case 0:  // confirmation page for style B & C
                    new PayPalContinue3(driver).Continue();
                    break;

                case 1:  // full login style B (no username nor password field)
                    driver.FindElement(By.XPath("//a[text()='Log In']")).Click();
                    new PayPalLoginEmail(driver).FillEmail(username).Next();
                    new PayPalLoginPassword(driver).FillPassword(password).Login();
                    new PayPalContinue3(driver).Continue();
                    break;

                case 2:  // full login style C (look&feel similar to B; username and password fields are visible)
                    var page = new PayPalLogin3(driver);
                    Task.Delay(5000).Wait();  // Elements might be still being generate by JS.
                    page.FillCredentials(username, password).Login();
                    new PayPalContinue3(driver).Continue();
                    break;
                    
                case 3:  // full login style A (username and password fields need to be loaded)
                    driver.FindElement(By.XPath("//*[@id='loadLogin']")).Click();
                    new PayPalLogin(driver).FillCredentials(username, password).Login();
                    new PayPalContinue(driver).Continue();
                    break;

                case 4:
                    new PayPalLogin2(driver).FillCredentials(username, password).Login();
                    new PayPalContinue2(driver).Continue();
                    break;

                case 11:  // like case 1, but starts from email page
                    new PayPalLoginEmail(driver).FillEmail(username).Next();
                    new PayPalLoginPassword(driver).FillPassword(password).Login();
                    new PayPalContinue3(driver).Continue();
                    break;

                default:
                    return;
            }
        }
    }
}
