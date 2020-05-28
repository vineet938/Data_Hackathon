using Shouldly;
using System;
using TechTalk.SpecFlow;

namespace GS1US.Tests.UIIS.Steps
{
    [Binding]
    public class RegressionSmallestRangeSteps : TechTalk.SpecFlow.Steps
    {

        [Given(@"I have smallest open (.*) range for auto vending with capacity (.*)")]
        public void GivenIHaveSmallestOpenUPCRangeForAutoVendingWithCapacity(string prefixType, int capacity)
        {
            var range = Setup.TestSetup.UiisDb.GetSmallestOpenRangeForAutoVend(prefixType, capacity);
            Setup.TestSetup.UiisContext.Range2 = range;
            Console.WriteLine($"{prefixType} range {range}");
        }

        [When(@"I open manual-vend (.*) range with capacity (.*) that is smaller than the auto-vend range")]
        public void WhenIOpenManual_VendUPCRangeWithCapacityThatIsSmallerThanTheAuto_VentRange(string prefixType, int capacity)
        {
            string range;
            switch (prefixType)
            {
                case "UPC":
                    range = Setup.TestSetup.UiisDb.GetPrefixRangeForManualVendToOpen("UPC", capacity, false);
                    break;

                case "EAN":
                    range = Setup.TestSetup.UiisDb.GetPrefixRangeForManualVendToOpen("EAN", capacity, false);
                    break;

                default:
                    throw new ArgumentException($"invalid prefix type {prefixType}");
            }

            Setup.TestSetup.UiisContext.Range = range;
            Console.WriteLine($"{prefixType} range {range}");

            And("I select Open a Prefix Range menu");
            And($"I click a RANGE button for {prefixType}");
            And($"I select {capacity} capacity radio button");
            And("I input the specific range");
            And("I input the reason for opening the range");
            And("I click on Next button in Open Prefix Range page");
            And("click on the OPEN RANGE button");
            Then("the range predicate get added to CompanyPrefixRange table");
            Then("I see top of the page show open confirmation message Prefix Range Opened");
        }

        [When(@"I auto-vend a (.*) prefix of capacity (.*)")]
        public void WhenIAuto_VendAUPCPrefixOfCapacity(string prefixType, int capacity)
        {
            And("I select Vend or Hold an Identifier menu");
            And("I select Vend an Identifier button");
            And("I input account number");
            And("I click on validate button");
            And("I observe validation passes with green check mark");
            And($"I click on the {prefixType} PREFIX button");
            if (prefixType == "UPC")
                And("I click on the STANDARD UPC PREFIX button");
            And($"I select {capacity} capacity radio button");
            And("I click on the NEXT AVAILABLE button");
            And("I click on the NEXT button");
            And("I click on VEND IDENTIFIER button on Review page");
            And("I wait for sync to be successful");
        }

        [Then(@"I see vended prefix is from the auto-vend range")]
        public void ThenISeeVendedPrefixIsFromTheAuto_VendRange()
        {
            var page = new Pages.Confirmation(Setup.TestSetup.Driver);
            var vendedPrefix = page.GetVendedPrefix();
            var range = Setup.TestSetup.UiisContext.Range2;
            vendedPrefix.StartsWith(range).ShouldBeTrue();
        }

    }
}
