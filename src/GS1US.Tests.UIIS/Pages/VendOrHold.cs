using GS1US.Test.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.UIIS.Pages
{
    class VendOrHold : PagesCommon
    {
        public VendOrHold(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"vend", By.XPath("//label[@for='vend']") },
            {"hold", By.XPath("//label[@for='hold']") },
            {"accountNumber",  By.Id("accountNumber") },
            {"validate", By.XPath("//button[text()='Validate']") },
            {"validationCheckMark", By.CssSelector("i.mdi-checkbox-marked-circle-outline") },
            {"accountNumberValidationError", By.CssSelector("div.error-wrapper span") },
            {"upcPrefixButton", By.XPath("//label[@for='UPC_RANGE']") },
            {"eanPrefixButton", By.XPath("//label[@for='EAN_RANGE']") },
            {"ediComIdButton", By.XPath("//label[@for='EDI']") },
            {"standardUpcPrefixButton", By.XPath("//label[@for='STANDARD_UPC_RANGE']") },
            {"ndcPrefixButton", By.XPath("//label[@for='NDC_RANGE']") },
            {"alliancePrefixButton", By.XPath("//label[@for='ALLIANCE_PREFIX_RANGE']") },
            {"cap10", By.XPath("//label[@for='capacity-1']") },
            {"cap100", By.XPath("//label[@for='capacity-2']") },
            {"cap1000", By.XPath("//label[@for='capacity-3']") },
            {"cap10000", By.XPath("//label[@for='capacity-4']") },
            {"cap100000", By.XPath("//label[@for='capacity-5']") },
            {"specificPrefix", By.XPath("//label[@for='SPECIFY']") },
            {"specificPrefixInput", By.Id("SPECIFY") },
            {"nextAvailable", By.XPath("//label[@for='NEXT_AVAILABLE']") },
            {"labelerCode", By.CssSelector("input.labeler-input") },
            {"labelerCodeValidate", By.CssSelector("button.validate.blue") },
            {"labeler", By.CssSelector("div.labeler div.company-match div") },
            {"labelerMatchError", By.CssSelector("div.labeler div.match-error") },
            {"reason", By.XPath("//span[text()='Why is this prefix being held?']/following-sibling::input") },
            {"next", By.CssSelector("button.next") },
            {"prefixValidationError", By.XPath("//text()[contains(.,'PREFIX NOT AVAILABLE')]/parent::div") }
        };

        public VendOrHold ClickVendAnIdentifier()
        {
            WaitUtils.WaitLocator(Driver, Locators["vend"], 5);
            elements["vend"].Click();
            return this;
        }

        public VendOrHold ClickHoldAPrefix()
        {
            WaitUtils.WaitLocator(Driver, Locators["hold"], 5);
            elements["hold"].Click();
            return this;
        }

        public VendOrHold EnterAccountNumber(string accountNumber)
        {
            elements["accountNumber"].SetText(accountNumber);
            return this;
        }

        public VendOrHold ClickValidate()
        {
            elements["validate"].Click();
            return this;
        }

        public bool IsValidateButtonDisabled()
        {
            return elements["validate"].GetAttribute("disabled") != null;
        }

        public bool IsSpeficPrefixButtonDisabled()
        {
            return elements["specificPrefixInput"].GetAttribute("disabled") != null;
        }

        public VendOrHold ClickUpcPrefix()
        {
            WaitUtils.WaitLocator(Driver, Locators["upcPrefixButton"], 15);
            elements["upcPrefixButton"].Click();
            return this;
        }

        public VendOrHold ClickEanPrefix()
        {
            WaitUtils.WaitLocator(Driver, Locators["eanPrefixButton"], 5);
            elements["eanPrefixButton"].Click();
            return this;
        }

        public VendOrHold ClickEdiComIdButton()
        {
            WaitUtils.WaitLocator(Driver, Locators["ediComIdButton"], 5);
            elements["ediComIdButton"].Click();
            return this;
        }

        public VendOrHold ClickStandardUpcPrefix()
        {
            WaitUtils.WaitLocator(Driver, Locators["standardUpcPrefixButton"], 5);
            elements["standardUpcPrefixButton"].Click();
            return this;
        }

        public VendOrHold ClickAlliancePrefix()
        {
            WaitUtils.WaitLocator(Driver, Locators["alliancePrefixButton"], 5);
            elements["alliancePrefixButton"].Click();
            return this;
        }

        public VendOrHold ClickNdcPrefix()
        {
            WaitUtils.WaitLocator(Driver, Locators["ndcPrefixButton"], 5);
            elements["ndcPrefixButton"].Click();
            return this;
        }

        public VendOrHold SelectCapacity(int capacity)
        {
            var key = $"cap{capacity}";
            WaitUtils.WaitLocator(Driver, Locators[key], 5);
            elements[key].Click();
            return this;
        }

        public VendOrHold ClickSpecificPrefix()
        {
            WaitUtils.WaitLocator(Driver, Locators["specificPrefix"], 5);
            elements["specificPrefix"].Click();
            return this;
        }

        public VendOrHold ClickNextAvailable()
        {
            WaitUtils.WaitLocator(Driver, Locators["nextAvailable"], 5);
            elements["nextAvailable"].Click();
            return this;
        }

        public VendOrHold ClickNext()
        {
            WaitUtils.WaitLocator(Driver, Locators["next"], 5);
            elements["next"].Click();
            return this;
        }

        public bool IsAccountValidated(int timeoutSecond=30)
        {
            var el = WaitUtils.WaitLocator(Driver, Locators["validationCheckMark"], timeoutSecond);
            return el != null;
        }

        public bool IsPrefixNumberValidated(int timeoutSecond=15)
        {
            var el = WaitUtils.WaitLocator(Driver, Locators["prefixValidationError"], timeoutSecond);
            return el != null;
        }

        public VendOrHold EnterSpeficPrefix(string prefix)
        {
            WaitUtils.Wait(Driver, 15, d => d
                .FindElements(By.CssSelector("div.input-group input"))
                .Tail()
                .All(el => el.Enabled && el.Displayed));
            var elements = Driver.FindElements(By.CssSelector("div.input-group input"));
            prefix.Tail().Zip(elements.Tail(), (c, el) => el.SetText(c.ToString())).Last();
            return this;
        }

        public VendOrHold WaitPrefixAvailableLabel()
        {
            WaitUtils.WaitLocator(Driver, By.CssSelector("div.status.green"), 15);
            return this;
        }

        public VendOrHold EnterLabelerCode(string labelerCode)
        {
            WaitUtils.WaitLocator(Driver, Locators["labelerCode"], 5);
            elements["labelerCode"].SetText(labelerCode);
            return this;
        }

        public VendOrHold ClickValidateCode()
        {
            elements["labelerCodeValidate"].Click();
            return this;
        }

        private string ReadLabel(string elementKey)
        {
            var el = WaitUtils.WaitLocator(Driver, Locators[elementKey], 15);
            return el.Text;
        }

        public string GetLabelerName() => ReadLabel("labeler");

        public string GetLabelerMatchError() => ReadLabel("labelerMatchError");

        public string GetAccountValidationError() => ReadLabel("accountNumberValidationError");

        public VendOrHold EnterReasonForHolding(string reason)
        {
            WaitUtils.WaitLocator(Driver, Locators["reason"], 5);
            elements["reason"].SetText(reason);
            return this;
        }

    }
}
