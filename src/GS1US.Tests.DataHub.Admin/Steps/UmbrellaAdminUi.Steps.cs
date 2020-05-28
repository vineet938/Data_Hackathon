using System;
using System.Net;
using Shouldly;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using P = GS1US.Tests.Common.Pages.DataHubAdmin;
using U = GS1US.Tests.Common.Pages.UserPortal;
using GS1US.Tests.Common.APIs.DataHub.Umbrella.Api;
using LanguageExt;
using static LanguageExt.Prelude;
using System.Collections.Generic;
using GS1US.Tests.Common.APIs.DataHub.Umbrella.Client;
using Newtonsoft.Json;
using GS1US.Tests.Common.APIs.DataHub.Umbrella.Model;
using System.Linq;

namespace GS1US.Tests.DataHub.Admin
{
    [Binding]
    public class UmbrellaAdminUiSteps
    {
        private IWebDriver driver;
        private TestConfig config;
        private ScenarioContext context;
        private DefaultApi api;

        public UmbrellaAdminUiSteps(IWebDriver driver, TestConfig config, ScenarioContext scenarioContext, DefaultApi api)
        {
            this.driver = driver;
            this.config = config;
            this.context = scenarioContext;
            this.api = api;
        }

        //
        //
        // Given
        //
        //

        [Given(@"parent company")]
        public void GivenParentCompany(Table table)
        {
            var c = table.CreateInstance<Models.Company>();
            context["parent"] = c;
        }

        //
        //
        // When
        //
        //

        [When(@"open Data Hub Admin page")]
        public void OpenDataHubAdminPage()
        {
            driver.Navigate().GoToUrl(config.BaseUrl);
        }

        [When(@"login")]
        public void Login()
        {
            new U.LoginPage(driver)
                .Login(config.Username, config.Password);

            new U.CompanyList(driver)
                .SelectCompany("GS1 US")
                .SignIn();
        }

        [When(@"click manage link for Umbrella Candidates")]
        public void ClickManageLinkForUmbrellaCandidates()
        {
            new P.Home(driver)
                .ClicManageUmbrellaCandidates();
        }

        [When(@"enter the parent company account number and click select")]
        public void EnterParentCompanyAccountNumber()
        {
            new P.UmbrellaAccountsAdmin(driver)
                .EnterParentAccountNumber(context.Get<Models.Company>("parent").Id)
                .ClickSelect()
                .WaitSpinner();
        }

        [When(@"call umbrella api to delete all members")]
        public void WhenCallUmbrellaApiToDeleteAllMembers()
        {
            var parent = context.Get<Models.Company>("parent").Id;
            var admin = config.UmbrellaAdmin;
            try
            {
                var r = api.DeleteUmbrellaWithHttpInfo(parent, admin);
                r.StatusCode.ShouldBe(HttpStatusCode.OK);
            }
            catch (ApiException e)
            {
                e.ErrorCode.ShouldBe(404);
            }
        }

        [When(@"call candidate api to delete all candidates")]
        public void WhenCallCandidateApiToDeleteAllCandidates()
        {
            var parent = context.Get<Models.Company>("parent").Id;
            var admin = config.UmbrellaAdmin;
            try
            {
                var r = api.DeleteUmbrellaCandidatesDefinitionWithHttpInfo(parent, admin);
                r.StatusCode.ShouldBe(HttpStatusCode.OK);
            }
            catch (ApiException e)
            {
                e.ErrorCode.ShouldBe(404);
            }
        }

        [When(@"call candidate api to define candidates")]
        public void WhenCallCandidateApiToDefineCandidates(Table table)
        {
            var parent = context.Get<Models.Company>("parent").Id;
            var admin = config.UmbrellaAdmin;
            var cs = table.CreateSet<Models.Company>().Map(x => x.Id).ToList();
            var req = new UmbrellaCandidatesDefinitionRequest(parent, cs);
            var res = api.CreateUmbrellaCandidatesWithHttpInfo(admin, req);
            res.StatusCode.ShouldBe(HttpStatusCode.Created);
        }

