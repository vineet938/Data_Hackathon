using GS1US.Test.Common;
using LanguageExt;
using static LanguageExt.Prelude;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.UIIS.Pages
{
    class OnHoldPrefixes : PagesCommon
    {
        public OnHoldPrefixes(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"columnHeaders", By.CssSelector("div.column-headers") },
            {"fastForwardButton", By.XPath("(//li[@class='double'])[last()]") },
            {"filter", By.Id("table-filter") },
            {"prefixCount", By.XPath("(//div[contains(@class, 'result-count')])[1]") },
            {"pageSize", By.Id("page-size-bottom") },
            {"clearFilter", By.CssSelector("i.fa-times-circle") },
            {"firstReleaseAndVendButton", By.XPath("(//button[text()='Release and Vend'])[1]") },
            {"firstReleaseButton", By.XPath("(//button[text()='Release'])[1]") }
        };

        private IEnumerable<IWebElement> Headers
        {
            get => WaitUtils.WaitLocator(Driver, Locators["columnHeaders"], 15)
                .FindElements(By.CssSelector("div"));
        }

        public IEnumerable<string> ColumnHeaders() => Headers.Select(el => el.Text);

        /// <summary>
        /// Click on the named header and return sort order.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Sort order
        /// <list type="bullet">
        /// <item><c>Some(true)</c> - asending</item>
        /// <item><c>Some(false)</c> - descending</item>
        /// <item><c>None</c> - exception - failed to click the head</item>
        /// </list>
        /// </returns>
        public Option<bool> ClickHeader(string name) =>
            Headers.Find(o => o.Text == name)
                .Map(el => { el.Click(); return el; })
                .Bind(el =>
                {
                    if (el.FindElements(By.CssSelector("i.fa-arrow-down")).Count > 0)
                        return Some(true);
                    else if (el.FindElements(By.CssSelector("i.fa-arrow-up")).Count > 0)
                        return Some(false);
                    else
                        return None;
                });

        public IEnumerable<string> GetColumn(string name)
        {
            var i = Headers.TakeWhile(el => el.Text != name).Count() + 1;
            return Driver
                .FindElements(By.CssSelector($"div.table-content > div:nth-child({i})"))
                .Select(el => el.Text);
        }

        public OnHoldPrefixes MoveToLastPage()
        {
            WaitUtils.WaitLocator(Driver, Locators["fastForwardButton"], 5).Click();
            return this;
        }

        public OnHoldPrefixes EnterFilter(string value)
        {
            WaitUtils.WaitLocator(Driver, Locators["filter"], 5).SetText(value);
            return this;
        }

        public OnHoldPrefixes ClearFilter()
        {
            WaitUtils.WaitLocator(Driver, Locators["clearFilter"], 5).Click();
            return this;
        }

        public int PrefixCount()
        {
            var s = WaitUtils.WaitLocator(Driver, Locators["prefixCount"], 5).Text;
            var ss = s.Trim().Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            return Int32.Parse(ss[ss.Count() - 2]);
        }

        public OnHoldPrefixes SelectItemsPerPage(int n)
        {
            WaitUtils.WaitLocator(Driver, Locators["pageSize"], 5).SelectValue($"{n}");
            return this;
        }

        public OnHoldPrefixes ClickFirstReleaseAndVendButton()
        {
            WaitUtils.WaitLocator(Driver, Locators["firstReleaseAndVendButton"], 5).Click();
            return this;
        }

        public OnHoldPrefixes ClickFirstReleaseButton()
        {
            WaitUtils.WaitLocator(Driver, Locators["firstReleaseButton"], 5).Click();
            return this;
        }
    }
}
