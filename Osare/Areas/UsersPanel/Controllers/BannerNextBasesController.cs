using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("manage")]
    public class BannerNextBasesController : Controller
    {
        
        private readonly IGenericService<BannerNextBase> _genericService;
        public BannerNextBasesController(IGenericService<BannerNextBase> genericService)
        {
            
            _genericService = genericService;
        }

        // GET: UsersPanel/BannerNextBases
        public async Task<IActionResult> Index()
        {
              return _genericService.GetAll() != null ? 
                          View(await _genericService.GetAllAsync()) :
                          Problem("Entity set 'MyContext.BannerNextBases'  is null.");
        }

        // GET: UsersPanel/BannerNextBases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _genericService.GetAll() == null)
            {
                return NotFound();
            }

            var bannerNextBase = await _genericService.GetByIdAsync(id.Value);
            if (bannerNextBase == null)
            {
                return NotFound();
            }

            return View(bannerNextBase);
        }

        // GET: UsersPanel/BannerNextBases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersPanel/BannerNextBases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BannerNextBase bannerNextBase)
        {
            if (ModelState.IsValid)
            {
                _genericService.Create(bannerNextBase);
                await _genericService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bannerNextBase);
        }

        // GET: UsersPanel/BannerNextBases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _genericService.GetAll() == null)
            {
                return NotFound();
            }

            var bannerNextBase = await _genericService.GetByIdAsync(id.Value);
            if (bannerNextBase == null)
            {
                return NotFound();
            }
            return View(bannerNextBase);
        }

        // POST: UsersPanel/BannerNextBases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  BannerNextBase bannerNextBase)
        {
            if (id != bannerNextBase.BannerNextBase_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _genericService.Edit(bannerNextBase);
                    await _genericService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannerNextBaseExists(bannerNextBase.BannerNextBase_Id))
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
            return View(bannerNextBase);
        }

        // GET: UsersPanel/BannerNextBases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _genericService.GetAll() == null)
            {
                return NotFound();
            }

            var bannerNextBase = await _genericService.GetByIdAsync(id.Value);
            if (bannerNextBase == null)
            {
                return NotFound();
            }

            return View(bannerNextBase);
        }

        // POST: UsersPanel/BannerNextBases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_genericService.GetAll() == null)
            {
                return Problem("Entity set 'MyContext.BannerNextBases'  is null.");
            }
            var bannerNextBase = await _genericService.GetByIdAsync(id);
            if (bannerNextBase != null)
            {
                _genericService.Delete(bannerNextBase);
            }
            
            await _genericService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BannerNextBaseExists(int id)
        {
          return (_genericService.ExistEntity(id));
        }
    }
}
