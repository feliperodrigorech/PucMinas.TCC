using PucMinas.TCC.Domain.Models;
using System.Threading.Tasks;

namespace PucMinas.TCC.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<UserModel>
    {
        Task<UserModel> FindByUserId(string userId);
    }
}
