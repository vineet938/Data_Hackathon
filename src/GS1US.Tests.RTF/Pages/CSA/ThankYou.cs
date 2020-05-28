using GS1US.Tests.RTF.Pages;
using GS1US.Tests.RTF.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.RTF.Pages.CSA
{
    class ThankYou
    {
        private readonly IWebDriver driver;
        private readonly PageElements elements;
        private readonly Dictionary<string, string> xpaths = new Dictionary<string, string>()
        {
            { "thankYou", "//*[normalize-space(text())='Thank you']" },
            { "prefixes", "//tr[./td/b[contains(text(), 'U.P.C. Company Prefix')]]/following-sibling::tr/td[last()]" },
            { "thankYouForApplying", "//b[starts-with(text(), 'Thank you for applying')]" },
        };

        public ThankYou(IWebDriver driver)
        {
            this.driver = driver;
            WaitUtils.WaitXpe(driver, xpaths["thankYou"], 120);
            elements = new PageElements(driver, xpaths);
        }

        public bool IsPrefixDisplayed() => Prefixes().Any();

        public bool IsThankYouForApplyingDisplayed() =>
            elements["thankYouForApplying"] != null;

        public string[] Prefixes()
        {
            var xpath = xpaths["prefixes"];
            return driver.FindElements(By.XPath(xpath)).Select(o => o.Text).ToArray();
        }
    }
}
