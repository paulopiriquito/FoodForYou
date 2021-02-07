using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FoodForYou.Core.Models.Costumers;
using FoodForYou.Core.Models.Shippers;
using FoodForYou.Core.Models.Users;

namespace FoodForYou.Core.Models.Relational
{
    public class CostumerCart
    {
        public CostumerCart(User user)
        {
            User = user;
        }
        
        public Customer Costumer { get; set; }
        
        public Shipper Shipper { get; set; }
        
        public User User { get; }
        public IList<OrderProduct> CartProducts { get; set; } = new List<OrderProduct>();
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.00 €}")]
        [DisplayName("Total Price")]
        public double TotalPrice => CartProducts.Sum(x=> x.PriceForOrderedQuantity);
    }
}