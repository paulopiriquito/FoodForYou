using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FoodForYou.Core.Application.ServiceContracts;
using FoodForYou.Persistence.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodForYou.Core.Application.Implementations.User
{
    public class UserService : IUserService
    {
        protected readonly AuthenticationDbContext DbContext;

        public UserService(AuthenticationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<IEnumerable<Models.Users.User>> GetAll(CancellationToken cancellationToken = default)
        {
            var users = await DbContext.Users
                .Select(shipper => Map(shipper))
                .ToListAsync(cancellationToken);

            return users;
        }

        public async Task<Models.Users.User> GetId(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await DbContext.Users
                .Where(x=>x.UserId == id)
                .Select(productDb => Map(productDb))
                .FirstOrDefaultAsync(cancellationToken);

            return user;
        }

        public async Task<Models.Users.User> Create(Models.Users.User model, CancellationToken cancellationToken = default)
        {
            var userEntry = await DbContext.Users.AddAsync(Map(model), cancellationToken);

            return Map(userEntry.Entity);
        }

        public async Task<Models.Users.User> Edit(Models.Users.User model, CancellationToken cancellationToken = default)
        {
            var userDb = await DbContext.Users
                .FirstOrDefaultAsync(x=>x.UserId == model.UserId, cancellationToken);

            userDb = DbContext.Users.Update(Map(userDb, model)).Entity;

            await DbContext.SaveChangesAsync(cancellationToken);
            
            return Map(userDb);
        }

        public async Task Delete(Models.Users.User model, CancellationToken cancellationToken = default)
        {
            var userDb = await DbContext.Users
                .Where(x=> x.UserId == model.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            DbContext.Users.Remove(userDb);

            await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> Exists(Guid id, CancellationToken cancellationToken = default)
        {
            return await DbContext.Users
                .AnyAsync(x => x.UserId == id, cancellationToken);
        }

        public async Task<Models.Users.User> Authorize(string username, string email, CancellationToken cancellationToken = default)
        {
            var userDb =
                await DbContext.Users.FirstOrDefaultAsync(x => x.Username == username && x.Email == email,
                    cancellationToken);

            return Map(userDb);
        }

        private static Models.Users.User Map(Persistence.EntityFramework.Entities.User dbUser)
        {
            return new Models.Users.User()
            {
                UserId = dbUser.UserId,
                Username = dbUser.Username,
                Email = dbUser.Email,
                Registered = dbUser.Registered,
            };
        }
        
        private static Persistence.EntityFramework.Entities.User Map(Models.Users.User model)
        {
            return new Persistence.EntityFramework.Entities.User()
            {
                UserId = model.UserId,
                Username = model.Username,
                Email = model.Email,
                Registered = model.Registered,
            };
        }

        private static Persistence.EntityFramework.Entities.User Map(Persistence.EntityFramework.Entities.User userDb, Models.Users.User model)
        {
            userDb.Username = model.Username;
            userDb.Email = model.Email;
            userDb.Registered = userDb.Registered;

            return userDb;
        }
    }
}