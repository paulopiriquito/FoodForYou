using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FoodForYou.Core.Application.ServiceContracts;
using FoodForYou.Core.Models.Employees;
using FoodForYou.Persistence.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodForYou.Core.Application.Implementations.Employee
{
    public class EmployeeService : IEmployeeService
    {
        protected readonly IStoreDbContext DbContext;

        public EmployeeService(IStoreDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<IEnumerable<Models.Employees.Employee>> GetAll(CancellationToken cancellationToken = default)
        {
            return await DbContext.Employees.Select(employee => new Models.Employees.Employee()
            {
                EmployeeId = employee.EmployeeId,
                FullName = employee.FullName,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy
            }).ToListAsync(cancellationToken);
        }

        async Task<IEnumerable<EmployeeDetails>> IEmployeeService.GetAll(CancellationToken cancellationToken)
        {
            return await DbContext.Employees.Select(employee => new EmployeeDetails()
            {
                EmployeeId = employee.EmployeeId,
                FullName = employee.FullName,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy,
                Address = employee.Address,
                BirthDate = employee.BirthDate,
                City = employee.City,
                Country = employee.Country,
                PostalCode = employee.PostalCode,
                HomePhone = employee.HomePhone,
                Region = employee.Region
            }).ToListAsync(cancellationToken);
        }

        public async Task<Models.Employees.Employee> GetId(int id, CancellationToken cancellationToken = default)
        {
            var employee = await DbContext.Employees
                .FirstOrDefaultAsync(x=> x.EmployeeId.Equals(id), cancellationToken);

            return new Models.Employees.Employee()
            {
                EmployeeId = employee.EmployeeId,
                FullName = employee.FullName,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy
            };
        }

        public async Task<Models.Employees.Employee> Create(EmployeeDetails model, CancellationToken cancellationToken = default)
        {
            var employee = await DbContext.Employees.AddAsync(new Persistence.EntityFramework.Entities.Employee()
            {
                Title = model.Title,
                TitleOfCourtesy = model.TitleOfCourtesy,
                Address = model.Address,
                BirthDate = model.BirthDate,
                City = model.City,
                PostalCode = model.PostalCode,
                Country = model.Country,
                FullName = model.FullName,
                HomePhone = model.HomePhone,
                Region = model.Region,
            }, cancellationToken);

            return await GetId(employee.Entity.EmployeeId, cancellationToken);
        }

        public async Task<EmployeeDetails> GetDetails(object id)
        {
            return await DbContext.Employees.Select(employee => new EmployeeDetails()
            {
                EmployeeId = employee.EmployeeId,
                FullName = employee.FullName,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy,
                Address = employee.Address,
                BirthDate = employee.BirthDate,
                City = employee.City,
                Country = employee.Country,
                PostalCode = employee.PostalCode,
                HomePhone = employee.HomePhone,
                Region = employee.Region
            }).FirstOrDefaultAsync(x=>x.EmployeeId.Equals(id));
        }

        public async Task<Models.Employees.Employee> Edit(EmployeeDetails model, CancellationToken cancellationToken = default)
        {
            var employee = await DbContext.Employees
                .FirstOrDefaultAsync(x=> x.EmployeeId == model.EmployeeId, cancellationToken);

            employee.Title = model.Title;
            employee.TitleOfCourtesy = model.TitleOfCourtesy;
            employee.Address = model.Address;
            employee.BirthDate = model.BirthDate;
            employee.City = model.City;
            employee.PostalCode = model.PostalCode;
            employee.Country = model.Country;
            employee.FullName = model.FullName;
            employee.HomePhone = model.HomePhone;
            employee.Region = model.Region;

            DbContext.Employees.Update(employee);
            await DbContext.SaveChangesAsync(cancellationToken);

            return await GetId(employee.EmployeeId, cancellationToken);
        }

        public async Task Delete(Models.Employees.Employee model, CancellationToken cancellationToken = default)
        {
            var employee = await DbContext.Employees
                .FirstOrDefaultAsync(x=> x.EmployeeId == model.EmployeeId, cancellationToken);

            if (employee != null)
            {
                DbContext.Employees.Remove(employee);
                await DbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<bool> Exists(int id, CancellationToken cancellationToken = default)
        {
            return await DbContext.Employees.AnyAsync(x => x.EmployeeId.Equals(id), cancellationToken);
        }

        public Task<Models.Employees.Employee> Create(Models.Employees.Employee model, CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException();
        }

        public Task<Models.Employees.Employee> Edit(Models.Employees.Employee model, CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException();
        }
    }
}