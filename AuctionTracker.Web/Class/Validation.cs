﻿using AuctionTracker.Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AuctionTracker.Web.Class
{
    public class Validation
    {
        private GeneralHelper _generalHelper = new GeneralHelper();

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
