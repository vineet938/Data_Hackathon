using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class ProductDetails : PagesCommon<ProductDetails>
    {
        public ProductDetails(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"Status", By.Id("ddlStatus") },
            {"GTIN", By.Id("txtGTIN") },
            {"Save", By.Id("btnSave") },
            {"Spinner", By.CssSelector("[name='chopper']") }
        };

        public ProductDetails SelectStatus(string s)
        {
            WaitLocator(Driver, By.XPath($"//option[text()='{s}']"), 60);
            Apply("Status", 15, 1, el => el.Select(s));
            return this;
        }

        public ProductDetails Save()
        {
            Apply("Save", 15, 1, Click);
            return this;
        }

        public string GTIN => elements["GTIN"].GetAttribute("value");

        public ProductDetails WaitSpinner()
        {
            WaitToDisappear(Driver, Locators["Spinner"], 180);
            return this;
        }
    }
}
