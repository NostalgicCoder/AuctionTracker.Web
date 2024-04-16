using AuctionTracker.Web.Class;
using AuctionTracker.Web.Data;
using AuctionTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuctionTracker.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _db;

        private CalculatePrices _calculatePrices = new CalculatePrices();

        /// <summary>
        /// Constructor
        /// </summary>
        public GameController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(Product? val)
        {
            Product product = new Product();

            product.Game = _db.Games;

            if (!string.IsNullOrEmpty(val.SearchCriteria))
            {
                product.Game = product.Game.Where(x => x.Name.ToLower().Contains(val.SearchCriteria.ToLower()));
            }

            switch (val.SelectedSortOrder)
            {
                case "PriceAsc":
                    product.Game = product.Game.OrderBy(x => x.Price);
                    break;
                case "PriceDsc":
                    product.Game = product.Game.OrderByDescending(x => x.Price);
                    break;
                case "Platform":
                    product.Game = product.Game.OrderBy(x => x.Platform);
                    break;
                case "Name":
                    product.Game = product.Game.OrderBy(x => x.Name);
                    break;
                case "DateAsc":
                    product.Game = product.Game.OrderBy(x => x.SaleDate);
                    break;
                case "DateDsc":
                    product.Game = product.Game.OrderByDescending(x => x.SaleDate);
                    break;
                case "Condition":
                    product.Game = product.Game.OrderBy(x => x.Condition);
                    break;
                case "Complete":
                    product.Game = product.Game.OrderByDescending(x => x.Complete);
                    break;
                default:
                    product.Game = product.Game.OrderBy(x => x.Name);
                    break;
            }

            return View(product);
        }

        public IActionResult GameInfo(string? valGameName, string? valSortOrder)
        {
            Product product = new Product();

            product.SelectedProduct = valGameName;
            product.Game = _db.Games.Where(x => x.Name == valGameName);

            switch (valSortOrder)
            {
                case "PriceAsc":
                    product.Game = product.Game.OrderBy(x => x.Price);
                    break;
                case "PriceDsc":
                    product.Game = product.Game.OrderByDescending(x => x.Price);
                    break;
                case "Platform":
                    product.Game = product.Game.OrderBy(x => x.Platform);
                    break;
                case "Name":
                    product.Game = product.Game.OrderBy(x => x.Name);
                    break;
                case "DateAsc":
                    product.Game = product.Game.OrderBy(x => x.SaleDate);
                    break;
                case "DateDsc":
                    product.Game = product.Game.OrderByDescending(x => x.SaleDate);
                    break;
                case "Condition":
                    product.Game = product.Game.OrderBy(x => x.Condition);
                    break;
                case "Complete":
                    product.Game = product.Game.OrderByDescending(x => x.Complete);
                    break;
                default:
                    product.Game = product.Game.OrderByDescending(x => x.SaleDate);
                    break;
            }

            product = _calculatePrices.GetGamePrices(product);

            return View(product);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GameSortOrder(Product product)
        {
            return RedirectToAction("GameInfo", "Game", new { valGameName = product.SelectedProduct, valSortOrder = product.SelectedSortOrder });
        }

        #region Create

        //GET
        public IActionResult Create()
        {
            var vm = new Game();

            return View(vm);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Game obj)
        {
            if (ModelState.IsValid)
            {
                _db.Games.Add(obj);
                _db.SaveChanges();

                return RedirectToAction("Index", "Game");
            }

            return View(obj);
        }

        #endregion

        #region Edit

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _db.Games.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Game obj)
        {
            if (ModelState.IsValid)
            {
                _db.Games.Update(obj);
                _db.SaveChanges(); // Not saved to the database until this command is run

                return RedirectToAction("Index", "Game"); // When performed redirect to the 'Index' action
            }

            return View(obj);
        }

        #endregion

        #region Delete

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _db.Games.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int? id)
        {
            var obj = _db.Games.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Games.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index", "Game");
        }

        #endregion
    }
}