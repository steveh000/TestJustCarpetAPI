using JustCarpet.Api.Enums;

namespace JustCarpet.Api.Models
{
    public class Search
    {
        public bool Pets { get; set; }
        public CarpetStyleEnum Style { get; set; }
        public int Budget { get; set; }
        public bool Hardwearing { get; set; }
        public bool SkipSearchParameters { get; set; }
    }
}
