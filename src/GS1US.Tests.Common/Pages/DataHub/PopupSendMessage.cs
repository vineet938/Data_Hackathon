using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class PopupSendMessage : Popup<PopupSendMessage>
    {
        public PopupSendMessage(IWebDriver driver) : base(driver)
        {
        }

        protected override By DismissButton => By.Id("btnCancelMessage");

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>(base.Locators)
        {
            {"Topic", By.CssSelector("#sendMessageForm input") },
            {"Message", By.Id("messageContents") },
            {"Send", By.Id("btnSendMessage") }
        };

        public PopupSendMessage SetTopic(string topic)
        {
            elements["Topic"].SetText(topic);
            return this;
        }

        public PopupSendMessage SetMessage(string message)
        {
            elements["Message"].SetText(message);
            return this;
        }

        public PopupSendMessage ClickSend()
        {
            elements["Send"].Click();
            return this;
        }
    }
}
