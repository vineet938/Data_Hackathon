using GS1US.Tests.RTF.Steps;
using GS1US.Tests.RTF.Setup;
using System;
using TechTalk.SpecFlow;
using Shouldly;

namespace GS1US.Tests.RTF.Steps
{
    [Binding]
    public class DatahubMemberSignupSteps : StepsCommon
    {
        [When(@"I navigate to Datahub Signup Portal")]
        public void GivenINavigateToDatahubSignupPortal()
        {
            Driver.Navigate().GoToUrl(TestSetup.DatahubUrl);
        }

        [When(@"I take a note of first and last name")]
        public void WhenITakeANoteOfFirstAndLastName()
        {
            var page = new Pages.CSA.ContactDetails(Driver);
            Ctx.PrimaryContact = page.Form;
        }

        [When(@"I select number of users for create/manage products: (.*)")]
        public void WhenISelectNumberOfUsersForCreateManageProducts(int n)
        {
            var page = new Pages.Datahub.ProductDetails(Driver);
            page.SelectCreateProductOption(n);
        }

        [When(@"I select number of users for view/use product data: (.*)")]
        public void WhenISelectNumberOfUsersForViewUseProductData(int n)
        {
            var page = new Pages.Datahub.ProductDetails(Driver);
            page.SelectAccessProductOption(n);
        }

        [When(@"I select number of users for create/manage locations: (.*)")]
        public void WhenISelectNumberOfUsersForCreateManageLocations(int n)
        {
            var page = new Pages.Datahub.ProductDetails(Driver);
            page.SelectCreateLocationOption(n);
        }

        [When(@"I select number of users for view/use location data: (.*)")]
        public void WhenISelectNumberOfUsersForViewUseLocationData(int n)
        {
            var page = new Pages.Datahub.ProductDetails(Driver);
            page.SelectAccessLocationOption(n);
        }

        [When(@"I select number of users for view/use company/prefix data: (.*)")]
        public void WhenISelectNumberOfUsersForViewUseCompanyPrefixData(int n)
        {
            var page = new Pages.Datahub.ProductDetails(Driver);
            page.SelectAccessCompanyOption(n);
        }

        [When(@"I click on the next button on datahub signup product details page")]
        public void WhenIClickOnTheNextButtonOnDatahubSignupProductDetailsPage()
        {
            var page = new Pages.Datahub.ProductDetails(Driver);
            page.ClickNext();
        }

        [When(@"I set API checkbox on addons page: (.*)")]
        public void WhenISetAPICheckboxOnAddonsPageCheck(string check)
        {
            if (check == "check")
            {
                new Pages.Datahub.AddOns(Driver).CheckApi();
            }
        }

        [When(@"I click on the next button on the datahub signup addons page")]
        public void WhenIClickOnTheNextButtonOnTheDatahubSignupAddonsPage()
        {
            new Pages.Datahub.AddOns(Driver).ClickNext();
        }

        [When(@"I scroll to bottom of the data hub agreement text")]
        public void WhenIScrollToBottomOfTheDataHubAgreementText()
        {
            new Pages.Datahub.PaymentDetailsModal(Driver).ScrollDownAgreement();
        }

        [When(@"I click on the data hub agreement checkbox")]
        public void WhenIClickOnTheDataHubAgreementCheckbox()
        {
            new Pages.Datahub.PaymentDetailsModal(Driver).CheckAgreementCheckbox();
        }

        [When(@"I fill in the data hub agreement form")]
        public void WhenIFillInTheDataHubAgreementForm()
        {
            new Pages.Datahub.PaymentDetailsModal(Driver)
                .SetFirstName(Ctx.PrimaryContact.FirstName)
                .SetLastName(Ctx.PrimaryContact.LastName)
                .SetEmail(TestSetup.TestAccountEmail);
        }

        [When(@"I click on the submit button on data hub agreement")]
        public void WhenIClickOnTheSubmitButtonOnDataHubAgreement()
        {
            new Pages.Datahub.PaymentDetailsModal(Driver).ClickSubmit();
        }

        [When(@"I click on the PayPal button on data hub signup payment page")]
        public void WhenIClickOnThePayPalButtonOnDataHubSignupPaymentPage()
        {
            new Pages.Datahub.PaymentDetails(Driver).ClickPayPalButton();
        }

        [Then(@"I see the Thank You page for data hub signup")]
        public void ThenISeeTheThankYouPageForDataHubSignup()
        {
            var page = new Pages.Datahub.ThankYou(Driver);
            var confirmationNumber = page.ConfirmationNumber;
            Console.WriteLine($"Confirmation Number: {confirmationNumber}");
            confirmationNumber.ShouldNotBeEmpty();
        }

    }

}
