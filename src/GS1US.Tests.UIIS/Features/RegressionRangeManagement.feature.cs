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
namespace GS1US.Tests.UIIS.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.4.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [TechTalk.SpecRun.FeatureAttribute("Range management regression scenarios", SourceFile="Features\\RegressionRangeManagement.feature", SourceLine=0)]
    public partial class RangeManagementRegressionScenariosFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "RegressionRangeManagement.feature"
#line hidden
        
        [TechTalk.SpecRun.FeatureInitialize()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Range management regression scenarios", null, ProgrammingLanguage.CSharp, ((string[])(null)));
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
        
        public virtual void OpenRangeForAutomaticVending(string capacity, string prefix_Type, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Open range for automatic vending", null, exampleTags);
#line 4
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 5
    testRunner.Given(string.Format("I have a {0} RANGE for capacity {1} which is not open", prefix_Type, capacity), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 6
 testRunner.When("I navigate to UIIS UI", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 7
 testRunner.And("I log in", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 8
 testRunner.And("I select Open a Prefix Range menu", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 9
 testRunner.And(string.Format("I click a RANGE button for {0}", prefix_Type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.And(string.Format("I select {0} capacity radio button", capacity), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
 testRunner.And("I input the specific range", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
 testRunner.And("I click on Enable Automatic Vending checkbox", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.And("I input the reason for opening the range", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.And("I click on Next button in Open Prefix Range page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
 testRunner.And("click on the OPEN RANGE button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
 testRunner.Then("I see this range removed from companyPrefixAvailableRange table", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 17
 testRunner.And("the range predicate get added to CompanyPrefixRange table", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
 testRunner.And("I see top of the page show open confirmation message Prefix Range Opened", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
 testRunner.And("I see in CompanyPrefixRange table AutoAssign = 1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for automatic vending, Examples 1, 10", new string[] {
                "debug",
                "light"}, SourceLine=23)]
        public virtual void OpenRangeForAutomaticVending_ExampleSet0_10()
        {
#line 4
this.OpenRangeForAutomaticVending("10", "UPC", new string[] {
                        "debug",
                        "light"});
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for automatic vending, Examples 2, 10", new string[] {
                "debug"}, SourceLine=28)]
        public virtual void OpenRangeForAutomaticVending_ExampleSet1_10()
        {
#line 4
this.OpenRangeForAutomaticVending("10", "UPC", new string[] {
                        "debug"});
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for automatic vending, Examples 1, 100", new string[] {
                "debug"}, SourceLine=23)]
        public virtual void OpenRangeForAutomaticVending_ExampleSet1_100()
        {
#line 4
this.OpenRangeForAutomaticVending("100", "EAN", new string[] {
                        "debug"});
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for automatic vending, Examples 1, Variant 0", SourceLine=23)]
        public virtual void OpenRangeForAutomaticVending_ExampleSet2_Variant0()
        {
#line 4
this.OpenRangeForAutomaticVending("10", "UPC", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for automatic vending, Examples 1, Variant 1", SourceLine=23)]
        public virtual void OpenRangeForAutomaticVending_ExampleSet2_Variant1()
        {
#line 4
this.OpenRangeForAutomaticVending("100", "UPC", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for automatic vending, Examples 1, Variant 2", SourceLine=23)]
        public virtual void OpenRangeForAutomaticVending_ExampleSet2_Variant2()
        {
#line 4
this.OpenRangeForAutomaticVending("1000", "UPC", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for automatic vending, Examples 1, Variant 3", SourceLine=23)]
        public virtual void OpenRangeForAutomaticVending_ExampleSet2_Variant3()
        {
#line 4
this.OpenRangeForAutomaticVending("10000", "UPC", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for automatic vending, Examples 1, Variant 4", SourceLine=23)]
        public virtual void OpenRangeForAutomaticVending_ExampleSet2_Variant4()
        {
#line 4
this.OpenRangeForAutomaticVending("100000", "UPC", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for automatic vending, Examples 1, Variant 5", SourceLine=23)]
        public virtual void OpenRangeForAutomaticVending_ExampleSet2_Variant5()
        {
#line 4
this.OpenRangeForAutomaticVending("10", "EAN", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for automatic vending, Examples 1, Variant 6", SourceLine=23)]
        public virtual void OpenRangeForAutomaticVending_ExampleSet2_Variant6()
        {
#line 4
this.OpenRangeForAutomaticVending("100", "EAN", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for automatic vending, Examples 1, Variant 7", SourceLine=23)]
        public virtual void OpenRangeForAutomaticVending_ExampleSet2_Variant7()
        {
#line 4
this.OpenRangeForAutomaticVending("1000", "EAN", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for automatic vending, Examples 1, Variant 8", SourceLine=23)]
        public virtual void OpenRangeForAutomaticVending_ExampleSet2_Variant8()
        {
#line 4
this.OpenRangeForAutomaticVending("10000", "EAN", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for automatic vending, Examples 1, Variant 9", SourceLine=23)]
        public virtual void OpenRangeForAutomaticVending_ExampleSet2_Variant9()
        {
#line 4
this.OpenRangeForAutomaticVending("100000", "EAN", ((string[])(null)));
#line hidden
        }
        
        public virtual void OpenRangeForSpecificVending(string capacity, string prefix_Type, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Open range for specific vending", null, exampleTags);
#line 46
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 47
    testRunner.Given(string.Format("I have a {0} RANGE for specific vending for capacity {1} which is not open", prefix_Type, capacity), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 48
 testRunner.When("I navigate to UIIS UI", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 49
 testRunner.And("I log in", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 50
 testRunner.And("I select Open a Prefix Range menu", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 51
 testRunner.And(string.Format("I click a RANGE button for {0}", prefix_Type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 52
 testRunner.And(string.Format("I select {0} capacity radio button", capacity), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 53
 testRunner.And("I input the specific range", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
 testRunner.And("I input the reason for opening the range", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
 testRunner.And("I click on Next button in Open Prefix Range page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 56
 testRunner.And("click on the OPEN RANGE button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
 testRunner.Then("the range predicate get added to CompanyPrefixRange table", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 58
 testRunner.And("I see this range unavailable from companyPrefixAvailableRange table", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 59
 testRunner.And("I see top of the page show open confirmation message Prefix Range Opened", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 60
 testRunner.And("I see in CompanyPrefixRange table AutoAssign = 0", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for specific vending, Examples 1, 100", new string[] {
                "light"}, SourceLine=64)]
        public virtual void OpenRangeForSpecificVending_ExampleSet0_100()
        {
#line 46
this.OpenRangeForSpecificVending("100", "EAN", new string[] {
                        "light"});
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for specific vending, Examples 1, 10000", new string[] {
                "debug"}, SourceLine=64)]
        public virtual void OpenRangeForSpecificVending_ExampleSet1_10000()
        {
#line 46
this.OpenRangeForSpecificVending("10000", "UPC", new string[] {
                        "debug"});
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for specific vending, Examples 1, 100", new string[] {
                "debug"}, SourceLine=64)]
        public virtual void OpenRangeForSpecificVending_ExampleSet1_100()
        {
#line 46
this.OpenRangeForSpecificVending("100", "EAN", new string[] {
                        "debug"});
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for specific vending, Examples 1, Variant 0", SourceLine=64)]
        public virtual void OpenRangeForSpecificVending_ExampleSet2_Variant0()
        {
#line 46
this.OpenRangeForSpecificVending("10", "UPC", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for specific vending, Examples 1, Variant 1", SourceLine=64)]
        public virtual void OpenRangeForSpecificVending_ExampleSet2_Variant1()
        {
#line 46
this.OpenRangeForSpecificVending("100", "UPC", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for specific vending, Examples 1, Variant 2", SourceLine=64)]
        public virtual void OpenRangeForSpecificVending_ExampleSet2_Variant2()
        {
#line 46
this.OpenRangeForSpecificVending("1000", "UPC", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for specific vending, Examples 1, Variant 3", SourceLine=64)]
        public virtual void OpenRangeForSpecificVending_ExampleSet2_Variant3()
        {
#line 46
this.OpenRangeForSpecificVending("10000", "UPC", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for specific vending, Examples 1, Variant 4", SourceLine=64)]
        public virtual void OpenRangeForSpecificVending_ExampleSet2_Variant4()
        {
#line 46
this.OpenRangeForSpecificVending("100000", "UPC", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for specific vending, Examples 1, Variant 5", SourceLine=64)]
        public virtual void OpenRangeForSpecificVending_ExampleSet2_Variant5()
        {
#line 46
this.OpenRangeForSpecificVending("10", "EAN", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for specific vending, Examples 1, Variant 6", SourceLine=64)]
        public virtual void OpenRangeForSpecificVending_ExampleSet2_Variant6()
        {
#line 46
this.OpenRangeForSpecificVending("100", "EAN", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for specific vending, Examples 1, Variant 7", SourceLine=64)]
        public virtual void OpenRangeForSpecificVending_ExampleSet2_Variant7()
        {
#line 46
this.OpenRangeForSpecificVending("1000", "EAN", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for specific vending, Examples 1, Variant 8", SourceLine=64)]
        public virtual void OpenRangeForSpecificVending_ExampleSet2_Variant8()
        {
#line 46
this.OpenRangeForSpecificVending("10000", "EAN", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Open range for specific vending, Examples 1, Variant 9", SourceLine=64)]
        public virtual void OpenRangeForSpecificVending_ExampleSet2_Variant9()
        {
#line 46
this.OpenRangeForSpecificVending("100000", "EAN", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Close a RANGE for auto vending", new string[] {
                "debug",
                "light"}, SourceLine=87)]
        public virtual void CloseARANGEForAutoVending()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Close a RANGE for auto vending", null, new string[] {
                        "debug",
                        "light"});
#line 88
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 89
 testRunner.Given("I have a open range for auto vending", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 90
 testRunner.When("I navigate to UIIS UI", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 91
 testRunner.And("I log in", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 92
 testRunner.And("I select Close a Prefix Range menu", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 93
 testRunner.And("I input the range which is open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 94
 testRunner.And("I input the reason for closing the range", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 95
 testRunner.And("I click on the NEXT button on Close a Prefix Range page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 96
 testRunner.And("I click on CLOSE RANGE button on Review page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 97
 testRunner.Then("the range predicate gets removed from CompanyPrefixRange table", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 101
 testRunner.And("I see top of the page show close confirmation message Prefix range has been close" +
                    "d", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Close a RANGE for manual vending", new string[] {
                "debug",
                "light"}, SourceLine=104)]
        public virtual void CloseARANGEForManualVending()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Close a RANGE for manual vending", null, new string[] {
                        "debug",
                        "light"});
#line 105
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 106
 testRunner.Given("I have a open range for manual vending", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 107
 testRunner.When("I navigate to UIIS UI", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 108
 testRunner.And("I log in", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 109
 testRunner.And("I select Close a Prefix Range menu", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 110
 testRunner.And("I input the range which is open", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 111
 testRunner.And("I input the reason for closing the range", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 112
 testRunner.And("I click on the NEXT button on Close a Prefix Range page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 113
 testRunner.And("I click on CLOSE RANGE button on Review page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 114
 testRunner.Then("the range predicate gets removed from CompanyPrefixRange table", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 115
 testRunner.And("I see top of the page show close confirmation message Prefix range has been close" +
                    "d", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
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
