using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodForYou.Persistence.EntityFramework.Entities
{
    [Table("Employees")]
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string TitleOfCourtesy { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
    }
}