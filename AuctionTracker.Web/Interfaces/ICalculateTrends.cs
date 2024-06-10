using AuctionTracker.Web.Models;

namespace AuctionTracker.Web.Interfaces
{
    public interface ICalculateTrends
    {
        string SpotPriceTrend(Product product, int caller);
        string CalcTrendResult(string trend, decimal newest, decimal oldest);
    }
}