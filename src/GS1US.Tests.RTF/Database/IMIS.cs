using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;

namespace GS1US.Tests.RTF.Database
{
    public class IMIS
    {
        private SqlConnection conn;

        public IMIS(SqlConnection connection)
        {
            conn = connection;
        }

        public void Close()
        {
            conn.Close();
        }

        public IEnumerable<Name> NamesByCompany(string company) =>
            conn.Query<Name>(
                "select * from uccdbi.dbo.Name where company=@Company",
                new { Company = company });

        public IEnumerable<Name> NamesByCoId(string coId) =>
            conn.Query<Name>(
                "select * from uccdbi.dbo.Name where ID=@CoId or CO_ID=@CoId",
                new { CoId = coId });

        public IEnumerable<Name> NamesByCompanyAndUser(string company, string firstName, string lastName) =>
            conn.Query<Name>(
                "select * from uccdbi.dbo.Name " +
                "where company=@Company " +
                "and FIRST_NAME=@FirstName " +
                "and LAST_NAME=@LastName " +
                "order by LAST_UPDATED desc",
                new { Company = company, FirstName = firstName, LastName = lastName });

        public IEnumerable<Name> NamesByCompanyAndEmail(string company, string email) =>
            conn.Query<Name>(
                "select * from uccdbi.dbo.Name " +
                "where company=@Company " +
                "and EMAIL=@Email " +
                "order by LAST_UPDATED desc",
                new { Company = company, Email = email });

        public IEnumerable<Relationship> RelationshipsByCoId(string companyId) =>
            conn.Query<Relationship>(
                "select * from uccdbi.dbo.Relationship where ID=@Id",
                new { Id = companyId });

        public IEnumerable<Subscription> SubscriptionsForCompany(string companyId) =>
            conn.Query<Subscription>(
                "select s.* " +
                "from uccdbi.dbo.Subscriptions s, uccdbi.dbo.Name n " +
                "where (n.ID=@CoId or (n.CO_ID=@CoId and n.MEMBER_TYPE='#UPC')) " +
                "and s.ID = n.ID",
                new { CoId = companyId });

        public IEnumerable<Name> FindAccountWithProration(int n) =>
            conn.Query<Name>(
                @"select top (@N) n.*
                from uccdbi.dbo.Name n
                left join uccdbi.dbo.Relationship r1 on r1.ID=n.ID and r1.RELATION_TYPE='CEO'
                left join uccdbi.dbo.Relationship r2 on r2.ID=n.ID and r2.RELATION_TYPE='KEY'
                left join uccdbi.dbo.Relationship r3 on r3.ID=n.ID and r3.RELATION_TYPE='BILL'
                where n.MEMBER_TYPE='CM'
                and r1.ID is not null
                and r2.ID is not null
                and r3.ID is not null
                and MONTH(n.PAID_THRU) != @Month
                order by RAND(CHECKSUM(NEWID()))
                "
                ,
                new
                {
                    N = n,
                    Month = DateTime.Now.Month
                },
                commandTimeout: 60
            );

        public IEnumerable<Name> FindAccountWithoutProration(int n)
        {
            var t = DateTime.Now;
            var y = t.Year;
            var m = t.Month;
            var d1 = new DateTime(y, m, DateTime.DaysInMonth(y, m));
            var d2 = new DateTime(y + 1, m, DateTime.DaysInMonth(y + 1, m));
            return conn.Query<Name>(
                @"select top (@N) n.*
                from uccdbi.dbo.Name n
                left join uccdbi.dbo.Relationship r1 on r1.ID=n.ID and r1.RELATION_TYPE='CEO'
                left join uccdbi.dbo.Relationship r2 on r2.ID=n.ID and r2.RELATION_TYPE='KEY'
                left join uccdbi.dbo.Relationship r3 on r3.ID=n.ID and r3.RELATION_TYPE='BILL'
                where n.MEMBER_TYPE='CM'
                and r1.ID is not null
                and r2.ID is not null
                and r3.ID is not null
                and MONTH(n.PAID_THRU) = @Month
                order by RAND(CHECKSUM(NEWID()))
                ",
                new
                {
                    N = n,
                    Month = m
                },
                commandTimeout: 60
            );
        }

        public NameAddress AddressForContact(string contactId) =>
            conn.QuerySingle<NameAddress>(
                "select top 1 * from uccdbi.dbo.Name_Address where ID=@Id",
                new { Id = contactId }
            );

        public IEnumerable<Trans> TransactionForBtId(string btId) =>
            conn.Query<Trans>(
                "select * from uccdbi.dbo.Trans where BT_ID=@BtId",
                new { BtId = btId }
            );
    }

    public class Name
    {
        public string ID;
        public string MEMBER_TYPE;
        public string CO_ID;
        public string COMPANY;
        public string FULL_ADDRESS;
        public string FULL_NAME;
        public string FIRST_NAME;
        public string LAST_NAME;
        public string EMAIL;
        public string WORK_PHONE;
        public string CITY;
        public string STATE_PROVINCE;
        public string ZIP;
        public string COUNTRY;
        public DateTime PAID_THRU;
        public DateTime LAST_UPDATED;
    }

    public class Relationship
    {
        public string ID;
        public string TARGET_ID;
        public string RELATION_TYPE;
    }

    public class Subscription
    {
        public string ID;
        public string PRODUCT_CODE;
        public double BILL_AMOUNT;
        public string BT_ID;
        public DateTime BILL_THRU;
        public DateTime DATE_ADDED;
    }

    public class NameAddress
    {
        public string ID;
        public string ADDRESS_NUM;
        public string ADDRESS_1;
        public string ADDRESS_2;
        public string CITY;
        public string STATE_PROVINCE;
        public string ZIP;
        public string COUNTRY;
    }

    public class Trans
    {
        public string BATCH_NUM;
        public string BT_ID;
        public string TRANSACTION_TYPE;
        public string PRODUCT_CODE;
    }
}
