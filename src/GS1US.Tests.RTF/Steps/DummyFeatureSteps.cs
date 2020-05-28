using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using Shouldly;
using GS1US.Tests.RTF.Setup;

namespace GS1US.Tests.RTF.Steps
{
    [Binding]
    public class DummyFeatureSteps
    {
        private static IWebDriver driver;

        [BeforeScenario]
        public static void GetDriver()
        {
            driver = TestSetup.Driver;
        }

        [When(@"I open google web site")]
        public void WhenIOpenGoogleWebSite()
        {
            driver.Navigate().GoToUrl("https://www.google.com/");
        }
        
        [Then(@"I observe the first word is About")]
        public void ThenIObserveTheFirstWordIsAbout()
        {
            var el = driver.FindElement(By.XPath("//div[@id='hptl']/a"));
            el.Text.ShouldMatch("About");

            var mail = new Mail.MailClient("outlook.office365.com", 993, "rtftest@gs1us.org", "summertime21$$");
            mail.ReceivedWelcomeKit(DateTime.Parse("2018-05-22"), "16081601", "").ShouldBeTrue();
        }
    }
}
