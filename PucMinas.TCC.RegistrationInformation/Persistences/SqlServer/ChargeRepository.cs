using Microsoft.Extensions.Configuration;
using PucMinas.TCC.Domain.Models;
using PucMinas.TCC.Domain.Repositories;
using PucMinas.TCC.Persistence.SqlServer.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PucMinas.TCC.RegistrationInformation.Persistences.SqlServer
{
    public class ChargeRepository : SqlServerRepository, IChargeRepository
    {
        public ChargeRepository(IConfiguration configuration)
            : base(configuration.GetConnectionString("SqlConnection"))
        {
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ChargeModel> Find(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChargeModel>> FindAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ChargeModel>> FindByCustomer(string cnpjCpf)
        {
            const string query =
                @"SELECT TOP 100
                      Ch.Id
                    , Ch.Notes
	                , Ch.ServiceOrder
	                , Ch.OriginCustomer_Id
	                , Ch.DestinationCustomer_Id
	                , Ch.TransDate
	                , Ch.SerialNumber
	                , Ch.DestinationAddress_Id
                FROM Charge Ch
                INNER JOIN Party P ON P.Id = Ch.DestinationCustomer_Id
                WHERE
                    P.CnpjCpf = @cnpjCpf
                ORDER BY Ch.TransDate DESC, Ch.Id DESC";

            return await FindAsync<ChargeModel>(query, new { cnpjCpf });
        }

        public async Task Save(ChargeModel model)
        {
            const string query =
                @"INSERT INTO Charge (
                      Notes
	                , ServiceOrder
	                , OriginCustomer_Id
	                , DestinationCustomer_Id
	                , TransDate
	                , SerialNumber
	                , DestinationAddress_Id
                )
                VALUES (
                      @notes
	                , @serviceOrder
	                , @originCustomer_Id
	                , @destinationCustomer_Id
	                , @transDate
	                , @serialNumber
	                , @destinationAddress_Id
                );
                SELECT CAST(SCOPE_IDENTITY() as int)";

            model.Id = await InsertAsync(query, model);
        }

        public Task Update(ChargeModel model)
        {
            throw new NotImplementedException();
        }
    }
}
