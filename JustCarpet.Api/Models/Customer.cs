using JustCarpet.Api.Models.Orders;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JustCarpet.Api.Models
{
    public class Customer
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("telephone")]
        public string TelephoneNumber { get; set; }
        [JsonProperty("emailaddress")]
        public string EmailAddress { get; set; }
        [JsonProperty("macaddress")]
        public string MacAddress { get; set; }

        public List<Order> Orders = new List<Order>();
    }
}
