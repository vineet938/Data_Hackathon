using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class ReportsCreate : PagesCommon<ReportsCreate>
    {
        public ReportsCreate(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"LocationShare", By.XPath("//*[contains(text(),'Location Share')]") },
            {"LocationShare-GLN", By.XPath("(//label[text()='GLN']/preceding-sibling::input)[3]") },
            {"OK", By.XPath("(//button[text()='OK'])[1]") }
        };

        public ReportsCreate ClickLocationShare()
        {
            Apply("LocationShare", 15, 1, Click);
            return this;
        }

        public ReportsCreate EnterGlnForLocationShare(string gln)
        {
            Apply("LocationShare-GLN", 15, 1, SetText(gln));
            return this;
        }

        public ReportsCreate ClickOK()
        {
            Apply("OK", 15, 1, Click);
            return this;
        }
    }
}
