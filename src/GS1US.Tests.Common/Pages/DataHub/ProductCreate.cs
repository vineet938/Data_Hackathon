using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class ProductCreate : PagesCommon<ProductCreate>
    {
        private readonly ProductDetails details;

        public ProductCreate(IWebDriver driver) : base(driver)
        {
            details = new ProductDetails(driver);
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"ProductDescription", By.Id("txtProductDescription") },
            {"BrandName", By.Id("txtBrandName") },
            {"Industry", By.Id("ddlIndustry") },
            {"PackagingLevel", By.Id("ddlPackagingLevel") },
            {"Save", By.Id("btnSave") },
            {"Cancel", By.Id("btnCancel") },
            {"AutoAssignGTIN", By.Id("btnAutoAssign") },
            {"Status", By.Id("ddlStatus") },
            {"GTIN", By.Id("txtGTIN") },
            {"Spinner", By.CssSelector("[name='chopper']") },
            {"AddContents", By.Id("btnAddContents")}
        };

        public ProductCreate SetProductDescription(string s)
        {
            elements["ProductDescription"].SetText(s);
            return this;
        }

        public ProductCreate SetBrandName(string s)
        {
            elements["BrandName"].SetText(s);
            return this;
        }

        public ProductCreate SelectIndustry(string s)
        {
            elements["Industry"].Select(s);
            return this;
        }

        public ProductCreate SelectPackagingLevel(string s)
        {
            Apply("PackagingLevel", 5, 1, x => x.Select(s));
            return this;
        }

        public ProductCreate Save()
        {
            Apply("Save", 15, 1, Click);
            return this;
        }

        public ProductCreate Cancel()
        {
            var el = Driver.FindElement(By.Id("btnCancel"));
            var actions = new Actions(Driver);
            actions.MoveToElement(el);
            actions.Click();
            actions.Perform();
            //Apply("Cancel", 5, 1, Click);
            return this;
        }

        public ProductCreate AutoAssignGtin()
        {
            Apply("AutoAssignGTIN", 30, 1, Click);
            return this;
        }

        public ProductCreate WaitSpinner()
        {
            WaitToDisappear(Driver, Locators["Spinner"], 180);
            return this;
        }

        public ProductCreate ClickAddContentsButton()
        {
            Apply("AddContents", 5, 1, Click);
            return this;
        }
    }
}
