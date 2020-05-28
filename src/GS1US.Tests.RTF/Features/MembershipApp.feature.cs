﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.4.0.0
//      SpecFlow Generator Version:2.4.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace GS1US.Tests.RTF.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.4.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [TechTalk.SpecRun.FeatureAttribute("Request prefixes from Membership Application", SourceFile="Features\\MembershipApp.feature", SourceLine=0)]
    public partial class RequestPrefixesFromMembershipApplicationFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "MembershipApp.feature"
#line hidden
        
        [TechTalk.SpecRun.FeatureInitialize()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Request prefixes from Membership Application", null, ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [TechTalk.SpecRun.FeatureCleanup()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        [TechTalk.SpecRun.ScenarioCleanup()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void ExistingCustomerBuysPrefixesUsingPayPal(string capacities, string upc, string n, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Existing customer buys prefixes using PayPal", null, exampleTags);
#line 3
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 4
 testRunner.When("I navigate to Membership Application site", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 5
 testRunner.And("I login to GS1 US User Portal using test account", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 6
 testRunner.And("I select my test company from company dropdown and signin", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 8
 testRunner.And("I click on the next button on contact details page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 9
 testRunner.And(string.Format("I select prefixes: {0}", capacities), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.And("I click on the policy consent checkbox", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
 testRunner.And("I click on the next button on program details page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
 testRunner.And("I click on the license agreement checkbox", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.And("I query contact info for test account", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.And("I fill contact information using primary contact", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
 testRunner.And("I click on the PayPal button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
 testRunner.And("I proceed with PayPal payment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
 testRunner.And("I confirm the payment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
 testRunner.Then(string.Format("I see {0} prefixes displayed", n), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "MemberType",
                        "Count"});
            table1.AddRow(new string[] {
                        "CM",
                        "1"});
            table1.AddRow(new string[] {
                        "I",
                        "0"});
            table1.AddRow(new string[] {
                        "#UPC",
                        string.Format("{0}", upc)});
#line 19
 testRunner.And("In Name table, following entries are created or updated", ((string)(null)), table1, "And ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "ProductCode",
                        "Count"});
            table2.AddRow(new string[] {
                        "PC_AN_CAP_A",
                        string.Format("{0}", upc)});
            table2.AddRow(new string[] {
                        "PC_AN_CAP_A1",
                        string.Format("{0}", upc)});
#line 24
 testRunner.And("Subscription entries are created as follows", ((string)(null)), table2, "And ");
#line 28
 testRunner.And(string.Format("If prorated, I should see {0} PC_AN_CAP_A2 entries", upc), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.And("In Subscriptions table, billing amounts are correct", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
 testRunner.And("In Trans table, payments are added to a batch", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Existing customer buys prefixes using PayPal, 10", SourceLine=33)]
        public virtual void ExistingCustomerBuysPrefixesUsingPayPal_10()
        {
#line 3
this.ExistingCustomerBuysPrefixesUsingPayPal("10", "1", "1", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Existing customer buys prefixes using PayPal, 100", SourceLine=33)]
        public virtual void ExistingCustomerBuysPrefixesUsingPayPal_100()
        {
#line 3
this.ExistingCustomerBuysPrefixesUsingPayPal("100", "1", "1", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Existing customer buys prefixes using PayPal, 1000", SourceLine=33)]
        public virtual void ExistingCustomerBuysPrefixesUsingPayPal_1000()
        {
#line 3
this.ExistingCustomerBuysPrefixesUsingPayPal("1000", "1", "1", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Existing customer buys prefixes using PayPal, 10000", SourceLine=33)]
        public virtual void ExistingCustomerBuysPrefixesUsingPayPal_10000()
        {
#line 3
this.ExistingCustomerBuysPrefixesUsingPayPal("10000", "1", "1", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Existing customer buys prefixes using PayPal, 100000", SourceLine=33)]
        public virtual void ExistingCustomerBuysPrefixesUsingPayPal_100000()
        {
#line 3
this.ExistingCustomerBuysPrefixesUsingPayPal("100000", "1", "1", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Existing customer buys prefixes using PayPal, 10, 10", SourceLine=33)]
        public virtual void ExistingCustomerBuysPrefixesUsingPayPal_1010()
        {
#line 3
this.ExistingCustomerBuysPrefixesUsingPayPal("10, 10", "2", "2", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Existing customer buys prefixes using PayPal, 100, 1000", SourceLine=33)]
        public virtual void ExistingCustomerBuysPrefixesUsingPayPal_1001000()
        {
#line 3
this.ExistingCustomerBuysPrefixesUsingPayPal("100, 1000", "2", "2", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Existing customer buys prefixes using PayPal, 10, 100, 1000", SourceLine=33)]
        public virtual void ExistingCustomerBuysPrefixesUsingPayPal_101001000()
        {
#line 3
this.ExistingCustomerBuysPrefixesUsingPayPal("10, 100, 1000", "3", "0", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Existing customer buys prefixes using PayPal, 100000, 10000, 10000, 10000, 1000, " +
            "100, 10", SourceLine=33)]
        public virtual void ExistingCustomerBuysPrefixesUsingPayPal_100000100001000010000100010010()
        {
#line 3
this.ExistingCustomerBuysPrefixesUsingPayPal("100000, 10000, 10000, 10000, 1000, 100, 10", "7", "0", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Existing customer buys prefixes using PayPal, 10,10000,10,100000,1000,10,100,10,1" +
            "0", SourceLine=33)]
        public virtual void ExistingCustomerBuysPrefixesUsingPayPal_1010000101000001000101001010()
        {
#line 3
this.ExistingCustomerBuysPrefixesUsingPayPal("10,10000,10,100000,1000,10,100,10,10", "9", "0", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.TestRunCleanup()]
        public virtual void TestRunCleanup()
        {
            TechTalk.SpecFlow.TestRunnerManager.GetTestRunner().OnTestRunEnd();
        }
    }
}
#pragma warning restore
#endregion
