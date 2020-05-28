using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS1US.Tests.RTF.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GS1US.Tests.RTF.Pages
{
    static class LocUtils
    {

        public static string Make_SuffixXpe(string s)
        {
            var n = s.Length;
            return $"//*[substring(@id, string-length(@id) - {n}) = '_{s}']";
        }

        public static string MakeIdXpe(string s) => $"//*[@id='{s}']";

        public static IWebElement Wait_Suffix(IWebDriver driver, string suffix, int seconds) =>
            WaitUtils.WaitXpe(driver, Make_SuffixXpe(suffix), seconds);
    }
}
