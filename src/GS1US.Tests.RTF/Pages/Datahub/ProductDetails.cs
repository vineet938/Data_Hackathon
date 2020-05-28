using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace GS1US.Tests.RTF.Pages.Datahub
{
    using GS1US.Tests.RTF.Common;
    using static LocUtils;

    class ProductDetails : PagesCommon
    {
        public ProductDetails(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, string>
        {
            {"productCreate-5", Make_SuffixXpe("rdProductCreate_0") },
            {"productCreate-10", Make_SuffixXpe("rdProductCreate_1") },
            {"productCreate-ulm", Make_SuffixXpe("rdProductCreate_2") },

            {"productAccess-1", Make_SuffixXpe("rdProductAccess_0") },
            {"productAccess-5", Make_SuffixXpe("rdProductAccess_1") },
            {"productAccess-10", Make_SuffixXpe("rdProductAccess_2") },
            {"productAccess-ulm", Make_SuffixXpe("rdProductAccess_3") },

            {"locationCreate-5", Make_SuffixXpe("rdLocationCreate_0") },
            {"locationCreate-10", Make_SuffixXpe("rdLocationCreate_1") },
            {"locationCreate-ulm", Make_SuffixXpe("rdLocationCreate_2") },

            {"locationAccess-1", Make_SuffixXpe("rdLocationAccess_0") },
            {"locationAccess-5", Make_SuffixXpe("rdLocationAccess_1") },
            {"locationAccess-10", Make_SuffixXpe("rdLocationAccess_2") },
            {"locationAccess-ulm", Make_SuffixXpe("rdLocationAccess_3") },

            {"companyAccess-1", Make_SuffixXpe("rdCompanyAccess_0") },
            {"companyAccess-5", Make_SuffixXpe("rdCompanyAccess_1") },
            {"companyAccess-10", Make_SuffixXpe("rdCompanyAccess_2") },
            {"companyAccess-ulm", Make_SuffixXpe("rdCompanyAccess_3") },

            {"next", Make_SuffixXpe("btnNext") }
        }.ToLocators();

        protected override void WaitForPage()
        {
            WaitUtils.WaitId(Driver, "ProductHeader", 120);
        }

        public ProductDetails ClickNext()
        {
            elements["next"].Click();
            return this;
        }

        public ProductDetails SelectCreateProductOption(int n)
        {
            if (n > 0)
            {
                if (n <= 5)
                    elements["productCreate-5"].Click();
                else if (n <= 10)
                    elements["productCreate-10"].Click();
                else
                    elements["productCreate-ulm"].Click();
            }
            return this;
        }

        public ProductDetails SelectAccessProductOption(int n)
        {
            if (n > 0)
            {
                if (n <= 1)
                    elements["productAccess-1"].Click();
                else if (n <= 5)
                    elements["productAccess-5"].Click();
                else if (n <= 10)
                    elements["productAccess-10"].Click();
                else
                    elements["productAccess-ulm"].Click();
            }
            return this;
        }

        public ProductDetails SelectCreateLocationOption(int n)
        {
            if (n > 0)
            {
                if (n <= 5)
                    elements["locationCreate-5"].Click();
                else if (n <= 10)
                    elements["locationCreate-10"].Click();
                else
                    elements["locationCreate-ulm"].Click();
            }
            return this;
        }

        public ProductDetails SelectAccessLocationOption(int n)
        {
            if (n > 0)
            {
                if (n <= 1)
                    elements["locationAccess-1"].Click();
                else if (n <= 5)
                    elements["locationAccess-5"].Click();
                else if (n <= 10)
                    elements["locationAccess-10"].Click();
                else
                    elements["locationAccess-ulm"].Click();
            }
            return this;
        }

        public ProductDetails SelectAccessCompanyOption(int n)
        {
            if (n > 0)
            {
                if (n <= 1)
                    elements["companyAccess-1"].Click();
                else if (n <= 5)
                    elements["companyAccess-5"].Click();
                else if (n <= 10)
                    elements["companyAccess-10"].Click();
                else
                    elements["companyAccess-ulm"].Click();
            }
            return this;
        }
    }
}
