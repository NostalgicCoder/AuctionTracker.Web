using AuctionTracker.Web.Class;
using AuctionTracker.Web.Interfaces;
using AuctionTracker.Web.Models;
using FluentAssertions;

namespace AuctionTracker.Tests.Class
{
    [TestClass]
    public class CalculateTrendsTests
    {
        private ICalculateTrends _calculateTrends = new CalculateTrends();

        #region Model Build Up - Test Cases

        private List<Game> GamePriceTrendingUp()
        {
            List<Game> game = new List<Game>()
            {
                new Game() { Name = "Name", Condition = "high", Complete = "yes", SaleDate = new DateTime(2020, 1, 1), Price = 5.00m },
                new Game() { Name = "Name", Condition = "mint", Complete = "yes", SaleDate = new DateTime(2020, 2, 1), Price = 5.50m },
                new Game() { Name = "Name", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2022, 1, 1), Price = 6.00m },
                new Game() { Name = "Name", Condition = "high", Complete = "yes", SaleDate = new DateTime(2022, 2, 1), Price = 6.50m },
                new Game() { Name = "Name", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2024, 1, 1), Price = 7.00m },
                new Game() { Name = "Name", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2024, 2, 1), Price = 7.50m },
            };

            return game;
        }

        private List<Game> GamePriceTrendingUpSmall()
        {
            List<Game> game = new List<Game>()
            {
                new Game() { Name = "Name", Condition = "high", Complete = "yes", SaleDate = new DateTime(2020, 1, 1), Price = 5.00m },
                new Game() { Name = "Name", Condition = "high", Complete = "yes", SaleDate = new DateTime(2024, 1, 1), Price = 7.50m },
            };

            return game;
        }

        private List<Game> GamePriceTrendingDown()
        {
            List<Game> game = new List<Game>()
            {
                new Game() { Name = "Name", Condition = "high", Complete = "yes", SaleDate = new DateTime(2020, 1, 1), Price = 7.50m },
                new Game() { Name = "Name", Condition = "mint", Complete = "yes", SaleDate = new DateTime(2020, 2, 1), Price = 7.00m },
                new Game() { Name = "Name", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2022, 1, 1), Price = 6.50m },
                new Game() { Name = "Name", Condition = "high", Complete = "yes", SaleDate = new DateTime(2022, 2, 1), Price = 6.00m },
                new Game() { Name = "Name", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2024, 1, 1), Price = 5.50m },
                new Game() { Name = "Name", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2024, 2, 1), Price = 5.00m },
            };

            return game;
        }

        private List<Game> GamePriceTrendingDownSmall()
        {
            List<Game> game = new List<Game>()
            {
                new Game() { Name = "Name", Condition = "high", Complete = "yes", SaleDate = new DateTime(2020, 1, 1), Price = 7.50m },
                new Game() { Name = "Name", Condition = "high", Complete = "yes", SaleDate = new DateTime(2024, 1, 1), Price = 5.00m },
            };

            return game;
        }

        private List<Toy> ToyPriceTrendingUp()
        {
            List<Toy> toy = new List<Toy>()
            {
                new Toy() { Name = "Name", Condition = "high", Complete = "yes", SaleDate = new DateTime(2020, 1, 1), Price = 5.00m },
                new Toy() { Name = "Name", Condition = "mint", Complete = "yes", SaleDate = new DateTime(2020, 2, 1), Price = 5.50m },
                new Toy() { Name = "Name", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2022, 1, 1), Price = 6.00m },
                new Toy() { Name = "Name", Condition = "high", Complete = "yes", SaleDate = new DateTime(2022, 2, 1), Price = 6.50m },
                new Toy() { Name = "Name", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2024, 1, 1), Price = 7.00m },
                new Toy() { Name = "Name", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2024, 2, 1), Price = 7.50m },
            };

            return toy;
        }

        private List<Toy> ToyPriceTrendingDown()
        {
            List<Toy> toy = new List<Toy>()
            {
                new Toy() { Name = "Name", Condition = "high", Complete = "yes", SaleDate = new DateTime(2020, 1, 1), Price = 7.50m },
                new Toy() { Name = "Name", Condition = "mint", Complete = "yes", SaleDate = new DateTime(2020, 2, 1), Price = 7.00m },
                new Toy() { Name = "Name", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2022, 1, 1), Price = 6.50m },
                new Toy() { Name = "Name", Condition = "high", Complete = "yes", SaleDate = new DateTime(2022, 2, 1), Price = 6.00m },
                new Toy() { Name = "Name", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2024, 1, 1), Price = 5.50m },
                new Toy() { Name = "Name", Condition = "medium", Complete = "yes", SaleDate = new DateTime(2024, 2, 1), Price = 5.00m },
            };

            return toy;
        }

        #endregion

        [TestMethod]
        public void CallSpotPriceTrendResultWithGameShouldBeGoingUp()
        {
            Product product = new Product();

            product.Game = GamePriceTrendingUp();

            string result = _calculateTrends.SpotPriceTrend(product, 1);

            result.Should().Be("Going up");
        }

        [TestMethod]
        public void CallSpotPriceTrendResultWithGameShouldBeGoingDown()
        {
            Product product = new Product();

            product.Game = GamePriceTrendingDown();

            string result = _calculateTrends.SpotPriceTrend(product, 1);

            result.Should().Be("Going down");
        }

        [TestMethod]
        public void CallSpotPriceTrendWithGame2ResultShouldBeGoingUp()
        {
            Product product = new Product();

            product.Game = GamePriceTrendingUpSmall();

            string result = _calculateTrends.SpotPriceTrend(product, 1);

            result.Should().Be("Going up");
        }

        [TestMethod]
        public void CallSpotPriceTrendWithGame2ResultShouldBeGoingDown()
        {
            Product product = new Product();

            product.Game = GamePriceTrendingDownSmall();

            string result = _calculateTrends.SpotPriceTrend(product, 1);

            result.Should().Be("Going down");
        }

        [TestMethod]
        public void CallSpotPriceTrendResultWithToyShouldBeGoingUp()
        {
            Product product = new Product();

            product.Toy = ToyPriceTrendingUp();

            string result = _calculateTrends.SpotPriceTrend(product, 2);

            result.Should().Be("Going up");
        }

        [TestMethod]
        public void CallSpotPriceTrendResultWithToyShouldBeGoingDown()
        {
            Product product = new Product();

            product.Toy = ToyPriceTrendingDown();

            string result = _calculateTrends.SpotPriceTrend(product, 2);

            result.Should().Be("Going down");
        }
    }
}