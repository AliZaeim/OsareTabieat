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
    public class PageBannersController : Controller
    {
       
        private readonly IGenericService<PageBanner> _pageBanner;
        public PageBannersController(IGenericService<PageBanner> pageBanner)
        {          
            _pageBanner = pageBanner;
        }

        // GET: UsersPanel/PageBanners
        public async Task<IActionResult> Index()
        {
            var pageBanners = await _pageBanner.GetAllAsync();
              return pageBanners != null ? 
                          View(pageBanners) :
                          Problem("Entity set 'MyContext.PageBanners'  is null.");
        }

        // GET: UsersPanel/PageBanners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _pageBanner.GetAllAsync() == null)
            {
                return NotFound();
            }

            var pageBanner = await _pageBanner.GetByIdAsync(id.Value);
            if (pageBanner == null)
            {
                return NotFound();
            }

            return View(pageBanner);
        }

        // GET: UsersPanel/PageBanners/Create
        public IActionResult Create()
        {
            return View();
        }
        
        // POST: UsersPanel/PageBanners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PageBanner pageBanner)
        {
            if (ModelState.IsValid)
            {
                pageBanner.RegDate = DateTime.Now;
                _pageBanner.Create(pageBanner);
                await _pageBanner.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pageBanner);
        }
        public async Task<bool> ChangeStatusPageBanner(int id, int status)
        {
            PageBanner? pageBanner = await _pageBanner.GetByIdAsync(id);
            if (pageBanner == null)
            {
                return false;
            }

            bool st = false;
            if (status == 1)
            {
                st = true;
            }
            pageBanner.IsActive = st;
            _pageBanner.Edit(pageBanner);
            await _pageBanner.SaveChangesAsync();
            return st;

        }

        // GET: UsersPanel/PageBanners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _pageBanner.GetAllAsync() == null)
            {
                return NotFound();
            }

            var pageBanner = await _pageBanner.GetByIdAsync(id.Value);
            if (pageBanner == null)
            {
                return NotFound();
            }
            return View(pageBanner);
        }

        // POST: UsersPanel/PageBanners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PageBanner pageBanner)
        {
            if (id != pageBanner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (pageBanner.RegDate == null)
                    {
                        pageBanner.RegDate = DateTime.Now;
                    }
                    _pageBanner.Edit(pageBanner);
                    await _pageBanner.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageBannerExists(pageBanner.Id))
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
            return View(pageBanner);
        }

        // GET: UsersPanel/PageBanners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _pageBanner.GetAllAsync() == null)
            {
                return NotFound();
            }

            var pageBanner = await _pageBanner.GetByIdAsync(id.Value);
            if (pageBanner == null)
            {
                return NotFound();
            }

            return View(pageBanner);
        }

        // POST: UsersPanel/PageBanners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _pageBanner.GetAllAsync() == null)
            {
                return Problem("Entity set 'MyContext.PageBanners'  is null.");
            }
            var pageBanner = await _pageBanner.GetByIdAsync(id);
            if (pageBanner != null)
            {
                _pageBanner.Delete(id);
            }

            await _pageBanner.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PageBannerExists(int id)
        {
          return _pageBanner.ExistEntity(id);
        }
    }
}
