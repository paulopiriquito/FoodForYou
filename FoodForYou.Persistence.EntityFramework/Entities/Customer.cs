using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodForYou.Persistence.EntityFramework.Entities
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Fax { get; set; }
    }
}