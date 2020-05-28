using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages
{
    public class PageElements
    {
        private readonly Dictionary<string, IWebElement> elements = new Dictionary<string, IWebElement>();
        private readonly Dictionary<string, By> locators;
        private readonly IWebDriver driver;

        public PageElements(IWebDriver driver, Dictionary<string, By> locators)
        {
            this.driver = driver;
            this.locators = locators;
        }

        public PageElements(IWebDriver driver, Dictionary<string, string> xpaths):
            this(driver, xpaths.ToLocators())
        {}

        public IWebElement this [string name, int timeoutSecond=5]
        {
            get
            {
                if (elements.ContainsKey(name))
                {
                    try
                    {
                        elements[name].GetAttribute("id");
                        return elements[name];
                    }
                    catch (StaleElementReferenceException)
                    {
                        elements.Remove(name);
                    }
                }

                if (locators.ContainsKey(name))
                {
                    var element = timeoutSecond > 0 ?
                        Wait(name, timeoutSecond) :
                        driver.FindElement(locators[name]);
                    elements[name] = element;
                    return element;
                }
                else
                {
                    return null;
                }
            }
        }

        private IWebElement Wait(string name, int timeoutSeconeds)
        {
            try
            {
                return new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconeds))
                    .Until(d => d.FindElement(locators[name]));
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
        }
    }

    static class PageElementExtension
    {
        public static IWebElement SetText(this IWebElement element, string text)
        {
            if (element != null && element.GetAttribute("readonly") == null)
            {
                element.Clear();
                element.SendKeys(text);
            }
            return element;
        }

        public static IWebElement Select(this IWebElement element, string text)
        {
            new SelectElement(element).SelectByText(text);
            return element;
        }

        public static IWebElement SelectValue(this IWebElement element, string value)
        {
            new SelectElement(element).SelectByValue(value);
            return element;
        }

        public static IWebElement ScrollTo(this IWebElement element)
        {
            var driver = (element as IWrapsDriver).WrappedDriver;
            (driver as IJavaScriptExecutor).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            return element;
        }

        public static IWebElement ScrollTo(this IWebElement element, int x, int y)
        {
            var driver = (element as IWrapsDriver).WrappedDriver;
            (driver as IJavaScriptExecutor).ExecuteScript($"arguments[0].scrollTo({x},{y});", element);
            return element;
        }

        public static string Value(this IWebElement element) =>
            element.GetAttribute("value");

        public static void ActionClick(this IWebElement element, IWebDriver driver) =>
            new Actions(driver).MoveToElement(element).Click().Build().Perform();
    }

    static class LocatorMapExtension
    {
        public static Dictionary<string, By> ToLocators(this Dictionary<string, string> xpaths) =>
            xpaths.ToDictionary(p => p.Key, p => By.XPath(p.Value));
    }
}
