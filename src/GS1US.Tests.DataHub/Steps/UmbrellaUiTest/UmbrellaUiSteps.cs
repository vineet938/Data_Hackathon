using System;
using TechTalk.SpecFlow;
using GS1US.Tests.Common.Pages.DataHub;
using GS1US.Tests.Common.APIs.DataHub.Umbrella.Api;
using Shouldly;
using GS1US.Tests.DataHub.Setup;
using GS1US.Tests.Common.APIs.DataHub.Umbrella.Model;
using System.Net;
using GS1US.Tests.Common.APIs.DataHub.Umbrella.Client;
using LanguageExt;
using Set = LanguageExt.Set;
using static LanguageExt.Prelude;
using LanguageExt.ClassInstances;
using System.Linq;
using GS1US.Tests.Common.Utils;

namespace GS1US.Tests.DataHub.Steps.UmbrellaUiTest
{
    [Binding]
    public class UmbrellaUiSteps
    {
        [When(@"go to manage company settings in administration menu")]
        public void WhenGoToManageCompanySettingsInAdministrationMenu()
        {
            new MainPage(TestSetup.Driver)
                .SelectAdminCompanySettings();
        }

        [When(@"check umbrella tab is visible")]
        public void WhenCheckUmbrellaTabIsVisible()
        {
            new AdminCompanySettings(TestSetup.Driver)
                .IsUmbrellaTabShown(5)
                .ShouldBeTrue();
        }

        [When(@"add all umbrella children")]
        public void WhenAddAllUmbrellaChildren()
        {
            new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .SelectAll()
                .ClickAddButton();
        }

        [When(@"add some umbrella children")]
        public void WhenAddSomeUmbrellaChildren()
        {
            var selected = new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .GetCandidateChildren()
                .Filter(s => new Random().NextDouble() > 0.45);

            new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .SelectCandidates(selected)
                .ClickAddButton();
        }

        [When(@"save umbrella definition changes")]
        public void WhenSaveUmbrellaDefinitionChanges()
        {
            new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .ClickSaveButton()
                .WaitSpinner();
        }

        [When(@"umbrella is deleted")]
        public void WhenUmbrellaIsDeleted()
        {
            var parentCompanyId = new MainPage(TestSetup.Driver).GetAccountId();
            WhenUmbrellaIsDeleted(parentCompanyId);
        }

        [When(@"umbrella is deleted: (.*)")]
        public void WhenUmbrellaIsDeleted(string parentCompanyId)
        {
            Console.WriteLine($"company id: {parentCompanyId}");
            var baseurl = TestSetup.Config.datahubUmbrellaApi.baseurl;
            var api = new DefaultApi(baseurl);
            try
            {
                var response = api.DeleteUmbrellaWithHttpInfo(parentCompanyId);
                response.StatusCode.ShouldBe(HttpStatusCode.OK);
            }
            catch (ApiException e)
            {
                e.ErrorCode.ShouldBe(404);
            }
        }

        [Then(@"right pane of umbrella UI is empty")]
        public void ThenRightPaneOfUmbrellaUIIsEmpty()
        {
            var n = new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .GetDefinitionChildren()
                .Length();
            n.ShouldBe(0);
        }

        [Then(@"left pane of umbrella UI is empty")]
        public void ThenLeftPaneOfUmbrellaUIIsEmpty()
        {
            var n = new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .GetCandidateChildren()
                .Length();
            n.ShouldBe(0);
        }

        [Then(@"response of get umbrella call reflects the changes")]
        public void ThenGetUmbrellaCallReflectsTheChanges()
        {
            var baseurl = TestSetup.Config.datahubUmbrellaApi.baseurl;
            var api = new DefaultApi(baseurl);
            var imisId = new MainPage(TestSetup.Driver).GetAccountId();
            var res = api.GetUmbrellaWithHttpInfo(imisId);
            var persistedIds = ((UmbrellaDefinitionResponse)res.Content)
                .Children
                .Map((child) => child.ChildAccountID);

            var uiIds = new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .GetDefinitionChildren();

            Set.createRange(persistedIds)
                .Equals(Set.createRange(uiIds))
                .ShouldBe(true);
        }

