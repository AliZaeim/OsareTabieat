using Core.DTOs.General;
using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Numerics;

namespace Web.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly IStoreService _storeService;
        private readonly IPageBannerService _pageBannerService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductsModel(IStoreService storeService,IPageBannerService pageBannerService, IHttpContextAccessor httpContextAccessor)
        {
            _storeService = storeService;
            _httpContextAccessor = httpContextAccessor;
            _pageBannerService = pageBannerService;
        }

        public ShopVM ShopVM { get; set; } = new()
        {
            Currency = "ریال"
        };
        public int? PageN { get; set; }
        public async Task OnGet(int? pageN = 1, string grName="",string tag="", string brand="")
        {
            ShopVM.ProductGroups = await _storeService.GetProductGroupsAsync();            
            int pageCap = 12;
            ShopVM.PageCap= pageCap;           
            List<Product> products = await _storeService.GetProductsAsync();
            products = products.Where(w => w.IsActive ).ToList();
            if (!string.IsNullOrEmpty(grName))
            {
                ProductGroup productGroup = await _storeService.GetProductGroupByEnTitleAsunc(grName);
                ShopVM.CurrentGroup = productGroup;
                if (productGroup != null)
                {
                    products = productGroup.Products.ToList();
                }                
            }
            if (!string.IsNullOrEmpty(brand))
            {
                products = products.Where(w => w.Brand == brand).ToList();
                ShopVM.Brand = brand;
            }
            if (!string.IsNullOrEmpty(tag))
            {
                products = products.Where(w => w.TagList.Any(a => a.Trim().Replace(" ","-") == tag)).ToList();
                ShopVM.Tag = tag;
            }
            int PrCount = 0;
            PrCount += products.Count;
            ShopVM.TotalProducts= PrCount;
            if (PrCount % pageCap == 0)
            {
                ShopVM.TotalPages = PrCount / pageCap;
            }
            else
            {
                ShopVM.TotalPages = (PrCount /pageCap) + 1;
            }
            ShopVM.CurrentPage = pageN.GetValueOrDefault();
            PageN= pageN;
            
            SiteInfo siteInfo = await _storeService.GetLastSiteInfo();
            ShopVM.Brands = await _storeService.GetProductBrandsAsync();
            ShopVM.ProductsByTagNew = await _storeService.GetProductsByTag("جدید");
            if (siteInfo != null)
            {
                ShopVM.Currency = siteInfo.SiteCurrency;
            }
            products = products.Skip((pageN.GetValueOrDefault() - 1) * pageCap).Take(pageCap).ToList();
            ShopVM.StorePageBanner= await _pageBannerService.GetLastStorePageBanner();
            ShopVM.Products = products;            
        }
        public async Task<IActionResult> OnGetAddtoCart(int pcount, int? itemid,string type)
        {
            ProductItem? productItem = null; Product? product= null;
            Core.Utility.CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string guid = string.Empty;
            if (Core.Utility.CookieExtensions.ExistCookie("crt"))
            {
                guid = Core.Utility.CookieExtensions.ReadCookie("crt").ToString();
            }            
            if (!string.IsNullOrEmpty(type))
            {
                (int price, int NetPrice, int discount, int percent, string comment) PriceInfo= (0,0,0,0,"");
                if (type == "pr")
                {
                    product = await _storeService.GetProductByIdAsync(itemid.GetValueOrDefault());
                    PriceInfo = await _storeService.GetNetPriceandDiscountOfProductAsync(itemid.GetValueOrDefault());
                }
                else if (type == "pritem")
                {
                    productItem = await _storeService.GetProductItemByIdAsync(itemid.GetValueOrDefault());
                    PriceInfo = await _storeService.GetNetPriceandDiscountOfProductItemAsync(itemid.GetValueOrDefault());
                }
                AddToCartVM addToCartVM = new()
                {
                    Price = PriceInfo.price,
                    NetPrice = PriceInfo.price,
                    Discount = PriceInfo.discount,
                    ProductItemId = productItem?.Id,
                    ProductId= product?.Id,
                    Quantity = pcount,
                    
                };
                if (!string.IsNullOrEmpty(guid))
                {
                    addToCartVM.CartId = Guid.Parse(guid);
                }
                (Cart cart,string result) = await _storeService.AddToCartOp(addToCartVM);
                if (cart != null)
                {
                    
                    Core.Utility.CookieExtensions.SetCookie("crt", cart.Id.ToString(), DateTime.Now.AddHours(72));
                }
                
                return new JsonResult(result);
            }

            return new JsonResult("add:no");
        }
        public async Task<IActionResult> GetShopCart()
        {
            Core.Utility.CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string guid = string.Empty;
            if (Core.Utility.CookieExtensions.ExistCookie("crt"))
            {
                guid = Core.Utility.CookieExtensions.ReadCookie("crt").ToString();
            }
            if (!string.IsNullOrEmpty(guid))
            {
                Cart cart = await _storeService.GetCartByIdAsync(guid);
                return ViewComponent("ShopCartComponent", new { cart });
            }
            return Content("سبد خرید خالی است !");
        }
        public async Task<JsonResult> OnGetRemoveItemFromCart(string cartId, int itemId)
        {
            await _storeService.RemoveItemFromCart(cartId, itemId);
            _storeService.SaveChanges();
            return new JsonResult("yes");
        }
    }
}
