using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodForYou.Persistence.EntityFramework.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        [ForeignKey("Shipper")]
        public int ShipperId { get; set; }
        
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Shipper Shipper { get; set; }
    }
}