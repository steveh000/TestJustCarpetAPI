using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestJustCarpetAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime Created { get; set; }
        public int CustomerId { get; set; }
        [DisplayName("Customer")]
        public string CustomerName { get; set; }
        public int InstallerId { get; set; }
        [DisplayName("Installer")]
        public string InstallerName { get; set; }
        [DisplayName("Install Date")]
        public string InstallerShortDateString { get; set; }
        [DisplayName("Time of Day")]
        public string TimeOfDay { get; set; }
    }
}
