using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class LocationDetails : LocationTabs<LocationDetails>
    {
        public LocationDetails(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>(base.Locators)
        {
            {"SendMessage", By.XPath("//span[@title='Send Message']") },
            {"MakeInactive", By.Id("btnMakeInactiveLocation") },
            {"Approve", By.Id("btnApprove") },
            {"Spinner", By.CssSelector("[name='chopper']") }
        };

        public LocationDetails ClickSendMessage()
        {
            WaitLocator(Driver, Locators["SendMessage"], 30).Click();
            return this;
        }

        public LocationDetails ClickMakeInactive()
        {
            WaitLocator(Driver, Locators["MakeInactive"], 30).ScrollTo();
            Thread.Sleep(1000);
            elements["MakeInactive"].Click();
            return this;
        }

        public LocationDetails ClickApprove()
        {
            WaitLocator(Driver, Locators["Approve"], 30).ScrollTo();
            Thread.Sleep(1000);
            Apply("Approve", 5, 1, Click);
            return this;
        }

        public LocationDetails WaitSpinner()
        {
            WaitToDisappear(Driver, Locators["Spinner"], 180);
            return this;
        }

    }
}
