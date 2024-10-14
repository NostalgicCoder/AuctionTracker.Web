using AuctionTracker.Web.Interfaces;
using AuctionTracker.Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AuctionTracker.Web.Class
{
    public class Validation : IValidation
    {
        private IGeneralHelper _generalHelper = new GeneralHelper();

        /// <summary>
        /// Verify the state of the 'ValidateSellThroughRate' user model (pass or fail) before sending it back to the action result
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="pricePerItem"></param>
        /// <returns></returns>
        public bool ValidateSellThroughRate(ModelStateDictionary modelState, PricePerItem pricePerItem)
        {
            bool pass = true;

            if (pricePerItem.QuantitySoldThatMonth == 0)
            {
                pass = false;
                modelState.AddModelError("Calculate", "No valid quantity sold that month.");
            }

            if (pricePerItem.QuantityAvailableThatMonth == 0)
            {
                pass = false;
                modelState.AddModelError("Calculate", "No valid quantity available that month.");
            }

            return pass;
        }

        /// <summary>
        /// Verify the state of the 'ValidatePricePerItem' user model (pass or fail) before sending it back to the action result
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="pricePerItem"></param>
        /// <returns></returns>
        public bool ValidatePricePerItem(ModelStateDictionary modelState, PricePerItem pricePerItem)
        {
            bool pass = true;

            if (pricePerItem.Quantity == 0)
            {
                pass = false;
                modelState.AddModelError("Calculate", "No valid quantity provided.");
            }

            if (pricePerItem.Price == 0.00m)
            {
                pass = false;
                modelState.AddModelError("Calculate", "No valid price provided.");
            }

            if (!_generalHelper.ValidDecimalNumber(pricePerItem.Price) || !_generalHelper.ValidDecimalNumber(pricePerItem.Postage))
            {
                pass = false;
                modelState.AddModelError("Create", "Price OR postage is not a number value.");
            }

            return pass;
        }

        /// <summary>
        /// Verify the state of the 'ValidateGame' user model (pass or fail) before sending it back to the action result
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public bool ValidateGame(ModelStateDictionary modelState, Game game)
        {
            bool pass = true;

            if (game.Condition == "Please select one")
            {
                pass = false;
                modelState.AddModelError("Create", "No 'Condition' provided.");
            }

            if (game.Complete == "Please select one")
            {
                pass = false;
                modelState.AddModelError("Create", "No 'Complete' provided.");
            }

            if (game.Price == 0.00m)
            {
                pass = false;
                modelState.AddModelError("Create", "No valid price provided.");
            }

            if (!_generalHelper.ValidDecimalNumber(game.Price) || !_generalHelper.ValidDecimalNumber(game.Postage))
            {
                pass = false;
                modelState.AddModelError("Create", "Price OR postage is not a number value.");
            }

            if (game.Platform == "Please select one")
            {
                pass = false;
                modelState.AddModelError("Create", "No 'Platform' provided.");
            }

            if (game.MediaType == "Please select one")
            {
                pass = false;
                modelState.AddModelError("Create", "No 'MediaType' provided.");
            }

            return pass;
        }

        /// <summary>
        /// Verify the state of the 'ValidateToy' user model (pass or fail) before sending it back to the action result
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="toy"></param>
        /// <returns></returns>
        public bool ValidateToy(ModelStateDictionary modelState, Toy toy)
        {
            bool pass = true;

            if (string.IsNullOrEmpty(toy.ToyLine) || toy.ToyLine == "Please select one")
            {
                pass = false;
                modelState.AddModelError("Create", "No 'Toyline' selected OR added");
            }

            if (toy.Price == 0.00m)
            {
                pass = false;
                modelState.AddModelError("Create", "No valid price provided.");
            }

            if (!_generalHelper.ValidDecimalNumber(toy.Price) || !_generalHelper.ValidDecimalNumber(toy.Postage))
            {
                pass = false;
                modelState.AddModelError("Create", "Price OR postage is not a number value.");
            }

            if (toy.Condition == "Please select one")
            {
                pass = false;
                modelState.AddModelError("Create", "No 'Condition' provided.");
            }

            if (toy.Complete == "Please select one")
            {
                pass = false;
                modelState.AddModelError("Create", "No 'Complete' provided.");
            }

            if (toy.Damaged == "Please select one")
            {
                pass = false;
                modelState.AddModelError("Create", "No 'Damaged' provided.");
            }

            if (toy.ToyLine.ToLower() != "motu")
            {
                toy.Stands = (toy.Stands == "Please select one") ? "Yes" : toy.Stands;
            }
            else
            {
                if (toy.Stands == "Please select one")
                {
                    pass = false;
                    modelState.AddModelError("Create", "No 'Stands' provided.");
                }
            }

            if (toy.ToyLine.ToLower() != "mimp")
            {
                toy.Colour = (toy.Colour == "Please select one") ? string.Empty : toy.Colour;
            }
            else
            {
                if (toy.Colour == "Please select one")
                {
                    pass = false;
                    modelState.AddModelError("Create", "No 'Colour' provided.");
                }
            }

            toy.DamagedAccessory = (toy.DamagedAccessory == "Please select one") ? "No" : toy.DamagedAccessory;

            return pass;
        }
    }
}