using PucMinas.TCC.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PucMinas.TCC.Domain.Repositories
{
    public interface IChargeRepository : IBaseRepository<ChargeModel>
    {
        Task<IEnumerable<ChargeModel>> FindByCustomer(string cnpjCpf);
    }
}
