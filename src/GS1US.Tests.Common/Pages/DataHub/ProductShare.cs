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
    public class ProductShare : PagesCommon<ProductShare>
    {
        public ProductShare(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"Add", By.Id("btnAddNew") },
            {"GtinFilter", By.Id("dtShareEligibleProductsGTIN3") },
            {"Products", By.CssSelector("table#dtShareEligibleProducts tbody tr") },
            {"ProductList-FirstRow", By.CssSelector("table#dtShareEligibleProducts tbody tr:first-child") },
            {"ShareButton", By.Id("btnContinue") }
        };

        public ProductShare AddNew()
        {
            Apply("Add", 15, 1, Click);
            return this;
        }

        public ProductShare FilterByGtin(string gtin)
        {
            elements["GtinFilter"].SetText(gtin);
            Wait(Driver, 15, d => d.FindElements(Locators["Products"]).Count == 1);
            return this;
        }

        public ProductShare SelectFirstRow()
        {
            Apply("ProductList-FirstRow", 15, 1, Click);
            return this;
        }

        public ProductShare ClickShare()
        {
            ScrollToTop();
            Thread.Sleep(1000);
            elements["ShareButton"].Click();
            return this;
        }
    }
}
