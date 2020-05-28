using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static GS1US.Tests.Common.Utils.WaitUtils;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class MessageCenter : PagesCommon<MessageCenter>
    {
        public MessageCenter(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"Topic", By.Id("dtMessageListTopic3") },
            {"FirstLink", By.CssSelector("#dtMessageList tbody tr:first-child td a") }
        };

        public MessageCenter EnterTopic(string topic)
        {
            Apply("Topic", 15, 1, SetText(topic));
            return this;
        }

        public MessageCenter ClickFirstLink()
        {
            Apply("FirstLink", 15, 1, Click);
            return this;
        }

        public int TableSize => Driver.FindElements(By.CssSelector("#dtMessageList tbody tr")).Count;
    }
}
