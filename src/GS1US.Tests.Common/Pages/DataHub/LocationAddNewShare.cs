using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class LocationAddNewShare : PagesCommon<LocationAddNewShare>
    {
        public LocationAddNewShare(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"CompanyName", By.Id("SpecificCompanyTableName1") },
            {"FirstRow", By.CssSelector("#SpecificCompanyTable tbody tr:first-child td:first-child") },
            {"AddNew", By.XPath("//button[text()='Add New']") }
        };

        public LocationAddNewShare SearchCompany(string name)
        {
            elements["CompanyName"].SetText(name);
            Thread.Sleep(1000);
            return this;
        }

        public LocationAddNewShare ClickFirstRow()
        {
            Apply("FirstRow", 15, 1, x => x.Click());
            return this;
        }

        public LocationAddNewShare ClickAddNew()
        {
            elements["AddNew"].Click();
            return this;
        }
    }
}
