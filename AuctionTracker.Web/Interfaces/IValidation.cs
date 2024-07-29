using AuctionTracker.Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AuctionTracker.Web.Interfaces
{
    public interface IValidation
    {
        bool ValidateGame(ModelStateDictionary modelState, Game game);
        bool ValidateToy(ModelStateDictionary modelState, Toy toy);

        bool ValidatePricePerItem(ModelStateDictionary modelState, PricePerItem pricePerItem);
        bool ValidateSellThroughRate(ModelStateDictionary modelState, PricePerItem pricePerItem);
    }
}