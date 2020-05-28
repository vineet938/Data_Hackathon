using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class ProductViewUse : PagesCommon<ProductViewUse>
    {
        public ProductViewUse(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"GTIN", By.CssSelector("input[name='GTIN']") },
            {"Search", By.XPath("//button[text()='Search']") },
            {"Processing", By.Id("ViewUseResultsTable_processing") },
            {"SearchResults", By.CssSelector("table#ViewUseResultsTable tbody tr") },
            {"SearchResults-FirstGTIN", By.CssSelector("table#ViewUseResultsTable tbody tr:first-child td:nth-child(4)") }
        };

        public ProductViewUse EnterGTIN(string gtin)
        {
            Apply("GTIN", 15, 1, SetText(gtin));
            return this;
        }

        public ProductViewUse ClickSearch()
        {
            elements["Search"].Click();
            WaitToDisappear(Driver, Locators["Processing"], 60);
            Wait(Driver, 15, d => d.FindElements(Locators["SearchResults"]).Count == 1);
            return this;
        }

        public string FirstGTIN
        {
            get
            {
                Wait(Driver, 15, d => elements["SearchResults-FirstGTIN"].Text.Trim() != "");
                return elements["SearchResults-FirstGTIN"].Text;
            }
        }
    }
}
