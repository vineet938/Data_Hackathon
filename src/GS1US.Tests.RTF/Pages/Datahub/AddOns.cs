using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace GS1US.Tests.RTF.Pages.Datahub
{
    using GS1US.Tests.RTF.Common;
    using static LocUtils;

    class AddOns : PagesCommon
    {
        public AddOns(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, string>
        {
            {"apiCheckBox", Make_SuffixXpe("chkAddons_0") },
            {"next", Make_SuffixXpe("btnNext") }
        }.ToLocators();

        protected override void WaitForPage()
        {
            WaitUtils.WaitId(Driver, "ui-accordion-accordion-header-0", 60);
        }

        public AddOns CheckApi()
        {
            elements["apiCheckBox"].Click();
            return this;
        }

        public AddOns ClickNext()
        {
            elements["next"].Click();
            return this;
        }
    }
}
