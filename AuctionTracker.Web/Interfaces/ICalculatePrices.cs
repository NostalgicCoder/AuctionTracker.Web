using AuctionTracker.Web.Models;

namespace AuctionTracker.Web.Interfaces
{
    public interface ICalculatePrices
    {
        Product GetGamePrices(Product product);
        Product GetToyPrices(Product product);
    }
}