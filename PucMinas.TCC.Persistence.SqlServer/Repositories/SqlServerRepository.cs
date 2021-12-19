using PucMinas.TCC.Persistence.Repositories;
using System.Data;
using System.Data.SqlClient;

namespace PucMinas.TCC.Persistence.SqlServer.Repositories
{
    public class SqlServerRepository : DapperRepository
    {
        readonly string ConnectionString;

        public SqlServerRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public override IDbConnection Connection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
