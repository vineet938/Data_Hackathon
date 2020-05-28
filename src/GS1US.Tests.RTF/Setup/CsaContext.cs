using GS1US.Tests.RTF.Database;
using GS1US.Tests.RTF.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.RTF.Setup
{
    public class CsaContext
    {
        private ContactForm executiveContact = null;

        public readonly Dictionary<int, (double, double)> BillingInfo =
            new Dictionary<int, (double, double)>() {
                {10, (250.0, 50.0) },
                {100, (750.0, 150.0) },
                {1000, (2500, 500.0) },
                {10000, (6500.0, 1300.0) },
                {100000, (10500.0, 2100.0) }
            };

        public readonly DateTime StartTime = DateTime.Now;
        public Account OriginalAccount = null;
        public bool SameExecutiveContact = false;
        public ContactForm PrimaryContact = null;
        public ContactForm ExecutiveContact
        {
            get => SameExecutiveContact ? PrimaryContact : executiveContact;
            set => executiveContact = value;
        }
        public List<int> Capacities = new List<int>();
        public bool NewAccount => OriginalAccount == null;

        public (double, double, double) BillingAmounts()
        {
            double prorate = 0.0;
            if (OriginalAccount != null)
            {
                int m1 = DateTime.Now.Month;
                int m2 = OriginalAccount.Company.PAID_THRU.Month;
                if (m2 < m1) m2 += 12;
                prorate = (m2 - m1) / 12.0;
            }

            return Capacities.Select(
                c =>
                {
                    var (prefixFee, renewalFee) = BillingInfo[c];
                    return (prefixFee * 0.2, prefixFee * 0.8, renewalFee * prorate);
                }
            ).Aggregate(
                (z, t) => (z.Item1 + t.Item1, z.Item2 + t.Item2, z.Item3 + t.Item3)
            );
        }
    }
}
