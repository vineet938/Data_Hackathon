using GS1US.Tests.RTF.Pages.UNSPSC;
using GS1US.Tests.RTF.Setup;
using Shouldly;
using System;
using TechTalk.SpecFlow;

namespace GS1US.Tests.RTF.Steps
{
    [Binding]
    public class UNSPSCMembershipAppSteps : StepsCommon
    {
        [When(@"I navigate to UNSPSC membership site")]
        public void WhenINavigateToUNSPSCMembershipSite()
        {
            Driver.Navigate().GoToUrl(TestSetup.UnspscUrl);
        }
        [When(@"I select membership type (.*) and click next")]
        public void WhenISelectMembershipTypeAndClickNext(string membershipType)
        {
            var page = new MembershipDetails(Driver);
            page.SelectMembershipType(membershipType).ClickNext();
        }

        [Then(@"I see the UNSPSC Thank You page with no prefix")]
        public void ThenISeeTheUNSPSCThankYouPageWithNoPrefix()
        {
            var page = new ThankYou(Driver);
            page.IsWelcomeToUnspscDisplayed().ShouldBeTrue();
        }

    }
}
