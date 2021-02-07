using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodForYou.Persistence.EntityFramework.Entities
{
    [Table("Shippers")]
    public class Shipper
    {
        [Key]
        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
    }
}