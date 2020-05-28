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
    class PayPalContinue3 : IPayPalContinue
    {
        private readonly IWebDriver driver;
        private readonly PageElements elements;
        private readonly Dictionary<string, By> locators = new Dictionary<string, By>()
        {
            {"continue", By.Id("confirmButtonTop")},
        };

        public PayPalContinue3(IWebDriver driver)
        {
            this.driver = driver;

            WaitUtils.WaitIdToDisappear(driver, "preloaderSpinner", 60);
            WaitUtils.WaitLocators(driver, 30, locators["continue"]);
            Task.Delay(5000).Wait(); // elements are still being created by java script

            elements = new PageElements(driver, locators);
        }

        public IPayPalContinue Continue()
        {
            elements["continue"].Click();
            return this;
        }
    }
}
