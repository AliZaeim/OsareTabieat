using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Context;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Core.Services.Interfaces;
using Core.DTOs.Admin;
using Core.Convertors;
using Core.Security;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("manage")]
    public class BestDealsController : Controller
    {
        
        private readonly IGenericService<BestDeal> _genericService;
        public BestDealsController(IGenericService<BestDeal> genericService)
        {            
            _genericService = genericService;
        }

        // GET: UsersPanel/BestDeals
        public async Task<IActionResult> Index()
        {
              return View(await _genericService.GetAllAsync());
        }

        // GET: UsersPanel/BestDeals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestDeal = await _genericService.GetByIdAsync(id.Value);
            if (bestDeal == null)
            {
                return NotFound();
            }

            return View(bestDeal);
        }

        // GET: UsersPanel/BestDeals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersPanel/BestDeals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BestDealVM bestDealVM)
        {
            if (ModelState.IsValid)
            {
                DateTime? Sdate = null;
                DateTime? Edate = null;
                if (!string.IsNullOrEmpty(bestDealVM.StartDate))
                {
                    Sdate = bestDealVM.StartDate.ToMiladiWithoutTime();
                }
                if (!string.IsNullOrEmpty(bestDealVM.EndDate))
                {
                    Edate = bestDealVM.EndDate.ToMiladiWithoutTime();
                }
                BestDeal bestDeal = new()
                {
                    Title= bestDealVM.Title,
                    Text= bestDealVM.Text,
                    StartDate = Sdate,
                    EndDate = Edate,
                    IsActive = bestDealVM.IsActive,
                    RegDate = DateTime.Now
                };              
                _genericService.Create(bestDeal);
                await _genericService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bestDealVM);
        }

        // GET: UsersPanel/BestDeals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestDeal = await _genericService.GetByIdAsync(id.Value);
            if (bestDeal == null)
            {
                return NotFound();
            }
            BestDealVM bestDealVM = new()
            {
                Id= bestDeal.Id,
                Title = bestDeal.Title,
                Text = bestDeal.Text,
                StartDate = bestDeal.StartDate.ToShamsiN(),
                EndDate = bestDeal.EndDate.ToShamsiN(),
                IsActive = bestDeal.IsActive,
            };
            return View(bestDealVM);
        }

        // POST: UsersPanel/BestDeals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,BestDealVM bestDealVM)
        {
            if (id != bestDealVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DateTime? Sdate = null;
                    DateTime? Edate = null;
                    if (!string.IsNullOrEmpty(bestDealVM.StartDate))
                    {
                        Sdate = bestDealVM.StartDate.ToMiladiWithoutTime();
                    }
                    if (!string.IsNullOrEmpty(bestDealVM.EndDate))
                    {
                        Edate = bestDealVM.EndDate.ToMiladiWithoutTime();
                    }
                    BestDeal? bestDeal = await _genericService.GetByIdAsync(id);
                    if (bestDeal != null)
                    {
                        bestDeal.StartDate= Sdate;
                        bestDeal.EndDate= Edate;
                        bestDeal.Title= bestDealVM.Title;
                        bestDeal.Text= bestDealVM.Text;
                        bestDeal.IsActive= bestDealVM.IsActive;
                    }
                    _genericService.Edit(bestDeal!);
                    await _genericService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BestDealExists(bestDealVM.Id))
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
            return View(bestDealVM);
        }

        // GET: UsersPanel/BestDeals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _genericService.GetAllAsync() == null)
            {
                return NotFound();
            }

            var bestDeal = await _genericService.GetByIdAsync(id.Value);
            if (bestDeal == null)
            {
                return NotFound();
            }

            return View(bestDeal);
        }

        // POST: UsersPanel/BestDeals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _genericService.GetAllAsync() == null)
            {
                return Problem("Entity set 'MyContext.BestDeals'  is null.");
            }
            var bestDeal = await _genericService.GetByIdAsync(id);
            if (bestDeal != null)
            {
                _genericService.Delete(bestDeal);
                await _genericService.SaveChangesAsync();
            }        
            
            return RedirectToAction(nameof(Index));
        }

        private bool BestDealExists(int id)
        {
          return _genericService.ExistEntity(id);
        }
    }
}
