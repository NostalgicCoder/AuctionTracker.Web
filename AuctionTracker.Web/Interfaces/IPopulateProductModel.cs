using AuctionTracker.Web.Data;
using AuctionTracker.Web.Models;

namespace AuctionTracker.Web.Interfaces
{
    public interface IPopulateProductModel
    {
        Product PopulateGameModel(ApplicationDbContext db, Product product);
        Product PopulateToyModel(ApplicationDbContext db, Product product);
    }
}