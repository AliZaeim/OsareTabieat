using Core.DTOs.General;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
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
    public class ProductGroupsController : Controller
    {
        
        private readonly IGenericService<ProductGroup> _genericService;
        public ProductGroupsController(IGenericService<ProductGroup> genericService)
        {           
            _genericService = genericService;
        }

        // GET: UsersPanel/ProductGroups
        public async Task<IActionResult> Index()
        {
            return View(await _genericService.GetAllAsync());
        }
        public async Task<bool> ChangeStatusGroup(int id, int status)
        {
            ProductGroup? productGroup = await _genericService.GetByIdAsync(id);
            if (productGroup == null)
            {
                return false;
            }

            bool st = false;
            if (status == 1)
            {
                st = true;
            }
            productGroup.IsActive = st;
            _genericService.Edit(productGroup);
            await _genericService.SaveChangesAsync();
            return st;

        }
        // GET: UsersPanel/ProductGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _genericService.GetAllAsync() == null)
            {
                return NotFound();
            }

            var productGroup = await _genericService.GetByIdAsync(id.Value);
            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroup);
        }

        // GET: UsersPanel/ProductGroups/Create
        public async Task<IActionResult> Create(int? pId)
        {
            ViewData["ParentId"] = new SelectList(await _genericService.GetAllAsync(), "Id", "EnTitle", pId);
            return View();
        }

        // POST: UsersPanel/ProductGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductGroup productGroup, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                if (Image == null)
                {
                    ModelState.AddModelError("Image", "تصویر اسلایدر رو انتخاب کنید !");
                    return View(productGroup);
                }
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp" };
                FileValidation fileValidation1 = await Image.UploadedImageValidation(50, ex);

                if (!fileValidation1.IsValid)
                {
                    ModelState.AddModelError("Image", fileValidation1.Message);
                    return View(productGroup);
                }
                productGroup.Image = Image.SaveUploadedImage("wwwroot/images/pgroups", true);
                _genericService.Create(productGroup);
                await _genericService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(await _genericService.GetAllAsync(), "Id", "Title", productGroup.ParentId);
            return View(productGroup);
        }

        // GET: UsersPanel/ProductGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _genericService.GetAll() == null)
            {
                return NotFound();
            }

            var productGroup = await _genericService.GetByIdAsync(id.Value);
            if (productGroup == null)
            {
                return NotFound();
            }
            List<ProductGroup> productGroups = (List<ProductGroup>)await _genericService.GetAllAsync();
            productGroups = productGroups.Where(w => w.Id != productGroup.Id).ToList();
            ViewData["ParentId"] = new SelectList(productGroups, "Id", "Title", productGroup.ParentId);
            return View(productGroup);
        }

        // POST: UsersPanel/ProductGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductGroup productGroup, IFormFile? Image)
        {
            if (id != productGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Image == null && string.IsNullOrEmpty(productGroup.Image))
                    {
                        ModelState.AddModelError("Image", "تصویر اسلایدر رو انتخاب کنید !");
                        return View(productGroup);
                    }
                    if (Image != null)
                    {
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp" };
                        FileValidation fileValidation1 = await Image.UploadedImageValidation(50, ex);

                        if (!fileValidation1.IsValid)
                        {
                            ModelState.AddModelError("Image", fileValidation1.Message);
                            return View(productGroup);
                        }
                        productGroup.Image = Image.SaveUploadedImage("wwwroot/images/pgroups", true);
                    }
                    
                    _genericService.Edit(productGroup);
                    await _genericService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductGroupExists(productGroup.Id))
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
            List<ProductGroup> productGroups = (List<ProductGroup>)await _genericService.GetAllAsync();
            productGroups = productGroups.Where(w => w.Id != productGroup.Id).ToList();
            ViewData["ParentId"] = new SelectList(productGroups, "Id", "Title", productGroup.ParentId);
            return View(productGroup);
        }

        // GET: UsersPanel/ProductGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _genericService.GetAllAsync() == null)
            {
                return NotFound();
            }

            var productGroup = await _genericService.GetByIdAsync(id.Value);
            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroup);
        }

        // POST: UsersPanel/ProductGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _genericService.GetAllAsync() == null)
            {
                return Problem("Entity set 'MyContext.ProductGroups'  is null.");
            }
            var productGroup = await _genericService.GetByIdAsync(id);
            if (productGroup != null)
            {
                _genericService.Delete(productGroup);
            }

            await _genericService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductGroupExists(int id)
        {
            return _genericService.ExistEntity(id);
        }
    }
}
