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

    class PaymentDetails
    {
        private PageElements elements;
        private Dictionary<string, string> xpaths = new Dictionary<string, string>()
        {
            {"agreement", "//input[@type='checkbox']" },
            {"firstName", Make_SuffixXpe("txtFirstNameForValidation") },
            {"lastName", Make_SuffixXpe("txtLastNameForValidation") },
            {"email", Make_SuffixXpe("txtEmailAddressForValidation") },
            {"cardType", Make_SuffixXpe("ddlCCType") },
            {"cardName", Make_SuffixXpe("txtName") },
            {"cardNumber", Make_SuffixXpe("txtCCNumber") },
            {"cardCode", Make_SuffixXpe("txtCCCode") },
            {"cardExpYear", Make_SuffixXpe("ddlExpYear") },
            {"cardSubmit", Make_SuffixXpe("btnSubmitPayment") },
            {"paypal", Make_SuffixXpe("imgPayPal") }
        };

        public PaymentDetails(IWebDriver driver)
        {
            Wait_Suffix(driver, "lblPricingHeader", 10);

            elements = new PageElements(driver, xpaths);
        }

        public PaymentDetails FillContactInfo(string firstName, string lastName, string email)
        {
            elements["firstName"].SetText(firstName);
            elements["lastName"].SetText(lastName);
            elements["email"].SetText(email);
            return this;
        }

        public PaymentDetails AgreeOnLicense()
        {
            elements["agreement"].Click();
            return this;
        }

        public PaymentDetails ClickPayPal()
        {
            elements["paypal"].Click();
            return this;
        }

        public PaymentDetails SelectCreditCardType(string type)
        {
            elements["cardType"].Select(type);
            return this;
        }

        public PaymentDetails EnterNameOnCard(string name)
        {
            elements["cardName"].SetText(name);
            return this;
        }

        public PaymentDetails EnterCardNumber(string number)
        {
            elements["cardNumber"].SetText(number);
            return this;
        }

        public PaymentDetails EnterSecurityCode(string code)
        {
            elements["cardCode"].SetText(code);
            return this;
        }

        public PaymentDetails ClickSubmitButton()
        {
            Task.Delay(1000).Wait();
            elements["cardSubmit"].Click();
            return this;
        }
    }
}
