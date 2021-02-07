using System.Threading;
using FoodForYou.Core.Models.Relational;

namespace FoodForYou.Core.Application.ServiceContracts
{
    public interface IShoppingService
    {
        CostumerCart Get(Models.Users.User user, CancellationToken cancellationToken = default);
        CostumerCart Create(Models.Users.User user, CostumerCart model, CancellationToken cancellationToken = default);
        CostumerCart Edit(Models.Users.User user, CostumerCart model, CancellationToken cancellationToken = default);
        void Delete(Models.Users.User user, CancellationToken cancellationToken = default);
        bool Exists(Models.Users.User user, CancellationToken cancellationToken = default);
        CostumerCart AddProduct(Models.Users.User user, Models.Products.Product product, CancellationToken cancellationToken = default);
        CostumerCart RemoveProduct(Models.Users.User user, Models.Products.Product product, CancellationToken cancellationToken = default);
        CostumerCart EditProductQuantity(Models.Users.User user, Models.Products.Product product, int quantity, CancellationToken cancellationToken = default);
    }
}