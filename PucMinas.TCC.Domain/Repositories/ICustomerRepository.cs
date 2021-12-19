using PucMinas.TCC.Domain.Models;
using System.Threading.Tasks;

namespace PucMinas.TCC.Domain.Repositories
{
    public interface ICustomerRepository : IBaseRepository<CustomerModel>
    {
        Task<CustomerModel> FindByPartyId(long? partyId);
    }
}
