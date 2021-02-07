using System;
using System.ComponentModel.DataAnnotations;

namespace FoodForYou.Core.Models.Employees
{
    public class EmployeeDetails : Employee
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
    }
}