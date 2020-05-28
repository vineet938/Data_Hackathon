using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class CompanyListMatch : PagesCommon<CompanyListMatch>
    {
        public CompanyListMatch(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"IdType", By.Id("Identifier") },
            {"Submit", By.Id("btnSubmit") },
            {"Refresh", By.XPath("//button[text()='Refresh']") },
            {"FileFilter", By.Id("tableFile3") },
            {"Processing", By.Id("table_processing") },
            {"FirstRow-FileName", By.CssSelector("#table tbody tr:first-child td:nth-child(2) a") },
            {"FirstRow-Status", By.CssSelector("#table tbody tr:first-child td:nth-child(3)") }
        };

        public CompanyListMatch SelectIdentifierType(string type)
        {
            elements["IdType"].Select(type);
            return this;
        }

        public CompanyListMatch UploadFile(string filename)
        {
            Driver.FindElement(By.Id("FileData")).SendKeys(filename);
            return this;
        }

        public CompanyListMatch ClickSubmit()
        {
            elements["Submit"].Click();
            return this;
        }

        public CompanyListMatch ClickRefresh()
        {
            elements["Refresh"].Click();
            return this;
        }

        public CompanyListMatch SearchFile(string pattern)
        {
            elements["FileFilter"].SetText(pattern);
            WaitToDisappear(Driver, Locators["Processing"], 60);
            return this;
        }

        public CompanyListMatch ClickFirstName()
        {
            elements["FirstRow-FileName"].Click();
            return this;
        }

        public string FirstFileName => elements["FirstRow-FileName"].Text;

        public string FirstStatus => elements["FirstRow-Status"].Text;
    }
}
