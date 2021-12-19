using PucMinas.TCC.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PucMinas.TCC.Domain.Repositories
{
    public interface IChargeHistoryRepository : IBaseRepository<ChargeHistoryModel>
    {
        Task<IEnumerable<ChargeHistoryModel>> FindByCharge(long? chargeId);
    }
}
