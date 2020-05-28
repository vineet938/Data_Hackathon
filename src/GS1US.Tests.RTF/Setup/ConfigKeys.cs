using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.RTF.Setup
{
    class ConfigKeys
    {
        public const string HEADLESS = "Selenium-Headless";
        public const string IMIS_CONN_STRING = "iMIS-ConnectionString";
        public const string DRIVER_LOC = "Selenium-DriverDirectory";
        public const string DEBUG = "Debug";
        public const string CSA_URL = "PublicAppUrl-PCIA";
        public const string MEMBER_APP_URL = "MemberAppUrl-RequestAdditionalPrefixes";
        public const string DATAHUB_URL = "MemberAppUrl-DatahubSignup";
        public const string UNSPSC_URL = "MemberAppUrl-UNSPSC";
        public const string RENEWAL_URL = "MemberAppUrl-Renewal";
        public const string PAYPAL_USER = "PayPal-Username";
        public const string PAYPAL_PASSWORD = "PayPal-Password";
        public const string TESTACCOUNT_EMAIL = "TestAccount-Email";
        public const string TESTACCOUNT_EMAIL_PASSWORD = "TestAccount-EmailPassword";
        public const string TESTACCOUNT_PORTAL_PASSWORD = "TestAccount-PortalPassword";
        public const string TESTACCOUNT_COMPANY_NAME = "TestAccount-CompanyName";
        public const string TESTACCOUNT_IMAP_HOST = "TestAccount-ImapHost";
        public const string TESTACCOUNT_IMAP_PORT = "TestAccount-ImapPort";
        public const string DB_TIMEOUT = "DbTimeout";
        public const string DB_POLL_INTERVAL = "DbPollInterval";
        public const string LOGIC_APP_WAIT = "LogicAppWait";
    }
}
