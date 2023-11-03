using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class MyOrdersModel : PageModel
    {
        private readonly IStoreService _storeService;
        public MyOrdersModel(IStoreService storeService)
        {
            _storeService = storeService;
        }
        public string? Message { get; set; }
        public List<Cart> Carts { get; set; } = new();
        public User LoginUser { get; set; }
        public async Task OnGet()
        {
            if (User.Identity!.IsAuthenticated)
            {
                Carts = await _storeService.GetLoginUserCartsAsync(User.Identity.Name!);
                LoginUser = await _storeService.GetUserByName(User.Identity.Name!);
            }
        }
    }
}
