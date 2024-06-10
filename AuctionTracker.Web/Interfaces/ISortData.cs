using AuctionTracker.Web.Models;

namespace AuctionTracker.Web.Interfaces
{
    public interface ISortData
    {
        Product SortGameOrder(Product product, Int32 caller);
        Product SortToyOrder(Product product, Int32 caller);
    }
}