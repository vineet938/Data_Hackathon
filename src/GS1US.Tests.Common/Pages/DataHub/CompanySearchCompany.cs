using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace GS1US.Tests.Common.Pages.DataHub
{
    public class CompanySearchCompany : CompanyTabs<CompanySearchCompany>
    {
        public CompanySearchCompany(IWebDriver driver) : base(driver)
        {
        }
    }
}
