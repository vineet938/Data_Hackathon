using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class ProductManage : PagesCommon<ProductManage>
    {
        public ProductManage(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"Add", By.Id("btnAddNew") },
            {"Export", By.Id("btnExportProducts") },
            {"GtinFilter", By.Id("dtProductListGTIN4") },
            {"DescriptionFilter", By.Id("dtProductListDescription3") },
            {"Products", By.XPath("//table[@id='dtProductList']/tbody/tr") },
            {"ProductList-FirstLink", By.CssSelector("table#dtProductList tbody tr:first-child td:nth-child(3) a") },
            {"ExportListInfo", By.Id("dtProductList_info") }
        };

        public ProductManage AddNew()
        {
            elements["Add"].Click();
            return this;
        }

        public ProductManage ClickExport()
        {
            elements["Export"].Click();
            return this;
        }

        public ProductManage FilterByGtin(string gtin)
        {
            Apply("GtinFilter", 15, 1, SetText(gtin));
            Wait(Driver, 15, d => d.FindElements(Locators["Products"]).Count == 1);
            return this;
        }

        public ProductManage FilterByDescription(string description)
        {
            Apply("DescriptionFilter", 15, 1, SetText(description));
            Wait(Driver, 15, d => d.FindElements(Locators["Products"]).Count == 1);
            return this;
        }

        public ProductManage SelectFirstItem()
        {
            Apply("ProductList-FirstLink", 15, 1, Click);
            return this;
        }

        public int ExportListSize
        {
            get
            {
                var s = Apply("ExportListInfo", 15, 1, x => x.Text);
                var m = Regex.Match(s, @".* \d+ to \d+ of (\d+) .*");
                return int.Parse(m.Groups[1].Value);
            }
        }
    }
}
