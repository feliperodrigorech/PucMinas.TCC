using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PucMinas.TCC.Persistence.Repositories
{
    public abstract class DapperRepository
    {
        public abstract IDbConnection Connection();

        public async Task<T> FindFirstOrDefaultAsync<T>(string query, object parameters = null)
        {
            using IDbConnection conn = Connection();
            conn.Open();
            return await conn.QueryFirstOrDefaultAsync<T>(query, parameters);
        }

        public async Task<IEnumerable<T>> FindAsync<T>(string query, object parameters = null)
        {
            using IDbConnection conn = Connection();
            conn.Open();
            return await conn.QueryAsync<T>(query, parameters);
        }

        public async Task ExecuteAsync(string query, object parameters = null, CommandType commandType = CommandType.Text)
        {
            using IDbConnection conn = Connection();
            conn.Open();
            await conn.ExecuteAsync(query, parameters, commandType: commandType);
        }

        public async Task<int> InsertAsync(string query, object parameters = null)
        {
            using IDbConnection conn = Connection();
            conn.Open();
            return await conn.QuerySingleAsync<int>(query, parameters);
        }
    }
}
