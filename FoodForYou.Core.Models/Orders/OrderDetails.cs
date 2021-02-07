using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FoodForYou.Core.Models.Costumers;
using FoodForYou.Core.Models.Employees;
using FoodForYou.Core.Models.Relational;
using FoodForYou.Core.Models.Shippers;

namespace FoodForYou.Core.Models.Orders
{
    public class OrderDetails : Order
    {
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        
        public virtual Shipper Shipper { get; set; }

        public IEnumerable<OrderProduct> Products { get; set; } = new List<OrderProduct>();

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.00 €}")]
        [DisplayName("Total Price")]
        public double TotalPrice => Products.Sum(x=> x.PriceForOrderedQuantity);
    }
}