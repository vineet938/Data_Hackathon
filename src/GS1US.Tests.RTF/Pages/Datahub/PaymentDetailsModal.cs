using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS1US.Tests.RTF.Common;
using OpenQA.Selenium;

namespace GS1US.Tests.RTF.Pages.Datahub
{
    class PaymentDetailsModal : PagesCommon
    {
        public PaymentDetailsModal(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"agreement", By.Id("div_agreement") },
            {"agreeCheckbox", By.Id("phAgreements") },
            {"firstName", By.Id("txtFirstNameForValidation") },
            {"lastName", By.Id("txtLastNameForValidation") },
            {"email", By.Id("txtEmailAddressForValidation") },
            {"submit", By.Id("btnAgreementFlipValidation") },
            {"agreementLastPara", By.XPath("//*[contains(text(), 'Any questions')]") }
        };

        protected override void WaitForPage()
        {
            WaitUtils.WaitLocator(Driver, Locators["agreementLastPara"], 60);
        }

        public PaymentDetailsModal ScrollDownAgreement()
        {
            elements["agreementLastPara"].ScrollTo();
            return this;
        }

        public PaymentDetailsModal CheckAgreementCheckbox()
        {
            elements["agreeCheckbox"].Click();
            return this;
        }

        public PaymentDetailsModal SetFirstName(string firstName)
        {
            elements["firstName"].SetText(firstName);
            return this;
        }

        public PaymentDetailsModal SetLastName(string lastName)
        {
            elements["lastName"].SetText(lastName);
            return this;
        }

        public PaymentDetailsModal SetEmail(string email)
        {
            elements["email"].SetText(email);
            return this;
        }

        public PaymentDetailsModal ClickSubmit()
        {
            elements["submit"].Click();
            return this;
        }
    }
}
