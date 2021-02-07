using System;
using System.ComponentModel.DataAnnotations;

namespace FoodForYou.Core.Models.Orders
{
    public abstract class Order
    {
        public int OrderId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime OrderDate { get; set; }
    }
}