        [When(@"call umbrella api to define members")]
        public void WhenCallUmbrellaApiToDefineMembers(Table table)
        {
            var parent = context.Get<Models.Company>("parent").Id;
            var admin = config.UmbrellaAdmin;
            var cs = table.CreateSet<Models.Company>().Map(x => x.Id).ToList();
            var req = new UmbrellaDefinitionRequest(parent, cs);
            var res = api.CreateUmbrellaWithHttpInfo(admin, req);
            res.StatusCode.ShouldBe(HttpStatusCode.Created);
        }

        [When(@"refresh page")]
        public void WhenRefreshPage()
        {
            driver.Navigate().Refresh();
        }

        [When(@"add candidates to the list")]
        public void WhenAddCandidatesToTheList(Table table)
        {
            void CreateCompany(Models.Company c)
            {
                new P.UmbrellaAccountsAdmin(driver)
                    .EnterChildAccountNumber(c.Id)
                    .ClickAdd();
            }

            var list = table.CreateSet<Models.Company>();
            list.Iter(CreateCompany);
            context["added"] = list;
        }

        [When(@"click save button")]
        public void WhenClickSaveButton()
        {
            new P.UmbrellaAccountsAdmin(driver)
                .ClickSave();
        }

        [When(@"remove companies from the list")]
        public void WhenRemoveCompaniesFromTheList(Table table)
        {
            void RemoveCompany(Models.Company c)
            {
                new P.UmbrellaAccountsAdmin(driver)
                    .RemoveCandidate(c.Id);
            }

            var cs = table.CreateSet<Models.Company>();
            cs.Iter(RemoveCompany);
            context["removed"] = cs;
        }

        [When(@"remove all candidates")]
        public void WhenRemoveAllCandidates()
        {
            void RemoveCompany(Models.Company c)
            {
                new P.UmbrellaAccountsAdmin(driver)
                    .RemoveCandidate(c.Id);
            }

            var cs = new P.UmbrellaAccountsAdmin(driver).CandidateList()
                .Map(t => new Models.Company { Id = t.id, Name = t.name });
            cs.Iter(RemoveCompany);
            context.Add("removed", cs);
        }

        [When(@"discard changes")]
        public void WhenDiscardChanges()
        {
            new P.UmbrellaAccountsAdmin(driver)
                .ClickDiscardChanges()
                .ConfirmDicardChanges();
        }

        //
        //
        // Then
        //
        //

        [Then(@"the candidate list is empty")]
        public void ThenTheCandidateListIsEmpty()
        {
            var list = new P.UmbrellaAccountsAdmin(driver).CandidateList();
            list.ShouldBeEmpty();
        }

        [Then(@"added candidate compan(?:y|ies) (?:is|are) shown in the list")]
        public void ThenAddedCandidateCompanyIsShownInTheList()
        {
            var list = toSet(new P.UmbrellaAccountsAdmin(driver).CandidateList().Map(t => t.id));
            context.Get<IEnumerable<Models.Company>>("added").ForAll(c => list.Contains(c.Id)).ShouldBeTrue();
        }

        [Then(@"added candidate compan(?:y|ies) (?:is|are) not shown in the list")]
        public void ThenAddedCandidateCompanyIsNotShownInTheList()
        {
            var list = toSet(new P.UmbrellaAccountsAdmin(driver).CandidateList().Map(t => t.id));
            context.Get<IEnumerable<Models.Company>>("added").ForAll(c => !list.Contains(c.Id)).ShouldBeTrue();
        }

        [Then(@"added candidate compan(?:y|ies) (?:is|are) returned in get api call")]
        public void ThenAddedCandidateCompanyIsReturnedInGetApiCall()
        {
            var parent = context.Get<Models.Company>("parent").Id;
            var r = api.GetUmbrellaCandidatesDefinitionWithHttpInfo(parent);
            r.StatusCode.ShouldBe(HttpStatusCode.OK);
            var list = toSet(r.Data.Children.Map(c => c.ChildAccountID));

            context.Get<IEnumerable<Models.Company>>("added").ForAll(c => list.Contains(c.Id)).ShouldBeTrue();
        }

