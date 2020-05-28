using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class PopupMessage : Popup<PopupMessage>
    {
        public PopupMessage(IWebDriver driver) : base(driver)
        {
        }

        protected override By DismissButton => By.XPath("(//button[text()='Return to Message Center'])[2]");

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>(base.Locators)
        {
            {"Message", By.XPath("//b[text()='Content:']/parent::h5/following-sibling::p") }
        };

        protected override void WaitForPage()
        {
            WaitLocator(Driver, Locators["Message"], 15);
        }

        public string Topic
        {
            get
            {
                var script = "$('#MessageThreadModal').scope().messageThread.MessageTopic.MessageTopic";
                return (string)(Driver as IJavaScriptExecutor).ExecuteScript($"return {script}");
            }
        }

        public string Message => elements["Message"].Text;
    }
}
