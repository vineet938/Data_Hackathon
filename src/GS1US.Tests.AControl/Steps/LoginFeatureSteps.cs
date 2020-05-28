using GS1US.Tests.AControl.Setup;
using GS1US.Tests.Common.Pages.AccessControl;
using System;
using TechTalk.SpecFlow;

namespace GS1US.Tests.AControl.Steps
{
    [Binding]
    public class LoginFeatureSteps
    {
        [When(@"I go to the Access Control website")]
        public void WhenIGoToTheAccessControlWebsite()
        {
            TestSetup.Driver.Navigate().GoToUrl(TestSetup.Config.AccessControl.url);
        }
        
        [When(@"click on the Login button")]
        public void WhenClickOnTheLoginButton()
        {
            new Login(TestSetup.Driver).ClickLoginButton();
        }


        [When(@"enter user credentials for Access Control")]
        public void WhenEnterUserCredentialsForAccessControl()
        {
            new SignUp(TestSetup.Driver)
                 .SetUsername(TestSetup.Config.AccessControl.username)
                 .SetPassword(TestSetup.Config.AccessControl.password)
                 .SubmitButton();
        }
        
        [Then(@"I verify that I'm logged in")]
        public void ThenIVerifyThatIMLoggedIn()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
