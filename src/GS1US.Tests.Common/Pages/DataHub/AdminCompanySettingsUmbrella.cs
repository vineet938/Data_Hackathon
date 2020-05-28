using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using LanguageExt;
using static LanguageExt.Prelude;
using System.Collections.ObjectModel;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class AdminCompanySettingsUmbrella : PagesCommon<AdminCompanySettingsUmbrella>
    {
        public AdminCompanySettingsUmbrella(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"CandidateCheckboxes", By.CssSelector("#candidatesTable tbody td input") },
            {"DefinitionCheckboxes", By.CssSelector("#definitionsTable tbody td input") },
            {"AddButton", By.Id("addCandidatesButton") },
            {"RemoveButton", By.Id("removeCandidatesButton") },
            {"SaveButton", By.Id("saveChangesButton") },
            {"SelectAllCandidatesCheckbox", By.XPath("//*[@for='select-candidate-all']") },
            {"NumberOfSelectedCandidatesLabel", By.XPath("//*[text()='Candidates']/following-sibling::h4") },
            {"Spinner", By.CssSelector("[name='chopper']") }
        };

        private By CheckboxLocator(int i) => By.XPath($"(//*[@id='candidatesTable']/tbody//label)[{i}]");

        public AdminCompanySettingsUmbrella SelectAll()
        {
            var checkboxes = Driver.FindElements(Locators["CandidateCheckboxes"]);
            Range(1, checkboxes.Count).Iter(i => Apply(CheckboxLocator(i), 5, 1, Click));
            return this;
        }

        public AdminCompanySettingsUmbrella ClickAddButton()
        {
            Apply("AddButton", 10, 1, Click);
            return this;
        }

        public AdminCompanySettingsUmbrella ClickRemoveButton()
        {
            Apply("RemoveButton", 10, 1, Click);
            return this;
        }

        public AdminCompanySettingsUmbrella ClickSaveButton()
        {
            Apply("SaveButton", 5, 1, Click);
            return this;
        }

        public AdminCompanySettingsUmbrella WaitSpinner()
        {
            Utils.WaitUtils.WaitToDisappear(Driver, Locators["Spinner"], 180);
            return this;
        }

        public AdminCompanySettingsUmbrella SelectCandidates(IEnumerable<string> ids)
        {
            var script = @"
                console.log(arguments);
                var ids = new Set(arguments[0]);
                var $scope = $('#candidatesTable').scope();
                $scope.candidates.forEach(c => {
                    if (ids.has(c.id)) {
                    c.selected = true;                    
                    }
                });
                $scope.$apply();
            ";
            ((IJavaScriptExecutor)Driver).ExecuteScript(script, ids);
            return this;
        }

        public AdminCompanySettingsUmbrella SelectCandidatesToRemove(IEnumerable<string> ids)
        {
            var script = @"
                console.log(arguments);
                var ids = new Set(arguments[0]);
                var $scope = $('#candidatesTable').scope();
                $scope.definitions.forEach(c => {
                    if (ids.has(c.id)) {
                    c.selected = true;                    
                    }
                });
                $scope.$apply();
            ";
            ((IJavaScriptExecutor)Driver).ExecuteScript(script, ids);
            return this;
        }

        public AdminCompanySettingsUmbrella ClickSelectAllCandidatesCheckbox()
        {
            Apply("SelectAllCandidatesCheckbox", 5, 1, Click);
            return this;
        }

        public IEnumerable<string> GetDefinitionChildren()
        {
            return Driver.FindElements(Locators["DefinitionCheckboxes"])
                .Map(e => e.GetAttribute("id").Split('-')[1]);
        }

        public IEnumerable<string> GetCandidateChildren()
        {
            return Driver.FindElements(Locators["CandidateCheckboxes"])
                .Map(e => e.GetAttribute("id").Split('-')[1]);
        }

        public IEnumerable<string> GetSelectedCandidateChildren()
        {
            var script = @"
                var $scope = $('#candidatesTable').scope();
                return $scope.candidates.filter(c => c.selected).map(c => c.id);
            ";
            var objList = (ReadOnlyCollection<object>)((IJavaScriptExecutor)Driver).ExecuteScript(script);
            return objList.Map(o => (string)o);
        }

        public bool IsSelectAllCandidateCheckboxChecked()
        {
            var script = @"
                var $scope = $('#candidatesTable').scope();
                return $scope.allCandidatesSelected;
            ";
            return (bool)((IJavaScriptExecutor)Driver).ExecuteScript(script);
        }

        public bool IsAddButtonEnabled()
        {
            return elements["AddButton"].Enabled;
        }

        public bool IsRemoveButtonEnabled()
        {
            return elements["RemoveButton"].Enabled;
        }

        public int GetDisplayedNumberOfSelectedCandidates()
        {
            var s = elements["NumberOfSelectedCandidatesLabel"].Text;
            return int.Parse(s.Split(' ')[0]);
        }
    }
}
