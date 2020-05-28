using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class AdminCompanySettings : PagesCommon<AdminCompanySettings>
    {
        public AdminCompanySettings(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"UmbrellaTab", By.Id("umbrellaTabLink") }
        };

        public bool IsUmbrellaTabShown(int n = 0)
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(n).Milliseconds);
            return elements["UmbrellaTab"].Displayed;
        }
    }
}
