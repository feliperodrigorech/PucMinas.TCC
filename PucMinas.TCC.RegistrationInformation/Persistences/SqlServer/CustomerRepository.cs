using Microsoft.Extensions.Configuration;
using PucMinas.TCC.Domain.Models;
using PucMinas.TCC.Domain.Repositories;
using PucMinas.TCC.Persistence.SqlServer.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PucMinas.TCC.RegistrationInformation.Persistences.SqlServer
{
    public class CustomerRepository : SqlServerRepository, ICustomerRepository
    {
        public CustomerRepository(IConfiguration configuration)
            : base(configuration.GetConnectionString("SqlConnection"))
        {
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerModel> Find(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerModel>> FindAll()
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerModel> FindByPartyId(long? partyId)
        {
            const string query =
                @"SELECT
                      Id
                    , Party_Id
                FROM Customer 
                WHERE 
                    Party_Id = @partyId";

            return await FindFirstOrDefaultAsync<CustomerModel>(query, new { partyId });
        }

        public async Task Save(CustomerModel model)
        {
            const string query =
                @"INSERT INTO Customer (
                    Party_Id
                )
                VALUES (
                    @party_Id
                );
                SELECT CAST(SCOPE_IDENTITY() as int)";

            model.Id = await InsertAsync(query, model);
        }

        public Task Update(CustomerModel model)
        {
            throw new NotImplementedException();
        }
    }
}
