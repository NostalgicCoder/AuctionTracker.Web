using AuctionTracker.Web.Class;
using AuctionTracker.Web.Models;
using FluentAssertions;

namespace AuctionTracker.Tests.Class
{
    [TestClass]
    public class CalculatePricesTests
    {
        private CalculatePrices _calculatePrices = new CalculatePrices();

        private List<Game> PriceTrendingUp()
        {
            List<Game> game = new List<Game>()
            {
                new Game() { Name = "Name", Condition = "high", Complete = "yes", SaleDate = new DateTime(2020, 1, 1), Price = 5.00m },
                new Game() { Name = "Name", Condition = "high", Complete = "yes", SaleDate = new DateTime(2020, 2, 1), Price = 5.50m },
                new Game() { Name = "Name", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2022, 1, 1), Price = 6.00m },
                new Game() { Name = "Name", Condition = "high", Complete = "yes", SaleDate = new DateTime(2022, 2, 1), Price = 6.50m },
                new Game() { Name = "Name", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2024, 1, 1), Price = 7.00m },
                new Game() { Name = "Name", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2024, 2, 1), Price = 7.50m },
            };

            return game;
        }

        private List<Game> PriceTrendingDown()
        {
            List<Game> game = new List<Game>()
            {
                new Game() { Name = "Name", Condition = "high", Complete = "yes", SaleDate = new DateTime(2020, 1, 1), Price = 7.50m },
                new Game() { Name = "Name", Condition = "high", Complete = "yes", SaleDate = new DateTime(2020, 2, 1), Price = 7.00m },
                new Game() { Name = "Name", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2022, 1, 1), Price = 6.50m },
                new Game() { Name = "Name", Condition = "high", Complete = "yes", SaleDate = new DateTime(2022, 2, 1), Price = 6.00m },
                new Game() { Name = "Name", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2024, 1, 1), Price = 5.50m },
                new Game() { Name = "Name", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2024, 2, 1), Price = 5.00m },
            };

            return game;
        }

        [TestMethod]
        public void CallSpotPriceTrendResultShouldBeGoingUp()
        {
            Product product = new Product();

            product.Game = PriceTrendingUp();

            string result = _calculatePrices.SpotPriceTrend(product);

            result.Should().Be("Going up");
        }

        [TestMethod]
        public void CallSpotPriceTrendResultShouldBeGoingDown()
        {
            Product product = new Product();

            product.Game = PriceTrendingDown();

            string result = _calculatePrices.SpotPriceTrend(product);

            result.Should().Be("Going down");
        }
    }
}