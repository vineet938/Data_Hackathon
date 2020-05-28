using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS1US.Tests.RTF.Common;
using OpenQA.Selenium;

namespace GS1US.Tests.RTF.Pages.PayPal
{
    class PayPalContinue : IPayPalContinue
    {
        private PageElements elements;
        private Dictionary<string, string> xpaths = new Dictionary<string, string>()
        {
            {"continue", "//*[@id='continue_abovefold']" },
        };

        public PayPalContinue(IWebDriver driver)
        {
            WaitUtils.WaitId(driver, "continue_abovefold", 30);

            elements = new PageElements(driver, xpaths);
        }

        public IPayPalContinue Continue()
        {
            elements["continue"].Click();
            return this;
        }
    }
}
