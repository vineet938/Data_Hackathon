using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using GS1US.Tests.RTF.Steps;
using System.Linq;
using Shouldly;
using GS1US.Tests.RTF.Setup;
using GS1US.Tests.RTF.Database;
using System.Collections.Generic;

namespace GS1US.Tests.RTF.Steps
{
    using GS1US.Tests.RTF.Pages;
    using GS1US.Tests.RTF.Pages.PayPal;
    using GS1US.Tests.RTF.Common;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using static ContextKeys;

    [Binding]
    public class CommonSignupAppSteps : StepsCommon
    {
        private readonly int DB_TIMEOUT = TestSetup.DbTimeout;
        private readonly int DB_INTERVAL = TestSetup.DbPollInterval;

        [Given(@"I am a new user")]
        public void GivenIAmANewUser()
        { }

        [Given(@"Im an an existing user")]
        public void GivenIAmAnExistingUser()
        { }


        [When(@"I navigate(?: back)? to CSA web site")]
        public void WhenINavigateToCSAWebSite()
        {
            Driver.Navigate().GoToUrl(TestSetup.AppUrl);
        }

        [When(@"I skip the modal")]
        public void WhenISkipTheModal()
        {
            new Pages.CSA.ContactDetailsModal(Driver).Skip();
        }

        [When(@"I select an account (with|without) proration")]
        public void WhenISelectAnAccountWithProration(string proration)
        {
            var imis = Context<IMIS>(IMIS);

            Name cm = null;
            int count = 0;
            while (count < 10)
            {
                IEnumerable<Name> names;
                if (proration == "with")
                    names = imis.FindAccountWithProration(15);
                else
                    names = imis.FindAccountWithoutProration(15);
                var coId = LockUtils.PickUnusedAccount("coid", names.Select(o => o.ID));
                if (coId != null)
                {
                    cm = names.Where(o => o.ID == coId).First();
                    break;
                }
                count += 1;
                Task.Delay(RandUtils.RandInt(1000, 2000)).Wait();
            }

            cm.ShouldNotBeNull();
            cm.ID.ShouldNotBeEmpty();

            Console.WriteLine($"CO_ID={cm.ID}");

            var account = new Account(imis, cm.ID);
            Ctx.OriginalAccount = account;
        }

        [When(@"I enter account number and zip code and submit")]
        public void WhenIEnterAccountNumberAndZipCodeAndSubmit()
        {
            var key = Ctx.OriginalAccount.Company;
            new Pages.CSA.ContactDetailsModal(Driver)
                .FillExistingUserInfo(key.ID, key.ZIP)
                .Submit();
        }

        [When(@"I fill primary contact form( with modification)?")]
        public void WhenIFillPrimaryContactForm(string modification)
        {
            ContactForm form;

            if (Ctx.NewAccount)
            {
                form = new ContactForm
                {
                    FirstName = "F" + RandUtils.RandomName(9),
                    LastName = "L" + RandUtils.RandomName(9),
                    CompanyName = "C" + RandUtils.RandomString(9),
                    Email = "E" + RandUtils.RandomString(6) + "@test.com.block",
                    PhoneNumber = "1" + RandUtils.RandomNumber(9),
                    AddressLine1 = "1009 Lenox Dr",
                    AddressLine2 = "",
                    City = "Lawrence Township",
                    State = "NJ",
                    Zip = "08648",
                    Country = "United States"
                };
            }
            else
            {
                var acct = Ctx.OriginalAccount;
                form = new ContactForm(acct.PrimaryContact, acct.PrimaryAddress);
            }

            if (modification.Length > 0)
            {
                form.FirstName = "F" + RandUtils.RandomName(9);
                form.LastName = "L" + RandUtils.RandomName(9);
                form.Email = "E" + RandUtils.RandomString(6) + "@test.com.block";
                form.PhoneNumber = "1" + RandUtils.RandomNumber(9);
                form.AddressLine1 = "1009 Lenox Dr";
                form.AddressLine2 = "";
                form.City = "Lawrence Township";
                form.State = "NJ";
                form.Zip = "08648";
                form.Country = "United States";
            }

            Ctx.PrimaryContact = form;

            new Pages.CSA.ContactDetails(Driver).FillPrimaryContact(
                form.FirstName,
                form.LastName,
                form.CompanyName,
                form.Email,
                form.PhoneNumber,
                form.City,
                form.State,
                form.Zip,
                form.AddressLine1,
                form.AddressLine2,
                form.Country
            );

            Console.WriteLine($"company={form.CompanyName} name={form.LastName}, {form.FirstName}");
        }

