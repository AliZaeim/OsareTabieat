using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Mvc;

namespace Web.Components
{
    public class ShopCartComponent : ViewComponent
    {
        private readonly IStoreService _storeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ShopCartComponent(IStoreService storeService, IHttpContextAccessor httpContextAccessor)
        {
            _storeService = storeService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string cartId = string.Empty;
            Cart cart = new();
            Core.Utility.CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            if (Core.Utility.CookieExtensions.ExistCookie("crt"))
            {
                cartId = Core.Utility.CookieExtensions.ReadCookie("crt");
                if (cartId != null)
                {
                    cart = await _storeService.GetCartByIdAsync(cartId);
                    if (cart == null)
                    {
                        Core.Utility.CookieExtensions.RemoveCookie("crt");
                    }
                }
            }
            return await Task.FromResult(View("/Pages/Components/_GetShopCart.cshtml",cart));
        }
    }
}
