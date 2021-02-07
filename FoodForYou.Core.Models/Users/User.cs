using System;

namespace FoodForYou.Core.Models.Users
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool Registered { get; set; } = true;
    }
}