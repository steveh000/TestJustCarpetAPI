using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestJustCarpetAPI.Models
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

    public class OrderLine
    {
        public int Id { get; set; }
        public int CarpetId { get; set; }
        public int CarpetSizeOptionId { get; set; }
        public int Qty { get; set; }
        public int CustomerOrderId { get; set; }



    }

    public class OrderConfirmation
    {
        public int Id { get; set; }
        public DateTime InstallDate { get; set; }
        public bool AM { get; set; }
        public string InstallerName { get; set; }
        public string InstallerTelephone { get; set; }
        public bool OrderSucess { get; set; }
    }
}
