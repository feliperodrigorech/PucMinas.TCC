using System.Collections.Generic;
using System.Threading.Tasks;

namespace PucMinas.TCC.Domain.Repositories
{
    public interface IBaseRepository<T>
    {
        Task Save(T model);
        Task Update(T model);
        Task<T> Find(long id);
        Task<IEnumerable<T>> FindAll();
        Task Delete(long id);
    }
}
