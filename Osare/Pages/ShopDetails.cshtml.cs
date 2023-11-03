using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly IStoreService _storeService;
        public ProductDetailsModel(IStoreService storeService)
        {
            _storeService = storeService;
        }
        public class ShopDetailsVM
        {
            public ProductItem? ProductItem { get; set; }
            public Product? Product { get; set; }
            public string Currency { get; set; } = "ریال";
            public (int price, int netPrice, int discount, int percent, string comment) ProductItemInfo { get; set; }
            public (int price, int netPrice, int discount, int percent,string comment) ProductPriceInfo { get; set; }
            public bool ExistInWarehouse { get; set; }
            public List<ProductItem> GroupProductItems { get; set; } = new();
            public List<Product> GroupProducts { get; set; } = new();
            public bool IsBulk { get; set; }

        }
        public ShopDetailsVM ShopDetailsvM { get; set; } = new();
        public async Task OnGet(string? name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Product? product = await _storeService.GetProductByEnNameAsync(name);
                if (product != null)
                {
                    bool Result = await _storeService.AddProductViewCount(product.Id);
                    if (Result)
                    {
                        _storeService.SaveChanges();
                    }
                }
                SiteInfo? siteInfo = await _storeService.GetLastSiteInfo();
                if (product != null)
                {
                    ShopDetailsvM.Product = product;
                    ShopDetailsvM.ProductPriceInfo = await _storeService.GetNetPriceandDiscountOfProductAsync(product.Id);
                    ShopDetailsvM.Currency = siteInfo?.SiteCurrency ?? "ریال";
                    ProductGroup? productGroup = await _storeService.GetProductGroupByIdAsync(product.ProductGroupId!.Value);
                    ShopDetailsvM.ExistInWarehouse = await _storeService.ExistProductInWareHouseAsync(product.Id);
                    if (product.ProductType == "bulk")
                    {
                        ShopDetailsvM.IsBulk = true;
                    }                    
                    ShopDetailsvM.GroupProducts = productGroup.Products.ToList();
                }
            }
        }
    }
}
