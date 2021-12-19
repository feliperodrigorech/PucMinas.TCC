using Microsoft.Extensions.Configuration;
using PucMinas.TCC.Domain.Models;
using PucMinas.TCC.Domain.Repositories;
using PucMinas.TCC.Persistence.SqlServer.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PucMinas.TCC.RegistrationInformation.Persistences.SqlServer
{
    public class AddressRepository : SqlServerRepository, IAddressRepository
    {
        public AddressRepository(IConfiguration configuration)
            : base(configuration.GetConnectionString("SqlConnection"))
        {
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<AddressModel> Find(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AddressModel>> FindAll()
        {
            throw new NotImplementedException();
        }

        public async Task Save(AddressModel model)
        {
            const string query =
                @"INSERT INTO Address (
                      Party_Id
	                , Street
	                , StreetNumber
	                , District
	                , State
	                , City
	                , Country
	                , BuildingCompliment
	                , ZipCode
	                , Latitude
	                , Longitude
	                , IsPrimary
                )
                VALUES (
                      @party_id
	                , @street
	                , @streetNumber
	                , @district
	                , @state
	                , @city
	                , @country
	                , @buildingCompliment
	                , @zipCode
	                , @latitude
	                , @longitude
	                , @isPrimary
                );
                SELECT CAST(SCOPE_IDENTITY() as int)";

            model.Id = await InsertAsync(query, model);
        }

        public Task Update(AddressModel model)
        {
            throw new NotImplementedException();
        }
    }
}
