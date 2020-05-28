using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace GS1US.Tests.RTF.Database
{
    class IDM
    {
        private SqlConnection conn;

        public IDM(SqlConnection connection)
        {
            conn = connection;
        }

        public void Close()
        {
            conn.Close();
        }

        // coId is IMIS company ID
        public IEnumerable<Claim> ClaimsByCoId(string coId) =>
            conn.Query<Claim>(
                "select c.ImisId, c.CompanyName, u.FirstName, u.LastName, ct.ClaimName " +
                "from IdmDb.dbo.Company c " +
                "join IdmDb.dbo.UserCompany uc on uc.CompanyId = c.Id " +
                "join IdmDb.dbo.[User] u on u.Id = uc.UserId " +
                "join IdmDb.dbo.UserCompanyClaim ucc on ucc.UserCompanyId = uc.Id " +
                "join IdmDb.dbo.ClaimType ct on ct.Id = ucc.ClaimTypeId " +
                "where c.ImisId = @CoId",
                new { CoId = coId }
            );
    }

    class Claim
    {
        public string ImisId { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClaimName { get; set; }
    }

    class Company
    {
        public int Id { get; set; }
        public string ImisId { get; set; }
        public string CompanyName { get; set; }
    }

    class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    class ClaimType
    {
        public int Id { get; set; }
        public string ClaimName { get; set; }
    }
}
