using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("manage")]
    public class StatusController : Controller
    {
        
        private readonly IGenericService<Status> _genericService;
        public StatusController(IGenericService<Status> genericService)
        {
            _genericService= genericService;
        }

        // GET: UsersPanel/Status
        public async Task<IActionResult> Index()
        {
              return View(await _genericService.GetAllAsync());
        }

        // GET: UsersPanel/Status/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _genericService.GetAllAsync() == null)
            {
                return NotFound();
            }

            var status = await _genericService.GetByIdAsync(id.Value);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // GET: UsersPanel/Status/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersPanel/Status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Status status)
        {
            if (ModelState.IsValid)
            {
                _genericService.Create(status);
                await _genericService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        // GET: UsersPanel/Status/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _genericService.GetAllAsync() == null)
            {
                return NotFound();
            }

            var status = await _genericService.GetByIdAsync(id.Value);
            if (status == null)
            {
                return NotFound();
            }
            return View(status);
        }

        // POST: UsersPanel/Status/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Status status)
        {
            if (id != status.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _genericService.Edit(status);
                    await _genericService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusExists(status.Id))
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
            return View(status);
        }

        // GET: UsersPanel/Status/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _genericService.GetAllAsync() == null)
            {
                return NotFound();
            }
            var status = await _genericService.GetByIdAsync(id.Value);
            if (status == null)
            {
                return NotFound();
            }
            return View(status);
        }

        // POST: UsersPanel/Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _genericService.GetAllAsync() == null)
            {
                return Problem("Entity set 'MyContext.Statuses'  is null.");
            }
            var status = await _genericService.GetByIdAsync(id);
            if (status != null)
            {
                status.IsDeleted= true;
                _genericService.Edit(status);
                await _genericService.SaveChangesAsync();
            }           
            
            return RedirectToAction(nameof(Index));
        }

        private bool StatusExists(int id)
        {
            return _genericService.ExistEntity(id);
        }
    }
}
