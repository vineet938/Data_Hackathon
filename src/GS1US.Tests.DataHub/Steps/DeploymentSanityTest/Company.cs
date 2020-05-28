using GS1US.Tests.Common.Pages.DataHub;
using GS1US.Tests.DataHub.Setup;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using static GS1US.Tests.Common.Utils.RandUtils;
using static GS1US.Tests.Common.Utils.PollUtils;
using static GS1US.Tests.Common.Utils.FileUtils;
using OfficeOpenXml;

namespace GS1US.Tests.DataHub.Steps.DeploymentSanityTest
{
    [Binding]
    class Company
    {
        [When(@"verify prefix search result")]
        public void WhenVerifyPrefixSearchResult(Table table)
        {
            new MainPage(TestSetup.Driver)
                .SelectCompanyViewUse();

            new CompanySearchCompany(TestSetup.Driver)
                .ClickPrefixTab();

            var search = table.CreateInstance<CompanySearch>();

            var companyName = new CompanySearchPrefix(TestSetup.Driver)
                .EnterPrefix(search.Key)
                .ClickSearch()
                .FirstCompanyName;

            companyName.ShouldStartWith(search.CompanyName);
        }

        [When(@"verify GTIN search result")]
        public void WhenVerifyGTINSearchResult(Table table)
        {
            new MainPage(TestSetup.Driver)
                .SelectCompanyViewUse();

            new CompanySearchCompany(TestSetup.Driver)
                .ClickGtinTab();

            var search = table.CreateInstance<CompanySearch>();

            var companyName = new CompanySearchGtin(TestSetup.Driver)
                .EnterGtin(search.Key)
                .ClickSearch()
                .FirstGtin;

            companyName.ShouldStartWith(search.CompanyName);
        }

        [When(@"verify GLN search result")]
        public void WhenVerityGLNSearchResult(Table table)
        {
            new MainPage(TestSetup.Driver)
                .SelectCompanyViewUse();

            new CompanySearchCompany(TestSetup.Driver)
                .ClickGlnTab();

            var search = table.CreateInstance<CompanySearch>();

            var companyName = new CompanySearchGln(TestSetup.Driver)
                .EnterGln(search.Key)
                .ClickSearch()
                .FirstGln;

            companyName.ShouldStartWith(search.CompanyName);
        }

        [When(@"download and verify active US prefix list")]
        public void WhenDownloadAndVerifyActiveUSPrefixList()
        {
            new MainPage(TestSetup.Driver)
                .SelectCompanyDownload();

            var (size, unit) = new CompanyDownload(TestSetup.Driver)
                .ActiveListDownloadSize;

            new CompanyDownload(TestSetup.Driver)
                .ClickDownloadActiveList();

            var files = Poll("Wait for file to be downloaded", 600, 20,
                () => FindFiles(TestSetup.Config.DownloadFolder, x => x.EndsWith(@"\PrefixListActive.csv")),
                xs => xs.Any()
            );

            files.ShouldHaveSingleItem();

            var n = new FileInfo(files.First()).Length;
            double s2;

            if (unit == "MB")
            {
                s2 = n / 1024.0 / 1024.0;
            }
            else if (unit == "KB")
            {
                s2 = n / 1024.0;
            }
            else
            {
                Console.WriteLine($"Unknown unit: {unit}");
                s2 = (double)(n);
            }

            Math.Abs(s2 - size).ShouldBeLessThan(0.5);
        }

        [When(@"in List Match, check GTIN list upload works")]
        public void WhenInListMatchCheckGTINListUploadWorks()
        {
            new MainPage(TestSetup.Driver)
                .SelectCompanyListMatch();

            var fileId = $"ListMatch-GTIN-{RandomString(12)}";
            Console.WriteLine($"uploaded file ID: {fileId}");

            var p = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "Assets", "ListMatchTestFile.xlsx");

            var p2 = Path.Combine(TestSetup.Config.DownloadFolder, $"{fileId}.xlsx");

            File.Copy(p, p2);

            new CompanyListMatch(TestSetup.Driver)
                .SelectIdentifierType("GTIN/U.P.C.")
                .UploadFile(p2)
                .ClickSubmit();


            Poll("Wait for file to be uploaded", 180, 5,
                () => new CompanyListMatch(TestSetup.Driver)
                    .ClickRefresh()
                    .SearchFile(fileId)
                    .FirstStatus,
                x => x == "Complete"
            );

            new CompanyListMatch(TestSetup.Driver)
                .FirstFileName
                .ShouldStartWith(fileId);

            new CompanyListMatch(TestSetup.Driver)
                .ClickFirstName();

            var files = Poll("Wait for file to be downloaded", 60, 5,
                () => FindFiles(TestSetup.Config.DownloadFolder, x => x.EndsWith(@"\List Match Results.xlsx")),
                xs => xs.Any()
            );

            // verify that there are at least some results in the file
            using (var excel = new ExcelPackage(new FileInfo(files.First())))
            {
                var ws = excel.Workbook.Worksheets["Results"];
                int count = 0;
                for (var i=1; i <= ws.Dimension.Rows; ++i)
                {
                    if (!String.IsNullOrWhiteSpace(ws.Cells[i, 2].Value as string))
                    {
                        count += 1;
                    }
                }
                Console.WriteLine(count);
                count.ShouldBeGreaterThan(1);
            }
        }

    }

}
