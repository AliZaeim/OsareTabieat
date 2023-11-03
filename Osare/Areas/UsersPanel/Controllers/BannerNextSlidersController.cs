using Core.DTOs.General;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("manage")]
    public class BannerNextSlidersController : Controller
    {
        
        private readonly IGenericService<BannerNextSlider> _bannerNextService;
        private readonly IGenericService<BannerNextBase> _bannerNextbaseService;
        public BannerNextSlidersController(IGenericService<BannerNextSlider> bannetNextService, IGenericService<BannerNextBase> bannerNextbaseService)
        {
            
            _bannerNextService = bannetNextService;
            _bannerNextbaseService = bannerNextbaseService;
        }

        // GET: UsersPanel/BannerNextSliders
        public async Task<IActionResult> Index(int? baseId)
        {
            List<BannerNextSlider> bannerNextSliders =(List<BannerNextSlider>) await _bannerNextService.GetAllAsync();

            if (baseId != null)
            {
                bannerNextSliders = bannerNextSliders.Where(w => w.BannerNextBaseId == baseId.Value ).ToList();
            }
            
            return View(bannerNextSliders);
        }

        // GET: UsersPanel/BannerNextSliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _bannerNextService.GetAll() == null)
            {
                return NotFound();
            }

            var bannerNextSlider = await _bannerNextService.GetByIdAsync(id.Value);
                
            if (bannerNextSlider == null)
            {
                return NotFound();
            }

            return View(bannerNextSlider);
        }

        // GET: UsersPanel/BannerNextSliders/Create
        public async Task<IActionResult> Create(int? bId)
        {
            if (bId != null)
            {
                ViewData["BannerNextBaseId"] = new SelectList(await _bannerNextbaseService.GetAllAsync(), "BannerNextBase_Id", "BannerNextBase_Title", bId);
            }
            else
            {
                ViewData["BannerNextBaseId"] = new SelectList(await _bannerNextbaseService.GetAllAsync(), "BannerNextBase_Id", "BannerNextBase_Title");
            }
            
            return View();
        }

        // POST: UsersPanel/BannerNextSliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BannerNextSlider bannerNextSlider, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                if (Image == null)
                {
                    ModelState.AddModelError("Image", "تصویر اسلایدر رو انتخاب کنید !");
                    return View(bannerNextSlider);
                }
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg" };
                FileValidation fileValidation1 = await Image.UploadedImageValidation(50, ex);

                if (!fileValidation1.IsValid)
                {
                    ModelState.AddModelError("Image", fileValidation1.Message);
                    return View(bannerNextSlider);
                }
                bannerNextSlider.Image = Image.SaveUploadedImage("wwwroot/images/banners", true);
                bannerNextSlider.RegDate = DateTime.Now;
                _bannerNextService.Create(bannerNextSlider);
                await _bannerNextService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BannerNextBaseId"] = new SelectList(await _bannerNextbaseService.GetAllAsync(), "BannerNextBase_Id", "BannerNextBase_Title", bannerNextSlider.BannerNextBaseId);
            return View(bannerNextSlider);
        }

        // GET: UsersPanel/BannerNextSliders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _bannerNextService.GetAll() == null)
            {
                return NotFound();
            }

            var bannerNextSlider = await _bannerNextService.GetByIdAsync(id.Value);
            if (bannerNextSlider == null)
            {
                return NotFound();
            }
            ViewData["BannerNextBaseId"] = new SelectList(await _bannerNextbaseService.GetAllAsync(), "BannerNextBase_Id", "BannerNextBase_Title", bannerNextSlider.BannerNextBaseId);
            return View(bannerNextSlider);
        }

        // POST: UsersPanel/BannerNextSliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BannerNextSlider bannerNextSlider, IFormFile? Image)
        {
            if (id != bannerNextSlider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(bannerNextSlider.Image) && Image == null)
                    {
                        ModelState.AddModelError("Image", "تصویر اسلایدر رو انتخاب کنید !");
                        return View(bannerNextSlider);
                    }
                    if (Image != null)
                    {
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg" };
                        FileValidation fileValidation1 = await Image.UploadedImageValidation(50, ex);

                        if (!fileValidation1.IsValid)
                        {
                            ModelState.AddModelError("Image", fileValidation1.Message);
                            return View(bannerNextSlider);
                        }
                        bannerNextSlider.Image = Image.SaveUploadedImage("wwwroot/images/banners", true);
                    }
                    _bannerNextService.Edit(bannerNextSlider);
                    await _bannerNextService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannerNextSliderExists(bannerNextSlider.Id))
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
            ViewData["BannerNextBaseId"] = new SelectList(await _bannerNextbaseService.GetAllAsync(), "BannerNextBase_Id", "BannerNextBase_Title", bannerNextSlider.BannerNextBaseId);
            return View(bannerNextSlider);
        }

        // GET: UsersPanel/BannerNextSliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _bannerNextService.GetAll() == null)
            {
                return NotFound();
            }

            var bannerNextSlider = await _bannerNextService.GetByIdAsync(id.Value);
            if (bannerNextSlider == null)
            {
                return NotFound();
            }

            return View(bannerNextSlider);
        }

        // POST: UsersPanel/BannerNextSliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _bannerNextService.GetAllAsync() == null)
            {
                return Problem("Entity set 'MyContext.BannerNextSliders'  is null.");
            }
            var bannerNextSlider = await _bannerNextService.GetByIdAsync(id);
            if (bannerNextSlider != null)
            {
                _bannerNextService.Delete(bannerNextSlider);
            }
            
            await _bannerNextService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BannerNextSliderExists(int id)
        {
          return (_bannerNextService.ExistEntity(id));
        }
    }
}
