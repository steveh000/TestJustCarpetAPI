using System;

namespace JustCarpet.Api.Models.Orders
{
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
