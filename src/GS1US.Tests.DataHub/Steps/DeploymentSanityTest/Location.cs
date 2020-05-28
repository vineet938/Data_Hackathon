using GS1US.Tests.Common.Pages.DataHub;
using GS1US.Tests.DataHub.Setup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using static GS1US.Tests.Common.Utils.JsUtils;
using static GS1US.Tests.Common.Utils.RandUtils;
using static GS1US.Tests.Common.Utils.PollUtils;
using static GS1US.Tests.Common.Utils.WaitUtils;
using static GS1US.Tests.Common.Utils.TableUtils;
using static GS1US.Tests.Common.Utils.FileUtils;
using static GS1US.Tests.Common.Utils.StringUtils;
using Shouldly;
using OfficeOpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.Text.RegularExpressions;
using System.Threading;
using Table = TechTalk.SpecFlow.Table;
using Setup = GS1US.Tests.DataHub.Setup;

namespace GS1US.Tests.DataHub.Steps.DeploymentSanityTest
{
    [Binding]
    class Location
    {
        [Given(@"location (.*)")]
        public void GivenLocation(string key, Table table)
        {
            var gln = table.Get("GLN");
            var name = table.Get("Name");
            FeatureContext.Current.Add(key, new Setup.Location(gln, name));
        }

        [Given(@"message (.*)")]
        public void GivenMessage(string key, Table table)
        {
            var topic = table.Get("topic");
            var sig = table.Get("signature");
            FeatureContext.Current.Add(key, new Setup.Message(topic, sig));
        }

        [When(@"do a location import and check imported location count")]
        public void WhenDoALocationImportAndCheckImportedLocationCount()
        {
            new MainPage(TestSetup.Driver)
                .SelectImportLocation();

            var fileId = $"Location-{RandomString(12)}";
            Console.WriteLine($"uploaded file ID: {fileId}");

            var p = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "Assets", "LocationCreate.xlsx");

            var p2 = Path.Combine(TestSetup.Config.DownloadFolder, $"{fileId}.xlsx");

            File.Copy(p, p2);

            new LocationImport(TestSetup.Driver)
                .UploadFile(p2);

            Poll("Wait for file to be uploaded", 240, 5,
                () =>
                {
                    var page = new LocationImport(TestSetup.Driver).Refresh();
                    var name = page.UploadTableFirstFileName;
                    var status = page.UploadTableFirstStatus;
                    return name.StartsWith(fileId + "_") && status == "Complete";
                },
                x => x
            ).ShouldBeTrue();

            var importPage = new ProductImport(TestSetup.Driver);
            importPage.UploadTableFirstProcessed.ShouldBe(2);
            importPage.UploadTableFirstSuccess.ShouldBe(2);
        }

        [When(@"export location hierarchy and verify")]
        public void WhenExportLocationHierarchyAndVerify()
        {
            new MainPage(TestSetup.Driver)
                .SelectManageLocation();

            var managePage = new LocationManage(TestSetup.Driver);

            var tlGln = managePage.TopLevelGln;

            managePage
                .SearchGLn(tlGln)
                .ClickFirstLocation();

            new LocationDetails(TestSetup.Driver)
                .ClickHierarchyTab();

            var locTreePage = new LocationHierarchy(TestSetup.Driver)
                .ExpandTree();

            var locs = locTreePage.ReadTree()
                .ToDictionary(x => x.GLN.Trim(), x => x.Name.Trim());

            locTreePage.ClickExportHierarchy();

            new PopupLocHierarchyExport(TestSetup.Driver)
                .SelectAllAncestorsAndAllDescendants()
                .ClickCustomExport();

            new PopupLocHierarchyExportSpec(TestSetup.Driver)
                .SelectFileType("Excel")
                .ClickExport();

            new MainPage(TestSetup.Driver)
                .ClickExportCenterIcon();

            var exportCenter = Poll(
                "Wait for export to become available", 60, 5,
                () => new ExportCenter(TestSetup.Driver).RefreshPage(),
                p => p.LatestItemStatus == "Complete");

            var filename = exportCenter.LatestItemFileName;

            exportCenter.DownloadLatestItem();

            var path = Path.Combine(TestSetup.Config.DownloadFolder, filename);
            Wait(TestSetup.Driver, 60, d => File.Exists(path)).ShouldBeTrue();

            using (var excel = new ExcelPackage(new FileInfo(path)))
            {
                var ws = excel.Workbook.Worksheets["ExportLocationHierarchy"];
                int count = 0;
                for (var i=1; i <= ws.Dimension.Rows; ++i)
                {
                    var gln = ws.Cells[i, 1].Text;
                    var name = ws.Cells[i, 2].Text;
                    if (locs.ContainsKey(gln))
                    {
                        locs[gln].Norm().ShouldBe(name.Norm());
                        locs.Remove(gln);
                        count += 1;
                    }
                }
                locs.Count.ShouldBe(0);
                count.ShouldBePositive();
            }
        }

