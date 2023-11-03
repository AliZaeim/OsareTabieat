using Core.DTOs.General;
using Core.Services;
using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Web.Components
{
    public class HeaderComponent : ViewComponent
    {        
        private readonly IStoreService _storeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HeaderComponent(IStoreService storeService, IHttpContextAccessor httpContextAccessor)
        {
            _storeService = storeService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync(string Srch, string GName)
        {
            string cartId = string.Empty;
            Cart cart = new();
            Core.Utility.CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            int cTotal = 0;
            bool ExistCart = Core.Utility.CookieExtensions.ExistCookie("crt");
            if (ExistCart)
            {
                cartId = Core.Utility.CookieExtensions.ReadCookie("crt");
                if (cartId != null)
                {
                    cart = await _storeService.GetCartByIdAsync(cartId);
                    if (cart != null)
                    {
                        if (cart.CartItems.Count != 0)
                        {
                            cTotal = cart.CartItems.Sum(x => x.Quantity * x.NetPrice);
                        }
                    }
                }
            }
            HeaderComponentVM headerComponentVM = new()
            {
                SiteInfo = await _storeService.GetLastSiteInfo(),
                ProductGroups = await _storeService.GetProductGroupsAsync(),
                CartTotalValue = cTotal,
                ExistCart = ExistCart,
                Search = Srch,
                GroupName = GName
            };
            
            return await Task.FromResult(View("/Pages/Components/_GetHeader.cshtml",headerComponentVM));
        }
    }
}
