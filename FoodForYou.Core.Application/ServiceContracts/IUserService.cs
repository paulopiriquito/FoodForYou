using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FoodForYou.Core.Application.ServiceContracts
{
    public interface IUserService
    {
        Task<IEnumerable<Models.Users.User>> GetAll(CancellationToken cancellationToken = default);
        Task<Models.Users.User> GetId(Guid id, CancellationToken cancellationToken = default);
        Task<Models.Users.User> Create(Models.Users.User model, CancellationToken cancellationToken = default);
        Task<Models.Users.User> Edit(Models.Users.User model, CancellationToken cancellationToken = default);
        Task Delete(Models.Users.User model, CancellationToken cancellationToken = default);
        Task<bool> Exists(Guid id, CancellationToken cancellationToken = default);
        Task<Models.Users.User> Authorize(string username, string email, CancellationToken cancellationToken = default);
    }
}