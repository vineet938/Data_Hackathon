using MailKit.Net.Imap;
using MailKit.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GS1US.Tests.RTF.Mail
{
    class MailClient
    {
        private readonly ImapClient client;

        public MailClient(string server, int port, string username, string password)
        {
            client = new ImapClient
            {
                ServerCertificateValidationCallback = (s, c, h, e) => true
            };
            client.Connect(server, port, true);
            client.Authenticate(username, password);
        }
    
        public void Disconnect()
        {
            client.Disconnect(true);
            client.Dispose();
        }

        public bool ReceivedWelcomeKit(DateTime since, string coId, string company)
        {
            Console.WriteLine($"SINCE {since}");
            Console.WriteLine($"{DateTime.Now} BEGIN");
            var inbox = client.Inbox;
            inbox.Open(MailKit.FolderAccess.ReadOnly);
            Console.WriteLine($"{DateTime.Now} OPENED");
            var query = SearchQuery.DeliveredAfter(since)
                .And(SearchQuery.SubjectContains($"Welcome to GS1 US"))
                .And(SearchQuery.BodyContains($"Account Number: {coId}"));
            foreach (var uid in inbox.Search(query))
            {
                Console.WriteLine($"{DateTime.Now} MAIL {uid}");
                var message = inbox.GetMessage(uid);
                Console.WriteLine($"{DateTime.Now} GOT MESSAGE");
                var m = Regex.Match(message.HtmlBody, $"Account Number: ([0-9]+)");
                if (m.Success && m.Groups[1].Value == coId)
                {
                    inbox.Close();
                    return true;
                }
                Console.WriteLine($"{DateTime.Now} NEXT");
            }
            Console.WriteLine($"{DateTime.Now} END");
            inbox.Close();
            return false;
        }
    }
}
