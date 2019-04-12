using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestJustCarpetAPI.Models
{
    public class Customer
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "address")]
        public string Address { get; set; }
        [DataMember(Name = "telephone")]
        public string TelephoneNumber { get; set; }
        [DataMember(Name = "emailaddress")]
        public string EmailAddress { get; set; }
        [DataMember(Name = "macaddress")]
        public string MacAddress { get; set; }

        public List<Order> Orders = new List<Order>();
    }
}
