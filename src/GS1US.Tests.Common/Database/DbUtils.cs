using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.Common.Database
{
    public static class DbUtils
    {
        public static Imis ConnectToImis(string connectionString)
        {
            var conn = new SqlConnection(connectionString);
            return new Imis(conn);
        }
    }
}
