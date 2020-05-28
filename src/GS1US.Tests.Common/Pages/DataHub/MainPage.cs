using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class MainPage : PagesCommon<MainPage>
    {
        public MainPage(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"ProductMenu", By.CssSelector("#product a") },
            {"Product-Create", By.CssSelector("#addnewproduct a") },
            {"Product-Manage", By.XPath("(//*[@id='home']/a)[2]") },
            {"Product-Share", By.CssSelector("#share a") },
            {"Product-ViewUse", By.CssSelector("#viewuse a") },
            {"Product-Import", By.CssSelector("#import a") },
            {"LocationMenu", By.Id("location") },
            {"Location-Create", By.CssSelector("#detail a") },
            {"Location-Manage", By.XPath("(//*[@id='home']/a)[2]") },
            {"Location-Import", By.CssSelector("#import a") },
            {"Location-ViewUse", By.CssSelector("#viewuse a") },
            {"Location-Download", By.CssSelector("#download a") },
            {"Location-GLN Index", By.CssSelector("#glnindex a") },
            {"CompanyMenu", By.Id("company") },
            {"Company-ViewUse", By.CssSelector("#viewuse a") },
            {"Company-Download", By.CssSelector("#download a") },
            {"Company-ListMatch", By.CssSelector("#listmatch a") },
            {"ReportsMenu", By.Id("reports") },
            {"Reports-Create", By.CssSelector("#create a") },
            {"ExportCenter", By.XPath("//li[@title='Export Center']") },
            {"MessageCenter", By.XPath("//li[@title='Read Messages']") },
            {"AdminMenu", By.CssSelector("#admin a") },
            {"Admin-ProductSettings", By.XPath("//*[text()='Manage product settings.']/following-sibling::button") },
            {"Admin-CompanySettings", By.XPath("//*[text()='Manage company settings.']/following-sibling::button") },
            {"AccountId", By.XPath("//*[contains(text(),'Account No.')]/following-sibling::*") }
        };

        protected override void WaitForPage()
        {
            WaitLocators(Driver, 30, Locators.Values.ToArray());
        }

        private MainPage SelectMenu(string primaryMenu, string secondaryMenu)
        {
            Apply($"{primaryMenu}Menu", 60, 1, Click);
            Apply($"{primaryMenu}-{secondaryMenu}", 60, 1, Click);
            return this;
        }

        public MainPage SelectCreateProduct() => SelectMenu("Product", "Create");

        public MainPage SelectManageProduct() => SelectMenu("Product", "Manage");

        public MainPage SelectShareProduct() => SelectMenu("Product", "Share");

        public MainPage SelectViewUseProduct() => SelectMenu("Product", "ViewUse");

        public MainPage SelectImportProduct() => SelectMenu("Product", "Import");

        public MainPage SelectCreateLocation() => SelectMenu("Location", "Create");

        public MainPage SelectImportLocation() => SelectMenu("Location", "Import");

        public MainPage SelectManageLocation() => SelectMenu("Location", "Manage");

        public MainPage SelectViewUseLocation() => SelectMenu("Location", "ViewUse");

        public MainPage SelectDownloadLocation() => SelectMenu("Location", "Download");

        public MainPage SelectLocationGlnIndex() => SelectMenu("Location", "GLN Index");

        public MainPage SelectCompanyViewUse() => SelectMenu("Company", "ViewUse");

        public MainPage SelectCompanyDownload() => SelectMenu("Company", "Download");

        public MainPage SelectCompanyListMatch() => SelectMenu("Company", "ListMatch");

        public MainPage SelectCreateReports() => SelectMenu("Reports", "Create");

        public MainPage SelectAdminProductSettings() => SelectMenu("Admin", "ProductSettings");

        public MainPage SelectAdminCompanySettings() => SelectMenu("Admin", "CompanySettings");

        public MainPage ClickExportCenterIcon()
        {
            Apply("ExportCenter", 15, 1, Click);
            return this;
        }

        public MainPage ClickMessageCenterIcon()
        {
            Apply("MessageCenter", 15, 1, Click);
            return this;
        }

        public string GetAccountId()
        {
            return elements["AccountId"].Text.Trim();
        }
    }
}
