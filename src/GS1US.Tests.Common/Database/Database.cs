using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.Common.Database
{
    abstract public class Database
    {
        protected readonly SqlConnection conn;

        protected Database(SqlConnection connection)
        {
            conn = connection;
        }

        public void Close()
        {
            conn.Close();
        }
    }
}
