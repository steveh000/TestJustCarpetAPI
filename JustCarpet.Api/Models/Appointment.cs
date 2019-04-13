using System;

namespace JustCarpet.Api.Models
{
    public class Appointment
    {
        public DateTime Date { get; set; }
        public bool AM { get; set; }
        public int InstallerId { get; set; }
    }
}
