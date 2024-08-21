using AuctionTracker.Web.Class;
using AuctionTracker.Web.Data;
using AuctionTracker.Web.Interfaces;
using AuctionTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionTracker.Web.Controllers
{
    public class ToyController : Controller
    {
        private readonly ApplicationDbContext _db;

        private IPopulateControls _populateControls = new PopulateControls();
        private IGeneralHelper _generalHelper = new GeneralHelper();
        private IValidation _validation = new Validation();
        private ISortData _sortData = new SortData();
        private IPopulateProductModel _populateProductModel = new PopulateProductModel();

        /// <summary>
        /// Constructor
        /// </summary>
        public ToyController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(Product? product)
        {
            if (string.IsNullOrEmpty(product.SelectedProductLine) || product.SelectedProductLine == "Please select one")
            {
                product.Toy = await _db.Toys.OrderBy(x => x.Name).OrderBy(x => x.Name).ToListAsync();

                return View(product);
            }
            else
            {
                product.Toy = await _db.Toys.Where(x => x.ToyLine == product.SelectedProductLine).ToListAsync();

                if (!string.IsNullOrEmpty(product.SearchCriteria))
                {
                    // Make search criteria lower case so keywords are not blocked by case sensitivity
                    product.Toy = product.Toy.Where(x => x.Name.ToLower().Contains(product.SearchCriteria.ToLower()));
                }

                product = _sortData.SortToyOrder(product, 1);

                return View("SelectedToyLine", product);
            }
        }

        public IActionResult ToyInfo(Product product)
        {
            product = _populateProductModel.PopulateToyModel(_db, product);

            return View(product);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToyInfoSortOrder(Product product)
        {
            product = _populateProductModel.PopulateToyModel(_db, product);

            return View("ToyInfo", product);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConvertToyModelToProductModel(Toy val)
        {
            Product product = new Product();

            product.SelectedProductLine = val.ToyLine;

            return RedirectToAction("Index", "Toy", product);
        }

        #region Create

        //GET
        public IActionResult Create()
        {
            Toy toy = new Toy();

            toy.ToyLineLst = _populateControls.GenerateToyLineListItems(_db);

            return View(toy);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Toy toy)
        {
            bool pass = _validation.ValidateToy(ModelState, toy);

            if (ModelState.IsValid && pass)
            {
                await _db.Toys.AddAsync(toy);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index", "Toy");
            }

            // Regenerate the list of toyLines otherwise JS wont have the data it needs on refresh
            toy.ToyLineLst = _populateControls.GenerateToyLineListItems(_db);

            return View(toy);
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

            var obj = await _db.Toys.FindAsync(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Toy obj)
        {
            bool pass = true;

            if (obj.Price == 0.00m)
            {
                pass = false;
                ModelState.AddModelError("Edit", "No valid price provided.");
            }

            if (!_generalHelper.ValidDecimalNumber(obj.Price) || !_generalHelper.ValidDecimalNumber(obj.Postage))
            {
                pass = false;
                ModelState.AddModelError("Edit", "Price OR postage is not a number value.");
            }

            if (obj.Condition == "Please select one")
            {
                pass = false;
                ModelState.AddModelError("Edit", "No 'Condition' provided.");
            }

            if (obj.Complete == "Please select one")
            {
                pass = false;
                ModelState.AddModelError("Edit", "No 'Complete' provided.");
            }

            if (obj.Damaged == "Please select one")
            {
                pass = false;
                ModelState.AddModelError("Edit", "No 'Damaged' provided.");
            }

            if (obj.ToyLine.ToLower() != "motu")
            {
                obj.Stands = (obj.Stands == "Please select one") ? "Yes" : obj.Stands;
            }
            else
            {
                if (obj.Stands == "Please select one")
                {
                    pass = false;
                    ModelState.AddModelError("Edit", "No 'Stands' provided.");
                }
            }

            if (obj.ToyLine.ToLower() != "mimp")
            {
                obj.Colour = (obj.Colour == "Please select one") ? string.Empty : obj.Colour;
            }
            else
            {
                if (obj.Colour == "Please select one")
                {
                    pass = false;
                    ModelState.AddModelError("Edit", "No 'Colour' provided.");
                }
            }

            obj.DamagedAccessory = (obj.DamagedAccessory == "Please select one") ? "No" : obj.DamagedAccessory;

            if (ModelState.IsValid && pass)
            {
                _db.Toys.Update(obj);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index", "Toy");
            }

            return View(obj);
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

            var obj = await _db.Toys.FindAsync(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            var obj = await _db.Toys.FindAsync(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Toys.Remove(obj);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "Toy");
        }

        #endregion
    }
}