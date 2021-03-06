﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.17929
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace SecureBanana.Console.Features.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Output Help when it is requested and in some cases when it is not.")]
    public partial class OutputHelpWhenItIsRequestedAndInSomeCasesWhenItIsNot_Feature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Help.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Output Help when it is requested and in some cases when it is not.", "SecureBanana need to be able to output usage directions when the user requests th" +
                    "em\r\nand when the user passes incorrect arguments to SecureBanana.", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Only the -Help argument")]
        public virtual void OnlyThe_HelpArgument()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Only the -Help argument", ((string[])(null)));
#line 5
this.ScenarioSetup(scenarioInfo);
#line hidden
#line 6
 testRunner.When("the following arguments are sent to SecureBanana:", "-Help", ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
 testRunner.And("SecureBanana is ran", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 11
 testRunner.Then("stdout should start with:", "Secure Banana\r\nSquadron 5 South\r\n----------------\r\nUsage: SecureBanana [Options]\r" +
                    "\n", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Only the -help argument (Case Insensitive)")]
        public virtual void OnlyThe_HelpArgumentCaseInsensitive()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Only the -help argument (Case Insensitive)", ((string[])(null)));
#line 20
this.ScenarioSetup(scenarioInfo);
#line hidden
#line 21
 testRunner.When("the following arguments are sent to SecureBanana:", "-help", ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 25
 testRunner.And("SecureBanana is ran", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 26
 testRunner.Then("stdout should start with:", "Secure Banana\r\nSquadron 5 South\r\n----------------\r\nUsage: SecureBanana [Options]\r" +
                    "\n", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Only the -? argument")]
        public virtual void OnlyThe_Argument()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Only the -? argument", ((string[])(null)));
#line 35
this.ScenarioSetup(scenarioInfo);
#line hidden
#line 36
 testRunner.When("the following arguments are sent to SecureBanana:", "-?", ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 40
 testRunner.And("SecureBanana is ran", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 41
 testRunner.Then("stdout should start with:", "Secure Banana\r\nSquadron 5 South\r\n----------------\r\nUsage: SecureBanana [Options]\r" +
                    "\n", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("When an unknown argument is passed")]
        public virtual void WhenAnUnknownArgumentIsPassed()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When an unknown argument is passed", ((string[])(null)));
#line 50
this.ScenarioSetup(scenarioInfo);
#line hidden
#line 51
 testRunner.When("the following arguments are sent to SecureBanana:", "asdfasdfoawieveniiiavh  asdovod e enenkkk -aaaaaaa", ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 55
 testRunner.And("SecureBanana is ran", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 56
 testRunner.Then("stdout should start with:", "Secure Banana\r\nSquadron 5 South\r\n----------------\r\nUsage: SecureBanana [Options]\r" +
                    "\n", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("When a known argument is passed along with -Help")]
        public virtual void WhenAKnownArgumentIsPassedAlongWith_Help()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When a known argument is passed along with -Help", ((string[])(null)));
#line 65
this.ScenarioSetup(scenarioInfo);
#line hidden
#line 66
 testRunner.When("the following arguments are sent to SecureBanana:", "-password=TestPassword -help", ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 70
 testRunner.And("SecureBanana is ran", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 71
 testRunner.Then("stdout should start with:", "Secure Banana\r\nSquadron 5 South\r\n----------------\r\nUsage: SecureBanana [Options]\r" +
                    "\n", ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
