using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class LocationSharing : LocationTabs<LocationSharing>
    {
        public LocationSharing(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>(base.Locators)
        {
            {"AddNew", By.XPath("//button[text()='Add New']") }
        };

        public LocationSharing ClickAddNew()
        {
            Apply("AddNew", 5, 1, Click);
            return this;
        }
    }
}
