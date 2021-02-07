namespace FoodForYou.Core.Models.Costumers
{
    public class CustomerDetails : Customer
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Fax { get; set; }
    }
}