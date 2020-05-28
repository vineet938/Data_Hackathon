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

    class ContactDetailsModal
    {
        private PageElements elements;
        private Dictionary<string, string> xpaths = new Dictionary<string, string>()
        {
            {"skip",  Make_SuffixXpe("imgbtnSkip") },
            {"accountNumber", Make_SuffixXpe("txtExistingAccountNumber") },
            {"zip", Make_SuffixXpe("txtExistingPostalCode") },
            {"submit", Make_SuffixXpe("btnExistingAddress") }
        };

        public ContactDetailsModal(IWebDriver driver)
        {
            elements = new PageElements(driver, xpaths);
        }

        public ContactDetailsModal Skip()
        {
            elements["skip"].Click();
            return this;
        }

        public ContactDetailsModal Submit()
        {
            elements["submit"].Click();
            return this;
        }

        public ContactDetailsModal FillExistingUserInfo(string accountNumber, string zip)
        {
            elements["accountNumber"].SetText(accountNumber);
            elements["zip"].SetText(zip);
            return this;
        }
    }
}
