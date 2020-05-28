using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace GS1US.Tests.RTF.Pages.CSA
{
    using GS1US.Tests.RTF.Common;
    using OpenQA.Selenium.Support.UI;
    using static LocUtils;

    class ContactDetails : PagesCommon
    {
        protected override Dictionary<string, By> Locators => new Dictionary<string, string>()
        {
            {"firstName", Make_SuffixXpe("txtPrimaryFName") },
            {"lastName", Make_SuffixXpe("txtPrimaryLName") },
            {"company", Make_SuffixXpe("txtCompanyName") },
            {"email", Make_SuffixXpe("txtPrimaryEmail") },
            {"phone", Make_SuffixXpe("txtPrimaryPhone") },
            {"address1", Make_SuffixXpe("txtPrimaryAddress1") },
            {"address2", Make_SuffixXpe("txtPrimaryAddress2") },
            {"city", Make_SuffixXpe("txtPrimaryCity") },
            {"state", Make_SuffixXpe("ddlPrimaryState") },
            {"zip", Make_SuffixXpe("txtPrimaryZipcode") },
            {"country", Make_SuffixXpe("ddlPrimaryCountry") },

            {"exeFirstName", Make_SuffixXpe("txtExecutiveFName") },
            {"exeLastName", Make_SuffixXpe("txtExecutiveLName") },
            {"exeEmail", Make_SuffixXpe("txtExecutiveEmail") },
            {"exePhone", Make_SuffixXpe("txtExecutivePhone") },
            {"exeAddress1", Make_SuffixXpe("txtExecutiveAddress1") },
            {"exeAddress2", Make_SuffixXpe("txtExecutiveAddress2") },
            {"exeCity", Make_SuffixXpe("txtExecutiveCity") },
            {"exeState", Make_SuffixXpe("ddlExecutiveState") },
            {"exeZip", Make_SuffixXpe("txtExecutiveZipcode") },
            {"exeCountry", Make_SuffixXpe("ddlExecutiveCountry") },

            {"same", Make_SuffixXpe("chkAddressSame") },
            {"next", Make_SuffixXpe("StartNextButton") }
        }.ToLocators();

        

        public ContactDetails(IWebDriver driver): base(driver)
        {
        }

        protected override void WaitForPage()
        {
            WaitUtils.Wait(Driver, 30, d =>
            {
                var e = d.FindElements(By.XPath(Make_SuffixXpe("txtPrimaryFName")));
                return e.Count() > 0;
            });
        }

        public ContactDetails FillPrimaryContact(
            string firstName,
            string lastName,
            string company,
            string email,
            string phone,
            string city,
            string state,
            string zip,
            string address1,
            string address2 = "",
            string country = "United States" )
        {
            elements["firstName"].SetText(firstName);
            elements["lastName"].SetText(lastName);
            elements["company"].SetText(company);
            elements["email"].SetText(email);
            elements["phone"].SetText(phone);
            elements["city"].SetText(city);
            elements["state"].SelectValue(state);
            elements["zip"].SetText(zip);
            elements["address1"].SetText(address1);
            elements["address2"].SetText(address2);
            elements["country"].Select(country);
            return this;
        }

        public ContactDetails FillExecutiveContact(
            string firstName,
            string lastName,
            string email,
            string phone,
            string city,
            string state,
            string zip,
            string address1,
            string address2 = "",
            string country = "United States")
        {
            elements["exeFirstName"].SetText(firstName);
            elements["exeLastName"].SetText(lastName);
            elements["exeEmail"].SetText(email);
            elements["exePhone"].SetText(phone);
            elements["exeCity"].SetText(city);
            elements["exeState"].SelectValue(state);
            elements["exeZip"].SetText(zip);
            elements["exeAddress1"].SetText(address1);
            elements["exeAddress2"].SetText(address2);
            elements["exeCountry"].Select(country);
            return this;
        }

        public ContactDetails FillPrimaryUserName(string firstName, string lastName)
        {
            elements["firstName"].SetText(firstName);
            elements["lastName"].SetText(lastName);
            return this;
        }

        public ContactDetails FillPrimaryEmail(string email)
        {
            elements["email"].SetText(email);
            return this;
        }

        public ContactDetails ClickSameAddressCheckBox()
        {
            elements["same"].Click();
            return this;
        }

        public ContactDetails ClickNextButton()
        {
            elements["next"].Click();
            return this;
        }

        public Steps.ContactForm Form
        {
            get
            {
                var form = new Steps.ContactForm();
                form.FirstName = elements["firstName"].Value();
                form.LastName = elements["lastName"].Value();
                form.CompanyName = elements["company"].Value();
                form.Email = elements["email"].Value();
                form.PhoneNumber = elements["phone"].Value();
                form.City = elements["city"].Value();
                form.State = elements["state"].Value();
                form.Zip = elements["zip"].Value();
                form.AddressLine1 = elements["address1"].Value();
                form.AddressLine2 = elements["address2"].Value();
                form.Country = new SelectElement(elements["country"]).AllSelectedOptions.First().Text;
                return form;
            }
        }
    }
}
