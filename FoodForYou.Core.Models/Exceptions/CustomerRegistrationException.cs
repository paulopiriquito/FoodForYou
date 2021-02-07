using System;

namespace FoodForYou.Core.Models.Exceptions
{
    public class CustomerRegistrationException : Exception
    {
        public CustomerRegistrationException() : base("Customer must be registered")
        {
        }
    }
}