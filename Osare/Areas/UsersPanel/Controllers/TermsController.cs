using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Context;
using DataLayer.Entities.Supplementary;
using Core.Security;
using Microsoft.AspNetCore.Authorization;
using Core.Services.Interfaces;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("manage")]
    public class TermsController : Controller
    {
        private readonly IGenericService<Term> _termService;
        public TermsController(IGenericService<Term> termService)
        {
            _termService = termService;
        }

    
        // GET: UsersPanel/Terms
        public async Task<IActionResult> Index()
        {
              return View(await _termService.GetAllAsync());
        }

        // GET: UsersPanel/Terms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _termService.GetAllAsync() == null)
            {
                return NotFound();
            }

            var term = await _termService.GetByIdAsync(id.Value);
            if (term == null)
            {
                return NotFound();
            }

            return View(term);
        }

        // GET: UsersPanel/Terms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersPanel/Terms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Term term)
        {
            if (ModelState.IsValid)
            {
                term.RegDate = DateTime.Now;
                _termService.Create(term);
                await _termService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(term);
        }

        // GET: UsersPanel/Terms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _termService.GetAllAsync() == null)
            {
                return NotFound();
            }

            var term = await _termService.GetByIdAsync(id.Value);
            if (term == null)
            {
                return NotFound();
            }
            return View(term);
        }

        // POST: UsersPanel/Terms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Term term)
        {
            if (id != term.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _termService.Edit(term);
                    await _termService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TermExists(term.Id))
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
            return View(term);
        }

        // GET: UsersPanel/Terms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _termService.GetAllAsync() == null)
            {
                return NotFound();
            }

            var term = await _termService.GetByIdAsync(id.Value);
            if (term == null)
            {
                return NotFound();
            }

            return View(term);
        }

        // POST: UsersPanel/Terms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _termService.GetAllAsync() == null)
            {
                return Problem("Entity set 'MyContext.Terms'  is null.");
            }
            var term = await _termService.GetByIdAsync(id);
            if (term != null)
            {
                _termService.Delete(term);
                await _termService.SaveChangesAsync();
            }           
            
            return RedirectToAction(nameof(Index));
        }

        private bool TermExists(int id)
        {
          return _termService.ExistEntity(id);
        }
    }
}
