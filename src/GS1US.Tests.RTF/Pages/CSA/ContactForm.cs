using GS1US.Tests.RTF.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.RTF.Steps
{
    public class ContactForm
    {
        private string country;

        public string FirstName;
        public string LastName;
        public string CompanyName;
        public string Email;
        public string PhoneNumber;
        public string AddressLine1;
        public string AddressLine2;
        public string City;
        public string State;
        public string Zip;
        public string Country
        {
            get => country;
            set => country = value.Length == 0 ? "United States" : value;
        }


        public ContactForm()
        {

        }

        public ContactForm(Name entry, NameAddress addr)
        {
            FirstName = entry.FIRST_NAME;
            LastName = entry.LAST_NAME;
            CompanyName = entry.COMPANY;
            Email = entry.EMAIL;
            PhoneNumber = entry.WORK_PHONE;
            AddressLine1 = addr.ADDRESS_1;
            AddressLine2 = addr.ADDRESS_2;
            City = addr.CITY;
            State = addr.STATE_PROVINCE;
            Zip = addr.ZIP;
            Country = addr.COUNTRY;
        }

        public ContactForm Clone() => (ContactForm)this.MemberwiseClone();
    }
}
