using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;
using static GS1US.Tests.Common.Utils.JsUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class Import<T> : PagesCommon<T> where T: Import<T>
    {
        public Import(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"Submit", By.Id("btnSubmit") },
            {"Refresh", By.XPath("//button[text()='Refresh']") },
            {"Processing", By.Id("dtFileImportResultsList_processing") },
            {"UploadTable-FirstFileName", By.CssSelector("table#dtFileImportResultsList tbody tr:first-child td:nth-child(1) a") },
            {"UploadTable-FirstStatus", By.CssSelector("table#dtFileImportResultsList tbody tr:first-child td:nth-child(4)") },
            {"UploadTable-FirstProcessed", By.CssSelector("table#dtFileImportResultsList tbody tr:first-child td:nth-child(5)") },
            {"UploadTable-FirstSuccess", By.CssSelector("table#dtFileImportResultsList tbody tr:first-child td:nth-child(6)") }
        };

        protected override void WaitForPage()
        {
            WaitLocators(Driver, 60, Locators.Values.ToArray());
        }

        public T UploadFile(string path)
        {
            Driver.FindElement(By.Id("FileData")).SendKeys(path);
            elements["Submit"].Click();
            return (T)this;
        }

        public T Refresh()
        {
            elements["Refresh"].Click();
            Task.Delay(500);
            WaitToDisappear(Driver, Locators["Processing"], 30);
            return (T)this;
        }

        public string UploadTableFirstFileName => elements["UploadTable-FirstFileName"]?.Text ?? "";

        public string UploadTableFirstStatus => elements["UploadTable-FirstStatus"].Text;

        public int UploadTableFirstProcessed => int.Parse(elements["UploadTable-FirstProcessed"].Text);

        public int UploadTableFirstSuccess => int.Parse(elements["UploadTable-FirstSuccess"].Text);

    }
}
