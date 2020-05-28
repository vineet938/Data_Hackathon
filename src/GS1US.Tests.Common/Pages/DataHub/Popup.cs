using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    abstract public class Popup<T> : PagesCommon<T> where T: Popup<T>
    {
        protected Popup(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            ["Dismiss"] = DismissButton
        };

        protected abstract By DismissButton { get; }

        public T Dismiss()
        {
            elements["Dismiss"].Click();
            WaitToDisappear(Driver, By.CssSelector("div.modal-backdrop.fade"), 30);
            return (T)this;
        }

        protected override void WaitForPage()
        {
            WaitLocator(Driver, DismissButton, 60);
        }
    }
}
