using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;
using static GS1US.Tests.Common.Utils.PollUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class LocationCreate : PagesCommon<LocationCreate>
    {
        public LocationCreate(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"LocationName", By.Id("Location_Name") },
            {"ParentButton", By.XPath("//div[@ng-bind-html='ParentLink']/following-sibling::button") },
            {"Save", By.Id("btnSaveLocation") },
            {"Industry", By.Id("IndustryID") },
            {"ServiceChainRole", By.Id("SupplyChainRoleID") },
            {"Address1", By.Id("Location_Address_AddressLine1") },
            {"Address2", By.Id("Location_Address_AddressLine2") },
            {"City", By.Id("Location_Address_City") },
            {"State", By.Id("Address_StateList") },
            {"Zip", By.Id("Location_Address_Zip") },
            {"Phone", By.Id("Location_Address_Phone") },
            {"AttLocType", By.CssSelector("[data-target='#collapse1']") },
            {"Relationship", By.CssSelector("[data-target='#collapse2']") },
            {"HC Corporate Relationship", By.Id("HCCorporateRelationship") },
            {"ClassOfTrade1", By.Id("ClassOfTrade1") },
            {"ClassOfTrade2", By.Id("ClassOfTrade2-3") },
            {"ClassOfTrade3", By.Id("ClassOfTrade3") },
            {"MakeActive", By.Id("btnMakeActiveLocation") },
            {"Spinner", By.CssSelector("[name='chopper']") },
            {"Approve", By.Id("btnApprove") },
            {"GLN", By.Id("Location_GLN") }

        };

        public LocationCreate EnterLocationName(string name)
        {
            ScrollToTop();
            Thread.Sleep(1000);
            elements["LocationName"].SetText(name);
            return this;
        }

        public LocationCreate ClickSetParentButton()
        {
            ScrollToTop();
            Thread.Sleep(1000);
            elements["ParentButton"].Click();
            return this;
        }

        public LocationCreate ClickSave()
        {
            elements["Save"].ScrollTo();
            Thread.Sleep(1000);
            elements["Save"].Click();
            return this;
        }

        public LocationCreate SelectIndustry(string industry)
        {
            elements["Industry"].Select(industry);
            return this;
        }

        public LocationCreate SelectSupplyChainRole(string role)
        {
            elements["ServiceChainRole"].Select(role);
            return this;
        }

        public LocationCreate EnterAddress1(string s)
        {
            elements["Address1"].SetText(s);
            return this;
        }

        public LocationCreate EnterAddress2(string s)
        {
            elements["Address2"].SetText(s);
            return this;
        }

        public LocationCreate EnterCity(string s)
        {
            elements["City"].SetText(s);
            return this;
        }

        public LocationCreate SelectState(string s)
        {
            elements["State"].Select(s);
            return this;
        }

        public LocationCreate EnterZip(string s)
        {
            elements["Zip"].SetText(s);
            return this;
        }

        public LocationCreate EnterPhone(string s)
        {
            elements["Phone"].SetText(s);
            return this;
        }

        public LocationCreate ExpandBusinessAttributesLocationType()
        {
            ScrollToTop();
            Thread.Sleep(1000);
            WaitLocator(Driver, Locators["AttLocType"], 30)
                .ScrollTo();
            Thread.Sleep(1000);
            elements["AttLocType"].Click();
            return this;
        }

        public LocationCreate ExpandSectorCorporateRelationship()
        {
            ScrollToTop();
            Thread.Sleep(1000);
            WaitLocator(Driver, Locators["Relationship"], 30)
                .ScrollTo();
            Thread.Sleep(1000);
            elements["Relationship"].Click();
            return this;
        }

        public LocationCreate ClickLocationType(string s)
        {
            ScrollToTop();
            Thread.Sleep(1000);
            var loc = By.XPath($"//label[normalize-space(text())='{s}']");
            var el = Poll<IWebElement>(
                $"Waiting for checkbox: {s}", 30, 2,
                () => Driver.FindElement(loc),
                x => x != null);
            el.ScrollTo(0, 500);
            Thread.Sleep(1000);
            el.Click();
            return this;
        }

        public LocationCreate SelectHcCorporateRelationship(string s)
        {
            elements["HC Corporate Relationship"].Select(s);
            return this;
        }

        public LocationCreate SelectClassOfTrade1(string s)
        {
            elements["ClassOfTrade1"].Select(s);
            return this;
        }

        public LocationCreate SelectClassOfTrade2(string s)
        {
            elements["ClassOfTrade2"].Select(s);
            return this;
        }

        public LocationCreate SelectClassOfTrade3(string s)
        {
            elements["ClassOfTrade3"].Select(s);
            return this;
        }

        public LocationCreate ClickMakeActive()
        {
            while (true)
            {
                try
                {
                    WaitLocator(Driver, Locators["MakeActive"], 30).ScrollTo(0, 500);
                    Thread.Sleep(1000);
                    Apply("MakeActive", 5, 1, Click);
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine("Oops, the element is stale. Trying again...");
                    Thread.Sleep(2000);
                }
            }
            return this;
        }

        public LocationCreate WaitSpinner()
        {
            WaitToDisappear(Driver, Locators["Spinner"], 180);
            return this;
        }

        public LocationCreate ClickApprove()
        {
            elements["Approve"].ScrollTo();
            Thread.Sleep(1000);
            elements["Approve"].Click();
            return this;
        }

        public string GLN => elements["GLN"].GetAttribute("value");
    }
}
