using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FoodForYou.Core.Models.Employees;

namespace FoodForYou.Core.Application.ServiceContracts
{
    public interface IEmployeeService : ICrudService<Employee>
    {
        
        public new Task<IEnumerable<EmployeeDetails>> GetAll(CancellationToken cancellationToken = default);
        public Task<Employee> Edit(EmployeeDetails model, CancellationToken cancellationToken = default);
        public Task<Employee> Create(EmployeeDetails model, CancellationToken cancellationToken = default);
        public Task<EmployeeDetails> GetDetails(object id);
    }
}