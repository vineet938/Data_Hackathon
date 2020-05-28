using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class PopupParentGlnSelection : Popup<PopupParentGlnSelection>
    {
        public PopupParentGlnSelection(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"Name", By.Id("LocationDetailEligibleParentDataTableName3") },
            {"FirstRow", By.CssSelector("#LocationDetailEligibleParentDataTable tbody tr:first-child") },
            {"SetParent", By.Id("btnParentGLNSelectionHead") }
        };

        protected override By DismissButton => By.Id("btnParentGLNSelectionCancelHead");

        public PopupParentGlnSelection SearchName(string name)
        {
            elements["Name"].SetText(name);
            Thread.Sleep(1000);
            return this;
        }

        public PopupParentGlnSelection ClickFirstRow()
        {
            elements["FirstRow"].Click();
            return this;
        }

        public PopupParentGlnSelection ClickSetParentGln()
        {
            elements["SetParent"].Click();
            return this;
        }
    }
}
