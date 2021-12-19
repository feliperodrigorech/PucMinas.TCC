using Microsoft.Extensions.Configuration;
using PucMinas.TCC.Domain.Models;
using PucMinas.TCC.Domain.Repositories;
using PucMinas.TCC.Persistence.SqlServer.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PucMinas.TCC.RegistrationInformation.Persistences.SqlServer
{
    public class PartyRepository : SqlServerRepository, IPartyRepository
    {
        public PartyRepository(IConfiguration configuration)
            : base(configuration.GetConnectionString("SqlConnection"))
        {
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<PartyModel> Find(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PartyModel>> FindAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PartyModel> FindByCnpjCpf(string cnpjCpf)
        {
            const string query =
                @"SELECT
                      Id
                    , CnpjCpf
	                , Name
	                , Description
	                , PartyType
                FROM Party
                WHERE CnpjCpf = @cnpjCpf";

            return await FindFirstOrDefaultAsync<PartyModel>(query, new { cnpjCpf });
        }

        public async Task Save(PartyModel model)
        {
            const string query =
                @"INSERT INTO Party (
                      CnpjCpf
	                , Name
	                , Description
	                , PartyType
                )
                VALUES (
                      @cnpjCpf
	                , @name
	                , @description
	                , @partyType
                );
                SELECT CAST(SCOPE_IDENTITY() as int)";

            model.Id = await InsertAsync(query, model);
        }

        public async Task Update(PartyModel model)
        {
            const string query =
                @"UPDATE Party
                SET
                      CnpjCpf = @cnpjCpf
	                , Name = @name
	                , Description = @description
	                , PartyType = @partyType
                WHERE Id = @Id";

            await ExecuteAsync(query, model);
        }
    }
}
