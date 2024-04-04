using AuctionTracker.Web.Class;
using AuctionTracker.Web.Data;
using AuctionTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuctionTracker.Web.Controllers
{
    public class ToyController : Controller
    {
        private readonly ApplicationDbContext _db;

        private CalculatePrices _calculatePrices = new CalculatePrices();

        /// <summary>
        /// Constructor
        /// </summary>
        public ToyController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(Product? product)
        {
            if (string.IsNullOrEmpty(product.SelectedProductLine) || product.SelectedProductLine == "Please select one")
            {
                product.Toy = _db.Toys.OrderBy(x => x.Name).OrderBy(x => x.Name);

                return View(product);
            }
            else
            {
                // Debated over adding new sort options for 'Toy' items but most of the unique new fields are unlikely to be searched against.  Can be added in if required later
                switch (product.SelectedSortOrder)
                {
                    case "PriceAsc":
                        product.Toy = _db.Toys.Where(x => x.ToyLine == product.SelectedProductLine).OrderBy(x => x.Price);
                        break;
                    case "PriceDsc":
                        product.Toy = _db.Toys.Where(x => x.ToyLine == product.SelectedProductLine).OrderByDescending(x => x.Price);
                        break;
                    case "Name":
                        product.Toy = _db.Toys.Where(x => x.ToyLine == product.SelectedProductLine).OrderBy(x => x.Name);
                        break;
                    case "DateAsc":
                        product.Toy = _db.Toys.Where(x => x.ToyLine == product.SelectedProductLine).OrderBy(x => x.SaleDate);
                        break;
                    case "DateDsc":
                        product.Toy = _db.Toys.Where(x => x.ToyLine == product.SelectedProductLine).OrderByDescending(x => x.SaleDate);
                        break;
                    case "Condition":
                        product.Toy = _db.Toys.Where(x => x.ToyLine == product.SelectedProductLine).OrderBy(x => x.Condition);
                        break;
                    case "Complete":
                        product.Toy = _db.Toys.Where(x => x.ToyLine == product.SelectedProductLine).OrderByDescending(x => x.Complete);
                        break;
                    default:
                        product.Toy = _db.Toys.Where(x => x.ToyLine == product.SelectedProductLine).OrderBy(x => x.Name);
                        break;
                }

                return View("SelectedToyLine", product);
            }
        }

        public IActionResult ToyInfo(string? valToyName, string? valToyLine, string? valToySortOrder)
        {
            Product product = new Product();

            product.SelectedProduct = valToyName;
            product.SelectedProductLine = valToyLine;

            // Debated over adding new sort options for 'Toy' items but most of the unique new fields are unlikely to be searched against.  Can be added in if required later
            switch (valToySortOrder)
            {
                case "PriceAsc":
                    product.Toy = _db.Toys.Where(x => x.ToyLine == product.SelectedProductLine && x.Name == product.SelectedProduct).OrderBy(x => x.Price);
                    break;
                case "PriceDsc":
                    product.Toy = _db.Toys.Where(x => x.ToyLine == product.SelectedProductLine && x.Name == product.SelectedProduct).OrderByDescending(x => x.Price);
                    break;
                case "Name":
                    product.Toy = _db.Toys.Where(x => x.ToyLine == product.SelectedProductLine && x.Name == product.SelectedProduct).OrderBy(x => x.Name);
                    break;
                case "DateAsc":
                    product.Toy = _db.Toys.Where(x => x.ToyLine == product.SelectedProductLine && x.Name == product.SelectedProduct).OrderBy(x => x.SaleDate);
                    break;
                case "DateDsc":
                    product.Toy = _db.Toys.Where(x => x.ToyLine == product.SelectedProductLine && x.Name == product.SelectedProduct).OrderByDescending(x => x.SaleDate);
                    break;
                case "Condition":
                    product.Toy = _db.Toys.Where(x => x.ToyLine == product.SelectedProductLine && x.Name == product.SelectedProduct).OrderBy(x => x.Condition);
                    break;
                case "Complete":
                    product.Toy = _db.Toys.Where(x => x.ToyLine == product.SelectedProductLine && x.Name == product.SelectedProduct).OrderByDescending(x => x.Complete);
                    break;
                default:
                    product.Toy = _db.Toys.Where(x => x.ToyLine == product.SelectedProductLine && x.Name == product.SelectedProduct).OrderBy(x => x.Name);
                    break;
            }

            product = _calculatePrices.GetToyPrices(product);

            return View(product);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToyInfoSortOrder(Product product)
        {
            return RedirectToAction("ToyInfo", "Toy", new { valToyName = product.SelectedProduct, valToyLine = product.SelectedProductLine, valToySortOrder = product.SelectedSortOrder });
        }

        #region Create

        //GET
        public IActionResult Create()
        {
            Toy toy = new Toy();

            var toyLines = _db.Toys.Select(x => x.ToyLine).Distinct();

            foreach (var x in toyLines)
            {
                toy.ToyLineLst.Add(new SelectListItem() { Text = x, Value = x });
            }

            return View(toy);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Toy obj)
        {
            if (ModelState.IsValid)
            {
                _db.Toys.Add(obj);
                //_db.SaveChanges();

                return RedirectToAction("Index", "Toy");
            }

            return View(obj);
        }

        #endregion
    }
}