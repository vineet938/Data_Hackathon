using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.RTF.Common
{
    abstract class PagesCommon
    {
        protected readonly IWebDriver Driver;

        protected readonly PageElements elements;

        protected abstract Dictionary<string, By> Locators { get; }

        public PagesCommon(IWebDriver driver)
        {
            Driver = driver;
            elements = new PageElements(driver, Locators);
            WaitForPage();
        }

        protected virtual void WaitForPage() { }
    }
}
