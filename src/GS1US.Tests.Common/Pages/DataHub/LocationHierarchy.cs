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
    public class LocationHierarchy : LocationTabs<LocationHierarchy>
    {
        public LocationHierarchy(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>(base.Locators)
        {
            {"CollapsedNode", By.XPath("//li[@role='treeitem' and @aria-expanded='false']") },
            {"TreeNodes", By.CssSelector("#jsTreeHierarchy a") },
            {"Export", By.Id("btnExportHierarchy") }
        };

        protected override void WaitForPage()
        {
            WaitLocator(Driver, Locators["CollapsedNode"], 60);
        }

        public LocationHierarchy ExpandTree()
        {
            while (true)
            {
                try
                {
                    var nodes = Driver.FindElements(Locators["CollapsedNode"]);
                    if (nodes.Count == 0)
                    {
                        // no more to expand
                        break;
                    }
                    else
                    {
                        var el = nodes.First();
                        var elid = el.GetAttribute("id");
                        el.FindElement(By.TagName("i")).Click();
                        // wait until xhr loading finishes
                        Wait(Driver, 60, d => !d
                                .FindElement(By.Id(elid))
                                .FindElement(By.TagName("i"))
                                .GetCssValue("background-image")
                                .Contains("throbber.gif"));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    break;
                }
            }
            return this;
        }

        public IEnumerable<(string Name, string GLN)> ReadTree() =>
            Driver.FindElements(Locators["TreeNodes"]).Select(x =>
            {
                var m = Regex.Match(x.Text, @"\s*(.*)\s*\((.*)\)");
                var name = m.Groups[1].Value;
                var gln = m.Groups[2].Value;
                return (Name: name, GLN: gln);
            })
            .Where(p => !String.IsNullOrWhiteSpace(p.GLN));

        public LocationHierarchy ClickExportHierarchy()
        {
            ScrollToTop();
            elements["Export"].Click();
            return this;
        }
    }
}
