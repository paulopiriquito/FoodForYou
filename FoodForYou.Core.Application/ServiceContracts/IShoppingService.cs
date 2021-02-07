using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FoodForYou.Core.Models.Products;
using FoodForYou.Core.Models.Relational;
using FoodForYou.Core.Models.Users;

namespace FoodForYou.Core.Application.ServiceContracts
{
    public interface IShoppingService
    {
        Task<CostumerCart> Get(Models.Users.User user, CancellationToken cancellationToken = default);
        Task<CostumerCart> Create(Models.Users.User user, CostumerCart model, CancellationToken cancellationToken = default);
        Task<CostumerCart> Edit(Models.Users.User user, CostumerCart model, CancellationToken cancellationToken = default);
        Task Delete(Models.Users.User user, CancellationToken cancellationToken = default);
        Task<bool> Exists(Models.Users.User user, CancellationToken cancellationToken = default);
        Task<CostumerCart> AddProduct(Models.Users.User user, Models.Products.Product product, CancellationToken cancellationToken = default);
        Task<CostumerCart> RemoveProduct(Models.Users.User user, Models.Products.Product product, CancellationToken cancellationToken = default);
        Task<CostumerCart> EditProductQuantity(Models.Users.User user, Models.Products.Product product, int quantity, CancellationToken cancellationToken = default);
    }
}