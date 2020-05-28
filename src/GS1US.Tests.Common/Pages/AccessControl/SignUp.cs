using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace GS1US.Tests.Common.Pages.AccessControl
{
    public class SignUp : PagesCommon<Login>
    {
        public SignUp(IWebDriver driver) : base(driver)
        {

        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"UserName", By.Id("signInName") },
            {"Password", By.Id("password") },
            {"Submit", By.Id("next") }
        };

        public SignUp SetUsername(string UserN)
        {
            elements["UserName"].SetText(UserN);
            return this;
        }

        public SignUp SetPassword(string UserP)
        {
            elements["Password"].SetText(UserP);
            return this;
        }

        public SignUp SubmitButton()
        {
            elements["Submit"].Click();
            return this;
        }

        public SignUp VerifySignUpTitle(bool SignUpTitle)
        {
            return this;

        }


    }
}
