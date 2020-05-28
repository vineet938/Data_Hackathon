using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS1US.Tests.Common.Utils;
using OpenQA.Selenium;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class PopupAddGtinToContainer : Popup<PopupAddGtinToContainer>
    {
        public PopupAddGtinToContainer(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>(base.Locators)
        {
            {"Spinner", By.Id("dtResource_processing")},
            {"Description", By.Id("dtResourceDescription2")},
            {"Add", By.Id("btnAddToContainer")}
        };

        protected override By DismissButton => By.Id("btnCloseAddContents");

        public PopupAddGtinToContainer ClickAddButton()
        {
            Apply("Add", 5, 1, Click);
            return this;
        }

        public PopupAddGtinToContainer FilterByDescription(string description)
        {
            Apply("Description", 5, 1, SetText(description));
            return this;
        }

        public PopupAddGtinToContainer SelectElementWithText(string s)
        {
            Apply(By.XPath($"//*[text()=\"{s}\"]"), 5, 1, Click);
            return this;
        }

        public PopupAddGtinToContainer WaitSpinner()
        {
            WaitUtils.WaitToDisappear(Driver, Locators["Spinner"], 10);
            return this;
        }
    }
}
