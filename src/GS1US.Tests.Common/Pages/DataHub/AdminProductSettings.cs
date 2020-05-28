using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class AdminProductSettings : PagesCommon<AdminProductSettings>
    {
        public AdminProductSettings(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"ShareAll", By.CssSelector("[for='chkShareAllproducts']") },
            {"Save", By.CssSelector("[value='Save']") }
        };

        public bool IsShareAllChecked()
        {
            var script = "return $('#chkShareAllproducts').scope().productSettings.EnableDefaultProductShare";
            return (bool)(Driver as IJavaScriptExecutor).ExecuteScript(script);
        }

        public AdminProductSettings SetShareAll(bool value)
        {
            if (IsShareAllChecked() ^ value)
            {
                elements["ShareAll"].Click();
            }
            return this;
        }

        public AdminProductSettings Save()
        {
            elements["Save"].Click();
            return this;
        }
    }
}
