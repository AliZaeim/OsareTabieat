using Core.Convertors;
using Core.DTOs.Admin;
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
    public class DiscountCoupensController : Controller
    {
        
        private readonly IStoreService _storeService;
        public DiscountCoupensController(IStoreService storeService)
        {
           
            _storeService = storeService;
        }

        // GET: UsersPanel/DiscountCoupens
        public async Task<IActionResult> Index()
        {
            return View(await _storeService.GetDiscountCoupensAsync());
        }

        // GET: UsersPanel/DiscountCoupens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _storeService.GetDiscountCoupensAsync() == null)
            {
                return NotFound();
            }

            var discountCoupen = await _storeService.GetDiscountCoupenByIdAsync(id.Value);
            if (discountCoupen == null)
            {
                return NotFound();
            }

            return View(discountCoupen);
        }

        // GET: UsersPanel/DiscountCoupens/Create
        public IActionResult Create()
        {
            return View();
        }
        public async Task<JsonResult> GetCoupen()
        {
            List<DiscountCoupen> discountCoupens =await _storeService.GetDiscountCoupensAsync();
            List<string?> coupens = new();
            coupens = discountCoupens.Select(x => x.Code).ToList();

            string getc = Core.Prodocers.Generators.GenerateUniqueString(coupens!, 3, 3, 4, 2);
            return Json(getc);
        }
        // POST: UsersPanel/DiscountCoupens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DiscountCoupenVM discountCoupenVM)
        {
            if (ModelState.IsValid)
            {
                DiscountCoupen discountCoupen = new()
                {
                    Code = discountCoupenVM.Code,
                    Comment = discountCoupenVM.Comment,
                    Number = discountCoupenVM.Number,
                    IsActive = discountCoupenVM.IsActive,
                    Percent = discountCoupenVM.Percent

                };
                if (!string.IsNullOrEmpty(discountCoupenVM.EndDate))
                {
                    discountCoupen.ExpiredDate = discountCoupenVM.EndDate.ToMiladiWithTime(discountCoupenVM.EndTime!);
                }
                _storeService.CreateDiscountCoupen(discountCoupen);
                await _storeService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discountCoupenVM);
        }

        // GET: UsersPanel/DiscountCoupens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _storeService.GetDiscountCoupensAsync() == null)
            {
                return NotFound();
            }

            var discountCoupen = await _storeService.GetDiscountCoupenByIdAsync(id.Value);
            if (discountCoupen == null)
            {
                return NotFound();
            }
            DiscountCoupenVM discountCoupenVM = new()
            {
                Id = discountCoupen.Id,
                Code = discountCoupen.Code,
                Number = discountCoupen.Number,
                Comment = discountCoupen.Comment,
                Percent = discountCoupen.Percent,
                IsActive = discountCoupen.IsActive
            };
            if (discountCoupen.ExpiredDate.HasValue)
            {
                discountCoupenVM.EndDate = discountCoupen.ExpiredDate.ToShamsiN();
                discountCoupenVM.EndTime = discountCoupen.ExpiredDate.Value.ToString("hh:ss");
            }
            return View(discountCoupenVM);
        }

        // POST: UsersPanel/DiscountCoupens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DiscountCoupen discountCoupen)
        {
            if (id != discountCoupen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _storeService.UpdateDiscountCoupen(discountCoupen);
                    await _storeService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountCoupenExists(discountCoupen.Id))
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
            return View(discountCoupen);
        }

        // GET: UsersPanel/DiscountCoupens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _storeService.GetDiscountCoupensAsync() == null)
            {
                return NotFound();
            }

            var discountCoupen = await _storeService.GetDiscountCoupenByIdAsync(id.Value);
            if (discountCoupen == null)
            {
                return NotFound();
            }

            return View(discountCoupen);
        }

        // POST: UsersPanel/DiscountCoupens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _storeService.GetDiscountCoupensAsync() == null)
            {
                return Problem("Entity set 'MyContext.DiscountCoupens'  is null.");
            }
            DiscountCoupen discountCoupen = await _storeService.GetDiscountCoupenByIdAsync(id);
            if (discountCoupen != null)
            {
                return NotFound();
            }
            _storeService.DeleteDiscountCoupen(discountCoupen!);
            await _storeService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscountCoupenExists(int id)
        {
          return _storeService.ExistCoupen(id);
        }
    }
}
