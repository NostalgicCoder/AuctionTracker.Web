using AuctionTracker.Web.Interfaces;
using AuctionTracker.Web.Models;

namespace AuctionTracker.Web.Class
{
    public class SortData : ISortData
    {
        /// <summary>
        /// Sort the GAME data by the order chosen by the user on the front end.  The default sort order changes dependant on if its the main or detailed game page call this function.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public Product SortGameOrder(Product product, Int32 caller)
        {
            switch (product.SelectedSortOrder)
            {
                case "PriceAsc":
                    product.Game = product.Game.OrderBy(x => x.Price);
                    break;
                case "PriceDsc":
                    product.Game = product.Game.OrderByDescending(x => x.Price);
                    break;
                case "Platform":
                    product.Game = product.Game.OrderBy(x => x.Platform);
                    break;
                case "Name":
                    product.Game = product.Game.OrderBy(x => x.Name);
                    break;
                case "DateAsc":
                    product.Game = product.Game.OrderBy(x => x.SaleDate);
                    break;
                case "DateDsc":
                    product.Game = product.Game.OrderByDescending(x => x.SaleDate);
                    break;
                case "Condition":
                    product.Game = product.Game.OrderBy(x => x.Condition);
                    break;
                case "Complete":
                    product.Game = product.Game.OrderByDescending(x => x.Complete);
                    break;
                case "Sealed":
                    product.Game = product.Game.OrderByDescending(x => x.Sealed);
                    break;
                default:
                    if (caller == 1)
                    {
                        product.Game = product.Game.OrderBy(x => x.Name);
                    }
                    else
                    {
                        product.Game = product.Game.OrderByDescending(x => x.SaleDate);
                    }
                    break;
            }

            return product;
        }

        /// <summary>
        /// Sort the TOY data by the order chosen by the user on the front end.  The default sort order changes dependant on if its the main or detailed game page call this function.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public Product SortToyOrder(Product product, Int32 caller)
        {
            switch (product.SelectedSortOrder)
            {
                case "PriceAsc":
                    product.Toy = product.Toy.OrderBy(x => x.Price);
                    break;
                case "PriceDsc":
                    product.Toy = product.Toy.OrderByDescending(x => x.Price);
                    break;
                case "Name":
                    product.Toy = product.Toy.OrderBy(x => x.Name);
                    break;
                case "DateAsc":
                    product.Toy = product.Toy.OrderBy(x => x.SaleDate);
                    break;
                case "DateDsc":
                    product.Toy = product.Toy.OrderByDescending(x => x.SaleDate);
                    break;
                case "Condition":
                    product.Toy = product.Toy.OrderBy(x => x.Condition);
                    break;
                case "Complete":
                    product.Toy = product.Toy.OrderByDescending(x => x.Complete);
                    break;
                case "Carded":
                    product.Toy = product.Toy.OrderByDescending(x => x.Carded);
                    break;
                case "Boxed":
                    product.Toy = product.Toy.OrderByDescending(x => x.Boxed);
                    break;
                case "Damaged":
                    product.Toy = product.Toy.OrderByDescending(x => x.Damaged);
                    break;
                case "DamagedAccessory":
                    product.Toy = product.Toy.OrderByDescending(x => x.DamagedAccessory);
                    break;
                default:
                    if (caller == 1)
                    {
                        product.Toy = product.Toy.OrderBy(x => x.Name);
                    }
                    else
                    {
                        product.Toy = product.Toy.OrderByDescending(x => x.SaleDate);
                    }
                    break;
            }

            return product;
        }
    }
}