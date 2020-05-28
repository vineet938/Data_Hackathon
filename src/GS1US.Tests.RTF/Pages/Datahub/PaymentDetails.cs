using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS1US.Tests.RTF.Common;
using OpenQA.Selenium;

namespace GS1US.Tests.RTF.Pages.Datahub
{
    using static LocUtils;
    using static WaitUtils;

    class PaymentDetails : PagesCommon
    {
        public PaymentDetails(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, string>
        {
            {"paypalButton", Make_SuffixXpe("imgPayPal") }
        }.ToLocators();

        protected override void WaitForPage()
        {
            WaitLocator(Driver, Locators["paypalButton"], 60);
        }

        public PaymentDetails ClickPayPalButton()
        {
            elements["paypalButton"].Click();
            return this;
        }
    }
}
