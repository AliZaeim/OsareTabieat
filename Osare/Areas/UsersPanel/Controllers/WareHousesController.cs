using Core.DTOs.Admin;
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
    public class WareHousesController : Controller
    {
        
        private readonly IStoreService _storeService;
        public WareHousesController( IStoreService storeService)  
        {
           
            _storeService = storeService;
        }

        // GET: UsersPanel/WareHouses
        public async Task<IActionResult> Index(int? gr,int? pr, int? pitem)
        {
            ViewData["pGroups"] = await _storeService.GetProductGroupsAsync();
            List<WareHouse> wareHouses = await _storeService.GetWareHousesAsync();
            if (gr != null)
            {
                ViewData["gr"]= gr.GetValueOrDefault();
                List<Product> products = await _storeService.GetProductsAsync();                
                products = products.Where(p => p.ProductGroupId == gr.GetValueOrDefault() ).ToList();
                ViewData["products"] = products.ToList();
                wareHouses = wareHouses.Where(w => w.ProductItem?.Product?.ProductGroupId.GetValueOrDefault() == gr.GetValueOrDefault()).ToList();
            }
            if (pr != null)
            {
                ViewData["pr"] = pr.GetValueOrDefault();                
                List<Product> products = await _storeService.GetProductsAsync();
                products = products.Where(p => p.ProductGroupId.GetValueOrDefault() == gr.GetValueOrDefault()).ToList() ;
                ViewData["products"] = products.ToList();
                Product product = await _storeService.GetProductByIdAsync(pr.GetValueOrDefault());
                List<ProductItem> productItems = product?.ProductItems!.ToList() ?? new List<ProductItem>();
                ViewData["productitems"] = productItems.ToList();
                wareHouses = wareHouses.Where(w => w.ProductItem!.Product!.Id == pr.GetValueOrDefault()).ToList();
            }
            if (pitem != null)
            {
                ViewData["pitm"] = pitem.GetValueOrDefault();
                Product product = await _storeService.GetProductByIdAsync(pr.GetValueOrDefault());
                List<ProductItem> productItems = product?.ProductItems!.ToList() ?? new List<ProductItem>();                
                ViewData["productitems"] = productItems.ToList();
                wareHouses = wareHouses.Where(w => w.ProductItem!.Id == pitem.GetValueOrDefault()).ToList();
            }

            //List<WareHouseVM> wareHouseVMs = await _storeService.ChangeWHToWHM(wareHouses.ToList());
            return View(wareHouses.ToList());
        }
        public async Task<JsonResult> GetProductGroups()
        {
            List<ProductGroup> productGroups = await _storeService.GetProductGroupsAsync();
            return Json(productGroups.Select(x => new {id = x.Id, name = x.Title}));
        }
        public async Task<JsonResult> GetGroupProducts(int gId)
        {
            ProductGroup productGroup = await _storeService.GetProductGroupByIdAsync(gId);
            var products = productGroup.Products;
            return Json(products.Select(x => new { id = x.Id, name = x.Name }));
        }
        public async Task<JsonResult> GetProductItems(int pId)
        {
            Product product = await _storeService.GetProductByIdAsync(pId);            
            List<ProductItem> productItems = product.ProductItems!.ToList();
            return Json(productItems.Select(x => new { id = x.Id, name = x.Name }));
        }

        // GET: UsersPanel/WareHouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _storeService.GetWareHousesAsync() == null)
            {
                return NotFound();
            }

            var wareHouse = await _storeService.GetWareHouseByIdAsync(id.GetValueOrDefault());
            if (wareHouse == null)
            {
                return NotFound();
            }

            return View(wareHouse);
        }

        // GET: UsersPanel/WareHouses/Create
        public async Task<IActionResult> Create()
        {
            List<ProductItem> productItems = await _storeService.GetProductItemsAsync();
            List<Product> products = await _storeService.GetProductsAsync();
            products = products.Where(w => w.IsActive).ToList();
            ViewData["ProductId"] = new SelectList(products.ToList(), "Id", "FullName");
            productItems = productItems.Where(w => w.IsActive).ToList();
            ViewData["ProductItemId"] = new SelectList(productItems.ToList(), "Id", "ItemFullName");
            
            return View();
        }

        // POST: UsersPanel/WareHouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WareHouse wareHouse)
        {
            if (ModelState.IsValid)
            {
                wareHouse.RegDate = DateTime.Now;
                _storeService.CreateWareHouse(wareHouse);
                await _storeService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            List<ProductItem> productItems = await _storeService.GetProductItemsAsync();
            productItems = productItems.Where(w => w.IsActive).ToList();
            ViewData["ProductItemId"] = new SelectList(productItems.ToList(), "Id", "ItemFullName",wareHouse.ProductItemId);
            List<Product> products = await _storeService.GetProductsAsync();
            products = products.Where(w => w.IsActive).ToList();
            ViewData["ProductId"] = new SelectList(products.ToList(), "Id", "FullName");
            return View(wareHouse);
        }

        // GET: UsersPanel/WareHouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _storeService.GetProductItemsAsync() == null)
            {
                return NotFound();
            }

            var wareHouse = await _storeService.GetWareHouseByIdAsync(id.GetValueOrDefault());
            if (wareHouse == null)
            {
                return NotFound();
            }
            List<Product> products = await _storeService.GetProductsAsync();
            products= products.Where(w => w.IsActive).ToList() ;
            ViewData["ProductId"] = new SelectList(products.ToList(), "Id", "FullName", wareHouse.ProductItemId);
            return View(wareHouse);
        }

        // POST: UsersPanel/WareHouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WareHouse wareHouse)
        {
            if (id != wareHouse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (wareHouse.RegDate == null)
                    {
                        wareHouse.RegDate = DateTime.Now;
                    }
                    
                    _storeService.UpdateWareHouse(wareHouse);
                    await _storeService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WareHouseExists(wareHouse.Id))
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
            List<ProductItem> productItems = await _storeService.GetProductItemsAsync();
            ViewData["ProductItemId"] = new SelectList(productItems.ToList(), "Id", "ItemFullName", wareHouse.ProductItemId);
            return View(wareHouse);
        }

        // GET: UsersPanel/WareHouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _storeService.GetWareHousesAsync() == null)
            {
                return NotFound();
            }

            var wareHouse = await _storeService.GetWareHouseByIdAsync(id.Value);
            if (wareHouse == null)
            {
                return NotFound();
            }

            return View(wareHouse);
        }

        // POST: UsersPanel/WareHouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _storeService.GetWareHousesAsync() == null)
            {
                return Problem("Entity set 'MyContext.WareHouses'  is null.");
            }
            var wareHouse = await _storeService.GetWareHouseByIdAsync(id);
            if (wareHouse != null)
            {
                _storeService.DeleteWareHouse(wareHouse);
            }
            
            await _storeService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WareHouseExists(int id)
        {
          return _storeService.ExistWareHouse(id);
        }
    }
}
