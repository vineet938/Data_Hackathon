using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;
using static GS1US.Tests.Common.Utils.JsUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class ProductImport : Import<ProductImport>
    {
        public ProductImport(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>(base.Locators)
        {
            {"DownloadAvailableGTINs", By.Id("exportGTINLink") }
        };

        public ProductImport ClickDownloadAvailableGtinsLink()
        {
            elements["DownloadAvailableGTINs"].Click();
            return this;
        }
    }
}
