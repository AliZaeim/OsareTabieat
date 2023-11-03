using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class CartModel : PageModel
    {
        private readonly IStoreService _storeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartModel(IStoreService storeService, IHttpContextAccessor httpContextAccessor)
        {
            _storeService = storeService;
            _httpContextAccessor = httpContextAccessor;
        }
        public class CartDetails
        {
            public SiteInfo? SiteInfo { get; set; }
            public Cart? Cart { get; set; }
            public List<CartItem>? CartItems { get; set; }
        }
        public CartDetails CartDets { get; set; } = new();
        public async Task OnGet()
        {
            
            Core.Utility.CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            if (Core.Utility.CookieExtensions.ExistCookie("crt"))
            {                
                Cart? cart = new();
                string cartId = Core.Utility.CookieExtensions.ReadCookie("crt");
                if (cartId != null)
                {
                    cart = await _storeService.GetCartByIdAsync(cartId);
                    CartDets.Cart = cart;
                    CartDets.CartItems = await _storeService.GetCartItemsofCart(cartId);
                }
                CartDets.SiteInfo = await _storeService.GetLastSiteInfo();
            }
        }
        public async Task<IActionResult> OnGetUpdateCartItemQuantity(string crtId,int cartitemId, int count)
        {
            string res = "no";
            if (!string.IsNullOrEmpty(crtId) && crtId != "0")
            {
                res = await _storeService.UpdateCartItemQuantity(cartitemId, count);
                _storeService.SaveChanges();                
            }
            return new JsonResult(res);
        }
        //public async Task<IActionResult> OnPostApplyDiscountCode(string Code)
        //{
        //    //bool 
        //    //DiscountCoupen discountCoupen = await _storeService.GetDiscountCoupenByCodeAsync(Code);
        //    return Page();
        //}
    }
}
