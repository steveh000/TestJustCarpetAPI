namespace JustCarpet.Api.Models.Orders
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int CarpetId { get; set; }
        public int CarpetSizeOptionId { get; set; }
        public int Qty { get; set; }
        public int CustomerOrderId { get; set; }



    }
}
