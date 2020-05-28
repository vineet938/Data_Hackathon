using GS1US.Tests.RTF.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace GS1US.Tests.RTF.Steps
{
    using GS1US.Tests.RTF.Pages.CSA;
    using OpenQA.Selenium;
    using static ContextKeys;

    public class StepsCommon : TechTalk.SpecFlow.Steps
    {
        protected IWebDriver Driver;

        protected T Context<T>(string key) => ScenarioContext.Current.Get<T>(key);

        protected CsaContext Ctx
        {
            get
            {
                if (ScenarioContext.Current.ContainsKey(POM_CONTEXT))
                    return ScenarioContext.Current.Get<CsaContext>(POM_CONTEXT);
                else
                {
                    var ctx = new CsaContext();
                    ScenarioContext.Current.Add(POM_CONTEXT, ctx);
                    return ctx;
                }
            }
        }

        protected void SaveContext(string key, object value) => ScenarioContext.Current[key] = value;

        public StepsCommon()
        {
            Driver = TestSetup.Driver;
        }

        public DateTime ImisStartTime
        {
            get => Ctx.StartTime + Context<TimeSpan>(IMIS_DTIMEDIFF);
        }
    }
}
