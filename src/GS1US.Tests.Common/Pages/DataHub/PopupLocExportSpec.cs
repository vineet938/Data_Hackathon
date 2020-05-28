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
    public class PopupLocExportSpec : Popup<PopupLocExportSpec>
    {
        public PopupLocExportSpec(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>(base.Locators)
        {
            {"Export", By.Id("customExportSubmit") },
            {"FileType", By.Id("customExportDatatableFormat") },
            {"Window", By.Id("customExportFromDataTable") },
            {"CbStatus", By.Id("entityState") },
            {"CbParentGLN", By.Id("parentGLN") }
        };

        protected override By DismissButton => By.XPath("//span[text()='Close']/parent::button");

        protected override void WaitForPage()
        {
            WaitLocators(Driver, 15, Locators.Values.ToArray());
        }

        public PopupLocExportSpec ClickExport()
        {
            elements["Window"].ScrollTo(9999, 9999);
            Thread.Sleep(1000);
            elements["Export"].Click();
            return this;
        }

        public PopupLocExportSpec SelectFileType(string type)
        {
            elements["Window"].ScrollTo(0, 9999);
            Thread.Sleep(1000);
            elements["FileType"].Select(type);
            return this;
        }

        public PopupLocExportSpec Close()
        {
            elements["Dismiss"].ScrollTo().Click();
            return this;
        }

        private PopupLocExportSpec ClickCheckbox(string id)
        {
            elements[$"Cb{id}"].Click();
            return this;
        }

        public PopupLocExportSpec ClickStatusCheckbox() => ClickCheckbox("Status");

        public PopupLocExportSpec ClickParentGlnCheckbox() => ClickCheckbox("ParentGLN");
    }
}
