using PucMinas.TCC.Domain.Models;
using System.Threading.Tasks;

namespace PucMinas.TCC.Domain.Repositories
{
    public interface IPartyRepository : IBaseRepository<PartyModel>
    {
        Task<PartyModel> FindByCnpjCpf(string cnpjCpf);
    }
}