        [When(@"download all healthcare locations and verify")]
        public void WhenDownloadAllHealthcareLocationsAndVerify()
        {
            new MainPage(TestSetup.Driver)
                .SelectDownloadLocation();

            var downloadPage = new LocationDownload(TestSetup.Driver);
            var n = downloadPage.DownloadSize;
            var created = downloadPage.DateCreated;

            (DateTime.Now - created).Days.ShouldBe(0);

            downloadPage
                .ClickDownload()
                .WaitSpinner();

            var path = Path.Combine(TestSetup.Config.DownloadFolder, "Healthcare_Locations_All.xlsx");

            Wait(TestSetup.Driver, 300, d => File.Exists(path)).ShouldBeTrue();

            Math.Abs(new FileInfo(path).Length - n).ShouldBeLessThan(100000);

            using (var excel = SpreadsheetDocument.Open(path, false))
            {
                var part = excel.WorkbookPart.WorksheetParts.First();
                using (var reader = OpenXmlReader.Create(part))
                {
                    int count = 0;
                    while (reader.Read())
                    {
                        if (reader.ElementType == typeof(Row))
                        {
                            count += 1;
                        }
                    }
                    count.ShouldBeGreaterThan(60000); // reflects the current list size
                }
            }
        }

        [When(@"open location view/use page")]
        public void WhenOpenLocationViewUsePage()
        {
            new MainPage(TestSetup.Driver)
                .SelectViewUseLocation();
        }

        [When(@"clear filter from current View/Use")]
        public void WhenClearFilterFromCurrentViewUse()
        {
            new LocationViewUse(TestSetup.Driver)
                .ClickClear();
        }

        [When(@"check that MyLocations is checked")]
        public void WhenCheckThatMyLocationsIsChecked()
        {
            new LocationViewUse(TestSetup.Driver)
                .IsMyLocationsChecked
                .ShouldBeTrue();
        }

        [When(@"uncheck MyLocations from current location view/use page")]
        public void WhenUncheckMyLocationsFromCurrentLocationViewUsePage()
        {
            var p = new LocationViewUse(TestSetup.Driver);
            if (p.IsMyLocationsChecked)
            {
                p.ClickMyLocations();
            }
        }

        [When(@"search (.*) industry from current view/use page")]
        public void WhenSearchIndustryFromCurrentViewUsePage(string industry)
        {
            new LocationViewUse(TestSetup.Driver)
                .SelectIndustry(industry)
                .ClickSearch2();
        }

        [When(@"export locations from current View/Use (with|without) MyLocations with fields: (.*)")]
        public void WhenExportLocationsFromViewUseWithMyLocationAndFields(
            string myLocCheck, string fieldsString)
        {
            WhenSearchIndustryFromCurrentViewUsePage("Foodservice");

            var viewPage = new LocationViewUse(TestSetup.Driver);

            var tableSize = viewPage.TableSize;

            viewPage
                .ClickExport();

            var exportPage = new PopupLocExportSpec(TestSetup.Driver)
                .SelectFileType("Excel");

            var selectedFields = new HashSet<string>();
            Regex.Split(fieldsString.Trim(), @"[ ,]+").Where(x => x != "").ToList().ForEach(f =>
            {
                switch (f)
                {
                    case "Status":
                        selectedFields.Add("Status");
                        exportPage.ClickStatusCheckbox();
                        break;

                    case "Parent GLN":
                        selectedFields.Add("ParentGLN");
                        exportPage.ClickParentGlnCheckbox();
                        break;

                    default:
                        throw new ArgumentException($"Unknown export field: {f}");
                }
            });

            exportPage
                .ClickExport();

            new MainPage(TestSetup.Driver)
                .ClickExportCenterIcon();

            var exportCenter = Poll(
                "Wait for export to become available", 60, 5,
                () => new ExportCenter(TestSetup.Driver).RefreshPage(),
                p => p.LatestItemStatus == "Complete");

            var filename = exportCenter.LatestItemFileName;

            exportCenter.DownloadLatestItem();

            var path = Path.Combine(TestSetup.Config.DownloadFolder, filename);
            Wait(TestSetup.Driver, 60, d => File.Exists(path)).ShouldBeTrue();

            using (var excel = new ExcelPackage(new FileInfo(path)))
            {
                var ws = excel.Workbook.Worksheets["ExportSharedLocation"];
                ws.Dimension.Rows.ShouldBe(tableSize + 1);

                var h = new HashSet<string>();
                for (var i=1; i <= ws.Dimension.Columns; ++i)
                {
                    var colName = ws.Cells[1, i].Value as string;
                    selectedFields.Remove(colName);
                    h.Add(colName);
                }

                selectedFields.ShouldBeEmpty();

                if (myLocCheck == "with")
                {
                    h.ShouldContain("MyLocations");
                }
                else if (myLocCheck == "without")
                {
                    h.ShouldNotContain("MyLocations");
                }
            }
        }

