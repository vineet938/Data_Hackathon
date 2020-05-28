using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace GS1US.Tests.RTF.Pages.UNSPSC
{
    using GS1US.Tests.RTF.Common;
    using static LocUtils;

    class MembershipDetails : PagesCommon
    {
        public MembershipDetails(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, string>
        {
            {"Corporate Global",                            Make_SuffixXpe("rdbGVRow_0") },
            {"Corporate Plus",                              Make_SuffixXpe("rdbGVRow_1") },
            {"Corporate",                                   Make_SuffixXpe("rdbGVRow_2") },
            {"Corporate Individual",                        Make_SuffixXpe("rdbGVRow_3") },
            {"Education/Charity/Religious Organization",    Make_SuffixXpe("rdbGVRow_4") },
            {"Government - National/State",                 Make_SuffixXpe("rdbGVRow_5") },
            {"Government - Local",                          Make_SuffixXpe("rdbGVRow_6") },
            {"Trade/Standards Organization",                Make_SuffixXpe("rdbGVRow_7") },
            {"Student/Educator/Researcher",                 Make_SuffixXpe("rdbGVRow_8") },
            {"Solution Resources",                          Make_SuffixXpe("rdbGVRow_9") },

            {"next", Make_SuffixXpe("btnNext") }
        }.ToLocators();

        public MembershipDetails ClickNext()
        {
            elements["next"].Click();
            return this;
        }

        public MembershipDetails SelectMembershipType(string memberShipType)
        {
            elements[memberShipType].Click();
            return this;
        }
    }
}