        [When(@"I update primary contact email using test email account")]
        public void WhenIUpdatePrimaryContactEmailUsingTestEmailAccount()
        {
            var page = new Pages.CSA.ContactDetails(Driver);
            page.FillPrimaryEmail(TestSetup.TestAccountEmail);

            var form = Ctx.PrimaryContact;
            form.Email = TestSetup.TestAccountEmail;
            Ctx.PrimaryContact = form;

            Console.WriteLine($"company={form.CompanyName} name={form.LastName}, {form.FirstName}");
        }

        [When(@"I fill in missing fields of primary contact( with test email account)?")]
        public void WhenIFillInMissingFieldsOfPrimaryContact(string withTestEmailAccount)
        {
            var acct = Ctx.OriginalAccount;
            var form = new ContactForm(acct.PrimaryContact, acct.PrimaryAddress);
            if (withTestEmailAccount.Length > 0)
                form.Email = TestSetup.TestAccountEmail;
            new Pages.CSA.ContactDetails(Driver)
                .FillPrimaryUserName(form.FirstName, form.LastName)
                .FillPrimaryEmail(form.Email);

            Ctx.PrimaryContact = form;

            Console.WriteLine($"company={form.CompanyName} name={form.LastName}, {form.FirstName}");
        }

        [When(@"I fill executive contact form( with modification)?")]
        public void WhenIFillExecutiveContactForm(string modification)
        {
            ContactForm form;

            if (Ctx.NewAccount)
            {
                form = new ContactForm
                {
                    FirstName = "F" + RandUtils.RandomName(9),
                    LastName = "L" + RandUtils.RandomName(9),
                    Email = "E" + RandUtils.RandomString(6) + "@test.com.block",
                    PhoneNumber = "1" + RandUtils.RandomNumber(9),
                    AddressLine1 = "1009 Lenox Dr",
                    AddressLine2 = "",
                    City = "Lawrence Township",
                    State = "NJ",
                    Zip = "08648",
                    Country = "United States"
                };
            }
            else
            {
                var acct = Ctx.OriginalAccount;
                form = new ContactForm(acct.ExecutiveContact, acct.ExecutiveAddress);
            }

            if (modification.Length > 0)
            {
                form.FirstName = "F" + RandUtils.RandomName(9);
                form.LastName = "L" + RandUtils.RandomName(9);
                form.Email = "E" + RandUtils.RandomString(6) + "@test.com.block";
                form.PhoneNumber = "1" + RandUtils.RandomNumber(9);
                form.AddressLine1 = "1009 Lenox Dr";
                form.AddressLine2 = "";
                form.City = "Lawrence Township";
                form.State = "NJ";
                form.Zip = "08648";
                form.Country = "United States";
            }

            Ctx.ExecutiveContact = form;

            new Pages.CSA.ContactDetails(Driver).FillExecutiveContact(
                form.FirstName,
                form.LastName,
                form.Email,
                form.PhoneNumber,
                form.City,
                form.State,
                form.Zip,
                form.AddressLine1,
                form.AddressLine2,
                form.Country
            );
        }


        [When(@"I check the same-as-primary-contact checkbox")]
        public void WhenICheckTheSame_As_Primary_ContactCheckbox()
        {
            new Pages.CSA.ContactDetails(Driver).ClickSameAddressCheckBox();
            Ctx.SameExecutiveContact = true;
        }


        [When(@"I click on the next button on contact details page")]
        public void WhenIClickOnTheNextButtonOnContactDetailsPage()
        {
            new Pages.CSA.ContactDetails(Driver).ClickNextButton();
        }

