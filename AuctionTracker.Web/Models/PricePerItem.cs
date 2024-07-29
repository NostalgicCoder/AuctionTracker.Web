namespace AuctionTracker.Web.Models
{
    public class PricePerItem
    {
        // Price Per Item:
        public Int32 Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Postage { get; set; }
        public decimal ItemPrice { get; set; }


        // Sell Through Rate:
        public Int32 QuantityAvailableThatMonth { get; set; }
        public Int32 QuantitySoldThatMonth { get; set; }
        public decimal Str { get; set; }
    }
}