using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS1US.Tests.RTF.Common;
using OpenQA.Selenium;

namespace GS1US.Tests.RTF.Pages.CSA
{
    using static WaitUtils;
    using static LocUtils;

    class PayPalConfirm
    {
        private PageElements elements;
        private Dictionary<string, string> xpaths = new Dictionary<string, string>()
        {
            {"confirm", MakeXpe("btnPayPalConfirm") },
            {"cancel", MakeXpe("btnPayPalCancel") }
        };

        public PayPalConfirm(IWebDriver driver)
        {
            Wait_Suffix(driver, "btnPayPalConfirm", 120);

            elements = new PageElements(driver, xpaths);
        }

        public PayPalConfirm Confirm()
        {
            elements["confirm"].Click();
            return this;
        }

        private static string MakeXpe(string s)
        {
            var n = s.Length;
            return $"//*[substring(@id, string-length(@id) - {n}) = '_{s}']";
        }
    }
}
