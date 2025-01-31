﻿using AuctionTracker.Web.Interfaces;
using AuctionTracker.Web.Models;

namespace AuctionTracker.Web.Class
{
    public class CalculatePrices : ICalculatePrices
    {
        private ICalculateTrends _calculateTrends = new CalculateTrends();

        /// <summary>
        /// Calculate prices associated with the current game thats been selected by the user
        /// - TODO: See if this code can be optimised to use better code reuse via introduction of generics to bypass the unique custom object issue with the queries
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Product GetGamePrices(Product product)
        {
            product.PriceAccuracy = GetPriceAccuracy(product.Game.Count());

            if (product.Game.Count() != 0)
            {
                product.MeanPrice = Math.Round((product.Game.Sum(x => x.Price) / product.Game.Count()), 2);
                product.MeanPostage = Math.Round((product.Game.Sum(x => x.Postage) / product.Game.Count()), 2);
                product.MeanPriceAndPostage = (product.MeanPrice + product.MeanPostage);

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

                product.MedianPriceAndPostage = (product.MedianPrice + product.MedianPostage);
                product.MaxPrice = product.Game.Select(x => x.Price).Max();
                product.MinPrice = product.Game.Select(x => x.Price).Min();

                if (product.Game.Where(x => x.SaleDate.Year == DateTime.Now.Year).Count() > 1)
                {
                    product.MaxPriceCurrentYear = product.Game.Where(x => x.SaleDate.Year == DateTime.Now.Year).Select(x => x.Price).Max();
                    product.MinPriceCurrentYear = product.Game.Where(x => x.SaleDate.Year == DateTime.Now.Year).Select(x => x.Price).Min();

                    product.MaxPostageCurrentYear = product.Game.Where(x => x.SaleDate.Year == DateTime.Now.Year).Select(x => x.Postage).Max();
                    product.MinPostageCurrentYear = product.Game.Where(x => x.SaleDate.Year == DateTime.Now.Year).Select(x => x.Postage).Min();

                    List<decimal> PriceAndPostageCollCurrentYear = new List<decimal>();

                    foreach (Game x in product.Game.Where(x => x.SaleDate.Year == DateTime.Now.Year).OrderBy(x => x.SaleDate))
                    {
                        PriceAndPostageCollCurrentYear.Add(x.Price + x.Postage);
                    }

                    product.MaxPriceAndPostageCurrentYear = PriceAndPostageCollCurrentYear.Max();
                    product.MinPriceAndPostageCurrentYear = PriceAndPostageCollCurrentYear.Min();
                }

                product.MaxPostage = product.Game.Select(x => x.Postage).Max();
                product.MinPostage = product.Game.Select(x => x.Postage).Min();

                List<decimal> PriceAndPostageColl = new List<decimal>();

                foreach (Game x in product.Game.OrderBy(x => x.SaleDate))
                {
                    PriceAndPostageColl.Add(x.Price + x.Postage);

                    product.LineGraph.Add(new GraphModel
                    {
                        SaleDate = x.SaleDate.ToShortDateString(),
                        Price = (x.Price + x.Postage)
                    });
                }

                product.MaxPriceAndPostage = PriceAndPostageColl.Max();
                product.MinPriceAndPostage = PriceAndPostageColl.Min();

                product.Trend = _calculateTrends.SpotPriceTrend(product, 1);
            }

            return product;
        }

        /// <summary>
        /// Calculate prices associated with the current toy thats been selected by the user
        /// - TODO: See if this code can be optimised to use better code reuse via introduction of generics to bypass the unique custom object issue with the queries
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Product GetToyPrices(Product product)
        {
            product.PriceAccuracy = GetPriceAccuracy(product.Toy.Count());

            if (product.Toy.Count() != 0)
            {
                product.MeanPrice = Math.Round((product.Toy.Sum(x => x.Price) / product.Toy.Count()), 2);
                product.MeanPostage = Math.Round((product.Toy.Sum(x => x.Postage) / product.Toy.Count()), 2);
                product.MeanPriceAndPostage = (product.MeanPrice + product.MeanPostage);

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

                product.MedianPriceAndPostage = (product.MedianPrice + product.MedianPostage);
                product.MaxPrice = product.Toy.Select(x => x.Price).Max();
                product.MinPrice = product.Toy.Select(x => x.Price).Min();

                if (product.Toy.Where(x => x.SaleDate.Year == DateTime.Now.Year).Count() > 1)
                {
                    product.MaxPriceCurrentYear = product.Toy.Where(x => x.SaleDate.Year == DateTime.Now.Year).Select(x => x.Price).Max();
                    product.MinPriceCurrentYear = product.Toy.Where(x => x.SaleDate.Year == DateTime.Now.Year).Select(x => x.Price).Min();

                    product.MaxPostageCurrentYear = product.Toy.Where(x => x.SaleDate.Year == DateTime.Now.Year).Select(x => x.Postage).Max();
                    product.MinPostageCurrentYear = product.Toy.Where(x => x.SaleDate.Year == DateTime.Now.Year).Select(x => x.Postage).Min();

                    List<decimal> PriceAndPostageCollCurrentYear = new List<decimal>();

                    foreach (Toy x in product.Toy.Where(x => x.SaleDate.Year == DateTime.Now.Year).OrderBy(x => x.SaleDate))
                    {
                        PriceAndPostageCollCurrentYear.Add(x.Price + x.Postage);
                    }

                    product.MaxPriceAndPostageCurrentYear = PriceAndPostageCollCurrentYear.Max();
                    product.MinPriceAndPostageCurrentYear = PriceAndPostageCollCurrentYear.Min(); 
                }

                product.MaxPostage = product.Toy.Select(x => x.Postage).Max();
                product.MinPostage = product.Toy.Select(x => x.Postage).Min();

                List<decimal> PriceAndPostageColl = new List<decimal>();

                foreach (Toy x in product.Toy.OrderBy(x => x.SaleDate))
                {
                    PriceAndPostageColl.Add(x.Price + x.Postage);

                    product.LineGraph.Add(new GraphModel
                    {
                        SaleDate = x.SaleDate.ToShortDateString(),
                        Price = (x.Price + x.Postage)
                    });
                }

                product.MaxPriceAndPostage = PriceAndPostageColl.Max();
                product.MinPriceAndPostage = PriceAndPostageColl.Min();

                product.Trend = _calculateTrends.SpotPriceTrend(product, 2);
            }

            return product;
        }

        /// <summary>
        /// Get the accuracy of a price based on the number of results available for analysis
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public string GetPriceAccuracy(Int32 count)
        {
            // TODO - Get this to also look at the age of results to factor in accuracy

            if(count == 0)
            {
                return "No Results";
            }
            else if (count > 0 && count <= 5)
            {
                return "Low Accuracy";
            }
            else if (count > 5 && count <= 20)
            {
                return "Medium Accuracy";
            }

            return "High Accuracy";
        }
    }
}