using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class PopupLocHierarchyExport : Popup<PopupLocHierarchyExport>
    {
        public PopupLocHierarchyExport(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>(base.Locators)
        {
            {"Export", By.Id("btnHierarchyExportCustom") },
            {"Radio-AllAll", By.CssSelector("#export-hierarchy-type-3 + label") }
        };

        protected override By DismissButton => By.Id("btnHierarchyExportCancel");

        public PopupLocHierarchyExport SelectAllAncestorsAndAllDescendants()
        {
            WaitLocator(Driver, Locators["Radio-AllAll"], 15).Click();
            return this;
        }

        public PopupLocHierarchyExport ClickCustomExport()
        {
            elements["Export"].Click();
            return this;
        }
    }
}
