using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.DataHub.Setup
{
    struct Product
    {
        public string GTIN;
        public Product(string gtin)
        {
            GTIN = gtin;
        }
    }

    struct Location
    {
        public string GLN;
        public string Name;
        public Location(string gln, string name)
        {
            GLN = gln;
            Name = name;
        }
    }

    struct Message
    {
        public string Topic;
        public string Signature;
        public Message(string topic, string signature)
        {
            Topic = topic;
            Signature = signature;
        }
    }

    struct CompanySearch
    {
        public string Key;
        public string CompanyName;
        public CompanySearch(string key, string companyName)
        {
            Key = key;
            CompanyName = companyName;
        }
    }
}
