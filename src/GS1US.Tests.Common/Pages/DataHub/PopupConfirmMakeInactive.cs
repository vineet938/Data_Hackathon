using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class PopupConfirmMakeInactive : Popup<PopupConfirmMakeInactive>
    {
        public PopupConfirmMakeInactive(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>(base.Locators)
        {
            {"Continue", By.Id("btnmakeInactiveConfirmationContinue") },
            {"Spinner", By.CssSelector("[name='chopper']") }
        };

        protected override By DismissButton => By.Id("btnmakeInactiveConfirmationCancel");

        public PopupConfirmMakeInactive ClickContinue()
        {
            elements["Continue"].Click();
            return this;
        }

        public PopupConfirmMakeInactive WaitSpinner()
        {
            WaitToDisappear(Driver, Locators["Spinner"], 180);
            return this;
        }

    }
}
