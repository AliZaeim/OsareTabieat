using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Context;
using DataLayer.Entities.Store;
using Core.Security;
using Microsoft.AspNetCore.Authorization;
using Core.Services.Interfaces;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("manage")]
    public class CartsController : Controller
    {
        private readonly IStoreService _storeService;

        public CartsController(IStoreService storeService)
        {
            _storeService= storeService;
        }

        // GET: UsersPanel/Carts
        public async Task<IActionResult> Index()
        {
            List<Cart> carts = await _storeService.GetCartsAsync();
            carts = carts.Where(w => w.CheckOut).ToList();
            return View(carts);
        }
        public async Task<IActionResult> GetCarts()
        {
            List<Cart> carts = await _storeService.GetCartsAsync();
            carts = carts.Where(w =>! w.CheckOut).ToList();
            return View(carts);
        }

        // GET: UsersPanel/Carts/Details/5
        public async Task<IActionResult> CartDetails(Guid? id)
        {
            if (id == null || await _storeService.GetCartsAsync() == null)
            {
                return NotFound();
            }

            var cart = await _storeService.GetCartByIdAsync(id.Value);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        

        

        // GET: UsersPanel/Carts/Edit/5
        public async Task<IActionResult> CartEdit(Guid? id)
        {
            if (id == null || await _storeService.GetCartsAsync()== null)
            {
                return NotFound();
            }

            var cart = await _storeService.GetCartByIdAsync(id.Value);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

        // POST: UsersPanel/Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _storeService.UpdateCart(cart);
                    await _storeService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CartExists(cart.Id))
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
            return View(cart);
        }

        

        private async Task<bool> CartExists(Guid id)
        {
            var carts =await _storeService.GetCartsAsync();
          return carts.Any(x => x.Id == id);
        }
    }
}
