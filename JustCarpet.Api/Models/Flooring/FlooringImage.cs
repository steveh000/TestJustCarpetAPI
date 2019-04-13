using JustCarpet.Api.Enums;

namespace JustCarpet.Api.Models.Flooring
{
    public class FlooringImage
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string AlternateText { get; set; }
        public string Link { get; set; }
        public CarpetImageTypeEnum ImageType { get; set; }
    }


}
