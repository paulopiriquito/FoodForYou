using System;
using Microsoft.AspNetCore.Identity;

namespace FoodForYou.Persistence.EntityFramework.Entities
{
    public class User : IdentityUser
    {
        public Guid UserId { get; set; }
        
        public string Username { get; set; }
        public bool Registered { get; set; }
    }
}