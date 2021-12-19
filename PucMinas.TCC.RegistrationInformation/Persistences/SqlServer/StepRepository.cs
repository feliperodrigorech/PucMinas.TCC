using Microsoft.Extensions.Configuration;
using PucMinas.TCC.Domain.Models;
using PucMinas.TCC.Domain.Repositories;
using PucMinas.TCC.Persistence.SqlServer.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PucMinas.TCC.RegistrationInformation.Persistences.SqlServer
{
    public class StepRepository : SqlServerRepository, IStepRepository
    {
        public StepRepository(IConfiguration configuration)
            : base(configuration.GetConnectionString("SqlConnection"))
        {
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<StepModel> Find(long id)
        {
            const string query =
                @"SELECT
                      Id
	                , Description
                    , Name
                    , Sequence
                    , IsDelivered
                FROM Step
                WHERE
                    Id = @id";

            return await FindFirstOrDefaultAsync<StepModel>(query, new { id });
        }

        public Task<IEnumerable<StepModel>> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task Save(StepModel model)
        {
            throw new NotImplementedException();
        }

        public Task Update(StepModel model)
        {
            throw new NotImplementedException();
        }
    }
}