        [Then(@"content of the left pane is correct")]
        public void ThenContentOfTheLeftPaneIsCorrect()
        {
            var baseurl = TestSetup.Config.datahubUmbrellaApi.baseurl;
            var api = new DefaultApi(baseurl);
            var imisId = new MainPage(TestSetup.Driver).GetAccountId();
            var res = api.GetUmbrellaWithHttpInfo(imisId);
            var persistedIds = ((UmbrellaDefinitionResponse)res.Content)
                .Children
                .Map((child) => child.ChildAccountID);

            var res2 = api.GetUmbrellaCandidatesWithHttpInfo(imisId);
            var candidates = ((UmbrellaDefinitionResponse)res2.Content)
                .Children
                .Map(child => child.ChildAccountID)
                .Except(persistedIds);

            var candidates2 = new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .GetCandidateChildren();

            candidates.Length().ShouldBe(candidates2.Length());
            candidates.Except(candidates2).ShouldBeEmpty();
        }

        [When(@"click add all button")]
        public void WhenClickAddAllButton()
        {
            new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .ClickSelectAllCandidatesCheckbox();
        }

        [Then(@"select all checkbox is checked")]
        public void ThenSelectAllCheckboxIsChecked()
        {
            new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .IsSelectAllCandidateCheckboxChecked()
                .ShouldBeTrue();
        }

        [Then(@"all candidate checkboxes are checked")]
        public void ThenAllCandidateCheckboxesAreChecked()
        {
            var n = new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .GetCandidateChildren()
                .Length();

            new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .GetSelectedCandidateChildren()
                .Length()
                .ShouldBe(n);
        }

        [Then(@"add button is enabled")]
        public void ThenAddButtonIsEnabled()
        {
            new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .IsAddButtonEnabled()
                .ShouldBeTrue();
        }

        [Then(@"add button is diabled")]
        public void ThenAddButtonIsDiabled()
        {
            new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .IsAddButtonEnabled()
                .ShouldBeFalse();
        }

        [When(@"umbrella is created as follows")]
        public void WhenUmbrellaIsCreatedAsFollows(Table table)
        {
            var parentId = TableUtils.Get(table, "parent");
            var children = TableUtils.Get(table, "children").Split(',').Map(s => s.Trim());
            var baseurl = TestSetup.Config.datahubUmbrellaApi.baseurl;
            var api = new DefaultApi(baseurl);
            try
            {
                api.DeleteUmbrellaWithHttpInfo(parentId)
                    .StatusCode.ShouldBe(HttpStatusCode.OK);
            }
            catch (ApiException e)
            {
                e.ErrorCode.ShouldBe(404);
            }

            var request = new UmbrellaDefinitionRequest(parentId, children.ToList());
            api.CreateUmbrellaWithHttpInfo(request)
                .StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Then(@"there is an unselected candidate: (.*)")]
        public void ThenThereIsAnUnselectedCandidate(string accountId)
        {
            var candidates = new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .GetCandidateChildren();
            var selectedCandidates = new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .GetSelectedCandidateChildren();
            var unselected = candidates.Except(selectedCandidates);
            unselected.Length().ShouldBeGreaterThanOrEqualTo(1);
            unselected.ShouldContain(accountId);
        }

        [Then(@"reported number of selected candidates is correct")]
        public void ThenReportedNumberOfSelectedCandidatesIsCorrect()
        {
            var selectedCandidates = new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .GetSelectedCandidateChildren();
            var n = new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .GetDisplayedNumberOfSelectedCandidates();
            selectedCandidates.Length().ShouldBe(n);
        }

        [Then(@"remove button is disabled")]
        public void ThenRemoveButtonIsDisabled()
        {
            new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .IsRemoveButtonEnabled()
                .ShouldBeFalse();
        }

        [Then(@"remove button is enabled")]
        public void ThenRemoveButtonIsEnabled()
        {
            new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .IsRemoveButtonEnabled()
                .ShouldBeTrue();
        }

        [When(@"select some umbrella children")]
        public void WhenSelectSomeUmbrellaChildren()
        {
            var selected = new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .GetDefinitionChildren()
                .Filter(s => new Random().NextDouble() > 0.45);
            new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .SelectCandidatesToRemove(selected);
        }

        [When(@"click remove button")]
        public void WhenClickRemoveButton()
        {
            new AdminCompanySettingsUmbrella(TestSetup.Driver)
                .ClickRemoveButton();
        }

    }
}
