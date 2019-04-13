using JustCarpet.Api.Enums;
using System.Collections.Generic;

namespace JustCarpet.Api.Models.Flooring
{
    public class Flooring
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CarpetStyleEnum Style { get; set; }
        public int DurabilityFactor { get; set; }
        public bool PetFriendly { get; set; }
        public decimal PriceM2 { get; set; }

        public List<string> Properties { get; set; } = new List<string>();
        public List<FlooringSizeDto> Sizes { get; set; } = new List<FlooringSizeDto>();
        public List<FlooringImage> Images { get; set; } = new List<FlooringImage>();


    }


}
