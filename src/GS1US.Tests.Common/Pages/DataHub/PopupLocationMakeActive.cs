using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class PopupLocationMakeActive : Popup<PopupLocationMakeActive>
    {
        public PopupLocationMakeActive(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>(base.Locators)
        {
            {"Continue", By.Id("btnstateChangeConfirmationContinue") }
        };

        protected override By DismissButton => By.Id("btnstateChangeConfirmationCancel");

        public PopupLocationMakeActive ClickContinue()
        {
            elements["Continue"].Click();
            return this;
        }
    }
}
