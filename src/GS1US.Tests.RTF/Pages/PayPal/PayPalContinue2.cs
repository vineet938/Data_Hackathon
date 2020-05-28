using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS1US.Tests.RTF.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace GS1US.Tests.RTF.Pages.PayPal
{
    class PayPalContinue2 : IPayPalContinue
    {
        private IWebDriver driver;
        private PageElements elements;
        private Dictionary<string, string> xpaths = new Dictionary<string, string>()
        {
            {"continue", "//*[@id='button']" },
        };

        public PayPalContinue2(IWebDriver driver)
        {
            this.driver = driver;

            WaitUtils.Wait(driver, 30, (d) =>
            {
                try
                {
                    var b1 = !d.FindElement(By.XPath("//*[@id='preloaderSpinner']")).Displayed;
                    var b2 = d.FindElement(By.XPath("//*[@id='button']")).Displayed;
                    return b1 && b2;
                }
                catch
                {
                    return false;
                }
            });

            
            elements = new PageElements(driver, xpaths);
        }

        public IPayPalContinue Continue()
        {
            elements["continue"].Click();
            return this;
        }
    }
}
