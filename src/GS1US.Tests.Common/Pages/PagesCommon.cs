using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages
{
    public abstract class PagesCommon<T> where T: PagesCommon<T>
    {
        protected readonly IWebDriver Driver;

        protected PageElements elements;

        protected abstract Dictionary<string, By> Locators { get; }

        protected PagesCommon(IWebDriver driver)
        {
            Driver = driver;
            elements = new PageElements(driver, Locators);
            WaitForPage();
        }

        protected virtual void WaitForPage() { }

        public T RefreshPage()
        {
            Driver.Navigate().Refresh();
            return (T)this;
        }

        public T ScrollToTop()
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0,0);");
            return (T)this;
        }

        protected Action<IWebElement> Click = x => x.Click();
        protected Action<IWebElement> SetText(string s) => x => x.SetText(s);

        private U Apply1<U>(By locator, int timeoutSeconds, int intervalSeconds, object callable)
        {
            var t0 = DateTime.Now;
            var t = t0;
            while (t - t0 < TimeSpan.FromSeconds(timeoutSeconds))
            {
                try
                {
                    var el = WaitLocator(Driver, locator, timeoutSeconds);
                    switch (callable)
                    {
                        case Action<IWebElement> a:
                            a.Invoke(el);
                            return default(U);
                        case Func<IWebElement, U> f:
                            return f.Invoke(el);
                    }
                }
                catch (StaleElementReferenceException)
                {
                    // do nothing
                }
                catch (WebDriverException e) when (e.Message.Contains("is not clickable at point"))
                {
                    // do nothing
                }
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(intervalSeconds));
                t = DateTime.Now;
            }
            throw new TimeoutException($"Couldn't finish action within {timeoutSeconds} seconds");
        }

        // repeat function if found element is stale
        public U Apply<U>(string locatorName, int timeoutSeconds, int intervalSeconds, Func<IWebElement, U> f) =>
            Apply1<U>(Locators[locatorName], timeoutSeconds, intervalSeconds, f);

        public U Apply<U>(By locator, int timeoutSeconds, int intervalSeconds, Func<IWebElement, U> f) =>
            Apply1<U>(locator, timeoutSeconds, intervalSeconds, f);

        // repeat action if found element is stale
        public void Apply(string locatorName, int timeoutSeconds, int intervalSeconds, Action<IWebElement> f) =>
            Apply1<object>(Locators[locatorName], timeoutSeconds, intervalSeconds, f);

        public void Apply(By locator, int timeoutSeconds, int intervalSeconds, Action<IWebElement> f) =>
            Apply1<object>(locator, timeoutSeconds, intervalSeconds, f);
    }
}
