using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FoodForYou.Core.Application.ServiceContracts
{
    public interface ICrudService<T>
    {
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);
        
        Task<T> GetId(int id, CancellationToken cancellationToken = default);
        
        Task<T> Create(T model, CancellationToken cancellationToken = default);
        Task<T> Edit(T model, CancellationToken cancellationToken = default);
        Task Delete(T model, CancellationToken cancellationToken = default);

        Task<bool> Exists(int id, CancellationToken cancellationToken = default);
    }
}