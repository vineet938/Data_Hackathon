using System;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;
using GS1US.Tests.DataHub.Setup;
using Shouldly;
using Pages = GS1US.Tests.Common.Pages;
using static GS1US.Tests.Common.Utils.PollUtils;
using static GS1US.Tests.Common.Utils.WaitUtils;
using static GS1US.Tests.Common.Utils.RandUtils;
using static GS1US.Tests.Common.Utils.JsUtils;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using OfficeOpenXml;
using System.Text;

namespace GS1US.Tests.DataHub.Steps
{
    [Binding]
    public class DeploymentSanityTestSteps
    {
        [Given(@"product (.*) with GTIN (.*)")]
        public void GivenProductPWithGTIN(string productKey, string gtin)
        {
            FeatureContext.Current.Add(productKey, new Product(gtin));
        }

        [When(@"login as (.*)")]
        public void WhenLoginAsMedicalDevicesRUs(string companyName)
        {
            var datahubUrl = TestSetup.Config.datahub.url;
            TestSetup.Driver.Navigate().GoToUrl(datahubUrl);

            var page = new Pages.UserPortal.LoginPage(TestSetup.Driver);
            page.Login(TestSetup.Config.datahub.username, TestSetup.Config.datahub.password);

            var page2 = new Pages.UserPortal.CompanyList(TestSetup.Driver);
            page2.SelectCompany(companyName).SignIn();
        }

        [When(@"wait and close Walk Me Through popup")]
        public void WhenWaitAndCloseWalkMeThroughPopup()
        {
            try
            {
                var page = new Pages.DataHub.WalkMeThroughPopup(TestSetup.Driver);
                page.Close();
            }
            catch (WebDriverTimeoutException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Probably WalkMeThrough is turned off. Continuing...");
            }
        }

        [When(@"create a product")]
        public void WhenCreateAProduct()
        {
            new Pages.DataHub.MainPage(TestSetup.Driver)
                .SelectCreateProduct();

            new Pages.DataHub.ProductCreate(TestSetup.Driver)
                .SetProductDescription($"Product-{RandomString(16)}")
                .SetBrandName($"Brand-{RandomString(16)}")
                .Save()
                .AutoAssignGtin();

            new Pages.DataHub.PopupAssignGTIN(TestSetup.Driver).Dismiss();

            Wait(TestSetup.Driver, 30, d =>
            {
                var details = new Pages.DataHub.ProductDetails(TestSetup.Driver);
                return Regex.Match(details.GTIN, @"^\d+$").Success;
            });
                                 
            new Pages.DataHub.ProductDetails(TestSetup.Driver).GTIN.ShouldMatch(@"^\d+$");
        }

        [When(@"create a product: (.*)")]
        public void WhenCreateAProduct(string key)
        {
            WhenCreateAProduct();
            var gtin = new Pages.DataHub.ProductDetails(TestSetup.Driver).GTIN;

            Console.WriteLine($"Product: key={key}  gtin={gtin}");

            var p = new Product(gtin);
            FeatureContext.Current.Add(key, p);
        }

        [When(@"change status of product (.*) to (.*)")]
        public void ChangeStatusOfProductTo(string productKey, string status)
        {
            new Pages.DataHub.MainPage(TestSetup.Driver)
                .SelectManageProduct();

            var p = FeatureContext.Current.Get<Product>(productKey);

            new Pages.DataHub.ProductManage(TestSetup.Driver)
                .FilterByGtin(p.GTIN)
                .SelectFirstItem();

            new Pages.DataHub.ProductDetails(TestSetup.Driver)
                .SelectStatus(status)
                .Save();

            void Handle<T>() where T: Pages.DataHub.Popup<T> =>
                ((T)Activator.CreateInstance(typeof(T), TestSetup.Driver)).Dismiss();

            var handlers = new Dictionary<string, Action>
            {
                {"In Use", () => Handle<Pages.DataHub.PopupStatusChangeInUse>() },
                {"Archived", () => Handle<Pages.DataHub.PopupStatusChangeArchived>() }
            };

            handlers.ShouldContainKey(status);

            handlers[status]();

            new Pages.DataHub.ProductDetails(TestSetup.Driver)
                .WaitSpinner();
        }

        [When(@"share the product (.*) with (.*)")]
        public void WhenShareTheProductWith(string productKey, string companyName)
        {
            new Pages.DataHub.MainPage(TestSetup.Driver)
                .SelectAdminProductSettings();

            var pageProdSettings = new Pages.DataHub.AdminProductSettings(TestSetup.Driver);

            if (pageProdSettings.IsShareAllChecked())
            {
                pageProdSettings
                    .SetShareAll(false)
                    .Save();

                new Pages.DataHub.PopupAddShare(TestSetup.Driver)
                    .Dismiss();
            }

            new Pages.DataHub.MainPage(TestSetup.Driver)
                .SelectShareProduct();

            var p = FeatureContext.Current.Get<Product>(productKey);

            new Pages.DataHub.ProductShare(TestSetup.Driver)
                .AddNew()
                .FilterByGtin(p.GTIN)
                .SelectFirstRow()
                .ClickShare();

            new Pages.DataHub.ProductShareAddNew(TestSetup.Driver)
                .FilterByCompanyName(companyName)
                .SelectFirstRow()
                .ClickContinue();

            new Pages.DataHub.PopupAddShare(TestSetup.Driver)
                .Dismiss();
        }

