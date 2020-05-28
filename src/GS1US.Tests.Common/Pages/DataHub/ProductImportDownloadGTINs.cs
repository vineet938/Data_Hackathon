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
    public class ProductImportDownloadGtins : PagesCommon<ProductImportDownloadGtins>
    {
        public ProductImportDownloadGtins(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"PrefixOptions", By.CssSelector("select#myid option") },
            {"Prefixes", By.CssSelector("select#myid") },
            {"Quantity", By.Id("Quantity") },
            {"Continue", By.XPath("(//*[@id='btnContinue'])[1]") }
        };

        public IEnumerable<(string,int)> Prefixes =>
            Driver
                .FindElements(Locators["PrefixOptions"])
                .Select(e => (Regex.Match(e.Text, @".*\(([0-9,]+)/([0-9,]+)\)"), e.Value()))
                .Where(p => p.Item1.Groups.Count == 3)
                .Select(p =>
                {
                    var m = p.Item1;
                    var i = int.Parse(m.Groups[1].Value.Replace(",", ""));
                    var n = int.Parse(m.Groups[2].Value.Replace(",", ""));
                    return (p.Item2, n - i);
                });

        public ProductImportDownloadGtins SelectPrefixWithCapacity(int n)
        {
            int Capacity(string s)
            {
                var m = Regex.Match(s, @".*\(([0-9,]+)/([0-9,]+)\)");
                if (m.Groups.Count == 3)
                {
                    return int.Parse(m.Groups[1].Value.Replace(",", ""));
                }
                else
                {
                    return 0;
                }
            }

            Wait(Driver, 15, d =>
                d.FindElements(Locators["PrefixOptions"]).Any(e => Capacity(e.Text) >= n));

            var value = Driver
                .FindElements(Locators["PrefixOptions"])
                .First(e => Capacity(e.Text) >= n)
                .Value();

            elements["Prefixes"].SelectValue(value);

            return this;
        }

        public ProductImportDownloadGtins EnterQuantity(int quantity)
        {
            elements["Quantity"].SetText($"{quantity}");
            return this;
        }

        public ProductImportDownloadGtins ClickContinue()
        {
            elements["Continue"].Click();
            return this;
        }
    }
}
