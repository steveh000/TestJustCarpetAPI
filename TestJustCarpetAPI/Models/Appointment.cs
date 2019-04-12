using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestJustCarpetAPI.Models
{
    public class Appointment
    {
        public DateTime Date { get; set; }
        public bool AM { get; set; }
        public int InstallerId { get; set; }
    }
}
