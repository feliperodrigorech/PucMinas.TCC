using Microsoft.Extensions.Configuration;
using PucMinas.TCC.Domain.Models;
using PucMinas.TCC.Domain.Repositories;
using PucMinas.TCC.Persistence.SqlServer.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PucMinas.TCC.RegistrationInformation.Persistences.SqlServer
{
    public class MerchandiseRepository : SqlServerRepository, IMerchandiseRepository
    {
        public MerchandiseRepository(IConfiguration configuration)
            : base(configuration.GetConnectionString("SqlConnection"))
        {
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<MerchandiseModel> Find(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MerchandiseModel>> FindAll()
        {
            throw new NotImplementedException();
        }

        public async Task Save(MerchandiseModel model)
        {
            const string query =
                @"INSERT INTO Merchandise (
	                  Sku
	                , Quantity
	                , UnitType
	                , PriceUnit
	                , Weight
	                , Width
	                , Length
	                , Height
	                , Wharehouse_Id
	                , Charge_Id
                )
                VALUES (
                      @sku
	                , @quantity
	                , @unitType
	                , @priceUnit
	                , @weight
	                , @width
	                , @length
	                , @height
	                , @wharehouse_Id
	                , @charge_Id
                );
                SELECT CAST(SCOPE_IDENTITY() as int)";

            model.Id = await InsertAsync(query, new
            {
                model.Sku,
                model.Quantity,
                model.UnitType,
                model.PriceUnit,
                model.Dimension.Weight,
                model.Dimension.Width,
                model.Dimension.Length,
                model.Dimension.Height,
                model.Wharehouse_Id,
                model.Charge_Id
            });
        }

        public Task Update(MerchandiseModel model)
        {
            throw new NotImplementedException();
        }
    }
}
