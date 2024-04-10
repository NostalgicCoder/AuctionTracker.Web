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

            switch (val.SelectedSortOrder)
            {
                case "PriceAsc":
                    if (!string.IsNullOrEmpty(val.SearchCriteria))
                    {
                        product.Game = _db.Games.Where(x => x.Name.Contains(val.SearchCriteria)).OrderBy(x => x.Price);
                    }
                    else
                    {
                        product.Game = _db.Games.OrderBy(x => x.Price);
                    }

                    return View(product);
                    break;
                case "PriceDsc":
                    if (!string.IsNullOrEmpty(val.SearchCriteria))
                    {
                        product.Game = _db.Games.Where(x => x.Name.Contains(val.SearchCriteria)).OrderByDescending(x => x.Price);
                    }
                    else
                    {
                        product.Game = _db.Games.OrderByDescending(x => x.Price);
                    }
                    
                    return View(product);
                    break;
                case "Platform":
                    if (!string.IsNullOrEmpty(val.SearchCriteria))
                    {
                        product.Game = _db.Games.Where(x => x.Name.Contains(val.SearchCriteria)).OrderBy(x => x.Platform);
                    }
                    else
                    {
                        product.Game = _db.Games.OrderBy(x => x.Platform);
                    }
                    
                    return View(product);
                    break;
                case "Name":
                    if (!string.IsNullOrEmpty(val.SearchCriteria))
                    {
                        product.Game = _db.Games.Where(x => x.Name.Contains(val.SearchCriteria)).OrderBy(x => x.Name);
                    }
                    else
                    {
                        product.Game = _db.Games.OrderBy(x => x.Name);
                    }
                    
                    return View(product);
                    break;
                case "DateAsc":
                    if (!string.IsNullOrEmpty(val.SearchCriteria))
                    {
                        product.Game = _db.Games.Where(x => x.Name.Contains(val.SearchCriteria)).OrderBy(x => x.SaleDate);
                    }
                    else
                    {
                        product.Game = _db.Games.OrderBy(x => x.SaleDate);
                    }
                    
                    return View(product);
                    break;
                case "DateDsc":
                    if (!string.IsNullOrEmpty(val.SearchCriteria))
                    {
                        product.Game = _db.Games.Where(x => x.Name.Contains(val.SearchCriteria)).OrderByDescending(x => x.SaleDate);
                    }
                    else
                    {
                        product.Game = _db.Games.OrderByDescending(x => x.SaleDate);
                    }
                    
                    return View(product);
                    break;
                case "Condition":
                    if (!string.IsNullOrEmpty(val.SearchCriteria))
                    {
                        product.Game = _db.Games.Where(x => x.Name.Contains(val.SearchCriteria)).OrderBy(x => x.Condition);
                    }
                    else
                    {
                        product.Game = _db.Games.OrderBy(x => x.Condition);
                    }
                    
                    return View(product);
                    break;
                case "Complete":
                    if (!string.IsNullOrEmpty(val.SearchCriteria))
                    {
                        product.Game = _db.Games.Where(x => x.Name.Contains(val.SearchCriteria)).OrderByDescending(x => x.Complete);
                    }
                    else
                    {
                        product.Game = _db.Games.OrderByDescending(x => x.Complete);
                    }
                    
                    return View(product);
                    break;
                default:
                    if(!string.IsNullOrEmpty(val.SearchCriteria))
                    {
                        product.Game = _db.Games.Where(x => x.Name.Contains(val.SearchCriteria));
                    }
                    else
                    {
                        product.Game = _db.Games.OrderBy(x => x.Name);
                    }

                    return View(product);
                    break;
            }
        }

        public IActionResult GameInfo(string? valGameName, string? valSortOrder)
        {
            Product product = new Product();

            product.SelectedProduct = valGameName;

            switch (valSortOrder)
            {
                case "PriceAsc":
                    product.Game = _db.Games.Where(x => x.Name == valGameName).OrderBy(x => x.Price);
                    break;
                case "PriceDsc":
                    product.Game = _db.Games.Where(x => x.Name == valGameName).OrderByDescending(x => x.Price);
                    break;
                case "Platform":
                    product.Game = _db.Games.Where(x => x.Name == valGameName).OrderBy(x => x.Platform);
                    break;
                case "Name":
                    product.Game = _db.Games.Where(x => x.Name == valGameName).OrderBy(x => x.Name);
                    break;
                case "DateAsc":
                    product.Game = _db.Games.Where(x => x.Name == valGameName).OrderBy(x => x.SaleDate);
                    break;
                case "DateDsc":
                    product.Game = _db.Games.Where(x => x.Name == valGameName).OrderByDescending(x => x.SaleDate);
                    break;
                case "Condition":
                    product.Game = _db.Games.Where(x => x.Name == valGameName).OrderBy(x => x.Condition);
                    break;
                case "Complete":
                    product.Game = _db.Games.Where(x => x.Name == valGameName).OrderByDescending(x => x.Complete);
                    break;
                default:
                    product.Game = _db.Games.Where(x => x.Name == valGameName).OrderByDescending(x => x.SaleDate);
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