using AuctionTracker.Web.Interfaces;
using AuctionTracker.Web.Models;
using IgdbApi.Lib;

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
            Igdb igdb = new Igdb();

            igdb.GetTwitchAccessToken();

            product.FullGameData = igdb.GetAllDataOnAGame(product.SelectedProduct, _generalHelper.ResolvePlatformId(product));

            return product;
        }
    }
}