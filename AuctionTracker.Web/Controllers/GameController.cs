using AuctionTracker.Web.Class;
using AuctionTracker.Web.Data;
using AuctionTracker.Web.Interfaces;
using AuctionTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionTracker.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _db;

        private IValidation _validation = new Validation();
        private ISortData _sortData = new SortData();
        private IPopulateProductModel _populateProductModel = new PopulateProductModel();

        /// <summary>
        /// Constructor
        /// </summary>
        public GameController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(Product? product)
        {
            product.Game = await _db.Games.ToListAsync();

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
            product = _populateProductModel.PopulateGameModel(_db, product);

            return View(product);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GameSortOrder(Product product)
        {
            product = _populateProductModel.PopulateGameModel(_db, product);

            return View("GameInfo", product);
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
        public async Task<IActionResult> Create(Game game)
        {
            bool pass = _validation.ValidateGame(ModelState, game);

            if (ModelState.IsValid && pass)
            {
                await _db.Games.AddAsync(game);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index", "Game");
            }

            return View(game);
        }

        #endregion

        #region Edit

        //GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = await _db.Games.FindAsync(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Game game)
        {
            bool pass = _validation.ValidateGame(ModelState, game);

            if (ModelState.IsValid && pass)
            {
                _db.Games.Update(game);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index", "Game");
            }

            return View(game);
        }

        #endregion

        #region Delete

        //GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = await _db.Games.FindAsync(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            var obj = await _db.Games.FindAsync(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Games.Remove(obj);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "Game");
        }

        #endregion
    }
}