        [When(@"export locations from current View/Use and verify")]
        public void WhenExportLocationsFromCurrentViewUseAndVerify()
        {
            WhenExportLocationsFromViewUseWithMyLocationAndFields("noCheck", "");
        }

        [When(@"export locations from current View/Use (with|without) MyLocations")]
        public void WhenExportLocationsFromViewUseWithMyLocations(string myLocCheck)
        {
            WhenExportLocationsFromViewUseWithMyLocationAndFields(myLocCheck, "");
        }

        [When(@"export locations from current View/Use with fields: (.*)")]
        public void WhenExportLocationsFromViewUseWithFields(string fieldsString)
        {
            WhenExportLocationsFromViewUseWithMyLocationAndFields("with", fieldsString);
        }

        [When(@"create a location (.*)")]
        public void WhenCreateALocation(string locId, Table table)
        {
            new MainPage(TestSetup.Driver)
                .SelectCreateLocation();

            var locName = $"Location-{RandomString(20)}";

            new LocationCreate(TestSetup.Driver)
                .EnterLocationName(locName)
                .ClickSetParentButton();

            new PopupParentGlnSelection(TestSetup.Driver)
                .SearchName(table.Get("parent"))
                .ClickFirstRow()
                .ClickSetParentGln();

            Thread.Sleep(3000);

            new LocationCreate(TestSetup.Driver)
                .SelectIndustry(table.Get("industry"));

            Thread.Sleep(1000);

            new LocationCreate(TestSetup.Driver)
                .SelectSupplyChainRole(table.Get("sc_role"))
                .EnterAddress1(table.Get("address1"))
                .EnterAddress2($"Suite {RandomNumber(8)}")
                .EnterCity(table.Get("city"))
                .SelectState(table.Get("state"))
                .EnterZip(table.Get("zip"))
                .EnterPhone(table.Get("phone"))
                .ClickSave()
                .WaitSpinner();

            new LocationCreate(TestSetup.Driver)
                .ExpandBusinessAttributesLocationType()
                .ExpandSectorCorporateRelationship();

            Regex.Split(table.Get("location_type").Trim(), @"\s*,\s*")
                .ToList()
                .ForEach(x => new LocationCreate(TestSetup.Driver).ClickLocationType(x));

            new LocationCreate(TestSetup.Driver)
                .SelectHcCorporateRelationship(table.Get("corporate_rel"));

            Thread.Sleep(1000);

            new LocationCreate(TestSetup.Driver)
                .SelectClassOfTrade1(table.Get("class_of_trade1"));

            Thread.Sleep(1000);

            new LocationCreate(TestSetup.Driver)
                .SelectClassOfTrade2(table.Get("class_of_trade2"));

            Thread.Sleep(1000);

            new LocationCreate(TestSetup.Driver)
                .SelectClassOfTrade3(table.Get("class_of_trade3"))
                .ClickSave()
                .WaitSpinner();

            new LocationCreate(TestSetup.Driver)
                .ClickMakeActive();

            new PopupLocationMakeActive(TestSetup.Driver)
                .ClickContinue();

            Thread.Sleep(1000);

            new LocationCreate(TestSetup.Driver)
                .WaitSpinner();

            Thread.Sleep(1000);

            var p = new LocationCreate(TestSetup.Driver)
                .ClickApprove()
                .WaitSpinner();

            Console.WriteLine($"GLN = {p.GLN}  LocName = {locName}");

            FeatureContext.Current.Add(locId, new Setup.Location(p.GLN, locName));
        }

        [When(@"draft new healthcare/provider location (.*) with parent (.*)")]
        public void WhenDraftNewHealthcareProviderLocation(string locId, string parentName)
        {
            new MainPage(TestSetup.Driver)
                .SelectCreateLocation();

            var locName = $"Location-{RandomString(20)}";

            new LocationCreate(TestSetup.Driver)
                .EnterLocationName(locName)
                .ClickSetParentButton();

            new PopupParentGlnSelection(TestSetup.Driver)
                .SearchName(parentName)
                .ClickFirstRow()
                .ClickSetParentGln();

            new LocationCreate(TestSetup.Driver)
                .ClickSave();
        }

        [When(@"share location (.*) with (.*)")]
        public void WhenShareLocationLWithCompany(string key, string companyName)
        {
            new MainPage(TestSetup.Driver)
                .SelectManageLocation();

            var loc = FeatureContext.Current.Get<Setup.Location>(key);

            new LocationManage(TestSetup.Driver)
                .SearchGLn(loc.GLN)
                .ClickFirstLocation();

            new LocationDetails(TestSetup.Driver)
                .ClickSharingTab();

            new LocationSharing(TestSetup.Driver)
                .ClickAddNew();

            new LocationAddNewShare(TestSetup.Driver)
                .SearchCompany(companyName)
                .ClickFirstRow()
                .ClickAddNew();

            new PopupLocationAddConfirm(TestSetup.Driver)
                .ClickContinue();
        }

