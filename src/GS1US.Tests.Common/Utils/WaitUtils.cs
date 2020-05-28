using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.Common.Utils
{
    public static class WaitUtils
    {
        public static IWebElement WaitIds(IWebDriver driver, int seconds, params string[] ids) =>
            WaitLocators(driver, seconds, ids.Select(s => By.Id(s)).ToArray()).Item2;


        public static IWebElement WaitId(IWebDriver driver, string id, int seconds) =>
            WaitIds(driver, seconds, id);

        public static IWebElement WaitXpes(IWebDriver driver, int seconds, params string[] xpes) =>
            WaitLocators(driver, seconds, xpes.Select((s) => By.XPath(s)).ToArray()).Item2;

        public static IWebElement WaitXpe(IWebDriver driver, string xpath, int seconds) =>
            WaitXpes(driver, seconds, xpath);

        public static IWebElement WaitLocator(IWebDriver driver, By locator, int seconds) =>
            WaitLocators(driver, seconds, locator).Item2;

        public static (int?, IWebElement) WaitLocators(IWebDriver driver, int seconds, params By[] bys)
        {
            int? n = null;
            var e = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until<IWebElement>(
                (d) =>
                {
                    n = null;
                    for (var i = 0; i < bys.Length; ++i)
                    {
                        var by = bys[i];
                        var r = d.FindElements(by);
                        try
                        {
                            if (r.Count > 0 && r.First().Displayed && r.First().Enabled)
                            {
                                n = i;
                                return r.First();
                            }
                        }
                        catch
                        {
                            return null;
                        }
                    }
                    return null;
                });
            return (n, e);
        }

        public static bool Wait(IWebDriver driver, int seconds, Func<IWebDriver, bool> predicate) =>
            new WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until<bool>(d => {
                try
                {
                    return predicate(d);
                }
                catch
                {
                    return false;
                }
            });

        public static bool WaitIdToDisappear(IWebDriver driver, string id, int seconds) =>
            WaitToDisappear(driver, By.Id(id), seconds);

        public static bool WaitToDisappear(IWebDriver driver, By locator, int seconds)
        {
            var e = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until<IWebElement>(
                d =>
                {
                    try
                    {
                        return d.FindElement(locator);
                    }
                    catch
                    {
                        return null;
                    }
                }
            );
            if (e != null)
                return Wait(driver, seconds, d =>
                {
                    try
                    {
                        var el = d.FindElements(locator);
                        return el.Count == 0 || !el.First().Displayed;
                    }
                    catch
                    {
                        // el might have been detached after located: StaleElementReferenceException
                        return false;
                    }
                });
            else
                return false;
        }
    }
}
