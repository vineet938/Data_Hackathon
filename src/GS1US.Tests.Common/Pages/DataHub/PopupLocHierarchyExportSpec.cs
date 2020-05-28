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
    public class PopupLocHierarchyExportSpec : Popup<PopupLocHierarchyExportSpec>
    {
        public PopupLocHierarchyExportSpec(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>(base.Locators)
        {
            {"Export", By.Id("customExportSubmit") },
            {"FileType", By.Id("customExportHierarchyFormat") },
            {"Window", By.Id("customExportFromHierarchy") }
        };

        protected override By DismissButton => By.XPath("//span[text()='Close']/parent::button");

        protected override void WaitForPage()
        {
            WaitLocators(Driver, 15, Locators.Values.ToArray());
        }

        public PopupLocHierarchyExportSpec ClickExport()
        {
            elements["Window"].ScrollTo(9999, 9999);
            Thread.Sleep(1000);
            elements["Export"].Click();
            return this;
        }

        public PopupLocHierarchyExportSpec SelectFileType(string type)
        {
            elements["Window"].ScrollTo(0, 9999);
            Thread.Sleep(1000);
            elements["FileType"].Select(type);
            return this;
        }

        public PopupLocHierarchyExportSpec Close()
        {
            elements["Dismiss"].ScrollTo().Click();
            return this;
        }
    }
}