        [When(@"I select prefixes: (.*)")]
        public void WhenISelectPrefixes(string capacitiesStr)
        {
            var page = new Pages.CSA.ProgramDetails(Driver);

            var capatities = Regex.Split(capacitiesStr, "[, ]+").Select(Int32.Parse);

            page.SelectNumberOfPrefixes(capatities.Count());

            int i = 0;
            foreach (var n in capatities)
            {
                page.SelectCapacity(i++, n);
                Ctx.Capacities.Add(n);
            }
        }

        [When(@"I click on the policy consent checkbox")]
        public void WhenIClickOnThePolicyConsentCheckbox()
        {
            var page = new Pages.CSA.ProgramDetails(Driver);
            page.ClickUnderstandCheckBox();
        }

        [When(@"I click on the next button on program details page")]
        public void WhenIClickOnTheNextButtonOnProgramDetailsPage()
        {
            new Pages.CSA.ProgramDetails(Driver).ClickNextButton();
        }

        [When(@"I click on the license agreement checkbox")]
        public void WhenIClickOnTheLicenseAgreementCheckbox()
        {
            new Pages.CSA.PaymentDetails(Driver).AgreeOnLicense();
        }

        [When(@"I fill contact information using primary contact")]
        public void WhenIFillContactInformationUsingPrimaryContact()
        {
            var pc = Ctx.PrimaryContact;
            new Pages.CSA.PaymentDetails(Driver).FillContactInfo(pc.FirstName, pc.LastName, pc.Email);
        }

        [When(@"I click on the PayPal button")]
        public void WhenIClickOnThePayPalButton()
        {
            new Pages.CSA.PaymentDetails(Driver).ClickPayPal();
        }

        [When(@"I proceed with PayPal payment")]
        public void WhenIProceedWithPayPalPayment()
        {
            new PayPalSite(Driver).Pay(TestSetup.PayPalUsername, TestSetup.PayPalPassword);
        }

        [When(@"I confirm the payment")]
        public void WhenIConfirmThePayment()
        {
            new Pages.CSA.PayPalConfirm(Driver).Confirm();
        }

        [When(@"I fill CC payment information and submit")]
        public void WhenIFillCCPaymentInformationAndSubmit()
        {
            var pc = Ctx.PrimaryContact;
            new Pages.CSA.PaymentDetails(Driver)
                .SelectCreditCardType("VISA")
                .EnterNameOnCard($"{pc.FirstName} {pc.LastName}")
                .EnterCardNumber("4111111111111111")
                .EnterSecurityCode("111")
                .ClickSubmitButton();
        }


        [When(@"I buy a prefix and update( both)? contacts?")]
        public void WhenIBuyAPrefixAndUpdateContact(string both)
        {
            if (Ctx.NewAccount)
            {
                WhenISkipTheModal();
                WhenIFillPrimaryContactForm("");
                WhenIUpdatePrimaryContactEmailUsingTestEmailAccount();
                WhenICheckTheSame_As_Primary_ContactCheckbox();
            }
            else
            {
                WhenIEnterAccountNumberAndZipCodeAndSubmit();
                WhenIFillInMissingFieldsOfPrimaryContact(" with test email account");
                WhenIFillExecutiveContactForm(both.Length > 0 ? " with modification" : "");
            }
            WhenIClickOnTheNextButtonOnContactDetailsPage();
            WhenISelectPrefixes("10");
            WhenIClickOnThePolicyConsentCheckbox();
            WhenIClickOnTheNextButtonOnProgramDetailsPage();
            WhenIClickOnTheLicenseAgreementCheckbox();
            WhenIFillContactInformationUsingPrimaryContact();
            WhenIClickOnThePayPalButton();
            WhenIProceedWithPayPalPayment();
            WhenIConfirmThePayment();
            ThenISeePrefixesDisplayed(1);
            Then("I receive the Welcome Kit email");
            WhenINavigateToCSAWebSite();
        }



        [When(@"I stop test")]
        public void WhenIStopTest()
        {
            Console.WriteLine("Failing test...");
            true.ShouldBeFalse();
        }



        [Then(@"I see (.*) prefixes displayed")]
        public void ThenISeePrefixesDisplayed(int n)
        {
            var page = new Pages.CSA.ThankYou(Driver);
            page.Prefixes().Count().ShouldBe(n);
            if (n == 0)
                page.IsThankYouForApplyingDisplayed().ShouldBeTrue();
        }

