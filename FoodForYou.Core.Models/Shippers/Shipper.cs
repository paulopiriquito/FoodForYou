using System.ComponentModel;

namespace FoodForYou.Core.Models.Shippers
{
    public class Shipper
    {
        public int ShipperId { get; set; }
        
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
    }
}