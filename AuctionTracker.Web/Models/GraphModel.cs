using AuctionTracker.Web.Interfaces;

namespace AuctionTracker.Web.Models
{
    public class GraphModel : IGraphModel
    {
        public string SaleDate { get; set; }
        public decimal Price { get; set; }
    }
}