        [When(@"export products and check exported product count")]
        public void WhenExportProductsAndCheckExportedProductCount()
        {
            new Pages.DataHub.MainPage(TestSetup.Driver)
                .SelectManageProduct();

            var managePage = new Pages.DataHub.ProductManage(TestSetup.Driver);

            var listSize = managePage.ExportListSize;

            managePage.ClickExport();

            new Pages.DataHub.PopupProductExport(TestSetup.Driver)
                .Dismiss();

            new Pages.DataHub.MainPage(TestSetup.Driver)
                .ClickExportCenterIcon();

            var exportCenter = Poll(
                "Poll the status of the latest export", 60, 5,
                () => new Pages.DataHub.ExportCenter(TestSetup.Driver).RefreshPage(),
                p => p.LatestItemStatus == "Complete"
            );

            var filename = exportCenter.LatestItemFileName;

            exportCenter.DownloadLatestItem();

            var path = Path.Combine(TestSetup.Config.DownloadFolder, filename);
            Wait(TestSetup.Driver, 60, d => File.Exists(path)).ShouldBeTrue();

            using (var excel = new ExcelPackage(new FileInfo(path)))
            {
                var n = excel.Workbook.Worksheets["ExportAllProducts"].Dimension.Rows - 1;
                n.ShouldBe(listSize);
            }
        }

        [When(@"view the product (.*) shared by (.*)")]
        public void WhenViewTheProductSharedByMedicalDevicesRUs(string productKey, string companyName)
        {
            new Pages.DataHub.MainPage(TestSetup.Driver)
                .SelectViewUseProduct();

            var p = FeatureContext.Current.Get<Product>(productKey);

            var gtin = new Pages.DataHub.ProductViewUse(TestSetup.Driver)
                .EnterGTIN(p.GTIN)
                .ClickSearch()
                .FirstGTIN;

            gtin.ShouldBe(p.GTIN);
        }

        [When(@"do a product import and check imported product count")]
        public void WhenDoAProductImportAndCheckImportedProductCount()
        {
            new Pages.DataHub.MainPage(TestSetup.Driver)
                .SelectImportProduct();

            new Pages.DataHub.ProductImport(TestSetup.Driver)
                .ClickDownloadAvailableGtinsLink();

            var importPopup = new Pages.DataHub.ProductImportDownloadGtins(TestSetup.Driver);

            int n = RandInt(3, 7);

            Console.WriteLine($"{n} products will be created");

            importPopup
                .SelectPrefixWithCapacity(n)
                .EnterQuantity(n)
                .ClickContinue();

            string GetPath() =>
                Directory.GetFiles(TestSetup.Config.DownloadFolder, "Download_*_AvailableGTINs_*.xlsx")
                    .OrderBy(x => x).Last();

            var path = Poll(
                "Wait for file to be downloaded", 30, 5,
                GetPath,
                x => (DateTime.Now - new FileInfo(x).CreationTime).TotalSeconds < 30);

            using (var excel = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = excel.Workbook.Worksheets[1];
                worksheet.Dimension.Rows.ShouldBe(n + 1);
                foreach (var i in Enumerable.Range(2, n))
                {
                    worksheet.SetValue(i, 4, "Each");
                    worksheet.SetValue(i, 5, $"Product-Import-{RandomString(16)}");
                    worksheet.SetValue(i, 7, $"Brand-Import-{RandomString(16)}");
                    worksheet.SetValue(i, 8, "PreMarket");
                }
                excel.Save();
            }

            var fileId = $"file_{RandomString(12)}";

            var p2 = Path.Combine(TestSetup.Config.DownloadFolder, $"{fileId}.xlsx");

            File.Copy(path, p2);

            new Pages.DataHub.ProductImport(TestSetup.Driver)
                .UploadFile(p2);

            Console.WriteLine($"Uploaded file ID: {fileId}");

            Poll("Wait for file to be uploaded", 60, 5,
                () =>
                {
                    var page = new Pages.DataHub.ProductImport(TestSetup.Driver).Refresh();
                    var name = page.UploadTableFirstFileName;
                    var status = page.UploadTableFirstStatus;
                    return name.StartsWith(fileId + "_") && status == "Complete";
                },
                x => x
            ).ShouldBeTrue();

            var importPage = new Pages.DataHub.ProductImport(TestSetup.Driver);
            importPage.UploadTableFirstProcessed.ShouldBe(n);
            importPage.UploadTableFirstSuccess.ShouldBe(n);
        }

    }
}
