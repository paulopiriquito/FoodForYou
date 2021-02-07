using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FoodForYou.Core.Models.Costumers;

namespace FoodForYou.Core.Application.ServiceContracts
{
    public interface ICustomerService : ICrudService<Customer>
    {
        new Task<IEnumerable<CustomerDetails>> GetAll(CancellationToken cancellationToken = default);
        
        Task<CustomerDetails> GetDetails(Customer costumer, CancellationToken cancellationToken = default);
        
        Task<CustomerDetails> GetDetails(int? costumerId, CancellationToken cancellationToken = default);

        Task<CustomerDetails> Create(CustomerDetails costumer, CancellationToken cancellationToken = default);
        
        Task<CustomerDetails> Edit(CustomerDetails costumer, CancellationToken cancellationToken = default);
    }
}