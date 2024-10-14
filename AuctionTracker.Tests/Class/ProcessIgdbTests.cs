using AuctionTracker.Web.Class;
using AuctionTracker.Web.Interfaces;
using AuctionTracker.Web.Models;
using FluentAssertions;

namespace AuctionTracker.Tests.Class
{
    [TestClass]
    public class ProcessIgdbTests
    {
        private IProcessIgdb _processIgdb = new ProcessIgdb();

        [TestMethod]
        public void CallIgdbIfAPIDownResultShouldBeNull()
        {
            Product product = new Product();

            product = _processIgdb.CallIgdb(product);

            product.FullGameData.Game.Should().BeNull();
        }

        [TestMethod]
        public void CallIgdbIfAPIUpResultShouldNotBeNull()
        {
            Product product = new Product();
            product.SelectedProduct = "Deus Ex";
            product.SelectedGamePlatform = "PC";

            product = _processIgdb.CallIgdb(product);

            product.FullGameData.Should().NotBeNull();
            product.FullGameData.Game.id.Should().Be(41);
            product.FullGameData.LargeCoverUrl.Should().Be("//images.igdb.com/igdb/image/upload/t_cover_big/co86dk.jpg");
        }
    }
}