using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.RTF.Database
{
    public class Account
    {
        private readonly string coId;
        private readonly IMIS imis;
        private readonly IEnumerable<Name> nameEntries;
        private readonly IEnumerable<Relationship> rels;
        private readonly string keyId;
        private readonly string billId;
        private readonly string ceoId;

        public readonly Name PrimaryContact;
        public readonly Name ExecutiveContact;
        public readonly Name Company;
        public readonly NameAddress PrimaryAddress;
        public readonly NameAddress ExecutiveAddress;

        public Account(IMIS imis, string coId)
        {
            this.coId = coId;
            this.imis = imis;
            this.nameEntries = imis.NamesByCoId(coId);
            this.rels = imis.RelationshipsByCoId(coId);
            keyId = rels.Where(o => o.RELATION_TYPE == "KEY").First().TARGET_ID;
            billId = rels.Where(o => o.RELATION_TYPE == "BILL").First().TARGET_ID;
            ceoId = rels.Where(o => o.RELATION_TYPE == "CEO").First().TARGET_ID;

            PrimaryContact = nameEntries.Where(o => o.ID == keyId).First();
            ExecutiveContact = nameEntries.Where(o => o.ID == ceoId).First();
            Company = nameEntries.Where(o => o.MEMBER_TYPE == "CM").First();
            PrimaryAddress = imis.AddressForContact(PrimaryContact.ID);
            ExecutiveAddress = imis.AddressForContact(ExecutiveContact.ID);
        }

        public Account(IMIS imis, string company, string firstName, string lastName) :
            this(imis, imis.NamesByCompanyAndUser(company, firstName, lastName).First().CO_ID)
        {
        }

        public IEnumerable<Name> this [string memberType]
        {
            get => nameEntries.Where(o => o.MEMBER_TYPE == memberType);
        }
    }
}
