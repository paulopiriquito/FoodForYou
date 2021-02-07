using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FoodForYou.Core.Models.Products;

namespace FoodForYou.Core.Models.Relational
{
    public class OrderProduct
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.00 €}")]
        [DisplayName("Price for Quantity")]
        public double PriceForOrderedQuantity => Quantity * Product.UnitPrice;
    }
}