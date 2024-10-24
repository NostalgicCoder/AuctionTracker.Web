﻿using AuctionTracker.Web.Interfaces;
using AuctionTracker.Web.Models;
using IgdbApi.Lib;
using IgdbApi.Lib.Interfaces;

namespace AuctionTracker.Web.Class
{
    public class ProcessIgdb : IProcessIgdb
    {
        private IGeneralHelper _generalHelper = new GeneralHelper();

        /// <summary>
        /// Call IGDB REST API acquire token and populate 'FullGameData' item inside 'Product' object
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Product CallIgdb(Product product)
        {
            IIgdb igdb = new Igdb();

            // REQUIREMENT - Provide your own Twitch Developer Portal 'clientId' and 'clientSecret' below in order for the IGDB calls to work.
            igdb.GetTwitchAccessToken("YourClientId", "YourClientSecret");

            product.FullGameData = igdb.GetAllDataOnAGame(product.SelectedProduct, _generalHelper.ResolvePlatformId(product));

            return product;
        }
    }
}