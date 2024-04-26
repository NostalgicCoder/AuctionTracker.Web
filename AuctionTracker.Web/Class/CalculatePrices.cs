using AuctionTracker.Web.Models;

namespace AuctionTracker.Web.Class
{
    public class CalculatePrices
    {
        public Product GetGamePrices(Product product)
        {
            if (product.Game.Count() != 0)
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

                SpotPriceTrend(product);
            }

            return product;
        }

        public Product GetToyPrices(Product product)
        {
            if (product.Toy.Count() != 0)
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
            }

            return product;
        }

        /// <summary>
        /// Analyse the search results and try and spot a trend for the price: up, down, same or not enough data.  
        /// - The results for this are not full proof as sometimes the data can just be erratic due to condition, completeness of a item affecting its price.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public string SpotPriceTrend(Product product)
        {
            string trend = "Not enough data";

            if (product.Game.Where(x => (x.Condition.ToLower() == "high" || x.Condition.ToLower() == "medium") && x.Complete.ToLower() == "yes").Count() == 2)
            {
                decimal newest = product.Game.OrderByDescending(x => x.SaleDate).Select(x => x.Price).FirstOrDefault();
                decimal oldest = product.Game.OrderBy(x => x.SaleDate).Select(x => x.Price).FirstOrDefault();

                if (oldest > newest)
                {
                    trend = "Going down";
                }

                if (newest > oldest)
                {
                    trend = "Going up";
                }

                if (oldest == newest)
                {
                    trend = "Staying the same";
                }
            }

            if(product.Game.Where(x => (x.Condition.ToLower() == "high" || x.Condition.ToLower() == "medium") && x.Complete.ToLower() == "yes").Count() > 2)
            {
                List<Game> result = product.Game.Where(x => (x.Condition.ToLower() == "high" || x.Condition.ToLower() == "medium") && x.Complete.ToLower() == "yes").OrderByDescending(x => x.SaleDate).ToList();

                decimal price1 = result[0].Price;
                decimal price2 = result[1].Price;
                decimal price3 = result[2].Price;

                if(price1 > price2 && price1 > price3)
                {
                    trend = "Going up";
                }

                if(price1 < price2 && price1 < price3)
                {
                    trend = "Going down";
                }

                if(price1 == price2 && price1 == price3)
                {
                    trend = "Staying the same";
                }
            }

            return trend;
        }
    }
}