using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class LocationTabs<T> : PagesCommon<T> where T : LocationTabs<T>
    {
        public LocationTabs(IWebDriver driver) : base(driver)
        {
        }

        protected override Dictionary<string, By> Locators => new Dictionary<string, By>
        {
            {"Details", By.XPath("//ul[@id='productDetailTabs']//a[text()='Details']") },
            {"Hierarchy", By.XPath("//ul[@id='productDetailTabs']//a[text()='Hierarchy']") },
            {"AssignUsers", By.XPath("//ul[@id='productDetailTabs']//a[text()='Assign Users']") },
            {"Sharing", By.XPath("//ul[@id='productDetailTabs']//a[text()='Sharing']") },
            {"History", By.XPath("//ul[@id='productDetailTabs']//a[text()='History']") },
            {"ApprovalHistory", By.XPath("//ul[@id='productDetailTabs']//a[text()='Approval History']") }
        };

        private T ClickTab(string name)
        {
            elements[name].Click();
            return (T)this;
        }

        public T ClickDetailsTab() => ClickTab("Details");
        public T ClickHierarchyTab() => ClickTab("Hierarchy");
        public T ClickAssignUsersTab() => ClickTab("AssignUsers");
        public T ClickSharingTab() => ClickTab("Sharing");
        public T ClickHistoryTab() => ClickTab("History");
        public T ClickApprovalHistoryTab() => ClickTab("ApprovalHistory");

    }
}
