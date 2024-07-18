using AuctionTracker.Web.Data;
using AuctionTracker.Web.Interfaces;
using AuctionTracker.Web.Models;

namespace AuctionTracker.Web.Class
{
    public class PopulateProductModel : IPopulateProductModel
    {
        private ICalculatePrices _calculatePrices = new CalculatePrices();
        private ISortData _sortData = new SortData();

        public Product PopulateGameModel(ApplicationDbContext db, Product product)
        {
            product.Game = db.Games.Where(x => x.Name == product.SelectedProduct);

            if (product.SelectedRow != null)
            {
                IEnumerable<Int32> selectedRows = product.SelectedRow.Where(x => x.IsSelected == true).Select(x => x.Id);

                if (selectedRows.Count() > 0)
                {
                    // Remove 'selectedRow' item from collection results
                    product.Game = product.Game.Where(x => !selectedRows.Contains(x.Id));
                }
            }

            if (product.SearchCondition)
            {
                product.Game = product.Game.Where(x => x.Condition.ToLower() == "high" || x.Condition.ToLower() == "mint");
            }

            if (product.SearchComplete)
            {
                product.Game = product.Game.Where(x => x.Complete.ToLower() == "yes");
            }

            if (product.SearchCurrentYear)
            {
                product.Game = product.Game.Where(x => x.SaleDate.Year == DateTime.Now.Year);
            }

            product = _sortData.SortGameOrder(product, 2);

            product = _calculatePrices.GetGamePrices(product);

            // Clear previous result set so the dynamic resizing of this object doesn't upset the view on a reload due to size mismatch
            product.SelectedRow = null;

            return product;
        }

        public Product PopulateToyModel(ApplicationDbContext db, Product product)
        {
            product.Toy = db.Toys.Where(x => x.ToyLine == product.SelectedProductLine && x.Name == product.SelectedProduct);

            if (product.SelectedRow != null)
            {
                IEnumerable<Int32> selectedRows = product.SelectedRow.Where(x => x.IsSelected == true).Select(x => x.Id);

                if (selectedRows.Count() > 0)
                {
                    // Remove 'selectedRow' item from collection results
                    product.Toy = product.Toy.Where(x => !selectedRows.Contains(x.Id));
                }
            }

            if (product.SearchCondition)
            {
                product.Toy = product.Toy.Where(x => x.Condition.ToLower() == "high" || x.Condition.ToLower() == "mint");
            }

            if (product.SearchComplete)
            {
                product.Toy = product.Toy.Where(x => x.Complete.ToLower() == "yes");
            }

            if (product.SearchCurrentYear)
            {
                product.Toy = product.Toy.Where(x => x.SaleDate.Year == DateTime.Now.Year);
            }

            if (product.SearchLoose)
            {
                product.Toy = product.Toy.Where(x => x.Carded == false && x.Boxed == false);
            }

            if (product.SearchCardedBoxed)
            {
                product.Toy = product.Toy.Where(x => x.Carded == true || x.Boxed == true);
            }

            product = _sortData.SortToyOrder(product, 2);

            product = _calculatePrices.GetToyPrices(product);

            // Clear previous result set so the dynamic resizing of this object doesn't upset the view on a reload due to size mismatch
            product.SelectedRow = null;

            return product;
        }
    }
}