        [Then(@"removed candidate compan(?:y|ies) (?:is|are) not shown in the list")]
        public void ThenRemovedCandidateCompanyIsNotShownInTheList()
        {
            var list = toSet(new P.UmbrellaAccountsAdmin(driver).CandidateList().Map(t => t.id));
            context.Get<IEnumerable<Models.Company>>("removed").ForAll(c => !list.Contains(c.Id)).ShouldBeTrue();
        }

        [Then(@"removed candidate compan(?:y|ies) (?:is|are) not returned in get api call")]
        public void ThenRemovedCandidateCompanyIsNotReturnedInGetApiCall()
        {
            var parent = context.Get<Models.Company>("parent").Id;
            var r = api.GetUmbrellaCandidatesDefinitionWithHttpInfo(parent);
            r.StatusCode.ShouldBe(HttpStatusCode.OK);
            var list = toSet(r.Data.Children.Map(c => c.ChildAccountID));

            context.Get<IEnumerable<Models.Company>>("removed").ForAll(c => !list.Contains(c.Id)).ShouldBeTrue();
        }

        [Then(@"removed candidate compan(?:y|ies) (?:is|are) shown in the list")]
        public void ThenRemovedCandidateCompaniesAreShownInTheList()
        {
            var list = toSet(new P.UmbrellaAccountsAdmin(driver).CandidateList().Map(t => t.id));
            context.Get<IEnumerable<Models.Company>>("removed").ForAll(c => list.Contains(c.Id)).ShouldBeTrue();
        }

        [Then(@"candidate list has no items")]
        public void ThenCandidateListHasNoItems()
        {
            new P.UmbrellaAccountsAdmin(driver).CandidateList()
                .ShouldBeEmpty();
        }

        [Then(@"get candidates api call returns (.*) items")]
        public void ThenGetCandidatesApiCallReturnsItems(int p0)
        {
            var parent = context.Get<Models.Company>("parent").Id;
            var r = api.GetUmbrellaCandidatesDefinitionWithHttpInfo(parent);
            r.StatusCode.ShouldBe(HttpStatusCode.OK);
            r.Data.Children.ShouldBeEmpty();
        }

        [Then(@"remove button for the following compan(?:y|ies) (?:is|are) disabled")]
        public void ThenRemoveButtonForTheFollowingCompanyIsDisabled(Table table)
        {
            bool IsRemoveButtonDisabled(string cid) =>
                new P.UmbrellaAccountsAdmin(driver).IsRemoveButtonDisabled(cid);

            table.CreateSet<Models.Company>()
                .ForAll(c => IsRemoveButtonDisabled(c.Id))
                .ShouldBeTrue();
        }

        [Then(@"remove button for the following compan(?:y|ies) (?:is|are) enabled")]
        public void ThenRemoveButtonForTheFollowingCompaniesAreEnabled(Table table)
        {
            bool IsRemoveButtonEnabled(string cid) =>
                !(new P.UmbrellaAccountsAdmin(driver).IsRemoveButtonDisabled(cid));

            table.CreateSet<Models.Company>()
                .ForAll(c => IsRemoveButtonEnabled(c.Id))
                .ShouldBeTrue();
        }

        [Then(@"the following items? (?:is|are) annotated with exclamation mark")]
        public void ThenTheFollowingItemIsAnnotatedWithExclamationMark(Table table)
        {
            bool IsItemMarked(string cid) =>
                new P.UmbrellaAccountsAdmin(driver).IsItemExclamationMarked(cid);

            table.CreateSet<Models.Company>()
                .ForAll(c => IsItemMarked(c.Id))
                .ShouldBeTrue();
        }

        [Then(@"the following items? (?:is|are) not annotated with exclamation mark")]
        public void ThenTheFollowingItemsAreNotAnnotatedWithExclamationMark(Table table)
        {
            bool IsItemUnmarked(string cid) =>
                !(new P.UmbrellaAccountsAdmin(driver).IsItemExclamationMarked(cid));

            table.CreateSet<Models.Company>()
                .ForAll(c => IsItemUnmarked(c.Id))
                .ShouldBeTrue();
        }
    }
}