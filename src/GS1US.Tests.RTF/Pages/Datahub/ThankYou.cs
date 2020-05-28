using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS1US.Tests.RTF.Common;
using OpenQA.Selenium;

namespace GS1US.Tests.RTF.Pages.Datahub
{
    class ThankYou : PagesCommon
    {
        public ThankYou(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, string>
        {
            {"thankYou", "//*[contains(text(), 'Thank you for subscribing to the GS1 US Data Hub!')]" },
            {"confirmation", "//td[text()='Confirmation Number:']/following-sibling::td" }
        }.ToLocators();

        protected override void WaitForPage()
        {
            WaitUtils.WaitLocator(Driver, Locators["thankYou"], 180);
        }

        public string ConfirmationNumber
        {
            get => elements["confirmation"].Text;
        }
    }
}
