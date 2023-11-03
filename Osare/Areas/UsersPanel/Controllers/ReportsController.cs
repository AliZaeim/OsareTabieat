using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("manage")]
    public class ReportsController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStoreService _storeService;
        public ReportsController(IUserService userService, IStoreService storeService)
        {
            _userService = userService;
            _storeService = storeService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetUsers()
        {
            return View(await _userService.GetAllUsersAsync());
        }
        public async Task<IActionResult> UserEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User user = await _userService.GetUserByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEdit(int id,User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            _userService.UpdateUser(user);
            await _userService.SaveChangesAsync();
            return RedirectToAction(nameof(GetUsers));
        }
        public async Task<ActionResult> CustomerContacts()
        {
            return View(await _storeService.GetCustomerContacts());
        }

    }
}
