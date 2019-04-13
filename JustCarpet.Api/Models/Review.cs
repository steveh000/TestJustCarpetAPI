namespace JustCarpet.Api.Models
{
    public class Review
    {
        // rates between 1 - 10 10 being best
        public int PunctualityFactor { get; set; }
        public int TimeKeepingFactor { get; set; }
        public int CleanupFactor { get; set; }
        public int QualityFactor { get; set; }
        public string Comments { get; set; }
        public int CustomerOrderId { get; set; }
    }
}
