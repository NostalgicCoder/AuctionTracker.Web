using AuctionTracker.Web.Models;

namespace AuctionTracker.Web.Class
{
    public class CalculatePrices
    {
        public Product GetGamePrices(Product product)
        {
            product.MeanPrice = Math.Round((product.Game.Sum(x => x.Price) / product.Game.Count()), 2);
            product.MeanPostage = Math.Round((product.Game.Sum(x => x.Postage) / product.Game.Count()), 2);

            Int32 middle = 0;

            if (product.Game.Count() % 2 == 0) // Even - 2,4,6,8 etc
            {
                middle = (product.Game.Count() / 2) - 1; // Subtract one due to arrays starting at '0'

                Int32 pos1 = middle;
                Int32 pos2 = middle + 1;

                product.MedianPrice = Math.Round((product.Game.OrderBy(x => x.Price).ToList()[pos1].Price + product.Game.OrderBy(x => x.Price).ToList()[pos2].Price) / 2, 2);
                product.MedianPostage = Math.Round((product.Game.OrderBy(x => x.Postage).ToList()[pos1].Postage + product.Game.OrderBy(x => x.Postage).ToList()[pos2].Postage) / 2, 2);
            }
            else // Odd - 1,3,5,7 etc
            {
                middle = (product.Game.Count() / 2);
                product.MedianPrice = Math.Round(product.Game.OrderBy(x => x.Price).ToList()[middle].Price, 2);
                product.MedianPostage = Math.Round(product.Game.OrderBy(x => x.Postage).ToList()[middle].Postage, 2);
            }

            product.MaxPrice = product.Game.Select(x => x.Price).Max();
            product.MinPrice = product.Game.Select(x => x.Price).Min();

            if (product.Game.Where(x => x.SaleDate.Year == DateTime.Now.Year).Count() > 1)
            {
                product.MaxPriceCurrentYear = product.Game.Where(x => x.SaleDate.Year == DateTime.Now.Year).Select(x => x.Price).Max();
                product.MinPriceCurrentYear = product.Game.Where(x => x.SaleDate.Year == DateTime.Now.Year).Select(x => x.Price).Min();
            }

            product.MaxPostage = product.Game.Select(x => x.Postage).Max();
            product.MinPostage = product.Game.Select(x => x.Postage).Min();

            return product;
        }

        public Product GetToyPrices(Product product)
        {
            product.MeanPrice = Math.Round((product.Toy.Sum(x => x.Price) / product.Toy.Count()), 2);
            product.MeanPostage = Math.Round((product.Toy.Sum(x => x.Postage) / product.Toy.Count()), 2);

            Int32 middle = 0;

            if (product.Toy.Count() % 2 == 0) // Even - 2,4,6,8 etc
            {
                middle = (product.Toy.Count() / 2) - 1; // Subtract one due to arrays starting at '0'

                Int32 pos1 = middle;
                Int32 pos2 = middle + 1;

                product.MedianPrice = Math.Round((product.Toy.OrderBy(x => x.Price).ToList()[pos1].Price + product.Toy.OrderBy(x => x.Price).ToList()[pos2].Price) / 2, 2);
                product.MedianPostage = Math.Round((product.Toy.OrderBy(x => x.Postage).ToList()[pos1].Postage + product.Toy.OrderBy(x => x.Postage).ToList()[pos2].Postage) / 2, 2);
            }
            else // Odd - 1,3,5,7 etc
            {
                middle = (product.Toy.Count() / 2);
                product.MedianPrice = Math.Round(product.Toy.OrderBy(x => x.Price).ToList()[middle].Price, 2);
                product.MedianPostage = Math.Round(product.Toy.OrderBy(x => x.Postage).ToList()[middle].Postage, 2);
            }

            product.MaxPrice = product.Toy.Select(x => x.Price).Max();
            product.MinPrice = product.Toy.Select(x => x.Price).Min();

            if (product.Toy.Where(x => x.SaleDate.Year == DateTime.Now.Year).Count() > 1)
            {
                product.MaxPriceCurrentYear = product.Toy.Where(x => x.SaleDate.Year == DateTime.Now.Year).Select(x => x.Price).Max();
                product.MinPriceCurrentYear = product.Toy.Where(x => x.SaleDate.Year == DateTime.Now.Year).Select(x => x.Price).Min();
            }

            product.MaxPostage = product.Toy.Select(x => x.Postage).Max();
            product.MinPostage = product.Toy.Select(x => x.Postage).Min();

            return product;
        }
    }
}
