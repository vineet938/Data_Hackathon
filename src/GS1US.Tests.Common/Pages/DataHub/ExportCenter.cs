using System.Collections.Generic;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class ExportCenter : PagesCommon<ExportCenter>
    {
        public ExportCenter(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"Exports",  By.CssSelector("table#dtExportCenter tbody tr") },
            {"FileNameFilter", By.Id("dtExportCenterExportFileName2") },
            {"ExportList-FirstStatus", By.CssSelector("table#dtExportCenter tbody tr:first-child td:first-child") },
            {"ExportList-FirstLink", By.CssSelector("table#dtExportCenter tbody tr:first-child td:nth-child(2) a") }
        };

        protected override void WaitForPage()
        {
            WaitXpe(Driver, "//h1[text()='Export Center']", 15);
        }

        public ExportCenter FilterByFileName(string name)
        {
            elements["FileNameFilter"].SetText(name);
            Wait(Driver, 15, d => d.FindElements(Locators["Exports"]).Count == 1);
            return this;
        }

        public ExportCenter DownloadLatestItem()
        {
            Wait(Driver, 60, d => elements["ExportList-FirstStatus"].Text == "Complete");
            elements["ExportList-FirstLink"].Click();
            return this;
        }

        public string LatestItemFileName =>
            WaitLocator(Driver, Locators["ExportList-FirstLink"], 15).Text;

        public string LatestItemStatus =>
            WaitLocator(Driver, Locators["ExportList-FirstStatus"], 15).Text;
    }
}
