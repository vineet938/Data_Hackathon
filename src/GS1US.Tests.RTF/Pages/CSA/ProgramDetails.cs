using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS1US.Tests.RTF.Common;
using OpenQA.Selenium;

namespace GS1US.Tests.RTF.Pages.CSA
{
    using static LocUtils;
    using static WaitUtils;

    class ProgramDetails
    {
        private readonly string[] sizes = new string[] {
            "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten"
        };

        private readonly PageElements elements;
        private Dictionary<string, string> xpaths = new Dictionary<string, string>()
        {
            {"ndcYes", Make_SuffixXpe("RadioButtonListNCD_0") },
            {"ndcNo", Make_SuffixXpe("RadioButtonListNCD_1") },
            {"understand", Make_SuffixXpe("cboUnderstand") },
            {"next", Make_SuffixXpe("btnNext") },
            {"more", Make_SuffixXpe("chkNeedMultiplePrefixes") },
            {"howMany", Make_SuffixXpe("cboNumberOfPrefixes") }
        };

        private readonly IWebDriver driver;

        public ProgramDetails(IWebDriver driver)
        {
            this.driver = driver;

            WaitXpe(driver, xpaths["more"], 120);

            for (var i=0; i < 10; ++i)
            {
                for (var j=0; j < 5; ++j)
                {
                    xpaths.Add($"cap{i}-{j}", Make_SuffixXpe($"RadioButtonList1_{i}_{j}_{i}"));
                }
            }
            elements = new PageElements(driver, xpaths);
        }

        public ProgramDetails SetNDC(bool yes)
        {
            if (yes)
                elements["ndcYes"].Click();
            else
                elements["ndcNo"].Click();
            return this;
        }

        public ProgramDetails SelectCapacity(int prefix, int capacity)
        {
            int i = (int) Math.Ceiling(Math.Log10(capacity));
            var key = $"cap{prefix}-{i - 1}";
            elements[key].Click();
            return this;
        }

        public ProgramDetails SelectCapacity(int capacity)
        {
            return SelectCapacity(0, capacity);
        }

        public ProgramDetails SelectNumberOfPrefixes(int n)
        {
            if (n >= 1 && n <= 10)
            {
                elements["more"].Click();

                // I observe that the checkbox is not checked sometimes.
                // So, try up to 5 times until it is checked.
                int count = 1;
                while (!elements["more"].Selected && count < 5)
                {
                    Task.Delay(1000).Wait();
                    elements["more"].Click();
                    count += 1;
                }

                WaitXpe(driver, xpaths["howMany"], 60);
                
                elements["howMany"].Select(sizes[n - 1]);
            }
            return this;
        }

        public ProgramDetails ClickUnderstandCheckBox()
        {
            elements["understand"].Click();
            return this;
        }

        public ProgramDetails ClickNextButton()
        {
            elements["next"].Click();
            return this;
        }
    }
}
