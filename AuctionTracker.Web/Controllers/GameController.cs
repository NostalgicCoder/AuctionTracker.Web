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
        private Validation _validation = new Validation();
        private SortData _sortData = new SortData();

        /// <summary>
        /// Constructor
        /// </summary>
        public GameController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(Product? product)
        {
            product.Game = _db.Games;

            if (!string.IsNullOrEmpty(product.SearchCriteria))
            {
                // Make search criteria lower case so keywords are not blocked by case sensitivity
                product.Game = product.Game.Where(x => x.Name.ToLower().Contains(product.SearchCriteria.ToLower()));
            }

            product = _sortData.SortGameOrder(product, 1);

            return View(product);
        }

        public IActionResult GameInfo(Product product)
        {
            product.Game = _db.Games.Where(x => x.Name == product.SelectedProduct);

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

            return View(product);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GameSortOrder(Product product)
        {
            return RedirectToAction("GameInfo", "Game", product);
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
        public IActionResult Create(Game game)
        {
            bool pass = _validation.ValidateGame(ModelState, game);

            if (ModelState.IsValid && pass)
            {
                _db.Games.Add(game);
                _db.SaveChanges();

                return RedirectToAction("Index", "Game");
            }

            return View(game);
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
        public IActionResult Edit(Game game)
        {
            bool pass = _validation.ValidateGame(ModelState, game);

            if (ModelState.IsValid && pass)
            {
                _db.Games.Update(game);
                _db.SaveChanges();

                return RedirectToAction("Index", "Game");
            }

            return View(game);
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