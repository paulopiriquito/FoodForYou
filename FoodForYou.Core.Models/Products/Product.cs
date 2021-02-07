using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodForYou.Core.Models.Products
{
    public class Product
    {
        public int ProductId { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.00 €}")]
        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }
    }
}