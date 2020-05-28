using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class PopupLocationAddConfirm : Popup<PopupLocationAddConfirm>
    {
        public PopupLocationAddConfirm(IWebDriver driver) : base(driver)
        {
        }

        protected override By DismissButton => By.Id("btnConfirmAddCancel");

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>(base.Locators)
        {
            {"Continue", By.Id("btnConfirmAdd") }
        };

        public PopupLocationAddConfirm ClickContinue()
        {
            elements["Continue"].Click();
            return this;
        }
    }
}
