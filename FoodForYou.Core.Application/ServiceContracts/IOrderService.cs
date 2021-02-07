using System.Threading;
using System.Threading.Tasks;
using FoodForYou.Core.Models.Employees;
using FoodForYou.Core.Models.Orders;
using FoodForYou.Core.Models.Relational;
using FoodForYou.Core.Models.Shippers;

namespace FoodForYou.Core.Application.ServiceContracts
{
    public interface IOrderService : ICrudService<OrderDetails>
    {
        Task<OrderDetails> CreateFromCart(CostumerCart cart, Shipper shipper, CancellationToken cancellationToken = default);
        Task<OrderDetails> AddManager(Order order, Employee manager, CancellationToken cancellationToken = default);
        Task<OrderDetails> EditDetails(OrderDetails order, CancellationToken cancellationToken = default);
    }
}