using Core.DTOs.General;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("manage")]
    public class StorePageBannersController : Controller
    {
        private readonly IPageBannerService _pageBannerService;

        public StorePageBannersController(IPageBannerService pageBannerService)
        {
            _pageBannerService = pageBannerService;
        }

        // GET: UsersPanel/StorePageBanners
        public async Task<IActionResult> Index()
        {
            return View(await _pageBannerService.GetStorePageBannersAsync());
        }

        // GET: UsersPanel/StorePageBanners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storePageBanner = await _pageBannerService.GetStorePageBannerByIdAsync(id.Value);
            if (storePageBanner == null)
            {
                return NotFound();
            }

            return View(storePageBanner);
        }

        // GET: UsersPanel/StorePageBanners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersPanel/StorePageBanners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StorePageBanner storePageBanner, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                if (Image == null)
                {
                    ModelState.AddModelError("Image", "تصویر بنر را انتخاب کنید !");                    
                    return View(storePageBanner);
                }
                else
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation ImgValid = await Image.UploadedImageValidation(50,ex);
                    if (!ImgValid.IsValid)
                    {
                        ModelState.AddModelError("Image", ImgValid.Message ?? "");                        
                        return View(storePageBanner);
                    }
                    storePageBanner.Image = Image.SaveUploadedImage("wwwroot/images/bg", true);
                }
                storePageBanner.CreatedDate = DateTime.Now;
                _pageBannerService.CreateStorePageBanner(storePageBanner);
                await _pageBannerService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storePageBanner);
        }

        // GET: UsersPanel/StorePageBanners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storePageBanner = await _pageBannerService.GetStorePageBannerByIdAsync(id.Value);
            if (storePageBanner == null)
            {
                return NotFound();
            }
            return View(storePageBanner);
        }

        // POST: UsersPanel/StorePageBanners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StorePageBanner storePageBanner, IFormFile? Image)
        {
            if (id != storePageBanner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Image != null) {
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                        FileValidation ImgValid = await Image.UploadedImageValidation(50, ex);
                        if (!ImgValid.IsValid)
                        {
                            ModelState.AddModelError("Image", ImgValid.Message ?? "");
                            return View(storePageBanner);
                        }
                        storePageBanner.Image = Image.SaveUploadedImage("wwwroot/images/bg", true);
                    }
                    _pageBannerService.UpdateStorePageBanner(storePageBanner);
                    await _pageBannerService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StorePageBannerExists(storePageBanner.Id))
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
            return View(storePageBanner);
        }

        // GET: UsersPanel/StorePageBanners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storePageBanner = await _pageBannerService.GetStorePageBannerByIdAsync(id.Value);
            if (storePageBanner == null)
            {
                return NotFound();
            }

            return View(storePageBanner);
        }

        // POST: UsersPanel/StorePageBanners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var storePageBanner = await _pageBannerService.GetStorePageBannerByIdAsync(id);
            if (storePageBanner != null)
            {
                _pageBannerService.DeleteStorePageBanner(storePageBanner);
                await _pageBannerService.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        private bool StorePageBannerExists(int id)
        {
            return _pageBannerService.ExistStorePageBanner(id);
        }
    }
}
