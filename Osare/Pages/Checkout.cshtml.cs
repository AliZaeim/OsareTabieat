using Core.DTOs.General;
using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace Web.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly IStoreService _storeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CheckoutModel(IStoreService storeService, IHttpContextAccessor httpContextAccessor)
        {
            _storeService = storeService;
            _httpContextAccessor = httpContextAccessor;
        }
        public class CartDetails
        {
            public SiteInfo? SiteInfo { get; set; }
            public Cart? Cart { get; set; }
            public List<CartItem>? CartItems { get; set; }

            public CheckOutVM? CheckOutVM { get; set; }
        }
        [BindProperty]
        public CartDetails CartDets { get; set; } = new();
        public async Task OnGet()
        {
            Core.Utility.CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            if (Core.Utility.CookieExtensions.ExistCookie("crt"))
            {
                string cartId = Core.Utility.CookieExtensions.ReadCookie("crt");
                if (cartId != null)
                {
                    Cart cart = await _storeService.GetCartByIdAsync(cartId);
                    CartDets.Cart = cart;
                    CartDets.CartItems = await _storeService.GetCartItemsofCart(cartId);
                }
                CartDets.SiteInfo = await _storeService.GetLastSiteInfo();
                if (User.Identity!.IsAuthenticated)
                {
                    User user = await _storeService.GetUserByName(User.Identity.Name ?? "");

                    CartDets.CheckOutVM = new()
                    {
                        CartId = cartId,
                        BuyerName = user?.Name,
                        BuyerFamily = user?.Family,
                        BuyerCellphone = user?.Cellphone,
                        CountyId = user?.CountyId,
                        County = user?.County,
                        State = user?.County?.State,
                        RecepientName = user?.Name,
                        RecepientFamily = user?.Family,
                        RecepientCellphone = user?.Cellphone,
                        States = await _storeService.GetStatesAsync(),
                        BuyerIsRecepient = true,

                    };
                }
                else
                {
                    CartDets.CheckOutVM = new()
                    {
                        CartId = cartId,
                        States = await _storeService.GetStatesAsync(),
                    };
                }
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            string? cartId = CartDets.CheckOutVM!.CartId;
            if (!ModelState.IsValid)
            {
                Core.Utility.CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);

                if (Core.Utility.CookieExtensions.ExistCookie("crt"))
                {
                    cartId = Core.Utility.CookieExtensions.ReadCookie("crt");
                    if (cartId != null)
                    {
                        Cart crt = await _storeService.GetCartByIdAsync(cartId);
                        CartDets.Cart = crt;
                        CartDets.CartItems = await _storeService.GetCartItemsofCart(cartId);
                    }
                    CartDets.SiteInfo = await _storeService.GetLastSiteInfo();
                    if (User.Identity!.IsAuthenticated)
                    {
                        User user = await _storeService.GetUserByName(User.Identity.Name ?? "");

                        CartDets.CheckOutVM = new()
                        {
                            CartId = cartId,
                            BuyerName = user?.Name,
                            BuyerFamily = user?.Family,
                            BuyerCellphone = user?.Cellphone,
                            CountyId = user?.CountyId,
                            County = user?.County,
                            State = user?.County?.State,
                            RecepientName = user?.Name,
                            RecepientFamily = user?.Family,
                            RecepientCellphone = user?.Cellphone,
                            States = await _storeService.GetStatesAsync(),
                            BuyerIsRecepient = true,

                        };
                    }
                    else
                    {
                        if (Core.Utility.CookieExtensions.ExistCookie("crt"))
                        {
                            cartId = Core.Utility.CookieExtensions.ReadCookie("crt");
                        }
                        CartDets.CheckOutVM = new()
                        {
                            CartId = cartId,
                            States = await _storeService.GetStatesAsync(),
                        };
                    }
                }
                CartDets.CheckOutVM!.States = await _storeService.GetStatesAsync();
                CartDets.CheckOutVM!.Counties = await _storeService.GetCountiesofStateAsync(CartDets.CheckOutVM.StateId.GetValueOrDefault());
                CartDets.SiteInfo = await _storeService.GetLastSiteInfo();
                return Page();
            }
            bool Res1 = await _storeService.CreateCustomerWithCartInfo(CartDets.CheckOutVM!);
            bool Res2 = await _storeService.UpdateCartWithCheckoutAsync(CartDets.CheckOutVM!);
            //Update Cart
            //Send to Payment
            //Create Order if pay is Success
            if (Res1 || Res2)
            {
                _storeService.SaveChanges();
            }
            

            if (CartDets.CheckOutVM!.CartId == null)
            {
                return NotFound("امکان پرداخت برای سبد خرید وجود ندارد !");
            }
            Cart? cart = await _storeService.GetCartByIdAsync(cartId!);
            string PayCur = "IRR";
            string BackUrl = "https://osaremarket.ir/PaymentResult";
            SiteInfo? siteInfo = await _storeService.GetLastSiteInfo();
            if (siteInfo != null)
            {
                if (siteInfo.SiteCurrency!.Trim() == "تومان")
                {
                    PayCur = "IRT";
                }
            }
            if (cart != null)
            {
                if (!cart.CheckOut)
                {

                    string OiD = cart.DedicatedCode!;
                    (bool Success, string Content) = _storeService.GetNextPayToken(cart.CartTotal.GetValueOrDefault(), OiD, cart.BuyerCellphone!, BackUrl, PayCur);

                    if (Success)
                    {
                        string json = Content;
                        dynamic data = JObject.Parse(json);
                        string tid = data["trans_id"];
                        string eUrl = "https://nextpay.org/nx/gateway/payment/" + tid;
                        return Redirect(eUrl);
                    }
                }
            }

            return Content("اطلاعات پرداخت کافی نمی باشد !");
        }

        public async Task<IActionResult> OnGetCountiesofState(int? stId)
        {
            if (stId != null)
            {
                List<County> counties = await _storeService.GetCountiesofStateAsync(stId.Value);
                return new JsonResult(counties.OrderBy(x => x.CountyName).Select(x => new { countyId = x.CountyId, countyName = x.CountyName, stateId = x.StateId, stateName = x.State!.StateName }).ToList());
            }
            return new JsonResult("0");
        }
        public async Task<JsonResult> OnGetFrCostofState(int? stId, float weight)
        {
            if (stId != null)
            {
                StateFreight stateFreight = await _storeService.GetStateFerWithWeight(stId.Value, weight);
                if (stateFreight != null)
                {
                    return new JsonResult(new { state = stateFreight.StateId, freight = stateFreight.Freight });
                }
                else
                {
                    return new JsonResult(new { state = 330, freight = 20000 });
                }

            }
            return new JsonResult(new { state = 330, freight = 200000 });
        }
    }
}
