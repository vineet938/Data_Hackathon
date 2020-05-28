using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace GS1US.Tests.Common.Utils
{
    public static class ScreenshotUtils
    {
        public static string MakeFileName()
        {
            var prefix = ScenarioContext.Current.ScenarioInfo.Title.Replace(" ", "_");
            var date = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            var uuid = Guid.NewGuid().ToString();
            return $"{prefix}-{date}-{uuid}.png";
        }

        public static string MakeFilePath() =>
            MakeFilePath(Directory.GetCurrentDirectory());

        public static string MakeFilePath(string folder) =>
            Path.Combine(folder, MakeFileName());

        public static void TakeScreenshot(this IWebDriver driver, string path)
        {
            var screenshot = (driver as ITakesScreenshot).GetScreenshot();
            screenshot.SaveAsFile(path);
            Console.WriteLine($"Screenshot: file:///{path}");
        }

        public static void TakeScreenshot(this IWebDriver driver) =>
            TakeScreenshot(driver, MakeFilePath());
    }
}
