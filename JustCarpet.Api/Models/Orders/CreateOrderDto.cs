using System;
using System.Collections.Generic;

namespace JustCarpet.Api.Models.Orders
{
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public int InstallerId { get; set; }
        public DateTime InstallDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool AM { get; set; }
        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
}
