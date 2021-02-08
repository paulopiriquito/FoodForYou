using FoodForYou.Persistence.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodForYou.Persistence.EntityFramework.Context
{
    public class AuthenticationDbContext : IdentityDbContext<User>
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
            : base(options)
        {
        }
    }
}