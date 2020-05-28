using GS1US.Tests.RTF.Database;
using GS1US.Tests.RTF.Setup;
using System;
using TechTalk.SpecFlow;

namespace GS1US.Tests.RTF.Steps
{
    using GS1US.Tests.RTF.Pages.UserPortal;
    using System.Linq;
    using static ContextKeys;

    [Binding]
    public class MembershipAppSteps : StepsCommon
    {
        [When(@"I navigate to Membership Application site")]
        public void WhenINavigateToMembershipApplicationSite()
        {
            Driver.Navigate().GoToUrl(TestSetup.MembershipAppUrl);
        }

        [When(@"I login to GS1 US User Portal using test account")]
        public void WhenILoginToGS1USUserPortalUsingTestAccount()
        {
            var page = new LoginPage(Driver);
            var username = TestSetup.TestAccountEmail;
            var password = TestSetup.TestAccountPortalPassword;
            page.Login(username, password);
        }

        [When(@"I select my test company from company dropdown and signin")]
        public void WhenISelectMyTestCompanyFromCompanyDropdownAndSignin()
        {
            var page = new CompanyList(Driver);
            var company = TestSetup.TestAccountCompanyName;
            page.SelectCompany(company).SignIn();
        }

        [When(@"I query contact info for test account")]
        public void WhenIQueryContactInfoForTestAccount()
        {
            var imis = Context<IMIS>(IMIS);
            var name = imis.NamesByCompanyAndEmail(
                TestSetup.TestAccountCompanyName, TestSetup.TestAccountEmail
            ).First();

            Console.WriteLine($"CO_ID={name.CO_ID}");

            var account = new Account(imis, name.CO_ID);

            Ctx.OriginalAccount = account;
            Ctx.PrimaryContact = new ContactForm(account.PrimaryContact, account.PrimaryAddress);
            Ctx.ExecutiveContact = new ContactForm(account.ExecutiveContact, account.ExecutiveAddress);
        }

    }
}
