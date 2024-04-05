using AuctionTracker.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuctionTracker.Web.Class
{
    public class PopulateControls
    {
        public List<SelectListItem> GenerateToyLineListItems(ApplicationDbContext db)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            var toyLines = db.Toys.Select(x => x.ToyLine).Distinct();

            foreach (var x in toyLines)
            {
                // Doing manual mapping here as a LAMBDA expression cannot be used directly on the db entity to create this (As wont translate to a SQL equivalent)
                result.Add(new SelectListItem() { Text = x, Value = x });
            }

            result.Add(new SelectListItem() { Text = "Add a new option", Value = null });

            return result;
        }
    }
}
