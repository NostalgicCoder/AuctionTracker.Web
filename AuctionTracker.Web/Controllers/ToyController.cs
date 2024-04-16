using AuctionTracker.Web.Class;
using AuctionTracker.Web.Data;
using AuctionTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuctionTracker.Web.Controllers
{
    public class ToyController : Controller
    {
        private readonly ApplicationDbContext _db;

        private CalculatePrices _calculatePrices = new CalculatePrices();
        private PopulateControls _populateControls = new PopulateControls();
        private GeneralHelper _generalHelper = new GeneralHelper();

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
                product.Toy = _db.Toys.Where(x => x.ToyLine == product.SelectedProductLine);

                if (!string.IsNullOrEmpty(product.SearchCriteria))
                {
                    // Make search criteria lower case so keywords are not blocked by case sensetivity
                    product.Toy = product.Toy.Where(x => x.Name.ToLower().Contains(product.SearchCriteria.ToLower()));
                }

                // Debated over adding new sort options for 'Toy' items but most of the unique new fields are unlikely to be searched against.  Can be added in if required later
                switch (product.SelectedSortOrder)
                {
                    case "PriceAsc":
                        product.Toy = product.Toy.OrderBy(x => x.Price);
                        break;
                    case "PriceDsc":
                        product.Toy = product.Toy.OrderByDescending(x => x.Price);
                        break;
                    case "Name":
                        product.Toy = product.Toy.OrderBy(x => x.Name);
                        break;
                    case "DateAsc":
                        product.Toy = product.Toy.OrderBy(x => x.SaleDate);
                        break;
                    case "DateDsc":
                        product.Toy = product.Toy.OrderByDescending(x => x.SaleDate);
                        break;
                    case "Condition":
                        product.Toy = product.Toy.OrderBy(x => x.Condition);
                        break;
                    case "Complete":
                        product.Toy = product.Toy.OrderByDescending(x => x.Complete);
                        break;
                    default:
                        product.Toy = product.Toy.OrderBy(x => x.Name);
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
                    product.Toy = _db.Toys.Where(x => x.ToyLine == product.SelectedProductLine && x.Name == product.SelectedProduct).OrderByDescending(x => x.SaleDate);
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
        public IActionResult Create(Toy obj)
        {
            bool pass = true;

            if (string.IsNullOrEmpty(obj.ToyLine) || obj.ToyLine == "Please select one")
            {
                pass = false;
                ModelState.AddModelError("Create", "No 'Toyline' selected OR added");
            }

            if (obj.Price == 0.00m || obj.Postage == 0.00m)
            {
                pass = false;
                ModelState.AddModelError("Create", "No valid price OR postage provided.");
            }

            if (!_generalHelper.ValidDecimalNumber(obj.Price) || !_generalHelper.ValidDecimalNumber(obj.Postage))
            {
                pass = false;
                ModelState.AddModelError("Create", "Price OR postage is not a number value.");
            }

            if (obj.Condition == "Please select one")
            {
                pass = false;
                ModelState.AddModelError("Create", "No 'Condition' provided.");
            }

            if (obj.Complete == "Please select one")
            {
                pass = false;
                ModelState.AddModelError("Create", "No 'Complete' provided.");
            }

            if (obj.Damaged == "Please select one")
            {
                pass = false;
                ModelState.AddModelError("Create", "No 'Damaged' provided.");
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
                    ModelState.AddModelError("Create", "No 'Stands' provided.");
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
                    ModelState.AddModelError("Create", "No 'Colour' provided.");
                }
            }

            obj.DamagedAccessory = (obj.DamagedAccessory == "Please select one") ? "No" : obj.DamagedAccessory;

            if (ModelState.IsValid && pass)
            {
                _db.Toys.Add(obj);
                _db.SaveChanges();

                return RedirectToAction("Index", "Toy");
            }

            // Regenerate the list of toyLines otherwise JS wont have the data it needs on refresh
            obj.ToyLineLst = _populateControls.GenerateToyLineListItems(_db);

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

            var obj = _db.Toys.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Toy obj)
        {
            bool pass = true;

            if (obj.Price == 0.00m || obj.Postage == 0.00m)
            {
                pass = false;
                ModelState.AddModelError("Edit", "No valid price OR postage provided.");
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
                _db.SaveChanges();

                return RedirectToAction("Index", "Toy");
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

            var obj = _db.Toys.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int? id)
        {
            var obj = _db.Toys.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Toys.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index", "Toy");
        }

        #endregion
    }
}