        [Then(@"In Name table, following entries are (?:created|created or updated)")]
        public void ThenInNameTableFollowingEntriesAreCreated(Table table)
        {
            var imis = Context<IMIS>(IMIS);

            var pc = Ctx.PrimaryContact;
            var account = PollUtils.Poll<Account>(
                $"Get initial name entries since {ImisStartTime}", DB_TIMEOUT, DB_INTERVAL,
                () => new Account(imis, pc.CompanyName, pc.FirstName, pc.LastName),
                (acct) => table.Rows.All(row =>
                {
                    var n = Int32.Parse(row["Count"]);
                    return acct[row["MemberType"]]
                    .Where(o => o.LAST_UPDATED >= ImisStartTime)
                    .Count() >= n;
                })
            );

            account.ShouldNotBeNull();

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));

            var coId = account["CM"].First().ID;
            account = new Account(imis, coId);

            foreach (var row in table.Rows)
            {
                var t = row["MemberType"];
                var n = Int32.Parse(row["Count"]);
                account[t].Where(o => o.LAST_UPDATED >= ImisStartTime).Count().ShouldBe(n);
            }
        }

        [Then(@"Relationship entries have correct target ID")]
        public void ThenRelationshipEntriesAreCreatedCorrectly()
        {
            var imis = Context<IMIS>(IMIS);
            var pc = Ctx.PrimaryContact;

            PollUtils.Poll(
                $"Query relationships for company {pc.CompanyName}", DB_TIMEOUT, DB_INTERVAL,
                () => {
                    var coId = imis.NamesByCompanyAndUser(pc.CompanyName, pc.FirstName, pc.LastName).First().CO_ID;
                    return imis.RelationshipsByCoId(coId);
                },
                (e) => {
                    try
                    {
                        return f(e);
                    }
                    catch
                    {
                        return false;
                    };
                }
            );

            bool f(IEnumerable<Relationship> entries)
            {
                var keyRel = entries.Where((o) => o.RELATION_TYPE == "KEY").First();
                var billRel = entries.Where((o) => o.RELATION_TYPE == "BILL").First();
                var ceoRel = entries.Where((o) => o.RELATION_TYPE == "CEO").First();

                var result = true;

                var coId = imis.NamesByCompanyAndUser(pc.CompanyName, pc.FirstName, pc.LastName).First().CO_ID;
                var names = imis.NamesByCoId(coId);

                var pName = names.Where(o => o.ID == keyRel.TARGET_ID).First();
                result = result && pName.FIRST_NAME == pc.FirstName;
                result = result && pName.LAST_NAME == pc.LastName;
                result = keyRel.TARGET_ID == billRel.TARGET_ID;

                if (result == false) return false;

                var ec = Ctx.ExecutiveContact;
                var eName = names.Where(o => o.ID == ceoRel.TARGET_ID).First();
                result = result && eName.FIRST_NAME == ec.FirstName;
                result = result && eName.LAST_NAME == ec.LastName;

                return result;
            }
        }

        [Then(@"I see the Thank You page with no prefix")]
        public void ThenISeeTheThankYouPageWithNoPrefix()
        {
            new Pages.CSA.ThankYou(Driver).IsThankYouForApplyingDisplayed().ShouldBeTrue();
        }

        [Then(@"Subscription entries are created as follows")]
        public void ThenSubscriptionEntriesAreCreatedAsFollows(Table table)
        {
            var imis = Context<IMIS>(IMIS);
            var pc = Ctx.PrimaryContact;
            var coId = imis.NamesByCompanyAndUser(pc.CompanyName, pc.FirstName, pc.LastName).First().CO_ID;

            Console.WriteLine($"CO_ID={coId}");

            var expectedCount = Ctx.NewAccount ? 1 : Ctx.Capacities.Count();

            PollUtils.Poll<IEnumerable<Subscription>>(
                $"Querying subscriptions table for CO_ID={coId}", DB_TIMEOUT, DB_INTERVAL,
                () => imis.SubscriptionsForCompany(coId),
                x => table.Rows.All(row =>
                    x.Where(o => o.DATE_ADDED > ImisStartTime && o.PRODUCT_CODE == row["ProductCode"])
                     .Count() == Int32.Parse(row["Count"])
                )
            );

            // Wait 10 more seconds just in case database changes further.
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));

            var subs = imis.SubscriptionsForCompany(coId).Where(o => o.DATE_ADDED >= ImisStartTime);

            var expectedTotal = table.Rows.Sum(o => Int32.Parse(o["Count"]));
            var extra = Ctx.NewAccount ? 0 : subs.Where(o => o.PRODUCT_CODE == "PC_AN_CAP").Count();
            subs.Count().ShouldBe(expectedTotal + extra);

            foreach (var row in table.Rows)
            {
                subs.Where(o => o.PRODUCT_CODE == row["ProductCode"])
                    .Count().ShouldBe(Int32.Parse(row["Count"]));
            }
        }

        [Then(@"If prorated, I should see (.*) PC_AN_CAP_A2 entries")]
        public void ThenIfProratedIShouldSeePC_AN_CAP_AEntries(int n)
        {
            var imis = Context<IMIS>(IMIS);
            var pc = Ctx.PrimaryContact;
            var coId = imis.NamesByCompanyAndUser(pc.CompanyName, pc.FirstName, pc.LastName).First().CO_ID;
            var account = new Account(imis, coId);

            var subs = PollUtils.Poll<IEnumerable<Subscription>>(
                $"Getting a new subscription for CO_ID={coId}", DB_TIMEOUT, DB_INTERVAL,
                () => imis.SubscriptionsForCompany(coId).Where(o => o.DATE_ADDED > ImisStartTime),
                x => x.Count() > 0
            );

            var paidThruMonth = subs.First().BILL_THRU.Month;
            var currentMonth = DateTime.Now.Month;
            var prorated = currentMonth == paidThruMonth;

            if (prorated)
            {
                var table = new Table("ProductCode", "Count");
                table.AddRow("PC_AN_CAP_A2", $"{n}");
                ThenSubscriptionEntriesAreCreatedAsFollows(table);
            }

        }

        [Then(@"In Subscriptions table, billing amounts are correct")]
        public void ThenInSubscriptionsTableBillingAmountsAreCorrect()
        {
            var imis = Context<IMIS>(IMIS);
            var pc = Ctx.PrimaryContact;
            var coId = imis.NamesByCompanyAndUser(pc.CompanyName, pc.FirstName, pc.LastName).First().CO_ID;

            Console.WriteLine($"CO_ID={coId}");

            int expectedCount;
            if (Ctx.NewAccount)
            {
                expectedCount = 2;
            }
            else
            {
                var cm = imis.NamesByCoId(coId).Where(o => o.MEMBER_TYPE == "CM").First();
                var multiplier = cm.PAID_THRU.Month == DateTime.Now.Month ? 2 : 3;
                expectedCount = multiplier * Ctx.Capacities.Count();
            }

            var subs = PollUtils.Poll<IEnumerable<Subscription>>(
                $"Querying subscriptions table for CO_ID={coId}", DB_TIMEOUT, DB_INTERVAL,
                () => imis.SubscriptionsForCompany(coId),
                x => {
                    var list = x.Where(o => o.DATE_ADDED >= ImisStartTime);
                    if (!Ctx.NewAccount)
                        list = list.Where(o => o.PRODUCT_CODE != "PC_AN_CAP");
                    return list.Count() == expectedCount;
                }
            ).Where(o => o.DATE_ADDED >= ImisStartTime);

            var (fee1, fee2, fee3) = Ctx.BillingAmounts();

            subs.Where(o => o.PRODUCT_CODE == "PC_AN_CAP" || o.PRODUCT_CODE == "PC_AN_CAP_A")
                .Select(o => o.BILL_AMOUNT)
                .Sum()
                .ShouldBeInRange(fee1 - 0.1, fee1 + 0.1);

            subs.Where(o => o.PRODUCT_CODE == "PC_AN_CAP1" || o.PRODUCT_CODE == "PC_AN_CAP_A1")
                .Select(o => o.BILL_AMOUNT)
                .Sum()
                .ShouldBeInRange(fee2 - 0.1, fee2 + 0.1);

            subs.Where(o => o.PRODUCT_CODE == "PC_AN_CAP_A2")
                .Select(o => o.BILL_AMOUNT)
                .Sum()
                .ShouldBeInRange(fee3 - 0.1, fee3 + 0.1);

        }

        [Then(@"In Trans table, payments are added to a batch")]
        public void ThenInTransTableThePaymentIsAddedToABatch()
        {
            var imis = Context<IMIS>(IMIS);
            var pc = Ctx.PrimaryContact;
            var coId = imis.NamesByCompanyAndUser(pc.CompanyName, pc.FirstName, pc.LastName).First().CO_ID;
            var account = new Account(imis, coId);

            var n = Ctx.NewAccount ? 1 : Ctx.Capacities.Count();
            var currentMonth = DateTime.Now.Month;
            var paidThruMonth = Ctx.NewAccount ? currentMonth : account.Company.PAID_THRU.Month;
            var m = currentMonth == paidThruMonth ? 2 : 3;

            var subs = PollUtils.Poll<IEnumerable<Subscription>>(
                $"Querying subscriptions table for CO_ID={coId} n={n * m}", DB_TIMEOUT, DB_INTERVAL,
                () =>
                {
                    var x = imis.SubscriptionsForCompany(coId).Where(o => o.DATE_ADDED > ImisStartTime);
                    if (!Ctx.NewAccount)
                        x = x.Where(o => o.PRODUCT_CODE != "PC_AN_CAP");
                    return x;
                },
                x => x.Count() == n * m
            );

            var btIds = subs.Where(o => o.BT_ID != "").Select(o => o.BT_ID).Distinct();
            btIds.Count().ShouldBe(n);

            foreach (var btId in btIds)
            {
                var trans = PollUtils.Poll<IEnumerable<Trans>>(
                    $"Querying transactions table for BT_ID={btId}", DB_TIMEOUT, DB_INTERVAL,
                    () => imis.TransactionForBtId(btId),
                    (x) => x.Count() >= m + 1
                );

                foreach (var tran in trans.Where(o => o.PRODUCT_CODE != ""))
                {
                    subs.Where(o => o.BT_ID == btId && o.PRODUCT_CODE == tran.PRODUCT_CODE)
                        .Count()
                        .ShouldBe(1);
                }
                trans.Where(o => o.PRODUCT_CODE == "").Count().ShouldBe(1);
            }
        }

        [Then(@"In IDM database, company, user and claims are created correctly")]
        public void ThenInIDMDatabaseCompanyUserAndClaimsAreCreatedCorrectly()
        {
            var pc = Ctx.PrimaryContact;
            var coId = Context<IMIS>(IMIS)
                .NamesByCompanyAndUser(pc.CompanyName, pc.FirstName, pc.LastName)
                .First()
                .CO_ID;

            var idm = Context<IDM>(IDM);

            var claims = PollUtils.Poll<IEnumerable<Claim>>(
                $"Querying IDM database with CO_ID={coId}", DB_TIMEOUT, DB_INTERVAL,
                () => idm.ClaimsByCoId(coId),
                x => x.Count() == 6
            );

            HashSet<string> expectedClaims = new HashSet<string>
            {
                "User Administrator",
                "CLAIM_DHGTN_ADMIN",
                "CLAIM_DHGLN_ADMIN",
                "CLAIM_DHGTN_C_01",
                "CLAIM_DHGLN_C_01"
            };

            var createdClaims = new HashSet<string>(claims.Select(o => o.ClaimName));

            expectedClaims.All(o => createdClaims.Contains(o)).ShouldBeTrue();
            claims.All(o => o.CompanyName == pc.CompanyName).ShouldBeTrue();
            // The following is not true in general.
            // Suppose someone created 2 company. If the user used the same primary email
            // but different first and/or last name, the IDM records retains the names
            // used for the first company.
            //claims.All(o => o.FirstName == pc.FirstName).ShouldBeTrue();
            //claims.All(o => o.LastName == pc.LastName).ShouldBeTrue();
        }

        [When(@"I wait for logic app to finish")]
        public void WhenIWaitForLogicAppToFinish()
        {
            Task.Delay(TestSetup.LogicAppWait * 1000).Wait();
        }

    }


}
