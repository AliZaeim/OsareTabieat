using Core.DTOs.General;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("manage")]
    public class ProductsController : Controller
    {

        private readonly IStoreService _storeService;
        public ProductsController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        // GET: UsersPanel/Products
        public async Task<IActionResult> Index()
        {
            var products = await _storeService.GetProductsAsync();
            return products != null ?
                        View(products) :
                        Problem("Entity set 'MyContext.Products'  is null.");
        }

        // GET: UsersPanel/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _storeService.GetProductsAsync() == null)
            {
                return NotFound();
            }

            var product = await _storeService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: UsersPanel/Products/Create
        public async Task<IActionResult> Create()
        {
            List<ProductGroup> groups = await _storeService.GetProductGroupsAsync();
            List<Product> products = await _storeService.GetProductsAsync();
            ViewData["ProductGroupId"] = new SelectList(groups.Where(w => w.IsActive).ToList(), "Id", "Title");
            ViewData["WareHouseProducts"] = new SelectList(products.Where(w => w.IsActive).ToList(), "Id", "Name");
            return View();
        }
        public IActionResult CreateProductItem()
        {
            return PartialView();
        }

        // POST: UsersPanel/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile? BigImage, IFormFile? SmallImage1, IFormFile? SmallImage2)
        {
            if (ModelState.IsValid)
            {
                if (BigImage == null)
                {
                    ModelState.AddModelError("BigImage", "تصویر بزرگ محصول انتخاب نشده است !");
                    return View(product);
                }
                if (SmallImage1 == null)
                {
                    ModelState.AddModelError("SmallImage1", "تصویر کوچک اول انتخاب نشده است !");
                    return View(product);
                }
                if (SmallImage2 == null)
                {

                    ModelState.AddModelError("SmallImage2", "تصویر کوچک دوم انتخاب نشده است !");
                    return View(product);
                }
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif", ".jfif" };
                FileValidation BigimgValid = await BigImage.UploadedImageValidationBaseOnMaxDim(50, 480, 400, ex);
                if (!BigimgValid.IsValid)
                {
                    ModelState.AddModelError("BigImage", BigimgValid.Message);
                    return View(product);
                }
                product.BigImage = BigImage?.SaveUploadedImage("wwwroot/images/products", true);

                FileValidation Smallimg1Valid = await SmallImage1.UploadedImageValidation(50, 192, 143, ex);
                if (!Smallimg1Valid.IsValid)
                {
                    ModelState.AddModelError("SmallImage1", Smallimg1Valid.Message);
                    return View(product);
                }
                product.SmallImage1 = SmallImage1?.SaveUploadedImage("wwwroot/images/products", true);

                FileValidation Smallimg2Valid = await SmallImage2.UploadedImageValidation(50, 91, 84, ex);
                if (!Smallimg2Valid.IsValid)
                {
                    ModelState.AddModelError("SmallImage2", Smallimg2Valid.Message);
                    return View(product);
                }
                product.SmallImage2 = SmallImage2?.SaveUploadedImage("wwwroot/images/products", true);

                product.CreatedDate = DateTime.Now;
                _storeService.CreateProduct(product);
                await _storeService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            //string messages = string.Join("; ", ModelState.Values
            //                            .SelectMany(x => x.Errors)
            //                            .Select(x => x.ErrorMessage));

            List<ProductGroup> groups = await _storeService.GetProductGroupsAsync();
            ViewData["ProductGroupId"] = new SelectList(groups.Where(w => w.IsActive).ToList(), "Id", "Title", product.ProductGroupId);
            List<Product> products = await _storeService.GetProductsAsync();
            ViewData["WareHouseProducts"] = new SelectList(products.Where(w => w.IsActive).ToList(), "Id", "Name");
            return View(product);
        }

        public async Task<bool> ChangeStatus(int? id, int status)
        {
            Product product = await _storeService.GetProductByIdAsync(id.GetValueOrDefault());
            if (product == null)
            {
                return false;
            }

            bool st = false;
            if (status == 1)
            {
                st = true;
            }
            product.IsActive = st;
            _storeService.UpdateProduct(product);
            await _storeService.SaveChangesAsync();
            return st;

        }
        // GET: UsersPanel/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _storeService.GetProductsAsync() == null)
            {
                return NotFound();
            }

            var product = await _storeService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            List<ProductGroup> groups = await _storeService.GetProductGroupsAsync();
            ViewData["ProductGroupId"] = new SelectList(groups.Where(w => w.IsActive).ToList(), "Id", "Title", product.ProductGroupId);
            List<Product> products = await _storeService.GetProductsAsync();
            products = products.Where(w => w.IsActive && w.Id != product.Id).ToList();
            ViewData["WareHouseProducts"] = new SelectList(products, "Id", "Name");
            return View(product);
        }

        // POST: UsersPanel/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile? BigImage, IFormFile? SmallImage1, IFormFile? SmallImage2)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (BigImage == null && string.IsNullOrEmpty(product.BigImage))
                    {
                        ModelState.AddModelError("BigImage", "تصویر بزرگ محصول انتخاب نشده است !");
                        return View(product);
                    }
                    if (SmallImage1 == null && string.IsNullOrEmpty(product.SmallImage1))
                    {
                        ModelState.AddModelError("SmallImage1", "تصویر کوچک اول انتخاب نشده است !");
                        return View(product);
                    }
                    if (SmallImage2 == null && string.IsNullOrEmpty(product.SmallImage2))
                    {
                        ModelState.AddModelError("SmallImage2", "تصویر کوچک دوم انتخاب نشده است !");
                        return View(product);
                    }
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif", ".jfif" };
                    if (BigImage != null)
                    {
                        FileValidation BigimgValid = await BigImage!.UploadedImageValidationBaseOnMaxDim(50,610,580, ex);
                        if (!BigimgValid.IsValid)
                        {
                            ModelState.AddModelError("BigImage", BigimgValid.Message);
                            return View(product);
                        }
                        product.BigImage = BigImage?.SaveUploadedImage("wwwroot/images/products", true);
                    }
                    if (SmallImage1 != null)
                    {
                        FileValidation Smallimg1Valid = await SmallImage1!.UploadedImageValidation(50, 192, 143, ex);
                        if (!Smallimg1Valid.IsValid)
                        {
                            ModelState.AddModelError("SmallImage1", Smallimg1Valid.Message);
                            return View(product);
                        }
                        product.SmallImage1 = SmallImage1?.SaveUploadedImage("wwwroot/images/products", true);
                    }
                    if (SmallImage2 != null)
                    {
                        FileValidation Smallimg2Valid = await SmallImage2!.UploadedImageValidation(50, 91, 84, ex);
                        if (!Smallimg2Valid.IsValid)
                        {
                            ModelState.AddModelError("SmallImage2", Smallimg2Valid.Message);
                            return View(product);
                        }
                        product.SmallImage2 = SmallImage2?.SaveUploadedImage("wwwroot/images/products", true);
                    }
                    if (product.CreatedDate == null)
                    {
                        product.CreatedDate = DateTime.Now;
                    }
                    _storeService.UpdateProduct(product);
                    await _storeService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            List<ProductGroup> groups = await _storeService.GetProductGroupsAsync();
            ViewData["ProductGroupId"] = new SelectList(groups.Where(w => w.IsActive).ToList(), "Id", "Title", product.ProductGroupId);
            List<Product> products = await _storeService.GetProductsAsync();
            products = products.Where(w => w.IsActive && w.Id != product.Id).ToList();
            ViewData["WareHouseProducts"] = new SelectList(products, "Id", "Name");
            return View(product);
        }

        // GET: UsersPanel/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _storeService.GetProductsAsync() == null)
            {
                return NotFound();
            }
            var product = await _storeService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: UsersPanel/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _storeService.GetProductsAsync() == null)
            {
                return Problem("Entity set 'MyContext.Products'  is null.");
            }
            var product = await _storeService.GetProductByIdAsync(id);
            if (product != null)
            {
                _storeService.DeleteProduct(product);
            }

            await _storeService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _storeService.ExistProductById(id);
        }
    }
}
