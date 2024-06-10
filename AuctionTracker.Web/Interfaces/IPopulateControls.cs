using AuctionTracker.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuctionTracker.Web.Interfaces
{
    public interface IPopulateControls
    {
        List<SelectListItem> GenerateToyLineListItems(ApplicationDbContext db);
    }
}