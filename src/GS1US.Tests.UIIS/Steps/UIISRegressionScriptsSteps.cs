using GS1US.Test.Common;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;
using LanguageExt;
using static LanguageExt.Prelude;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GS1US.Tests.UIIS.Steps
{
    [Binding]
    public class UIISRegressionScriptsSteps
    {
        [Given(@"I have an account information such that member type is (.*)")]
        public void GivenIHaveAnAccountInformationSuchThatMemberTypeIsXS(string memberTypes)
        {
            var pat = new Regex("\\s*(?:,|or)\\s*", RegexOptions.IgnoreCase);
            var mTypesArr = pat.Split(memberTypes);
            Setup.TestSetup.UiisContext.TestAccount = Setup.TestSetup.Imis.SelectAccountByMemberTypes(mTypesArr).First();
            Console.WriteLine($"account = {Setup.TestSetup.UiisContext.TestAccount.ID}");
        }

        [Given(@"I have a CM account with an active prefix")]
        public void GivenIHaveACMAccountWithAnActivePrefix()
        {
            var types = new string[] { "CM" };
            var account = Setup.TestSetup.Imis.SelectAccountByMemberTypesWithActiveAccount(types, 1).First();
            Setup.TestSetup.UiisContext.TestAccount = account;
            Console.WriteLine($"account = {Setup.TestSetup.UiisContext.TestAccount.ID}");
        }

        [Given(@"I have a (.*) RANGE for capacity (.*) which is not open")]
        public void GivenIHaveARANGEForCapacityXWhichIsNotOpen(string prefixType, int capacity)
        {
            string GetRange()
            {
                switch (prefixType)
                {
                    case "UPC":
                        return Setup.TestSetup.UiisDb.GetPrefixRangeForAutoVendToOpen("UPC", capacity);
                    case "EAN":
                        return Setup.TestSetup.UiisDb.GetPrefixRangeForAutoVendToOpen("EAN", capacity);
                    default:
                        throw new NotImplementedException();
                }
            }

            string range = GetRange();
            Console.WriteLine($"Range = {range}");
            Setup.TestSetup.UiisContext.Range = range;
        }

        [Given(@"I have a (.*) RANGE for specific vending for capacity (.*) which is not open")]
        public void GivenIHaveARANGEForSpecificVendingForCapacityXWhichIsNotOpen(string prefixType, int capacity)
        {
            string GetRange()
            {
                switch (prefixType)
                {
                    case "UPC":
                        return Setup.TestSetup.UiisDb.GetPrefixRangeForManualVendToOpen("UPC", capacity);
                    case "EAN":
                        return Setup.TestSetup.UiisDb.GetPrefixRangeForManualVendToOpen("EAN", capacity);
                    default:
                        throw new NotImplementedException();
                }
            }

            string range = GetRange();
            Console.WriteLine($"Range = {range}");
            Setup.TestSetup.UiisContext.Range = range;
        }

        [Given(@"I have a open range for (.*) vending")]
        public void GivenIHaveAOpenRangeForVendingType(string vendingType)
        {
            string range;
            switch (vendingType)
            {
                case "auto":
                    range = Setup.TestSetup.UiisDb.GetOpenRangeToCloseForAutoVend();
                    break;

                case "manual":
                    range = Setup.TestSetup.UiisDb.GetOpenRangeToCloseForManualVend();
                    break;

                default:
                    throw new NotImplementedException();
            }
            Console.WriteLine($"Range = {range}");
            Setup.TestSetup.UiisContext.Range = range;
        }

        [Given(@"I have an invalid account number")]
        public void GivenIHaveAnInvalidAccountNumber()
        {
            var coId = Setup.TestSetup.Imis.MaxCoId();
            Setup.TestSetup.UiisContext.TestAccount = new Database.Name
            {
                ID = $"{Int64.Parse(coId) + 1111}".PadLeft(8, '0')
            };
        }

        [Given(@"I have a held (.*) prefix")]
        public void GivenIHaveAHeldPrefix(string prefixType)
        {
            var prefix = Setup.TestSetup.UiisDb.GetHeldPrefixByType(prefixType, 1).First();
            var account = Setup.TestSetup.Imis.NamesByCoId(prefix.Account).Where(o => o.ID == prefix.Account).First();
            Setup.TestSetup.UiisContext.TestAccount = account;
            Setup.TestSetup.UiisContext.PrefixToVendOrHold = prefix.Value;
            Console.WriteLine($"prefix = {prefix.Value} / account = {account.ID}");
        }

        [Given(@"I have a (.*) range which is already open")]
        public void GivenIHaveARangeWhichIsAlreadyOpen(string prefixType)
        {
            var range = Setup.TestSetup.UiisDb.GetOpenRange(prefixType);
            Setup.TestSetup.UiisContext.Range = range;
        }

        [When(@"I navigate to UIIS UI")]
        public void WhenINavigateToUIISUI()
        {
            var appUrl = Setup.TestSetup.Config.uiis.url;
            Setup.TestSetup.Driver.Navigate().GoToUrl(appUrl);
        }
        
        [When(@"I log in")]
        public void WhenILogIn()
        {
            var page = new Pages.SSO.MsSsoUserName(Setup.TestSetup.Driver);
            var username = Setup.TestSetup.Config.uiis.username;
            page.EnterUserName(username).ClickNext();

            var page2 = new Pages.SSO.MsSsoPassword(Setup.TestSetup.Driver);
            var password = Setup.TestSetup.Config.uiis.password;
            page2.EnterPassword(password).ClickNext();

            var page3 = new Pages.SSO.MsSsoStaySignedIn(Setup.TestSetup.Driver);
            page3.ClickNo();
        }
        
        [When(@"I select Vend or Hold an Identifier menu")]
        public void WhenISelectVendOrHoldAnIdentifierMenu()
        {
            var page = new Pages.Main(Setup.TestSetup.Driver);
            page.SelectVendOrHold();
        }

        [When(@"I select Open a Prefix Range menu")]
        public void WhenISelectOpenAPrefixRangeMenu()
        {
            var page = new Pages.Main(Setup.TestSetup.Driver);
            page.SelectOpenPrefixRange();
        }

        [When(@"I select Vend an Identifier button")]
        public void WhenISelectVendAnIdentifierButton()
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            page.ClickVendAnIdentifier();
        }
        
        [When(@"I input account number")]
        public void WhenIInputAccountNumber()
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            var accountNumber = Setup.TestSetup.UiisContext.TestAccount.ID;
            Console.WriteLine($"Account Number: {accountNumber}");
            page.EnterAccountNumber(accountNumber);
        }

        [When(@"I enter a short account number")]
        public void WhenIEnterAShortAccountNumber()
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            var accountNumber = "1234";
            Console.WriteLine($"Account Number: {accountNumber}");
            page.EnterAccountNumber(accountNumber);
        }

        [When(@"I click on validate button")]
        public void WhenIClickOnValidateButton()
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            page.ClickValidate();
        }
        
        [When(@"I click on the (.*) PREFIX button")]
        public void WhenIClickOnXPREFIXButton(string buttonType)
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            if (buttonType == "UPC")
                page.ClickUpcPrefix();
            else if (buttonType == "EAN")
                page.ClickEanPrefix();
            else if (buttonType == "ALLIANCE")
                page.ClickAlliancePrefix();
            else if (buttonType == "STANDARD UPC")
                page.ClickStandardUpcPrefix();
            else if (buttonType == "SPECIFIC")
                page.ClickSpecificPrefix();
            else if (buttonType == "NDC")
                page.ClickNdcPrefix();
            else
                throw new ArgumentException($"invalid button type: {buttonType}");
        }

        [When(@"I select (.*) capacity radio button")]
        public void WhenISelectCapacityRadioButton(int capacity)
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            page.SelectCapacity(capacity);
            Setup.TestSetup.UiisContext.SelectedCapacity = capacity;
        }
        
        [When(@"I click on the NEXT AVAILABLE button")]
        public void WhenIClickOnTheNEXTAVAILABLEButton()
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            page.ClickNextAvailable();
        }
        
        [When(@"I click on the NEXT button")]
        public void WhenIClickOnTheNEXTButton()
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            page.ClickNext();
        }
        
        [When(@"I click on VEND IDENTIFIER button on Review page")]
        public void WhenIClickOnVENDIDENTIFIERButtonOnReviewPage()
        {
            var page = new Pages.Review(Setup.TestSetup.Driver);
            page.ClickVendIdentifier();
        }
        
        [When(@"I wait for sync to be successful")]
        public void WhenIWaitForSyncToBeSuccessful()
        {
            var page = new Pages.Confirmation(Setup.TestSetup.Driver);
            page.IsSyncSucceeded().ShouldBeTrue();
        }

        [When(@"I click a RANGE button for (.*)")]
        public void WhenIClickARANGEButtonForType(string prefixType)
        {
            var page = new Pages.OpenPrefixRange(Setup.TestSetup.Driver);
            switch (prefixType)
            {
                case "UPC":
                    page.ClickUpcRangeButton();
                    break;

                case "EAN":
                    page.ClickEanRangeButton();
                    break;

                default:
                    throw new ArgumentException($"invalid prefix type {prefixType}");
            }
        }

        [When(@"I input the specific range")]
        public void WhenIInputTheSpecificRange()
        {
            var page = new Pages.OpenPrefixRange(Setup.TestSetup.Driver);
            var range = Setup.TestSetup.UiisContext.Range;
            page.EnterSpeficRange(range);
        }

        [When(@"I click on Enable Automatic Vending checkbox")]
        public void WhenIClickOnEnableAutomaticVendingCheckbox()
        {
            var page = new Pages.OpenPrefixRange(Setup.TestSetup.Driver);
            page.ClickEnableAutomaticVending();
        }

        [When(@"I input the reason for opening the range")]
        public void WhenIInputTheReasonForOpeningTheRange()
        {
            var page = new Pages.OpenPrefixRange(Setup.TestSetup.Driver);
            page.EnterReasonForOpening("open by automated test");
        }

        [When(@"I input the reason for closing the range")]
        public void WhenIInputTheReasonForClosingTheRange()
        {
            var page = new Pages.ClosePrefixRange(Setup.TestSetup.Driver);
            page.EnterReasonForClosing("closed by automated test");
        }

        [When(@"I click on Next button in Open Prefix Range page")]
        public void WhenIClickOnNextButtonInOpenPrefixRangePage()
        {
            var page = new Pages.OpenPrefixRange(Setup.TestSetup.Driver);
            page.ClickNextButton();
        }

        [When(@"I click on the NEXT button on Close a Prefix Range page")]
        public void WhenIClickOnTheNEXTButtonOnCloseAPrefixRangePage()
        {
            var page = new Pages.ClosePrefixRange(Setup.TestSetup.Driver);
            page.ClickNextButton();
        }

        [When(@"click on the OPEN RANGE button")]
        public void WhenClickOnTheOPENRANGEButton()
        {
            var page = new Pages.OpenPrefixRangeReview(Setup.TestSetup.Driver);
            page.ClickOpenRange();
        }

        [When(@"I click on CLOSE RANGE button on Review page")]
        public void WhenIClickOnCLOSERANGEButtonOnReviewPage()
        {
            var page = new Pages.ClosePrefixRangeReview(Setup.TestSetup.Driver);
            page.ClickCloseRange();
        }

        [Then(@"I check that dispalyed identifier matches name table")]
        public void ThenICheckThatDispalyedIdentifierMatchesNameTable()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I select Close a Prefix Range menu")]
        public void WhenISelectCloseAPrefixRangeMenu()
        {
            var page = new Pages.Main(Setup.TestSetup.Driver);
            page.SelectClosePrefixRange();
        }

        [When(@"I input the range which is open")]
        public void WhenIInputTheRangeWhichIsOpen()
        {
            var page = new Pages.ClosePrefixRange(Setup.TestSetup.Driver);
            page.EnterRange(Setup.TestSetup.UiisContext.Range);
        }

        [When(@"I click on EAN PREFIX button")]
        public void WhenIClickOnEANPREFIXButton()
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            page.ClickEanPrefix();
        }

        [When(@"I click on EDI COMM ID button")]
        public void WhenIClickOnEDICOMMIDButton()
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            page.ClickEdiComIdButton();
        }

        [When(@"I observe validation passes with green check mark")]
        public void WhenIObserveValidationPassesWithGreenCheckMark()
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            page.IsAccountValidated(90).ShouldBeTrue();
        }

        [When(@"I select (.*) prefix from CompanyPrefix table")]
        public void WhenISelectXPrefixFromCompanyPrefixTable(string prefixType)
        {
            var capacity = Setup.TestSetup.UiisContext.SelectedCapacity;
            Setup.TestSetup.UiisDb
                .GetLastPrefixVended(prefixType, capacity)
                .Match(
                    lastPrefix =>
                    {
                        var width = 12 - (int)Math.Ceiling(Math.Log10(capacity));
                        var newPrefix = Int64.Parse(lastPrefix) + 1;
                        var prefix = $"{newPrefix}";
                        if (lastPrefix.StartsWith("0"))
                            prefix = prefix.PadLeft(width, '0');
                        Setup.TestSetup.UiisContext.PrefixToVendOrHold = prefix;
                        Console.WriteLine($"Prefix to vend = {prefix}");
                    },
                    () => Assert.Fail("failed to find usable test data in the database")
                );
        }

        [When(@"I input selected prefix")]
        public void WhenIInputSelectedPrefix()
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            page.EnterSpeficPrefix(Setup.TestSetup.UiisContext.PrefixToVendOrHold);
        }

        [When(@"I verify Prefix Available")]
        public void WhenIVerifyPrefixAvailable()
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            page.WaitPrefixAvailableLabel();
        }

        [When(@"I select labeler code from LabelerCode table")]
        public void WhenISelectLabelerCodeFromLabelerCodeTable()
        {
            var lc = Setup.TestSetup.UiisDb.GetFreeLabelerCodes(1).First();
            Setup.TestSetup.UiisContext.LabelerCode = lc;
        }

        [When(@"I enter the labeler code")]
        public void WhenIEnterTheLabelerCode()
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            var lc = Setup.TestSetup.UiisContext.LabelerCode.Code;
            page.EnterLabelerCode(lc);
        }

        [When(@"I click VALIDATE CODE button")]
        public void WhenIClickVALIDATECODEButton()
        {
            new Pages.VendOrHold(Setup.TestSetup.Driver).ClickValidateCode();
        }

        [When(@"I check the company name matches with LabelerCode table")]
        public void WhenICheckTheCompanyNameMatchesWithLabelerCodeTable()
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            var labeler = page.GetLabelerName();
            labeler.ShouldBe(Setup.TestSetup.UiisContext.LabelerCode.FirmName);
        }

        [When(@"I enter invalid labeler code: (.*)")]
        public void WhenIEnterInvalidLabelerCode(string labelerCode)
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            page.EnterLabelerCode(labelerCode);
        }

        [When(@"I verify Not a valid labeler code")]
        public void WhenIVerifyNotAValidLabelerCode()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I enter the invalid account number")]
        public void WhenIEnterTheInvalidAccountNumber()
        {
            WhenIInputAccountNumber();
        }

        [When(@"I validate that EDI COMM ID button is disable")]
        public void WhenIValidateThatEDICOMMIDButtonIsDisable()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I validate SPECIFIC PREFIX button is disable")]
        public void WhenIValidateSPECIFICPREFIXButtonIsDisable()
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            page.IsSpeficPrefixButtonDisabled().ShouldBeTrue();
        }

        [When(@"I click on link On Hold Prefix")]
        public void WhenIClickOnLinkOnHoldPrefix()
        {
            var page = new Pages.Main(Setup.TestSetup.Driver);
            page.SelectOnHoldPrefixes();
        }


        [When(@"I enter the prefix in the filter field")]
        public void WhenIEnterThePrefixInTheFilterField()
        {
            var prefix = Setup.TestSetup.UiisContext.PrefixToVendOrHold;
            new Pages.OnHoldPrefixes(Setup.TestSetup.Driver).EnterFilter(prefix);
        }

        [When(@"I click on Release and Vend button in the first row")]
        public void WhenIClickOnReleaseAndVendButtonInTheFirstRow()
        {
            new Pages.OnHoldPrefixes(Setup.TestSetup.Driver)
                .ClickFirstReleaseAndVendButton();
        }

        [When(@"I click on Release button in the first row")]
        public void WhenIClickOnReleaseButtonInTheFirstRow()
        {
            new Pages.OnHoldPrefixes(Setup.TestSetup.Driver)
                .ClickFirstReleaseButton();
        }

        [When(@"I click on HOLD A PREFIX button")]
        public void WhenIClickOnHOLDAPREFIXButton()
        {
            new Pages.VendOrHold(Setup.TestSetup.Driver).ClickHoldAPrefix();
        }

        [When(@"I enter reason for holding the prefix")]
        public void WhenIEnterReasonForHoldingThePrefix()
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            page.EnterReasonForHolding("held by automated test");
        }

        [When(@"I click on Hold PREFIX button on Review page")]
        public void WhenIClickOnHoldPREFIXButtonOnReviewPage()
        {
            new Pages.Review(Setup.TestSetup.Driver).ClickHoldPrefix();
        }

        [When(@"I check this Identifier available in CompanyPrefixHold table")]
        public void WhenICheckThisIdentifierAvailableInCompanyPrefixHoldTable()
        {
            var prefix = Setup.TestSetup.UiisContext.PrefixToVendOrHold;
            PollUtils.Poll(
                $"Querying held prefix {prefix}", 60, 5,
                () => Setup.TestSetup.UiisDb.GetHeldPrefixByValue(prefix),
                o => o.Count() > 0
            ).First().Value.ShouldBe(prefix);
        }

        [When(@"I get (?:.*) showing (.*)")]
        public void WhenIGetTheReviewPageShowingLables(string labels)
        {
            var page = new Pages.OnHoldPrefixesReview(Setup.TestSetup.Driver);
            labels.Split(',').Select(o => o.Trim())
                .All(o => page.HasLabel(o) && !String.IsNullOrEmpty(page.ReadLabel(o)))
                .ShouldBeTrue();
        }

        [When(@"I click on vend prefix button")]
        public void WhenIClickOnVendPrefixButton()
        {
            var page = new Pages.OnHoldPrefixesReview(Setup.TestSetup.Driver);
            page.ClickVendPrefix();
        }

        [When(@"I click on Yes button")]
        public void WhenIClickOnYesButton()
        {
            new Pages.OnHoldPrefixesReview(Setup.TestSetup.Driver).ClickYesButton();
        }

        [When(@"I select a (.*) prefix from range for different capacity than (.*)")]
        public void WhenIObtainAPrefixWhoseRangeSpecifiesDifferentCapacityThan(string prefixType, int capacity)
        {
            var lastPrefix = Setup.TestSetup.UiisDb.GetLastPrefixVended2(prefixType, capacity);
            var width = 12 - (int)Math.Ceiling(Math.Log10(capacity));
            var newPrefix = Int64.Parse(lastPrefix) + 1;
            var prefix = $"{newPrefix}".PadLeft(width, '0');
            Setup.TestSetup.UiisContext.PrefixToVendOrHold = prefix;
        }


        [Then(@"I get multiple columns such as (.*)")]
        public void ThenIGetMultipleColumnsSuchAsX(string names)
        {
            var page = new Pages.OnHoldPrefixes(Setup.TestSetup.Driver);
            var header = page.ColumnHeaders();
            names.Split(',').All(s => header.Contains(s.Trim())).ShouldBeTrue();
        }

        [Then(@"I see a new entity GLN displayed")]
        public void ThenISeeANewEntityGLNDisplayed()
        {
            var page = new Pages.Confirmation(Setup.TestSetup.Driver);
            var prefix = page.GetVendedPrefix();
            var entityGln = page.GetVendedEntityGLN();
            Console.WriteLine($"Vended Prefix = {prefix}; EntityGLN = {entityGln}");
            prefix.ShouldStartWith(prefix);
        }

        [Then(@"I check that account information is correct")]
        public void ThenICheckThatAccountInformationIsCorrect()
        {
            var page = new Pages.Confirmation(Setup.TestSetup.Driver);
            var account = Setup.TestSetup.UiisContext.TestAccount;
            page.GetAccountNumber().ShouldBe(account.ID);
            var companyName = Regex.Replace(account.COMPANY.Trim().ToLower(), "\\s+", " ");
            page.GetCompanyName().ToLower().ShouldBe(companyName);

            var address = Setup.TestSetup.Imis.AddressForContact(account.ID);
            var statePart = String.IsNullOrWhiteSpace(address.STATE_PROVINCE) ? "" : (" " + address.STATE_PROVINCE.Trim());
            var countryPart = String.IsNullOrWhiteSpace(address.COUNTRY) ? "" : (" " + address.COUNTRY.Trim());
            Console.WriteLine($"city=({address.CITY.Trim()}) state=({statePart}) country=({countryPart})");
            var loc = $"{address.CITY.Trim()}{statePart}{countryPart}";

            if (!String.IsNullOrWhiteSpace(loc))
                page.GetCompanyLocation().ToLower().ShouldBe(loc.ToLower());
        }

        [Then(@"I check that identifier type is (.*)")]
        public void ThenICheckThatIdentifierTypeIsX(string identifierType)
        {
            var page = new Pages.Confirmation(Setup.TestSetup.Driver);
            page.GetIdentifierType().ShouldBe(identifierType);
        }

        [Then(@"I check that capacity is (.*)")]
        public void ThenICheckThatCapacityIs(int capacity)
        {
            var page = new Pages.Confirmation(Setup.TestSetup.Driver);
            page.GetCapacity().ShouldBe(capacity);
        }

        [Then(@"I check that UPC Range is (.*)")]
        public void ThenICheckThatUPCRangeIsX(string upcRange)
        {
            var page = new Pages.Confirmation(Setup.TestSetup.Driver);
            page.GetRangeType().ShouldBe(upcRange);
        }

        [Then(@"I check something")]
        public void ThenICheckSomething()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I check that vended identifier matches major key in Name table( with leading 0 removed)?")]
        public void ThenICheckThatVendedIdentifierMatchesMajorKeyInNameTable(string removeLeading0)
        {
            var page = new Pages.Confirmation(Setup.TestSetup.Driver);
            var account = Setup.TestSetup.UiisContext.TestAccount;
            var prefix = page.GetVendedPrefix();
            if (!String.IsNullOrEmpty(removeLeading0))
                prefix = prefix.TrimStart('0');
            Console.WriteLine($"Vended Prefix = {prefix}");
            var names = PollUtils.Poll(
                $"Querying account by ID {account.ID}", 60, 5,
                () => Setup.TestSetup.Imis.NamesByCoId(account.ID),
                xs => xs.Where(x => x.MAJOR_KEY == prefix).Count() > 0
            );
            names.Where(o => o.MAJOR_KEY == prefix).ShouldNotBeEmpty();
        }

        [Then(@"I check that vended identifier matches last name in Name table")]
        public void ThenICheckThatVendedIdentifierMatchesLastNameInNameTable()
        {
            var page = new Pages.Confirmation(Setup.TestSetup.Driver);
            var account = Setup.TestSetup.UiisContext.TestAccount;
            var names = Setup.TestSetup.Imis.NamesByCoId(account.ID);
            var prefix = page.GetVendedPrefix().TrimStart('0');
            Console.WriteLine($"Vended Prefix = {prefix}");
            names.Where(o => o.LAST_NAME == prefix).ShouldNotBeEmpty();
        }

        [Then(@"I check that member type is (.*)")]
        public void ThenICheckThatMemberTypeIsX(string memberType)
        {
            var page = new Pages.Confirmation(Setup.TestSetup.Driver);
            var account = Setup.TestSetup.UiisContext.TestAccount;
            var names = Setup.TestSetup.Imis.NamesByCoId(account.ID);
            var prefix = page.GetVendedPrefix().TrimStart('0');
            Console.WriteLine($"Vended Prefix = {prefix}");
            names.Where(o => o.LAST_FIRST == prefix).First().MEMBER_TYPE.ShouldBe(memberType);
        }

        [Then(@"I check that category is (.*)")]
        public void ThenICheckThatCategoryIsX(string category)
        {
            var page = new Pages.Confirmation(Setup.TestSetup.Driver);
            var account = Setup.TestSetup.UiisContext.TestAccount;
            var names = Setup.TestSetup.Imis.NamesByCoId(account.ID);
            var prefix = page.GetVendedPrefix().TrimStart('0');
            Console.WriteLine($"Vended Prefix = {prefix}");
            names.Where(o => o.LAST_FIRST == prefix).First().CATEGORY.ShouldBe(category);
        }

        [Then(@"I check that labeler code validation error message is displayed")]
        public void ThenICheckThatLabelerCodeValidationErrorMessageIsDisplayed()
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            page.GetLabelerMatchError().ShouldBe("Not a valid Labeler Code");
        }

        [Then(@"I observe that validate button is disable")]
        public void ThenIObserveThatValidateButtonIsDisable()
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            page.IsValidateButtonDisabled().ShouldBeTrue();
        }

        [Then(@"I validate SPECIFIC PREFIX button is disable")]
        public void ThenIValidateSPECIFICPREFIXButtonIsDisable()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I validate that EDI COMM ID button is disable")]
        public void ThenIValidateThatEDICOMMIDButtonIsDisable()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I see account number validation error")]
        public void ThenISeeAccountNumberValidationError()
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            page.GetAccountValidationError().ShouldBe("Account Entered Is Not Valid");
        }

        [Then(@"the range predicate get added to CompanyPrefixRange table")]
        public void ThenTheRangePredicateGetAddedToCompanyPrefixRangeTable()
        {
            var range = Setup.TestSetup.UiisContext.Range;            
            var ranges = PollUtils.Poll(
                $"Query open range for {range}", 60, 5,
                () => Setup.TestSetup.UiisDb.OpenRangeByPredicate(range),
                xs => xs.Count() > 0
            );
            ranges.Count().ShouldBe(1);
        }

        [Then(@"I see this range (?:removed|unavailable) from companyPrefixAvailableRange table")]
        public void ThenISeeThisRangeRemovedFromCompanyPrefixAvailableRangeTable()
        {
            var range = Setup.TestSetup.UiisContext.Range;
            var ars = PollUtils.Poll(
                $"Query available range for {range}", 60, 5,
                () => Setup.TestSetup.UiisDb.AvailableRangeByPredicate(range),
                xs => xs.Count() == 0
            );
            ars.Count().ShouldBe(0);
        }

        [Then(@"the range predicate gets removed from CompanyPrefixRange table")]
        public void ThenTheRangePredicateGetsRemovedFromCompanyPrefixRangeTable()
        {
            var range = Setup.TestSetup.UiisContext.Range;
            var ars = PollUtils.Poll(
                $"Query open range for {range}", 60, 5,
                () => Setup.TestSetup.UiisDb.OpenRangeByPredicate(range),
                xs => xs.Count() == 0
            );
            ars.Count().ShouldBe(0);
        }

        [Then(@"I see top of the page show open confirmation message (.*)")]
        public void ThenISeeTopOfThePageShowOpenConfirmationMessageX(string message)
        {
            var page = new Pages.OpenPrefixRangeConfirmation(Setup.TestSetup.Driver);
            page.GetTitle().ShouldBe(message);
        }

        [Then(@"I see in CompanyPrefixRange table AutoAssign = (.*)")]
        public void ThenISeeInCompanyPrefixRangeTableAutoAssignIs1(int autoAssign)
        {
            var range = Setup.TestSetup.UiisContext.Range;
            var prs = PollUtils.Poll(
                $"Query open range for {range}", 60, 5,
                () => Setup.TestSetup.UiisDb.OpenRangeByPredicate(range),
                xs => xs.Count() > 0
            );
            prs.All(o => o.AutoAssign);
        }

        [Then(@"I see this range in CompanyPrefixAvailableRange table")]
        public void ThenISeeThisRangeInCompanyPrefixAvailableRangeTable()
        {
            var range = Setup.TestSetup.UiisContext.Range;
            var ars = PollUtils.Poll(
                $"Query available range for {range}", 60, 5,
                () => Setup.TestSetup.UiisDb.AvailableRangeByPredicate(range),
                xs => xs.Count() > 0
            );
            ars.Count().ShouldBe(1);
        }

        [Then(@"I see top of the page show close confirmation message (.*)")]
        public void ThenISeeTopOfThePageShowCloseConfirmationMessageX(string message)
        {
            var page = new Pages.ClosePrefixRangeConfirmation(Setup.TestSetup.Driver);
            page.GetTitle().ShouldBe(message);
        }



        [Then(@"All the columns are sortable")]
        public void ThenAllTheColumnsAreSortable()
        {
            var page = new Pages.OnHoldPrefixes(Setup.TestSetup.Driver);

            bool check<T>(string h, Func<string,T> trans) where T: IComparable =>
                page.ClickHeader(h).Map(ascending =>
                {
                    Console.WriteLine($"Header {h} / Ascending {ascending}");
                    var f = ascending
                        ? fun((T a, T b) => a.CompareTo(b) <= 0 ? 0 : 1)
                        : fun((T a, T b) => a.CompareTo(b) >= 0 ? 0 : 1);
                    var xs = page.GetColumn(h).Select(trans);
                    return xs.Tail().Fold((s: xs.Head(), n: 0), (t, v) => (s: v, n: t.n + f(t.s, v))).n == 0;
                }).Exists(identity);

            page.ColumnHeaders().Filter(s => s != "Reason" && s != "Actions" && s != "Capacity" && s != "Date")
                .All(h => check(h, identity) && check(h, identity))
                .ShouldBeTrue();

            DateTime dateTrans(string s) => DateTime.Parse(s);

            (check("Date", dateTrans) && check("Date", dateTrans)).ShouldBeTrue();

            (check("Capacity", Int32.Parse) && check("Capacity", Int32.Parse)).ShouldBeTrue();
        }

        [Then(@"Filter columns fetches the desired data based upon filter conditions")]
        public void ThenFilterColumnsFetchesTheDesiredDataBasedUponFilterConditions()
        {
            var page = new Pages.OnHoldPrefixes(Setup.TestSetup.Driver);
            var headers = page.ColumnHeaders().Filter(s => s != "Reason" && s != "Actions");
            page.MoveToLastPage();
            var values = headers.ToDictionary(identity, h => page.GetColumn(h).Last());

            string fix(string s)
            {
                var vv = s.Trim().Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                return vv.Count() > 1 ? vv[1] : vv[0];
            }

            bool check(string h)
            {
                var v = fix(values[h]);
                Console.WriteLine($"Header {h} / Value {v}");
                page.EnterFilter(v);
                System.Threading.Thread.Sleep(500);
                return page.GetColumn(h).All(x => fix(x).Contains(v));
            }

            headers.All(check).ShouldBeTrue();
        }

        [Then(@"items page fetches the count based upon the selection")]
        public void ThenItemsPageFetchesTheCountBasedUponTheSelection()
        {
            var page = new Pages.OnHoldPrefixes(Setup.TestSetup.Driver);
            page.ClearFilter();
            System.Threading.Thread.Sleep(500);
            var n = page.PrefixCount();
            Console.WriteLine($"Prefix cound on page = {n}");

            bool check(int cap)
            {
                var m = Math.Min(n, cap);
                page.SelectItemsPerPage(cap);
                System.Threading.Thread.Sleep(500);
                var k = page.GetColumn("Capacity").Count();
                Console.WriteLine($"page size {cap} / expected count {m} / real count {k}");
                return m == k;
            }

            List(10, 25, 50).All(check).ShouldBeTrue();
        }

        [Then(@"I check this prefix in CompanyPrefix table")]
        public void ThenICheckThisPrefixInCompanyPrefixTable()
        {
            var page = new Pages.Confirmation(Setup.TestSetup.Driver);
            var prefix = page.GetVendedPrefix();
            Console.WriteLine($"prefix = {prefix}");
            PollUtils.Poll(
                $"Querying prefix {prefix}", 60, 5,
                () => Setup.TestSetup.UiisDb.GetPrefixByValue(prefix),
                o => o.Count() > 0
            ).First().Value.ShouldBe(prefix);
        }

        [Then(@"I check this prefix is not in CompanyPrefix table")]
        public void ThenICheckThisPrefixIsNotInCompanyPrefixTable()
        {
            var prefix = Setup.TestSetup.UiisContext.PrefixToVendOrHold;
            PollUtils.Poll(
                $"Querying prefix {prefix}", 60, 5,
                () => Setup.TestSetup.UiisDb.GetPrefixByValue(prefix),
                o => o.Count() == 0
            ).ShouldBeEmpty();
        }


        [Then(@"I check this prefix is not available in CompanyPrefixHold table")]
        public void ThenICheckThisPrefixIsNotAvailableInCompanyPrefixHoldTable()
        {
            var prefix = Setup.TestSetup.UiisContext.PrefixToVendOrHold;
            Console.WriteLine($"prefix {prefix}");
            PollUtils.Poll(
                $"Querying held prefix {prefix}", 60, 5,
                () => Setup.TestSetup.UiisDb.GetHeldPrefixByValue(prefix),
                o => o.Count() == 0
            ).ShouldBeEmpty();
        }

        [Then(@"I check that displayed vended prefix and account number is correct")]
        public void ThenICheckThatDisplayedVendedPrefixAndAccountNumberIsCorrect()
        {
            var page = new Pages.OnHoldPrefixesConfirmation(Setup.TestSetup.Driver);
            page.GetPrefix().ShouldBe(Setup.TestSetup.UiisContext.PrefixToVendOrHold);
            page.GetVendedPrefix().ShouldBe(Setup.TestSetup.UiisContext.PrefixToVendOrHold);
            page.GetAccountNumber().ShouldBe(Setup.TestSetup.UiisContext.TestAccount.ID);
        }

        [Then(@"I see message for PREFIX NOT AVAILABLE")]
        public void ThenISeeMessageForPREFIXNOTAVAILABLE()
        {
            var page = new Pages.VendOrHold(Setup.TestSetup.Driver);
            page.IsPrefixNumberValidated().ShouldBeTrue();
        }

        [Then(@"I see error on open range review page: (.*)")]
        public void ThenISeeErrorOnOpenRangeReviewPage(String message)
        {
            var page = new Pages.OpenPrefixRangeReview(Setup.TestSetup.Driver);
            page.GetError().ShouldBe(message);
        }

        [Then(@"If (.*) = (.*), I check that displayed GLN does( not)? match ENTITY_GLN in Demog_All_W table")]
        public void ThenIfCaseICheckThatDisplayedGLNWithDemog_All_WTable(string case1, string caseVal, string negation)
        {
            if (case1 == caseVal)
            {
                var page = new Pages.Confirmation(Setup.TestSetup.Driver);
                var displayedEntityGln = page.GetVendedEntityGLN();
                var account = Setup.TestSetup.UiisContext.TestAccount;
                var entityGln = PollUtils.Poll(
                    $"Querying entity GLN for coid={account.ID}", 60, 5,
                    () => Setup.TestSetup.Imis.EntityGlnByCoId(account.ID),
                    o => !String.IsNullOrEmpty(o)
                );
                if (String.IsNullOrEmpty(negation))
                    entityGln.ShouldBe(displayedEntityGln);
                else
                    entityGln.ShouldNotBe(displayedEntityGln);
            }
        }

        [Then(@"If (.*) = New, I check that entity GLN is not created in Demog_All_W table")]
        public void ThenIfCaseIsNewICheckThatEntityGLNIsNotCreatedInDemog_All_WTable(string case1)
        {
            if (case1 == "New")
            {
                var account = Setup.TestSetup.UiisContext.TestAccount;
                try
                {
                    var gln = PollUtils.Poll(
                        $"Querying entity GLN for coid={account.ID}", 60, 5,
                        () => Setup.TestSetup.Imis.EntityGlnByCoId(account.ID),
                        o => !String.IsNullOrEmpty(o)
                    );
                    Assert.Fail($"entity GLN found: {gln}");
                }
                catch (TimeoutException)
                {
                    // success
                }
            }
        }

    }
}