using Microsoft.Extensions.Configuration;
using PucMinas.TCC.Domain.Models;
using PucMinas.TCC.Domain.Repositories;
using PucMinas.TCC.Persistence.SqlServer.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PucMinas.TCC.RegistrationInformation.Persistences.SqlServer
{
    public class ChargeHistoryRepository : SqlServerRepository, IChargeHistoryRepository
    {
        public ChargeHistoryRepository(IConfiguration configuration)
            : base(configuration.GetConnectionString("SqlConnection"))
        {
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ChargeHistoryModel> Find(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChargeHistoryModel>> FindAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ChargeHistoryModel>> FindByCharge(long? chargeId)
        {
            const string query =
                @"SELECT
                      CH.Id
	                , CH.Description
                    , CH.Charge_Id
	                , CH.Step_Id
                FROM ChargeHistory CH
                INNER JOIN Charge C ON C.Id = CH.Charge_Id
                WHERE
                    C.Id = @chargeId";

            return await FindAsync<ChargeHistoryModel>(query, new { chargeId });
        }

        public Task Save(ChargeHistoryModel model)
        {
            throw new NotImplementedException();
        }

        public Task Update(ChargeHistoryModel model)
        {
            throw new NotImplementedException();
        }
    }
}
