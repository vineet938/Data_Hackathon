using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class PopupProductExport : Popup<PopupProductExport>
    {
        public PopupProductExport(IWebDriver driver) : base(driver)
        {
        }

        protected override By DismissButton => By.Id("exportConfirmationContinueButton");
    }
}
