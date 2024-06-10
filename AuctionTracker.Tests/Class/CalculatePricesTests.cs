using AuctionTracker.Web.Class;
using FluentAssertions;
using AuctionTracker.Web.Models;
using AuctionTracker.Web.Interfaces;

namespace AuctionTracker.Tests.Class
{
    [TestClass]
    public class CalculatePricesTests
    {
        private ICalculatePrices _calculatePrices = new CalculatePrices();

        private Product BuildProductObj()
        {
            Product product = new Product();

            List<Game> game = new List<Game>()
            {
                new Game() { Name = "Doom", Condition = "high", Complete = "yes", SaleDate = new DateTime(2020, 1, 1), Price = 5.00m, Postage = 3.29m },
                new Game() { Name = "Doom", Condition = "mint", Complete = "yes", SaleDate = new DateTime(2020, 2, 1), Price = 5.50m, Postage = 3.39m  },
                new Game() { Name = "Doom", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2022, 1, 1), Price = 6.00m, Postage = 5.09m  },
                new Game() { Name = "Doom", Condition = "high", Complete = "yes", SaleDate = new DateTime(2022, 2, 1), Price = 6.50m, Postage = 3.29m  },
                new Game() { Name = "Doom", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2024, 1, 1), Price = 7.00m, Postage = 3.39m  },
                new Game() { Name = "Doom", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2024, 2, 1), Price = 7.50m, Postage = 2.10m  },
            };

            product.Game = game;

            return product;
        }

        [TestMethod]
        public void CallGetGamePricesResultShouldMatchExpectedAndPass()
        {
            Product result = _calculatePrices.GetGamePrices(BuildProductObj());

            result.Game.Count().Should().Be(6);
            result.MaxPrice.Should().Be(7.50m);
            result.MinPrice.Should().Be(5.00m);
            result.MinPostage.Should().Be(2.10m);
            result.MaxPostage.Should().Be(5.09m);
            result.MeanPrice.Should().Be(6.25m);
            result.MeanPostage.Should().Be(3.42m);
            result.MeanPriceAndPostage.Should().Be(9.67m);
            result.MedianPriceAndPostage.Should().Be(9.59m);
            result.MaxPriceAndPostage.Should().Be(12.59m);
            result.MinPriceAndPostage.Should().Be(7.10m);
        }
    }
}