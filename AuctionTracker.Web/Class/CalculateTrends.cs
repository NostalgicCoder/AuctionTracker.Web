using AuctionTracker.Web.Interfaces;
using AuctionTracker.Web.Models;

namespace AuctionTracker.Web.Class
{
    public class CalculateTrends : ICalculateTrends
    {
        private string _trendUp = "Going up";
        private string _trendDown = "Going down";
        private string _trendSame = "Staying the same";

        /// <summary>
        /// Analyse the search results and try and spot a trend for the price: up, down, same or not enough data.  
        /// - The results for this are not full proof as sometimes the data can just be erratic due to condition, completeness of a item affecting its price.
        /// - TODO: May be able to improve code reuse with this later by using generics on IENUMERABLE object for game/toy
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public string SpotPriceTrend(Product product, int caller)
        {
            string trend = "Not enough data / No trend can be observed";

            decimal newest;
            decimal oldest;
            decimal price1;
            decimal price2;
            decimal price3;

            switch (caller)
            {
                case 1:
                    List<Game> gameResult = product.Game.Where(x => (x.Condition.ToLower() == "mint" || x.Condition.ToLower() == "high" || x.Condition.ToLower() == "medium") && x.Complete.ToLower() == "yes").OrderByDescending(x => x.SaleDate).ToList();

                    if (gameResult.Count() == 2)
                    {
                        if (gameResult[0].SaleDate != gameResult[1].SaleDate)
                        {
                            newest = gameResult.OrderByDescending(x => x.SaleDate).Select(x => x.Price).FirstOrDefault();
                            oldest = gameResult.OrderBy(x => x.SaleDate).Select(x => x.Price).FirstOrDefault();

                            trend = CalcTrendResult(trend, newest, oldest);
                        }
                    }

                    if (gameResult.Count() > 2)
                    {
                        price1 = gameResult[0].Price;
                        price2 = gameResult[1].Price;
                        price3 = gameResult[2].Price;

                        trend = CalcTrendResult(trend, price1, price2, price3);
                    }
                    break;
                case 2:
                    List<Toy> toyResult = product.Toy.Where(x => (x.Condition.ToLower() == "mint" || x.Condition.ToLower() == "high" || x.Condition.ToLower() == "medium") && x.Complete.ToLower() == "yes").OrderByDescending(x => x.SaleDate).ToList();

                    if (toyResult.Count() == 2)
                    {
                        if (toyResult[0].SaleDate != toyResult[1].SaleDate)
                        {
                            newest = toyResult.OrderByDescending(x => x.SaleDate).Select(x => x.Price).FirstOrDefault();
                            oldest = toyResult.OrderBy(x => x.SaleDate).Select(x => x.Price).FirstOrDefault();

                            trend = CalcTrendResult(trend, newest, oldest);
                        }
                    }

                    if (toyResult.Count() > 2)
                    {
                        price1 = toyResult[0].Price;
                        price2 = toyResult[1].Price;
                        price3 = toyResult[2].Price;

                        trend = CalcTrendResult(trend, price1, price2, price3);
                    }
                    break;
            }

            return trend;
        }

        /// <summary>
        /// Spot trend by comparing the values
        /// </summary>
        /// <param name="trend"></param>
        /// <param name="newest"></param>
        /// <param name="oldest"></param>
        /// <returns></returns>
        public string CalcTrendResult(string trend, decimal newest, decimal oldest)
        {
            if (oldest > newest)
            {
                trend = _trendDown;
            }

            if (newest > oldest)
            {
                trend = _trendUp;
            }

            if (oldest == newest)
            {
                trend = _trendSame;
            }

            return trend;
        }

        /// <summary>
        /// Spot trend by comparing the values
        /// </summary>
        /// <param name="trend"></param>
        /// <param name="price1"></param>
        /// <param name="price2"></param>
        /// <param name="price3"></param>
        /// <returns></returns>
        private string CalcTrendResult(string trend, decimal price1, decimal price2, decimal price3)
        {
            if (price1 > price2 && price1 > price3)
            {
                trend = _trendUp;
            }

            if (price1 < price2 && price1 < price3)
            {
                trend = _trendDown;
            }

            if (price1 == price2 && price1 == price3)
            {
                trend = _trendSame;
            }

            // Less accurate trend scenarios - Below:

            if (price3 > price2 && price3 > price1)
            {
                trend = _trendDown;
            }

            if (price3 < price2 && price3 < price1)
            {
                trend = _trendUp;
            }

            return trend;
        }
    }
}