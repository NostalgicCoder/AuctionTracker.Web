using AuctionTracker.Web.Class;
using AuctionTracker.Web.Interfaces;
using AuctionTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuctionTracker.Web.Controllers
{
    public class ToolsController : Controller
    {
        private IValidation _validation = new Validation();

        public IActionResult Index(PricePerItem? pricePerItem)
        {
            return View(pricePerItem);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CalculateItemPrice(PricePerItem pricePerItem)
        {
            bool pass = _validation.ValidatePricePerItem(ModelState, pricePerItem);

            if (ModelState.IsValid && pass)
            {
                pricePerItem.ItemPrice = (pricePerItem.Price + pricePerItem.Postage) / pricePerItem.Quantity;

                return RedirectToAction("Index", "Tools", pricePerItem);
            }

            return View("Index", pricePerItem);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CalculateStr(PricePerItem pricePerItem)
        {
            bool pass = _validation.ValidateSellThroughRate(ModelState, pricePerItem);

            if (ModelState.IsValid && pass)
            {
                pricePerItem.Str = ((decimal)pricePerItem.QuantitySoldThatMonth / pricePerItem.QuantityAvailableThatMonth) * 100;

                return RedirectToAction("Index", "Tools", pricePerItem);
            }

            return View("Index", pricePerItem);
        }
    }
}