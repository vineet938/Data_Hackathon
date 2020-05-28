using GS1US.Tests.RTF.Database;
using GS1US.Tests.RTF.Setup;
using GS1US.Tests.RTF.Common;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace GS1US.Tests.RTF.Steps
{
    [Binding]
    public class WelcomeKitSteps : StepsCommon
    {
        [Then(@"I receive the Welcome Kit email")]
        public void ThenIReceiveTheWelcomeKitEmail()
        {
            var host = TestSetup.TestAccountImapHost;
            var port = TestSetup.TestAccountImapPort;
            var email = TestSetup.TestAccountEmail;
            var password = TestSetup.TestAccountEmailPassword;

            var imis = Context<IMIS>(ContextKeys.IMIS);

            var pc = Ctx.PrimaryContact;
            var coId = PollUtils.Poll<IEnumerable<Name>>(
                $"Query name entry by company and primary: {pc.CompanyName} {pc.FirstName} {pc.LastName}",
                TestSetup.DbTimeout, TestSetup.DbPollInterval,
                () => imis.NamesByCompanyAndUser(pc.CompanyName, pc.FirstName, pc.LastName),
                x => x.Where(o => o.LAST_UPDATED > ImisStartTime).Count() > 0
            ).First().CO_ID;

            PollUtils.Poll<bool>(
                $"Search Welcome Kit in inbox for CO_ID={coId}", 180, 30,
                () => {
                    var mail = new Mail.MailClient(host, port, email, password);
                    var r = mail.ReceivedWelcomeKit(Ctx.StartTime, coId, pc.CompanyName);
                    mail.Disconnect();
                    return r;
                },
                x => x == true
            ).ShouldBeTrue();
        }
    }
}
