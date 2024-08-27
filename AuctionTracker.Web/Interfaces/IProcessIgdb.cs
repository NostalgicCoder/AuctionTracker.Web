using AuctionTracker.Web.Models;

namespace AuctionTracker.Web.Interfaces
{
    public interface IProcessIgdb
    {
        Product CallIgdb(Product product);
    }
}