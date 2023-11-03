using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("manage")]
    public class ProductItemsController : Controller
    {
       
        private readonly IStoreService _storeService;
        public ProductItemsController( IStoreService storeService)
        {
           
            _storeService = storeService;
        }

        // GET: UsersPanel/ProductItems
        public async Task<IActionResult> Index(int? Pid)
        {            
            List<ProductItem> productItems = await _storeService.GetProductItemsAsync();
            if (Pid == null)
            {
                ViewData["ProductName"] = null;
                ViewData["Pid"] = null;
                return View(productItems.ToList());
            }
            else
            {
                Product product = await _storeService.GetProductByIdAsync(Pid.GetValueOrDefault());
                ViewData["Pid"] = Pid.GetValueOrDefault();
                ViewData["ProductName"] = product.Name;
                return View(productItems.Where(w => w.ProductId == Pid.GetValueOrDefault()).ToList());
            }
            
        }

        // GET: UsersPanel/ProductItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _storeService.GetProductItemsAsync() == null)
            {
                return NotFound();
            }

            var productItem = await _storeService.GetProductItemByIdAsync(id.GetValueOrDefault());
            if (productItem == null)
            {
                return NotFound();
            }

            return View(productItem);
        }

        // GET: UsersPanel/ProductItems/Create
        public async Task<IActionResult> Create(int? Pid)
        {
            if (Pid == null)
            {
                ViewData["ProductId"] = new SelectList(await _storeService.GetProductsAsync(), "Id", "Name");
                ViewData["Pid"] = null;
                ViewData["ProductName"] = null;
            }
            else
            {
                ViewData["ProductId"] = new SelectList(await _storeService.GetProductsAsync(), "Id", "Name", Pid.GetValueOrDefault());
                Product product = await _storeService.GetProductByIdAsync(Pid.GetValueOrDefault());
                ViewData["ProductName"] = product.Name;
                ViewData["Pid"] = Pid.GetValueOrDefault();
            }
            return View();
        }

        // POST: UsersPanel/ProductItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductItem productItem, IFormFile? BigImage, IFormFile? SmallImage1, IFormFile? SmallImage2)
        {
            if (ModelState.IsValid)
            {
                               
                
                productItem.RegDate = DateTime.Now;
                _storeService.CreateProductItem(productItem);
                await _storeService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(await _storeService.GetProductsAsync(), "Id", "Name", productItem.ProductId);
            Product product = await _storeService.GetProductByIdAsync(productItem.ProductId.GetValueOrDefault());
            ViewData["ProductName"] = product.Name;
            return View(productItem);
        }

        // GET: UsersPanel/ProductItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _storeService.GetProductItemsAsync() == null)
            {
                return NotFound();
            }

            var productItem = await _storeService.GetProductItemByIdAsync(id.GetValueOrDefault());
            if (productItem == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(await _storeService.GetProductsAsync(), "Id", "Name", productItem.ProductId);
            return View(productItem);
        }

        // POST: UsersPanel/ProductItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductItem productItem)
        {
            if (id != productItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {               
                    
                    if (productItem.RegDate != null)
                    {
                        productItem.RegDate = DateTime.Now;
                    }
                    _storeService.UpdateProductItem(productItem);
                    await _storeService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductItemExists(productItem.Id))
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
            ViewData["ProductId"] = new SelectList(await _storeService.GetProductsAsync(), "Id", "Comment", productItem.ProductId);
            return View(productItem);
        }

        // GET: UsersPanel/ProductItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null ||await _storeService.GetProductItemsAsync() == null)
            {
                return NotFound();
            }

            var productItem = await _storeService.GetProductItemByIdAsync(id.GetValueOrDefault());
            if (productItem == null)
            {
                return NotFound();
            }

            return View(productItem);
        }

        // POST: UsersPanel/ProductItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _storeService.GetProductItemsAsync() == null)
            {
                return Problem("Entity set 'MyContext.ProductItems'  is null.");
            }
            var productItem = await _storeService.GetProductItemByIdAsync(id);
            if (productItem != null)
            {
                _storeService.DeleteProductItem(productItem);
                await _storeService.SaveChangesAsync();
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        private bool ProductItemExists(int id)
        {
          return _storeService.ExistProductItem(id);
        }
    }
}
