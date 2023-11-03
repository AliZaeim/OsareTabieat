using Core.DTOs.General;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Osare_Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("manage")]
    public class SlidersController : Controller
    {
        
        private readonly IGenericService<Slider> _genericService;
        public SlidersController(IGenericService<Slider> genericService)
        {
            
            _genericService = genericService;
        }

        // GET: UsersPanel/Sliders
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders =(List<Slider>) await _genericService.GetAllAsync();
              return sliders != null ? 
                          View(sliders) :
                          Problem("Entity set 'MyContext.Sliders'  is null.");
        }

        // GET: UsersPanel/Sliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _genericService.GetAll()== null)
            {
                return NotFound();
            }

            var slider = await _genericService.GetByIdAsync(id.Value);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // GET: UsersPanel/Sliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersPanel/Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                if (Image == null)
                {
                    ModelState.AddModelError("Image", "تصویر اسلایدر رو انتخاب کنید !");
                    return View(slider);
                }
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg" };
                FileValidation fileValidation1 = await Image.UploadedImageValidation(50, ex);

                if (!fileValidation1.IsValid)
                {
                    ModelState.AddModelError("Image", fileValidation1.Message);
                    return View(slider);
                }
                slider.Image = Image.SaveUploadedImage("wwwroot/images/sliders", true);
                _genericService.Create(slider);
                await _genericService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        // GET: UsersPanel/Sliders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _genericService.GetAll() == null)
            {
                return NotFound();
            }
            var slider = await _genericService.GetByIdAsync(id.Value);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: UsersPanel/Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Slider slider, IFormFile? Image)
        {
            if (id != slider.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(slider.Image) && Image == null)
                    {
                        ModelState.AddModelError("Image", "تصویر اسلایدر رو انتخاب کنید !");
                        return View(slider);
                    }
                    if (Image != null)
                    {
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg" };
                        FileValidation fileValidation1 = await Image.UploadedImageValidation(50, ex);

                        if (!fileValidation1.IsValid)
                        {
                            ModelState.AddModelError("Image", fileValidation1.Message);
                            return View(slider);
                        }
                        slider.Image = Image.SaveUploadedImage("wwwroot/images/sliders", true);
                    }
                    _genericService.Edit(slider);
                    await _genericService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.Id))
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
            return View(slider);
        }

        // GET: UsersPanel/Sliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _genericService.GetAll() == null)
            {
                return NotFound();
            }

            var slider = await _genericService.GetByIdAsync(id.Value);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: UsersPanel/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _genericService.GetAllAsync() == null)
            {
                return Problem("Entity set 'MyContext.Sliders'  is null.");
            }
            var slider = await _genericService.GetByIdAsync(id);
            if (slider != null)
            {
                _genericService.Delete(slider);
            }

            await _genericService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(int id)
        {
          return _genericService.ExistEntity(id);
        }
    }
}
