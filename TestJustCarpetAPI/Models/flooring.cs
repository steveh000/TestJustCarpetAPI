using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestJustCarpetAPI.Models
{
    public class Flooring
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CarpetStyle Style { get; set; }
        public int DurabilityFactor { get; set; }
        public bool PetFriendly { get; set; }
        public decimal PriceM2 { get; set; }

        public List<string> Properties { get; set; } = new List<string>();
        public List<FlooringSizeDto> Sizes { get; set; } = new List<FlooringSizeDto>();
        public List<FlooringImage> Images { get; set; } = new List<FlooringImage>();


    }
    public enum CarpetStyle
    {
        Unknown = 0,
        Contemporary = 1,
        Classical = 2
    }

    public class FlooringSizeDto
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public int M2 { get; set; }
    }

    public class FlooringImage
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string AlternateText { get; set; }
        public string Link { get; set; }
        public CarpetImageType ImageType { get; set; }
    }

    public enum CarpetImageType
    {
        Unknown = 0,
        MainImage = 1,
        Other = 2
    }


}
