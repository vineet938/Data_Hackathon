using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class LocationGlnSearch : PagesCommon<LocationGlnSearch>
    {
        public LocationGlnSearch(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"LocationName", By.Id("txtCompanyName") },
            {"Search", By.Id("btnApplyFilters") },
            {"Spinner", By.CssSelector("[name='chopper']") }
        };

        public LocationGlnSearch EnterName(string name)
        {
            elements["LocationName"].SetText(name);
            return this;
        }

        public LocationGlnSearch ClickSearch()
        {
            elements["Search"].Click();
            return this;
        }

        public LocationGlnSearch WaitSpinner()
        {
            WaitToDisappear(Driver, Locators["Spinner"], 60);
            return this;
        }
    }
}