        [When(@"view location (.*)")]
        public void WhenViewLocation(string key)
        {
            new MainPage(TestSetup.Driver)
                .SelectViewUseLocation();

            var loc = FeatureContext.Current.Get<Setup.Location>(key);

            new LocationViewUse(TestSetup.Driver)
                .SearchGLN(loc.GLN)
                .ClickSearch2()
                .FirstGln
                .ShouldBe(loc.GLN);
        }

        [When(@"search location (.*) from GLN index")]
        public void WhenSearchLocationFromGLNIndex(string key)
        {
            new MainPage(TestSetup.Driver)
                .SelectLocationGlnIndex();

            var loc = FeatureContext.Current.Get<Setup.Location>(key);

            new LocationGlnSearch(TestSetup.Driver)
                .EnterName(loc.Name)
                .ClickSearch()
                .WaitSpinner();
        }

        [When(@"send message (.*) via shared location (.*)")]
        public void WhenMessageCompanyViaSharedLocation(string messageKey, string locationKey)
        {
            new MainPage(TestSetup.Driver)
                .SelectViewUseLocation();

            var loc = FeatureContext.Current.Get<Setup.Location>(locationKey);

            new LocationViewUse(TestSetup.Driver)
                .SearchGLN(loc.GLN)
                .ClickSearch2()
                .ClickFirstLink();

            new LocationDetails(TestSetup.Driver)
                .ClickSendMessage();

            var topic = $"Topic {RandomString(32)}";
            var sig = RandomString(64);
            var message = $"Message\n\n{sig}";

            Console.WriteLine($"Topic={topic}  Signature={sig}");

            FeatureContext.Current.Add(messageKey, new Setup.Message(topic, sig));

            new PopupSendMessage(TestSetup.Driver)
                .SetTopic(topic)
                .SetMessage(message)
                .ClickSend();
        }

        [When(@"check message (.*) in inbox")]
        public void WhenCheckMessageInInbox(string messageKey)
        {
            new MainPage(TestSetup.Driver)
                .ClickMessageCenterIcon();

            var m = FeatureContext.Current.Get<Setup.Message>(messageKey);

            new MessageCenter(TestSetup.Driver)
                .EnterTopic(m.Topic);

            Poll("Wait for filtering to finish", 60, 5,
                () => new MessageCenter(TestSetup.Driver).TableSize,
                x => x == 1
            );

            new MessageCenter(TestSetup.Driver)
                .ClickFirstLink();

            var popup = new PopupMessage(TestSetup.Driver);

            popup.Topic.ShouldBe(m.Topic);
            popup.Message.ShouldContain(m.Signature);

            popup.Dismiss();
        }

        [When(@"run location share report for (.*)")]
        public void WhenRunLocationShareReportFor(string locationKey)
        {
            new MainPage(TestSetup.Driver)
                .SelectCreateReports();

            var loc = FeatureContext.Current.Get<Setup.Location>(locationKey);

            new ReportsCreate(TestSetup.Driver)
                .ClickLocationShare()
                .EnterGlnForLocationShare(loc.GLN)
                .ClickOK();

            var files = Poll("Wait for download to complete", 60, 5,
                () => FindFiles(TestSetup.Config.DownloadFolder, f =>
                    Path.GetFileName(f).StartsWith("LocationShare Report ") && f.EndsWith(".xls")),
                xs => xs.Any());

            var workbook = ExcelLibrary.SpreadSheet.Workbook.Load(files.First());
            var sheet = workbook.Worksheets[0];
            var gln = sheet.Cells[3, 0].Value as string;
            gln.ShouldBe(loc.GLN);
        }

        [When(@"make active location (.*) to inactive")]
        public void WhenMakeActiveLocationLToInactive(string locationKey)
        {
            new MainPage(TestSetup.Driver)
                .SelectManageLocation();

            var loc = FeatureContext.Current.Get<Setup.Location>(locationKey);

            new LocationManage(TestSetup.Driver)
                .SearchGLn(loc.GLN);

            Thread.Sleep(1000);

            new LocationManage(TestSetup.Driver)
                .ClickFirstLocation();

            new LocationDetails(TestSetup.Driver)
                .ClickMakeInactive();

            new PopupConfirmMakeInactive(TestSetup.Driver)
                .ClickContinue()
                .WaitSpinner();

            new LocationDetails(TestSetup.Driver)
                .ClickApprove()
                .WaitSpinner();
        }

    }
}
