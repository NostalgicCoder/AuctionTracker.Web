namespace AuctionTracker.Web.Models
{
    public class PricePerItem
    {
        public Int32 Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Postage { get; set; }
        public decimal ItemPrice { get; set; }
    }
}