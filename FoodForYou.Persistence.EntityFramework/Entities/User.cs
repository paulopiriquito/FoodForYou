using System;
using System.ComponentModel.DataAnnotations;

namespace FoodForYou.Persistence.EntityFramework.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        
        public string Username { get; set; }
        public string Email { get; set; }
        public bool Registered { get; set; }
    }
}