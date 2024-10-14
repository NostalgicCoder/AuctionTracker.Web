using AuctionTracker.Web.Data;
using AuctionTracker.Web.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuctionTracker.Web.Class
{
    public class PopulateControls : IPopulateControls
    {
        /// <summary>
        /// Convert the individual toy results into a distinct list of toylines that are found inside the database, these will later be used to populate controls
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public List<SelectListItem> GenerateToyLineListItems(ApplicationDbContext db)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            List<string> toyLines = db.Toys.Select(x => x.ToyLine).Distinct().ToList();

            foreach (string name in toyLines)
            {
                // Doing manual mapping here as a LAMBDA expression cannot be used directly on the db entity to create this (As wont translate to a SQL equivalent)
                result.Add(new SelectListItem() { Text = name, Value = name });
            }

            result.Add(new SelectListItem() { Text = "Add a new option", Value = null });

            return result;
        }
    }
}