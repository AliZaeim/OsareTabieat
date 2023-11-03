using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;

namespace Core.DTOs.General
{
    public class ShopVM
    {
        public List<ProductGroup>? ProductGroups { get; set; }
        public List<ProductItem>? ProductItems { get; set; }
        public List<Product>? Products { get; set; }
        public string? Currency { get; set; }
        public List<string>? Brands { get; set; }
        public List<Product>? ProductsByTagNew { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int TotalProducts { get; set; }
        public int PageCap { get; set; }
        public ProductGroup? CurrentGroup { get; set; }
        public string? Brand { get; set; }
        public StorePageBanner StorePageBanner { get; set; }
        public string? Tag { get; set; }
        public string? Title { get; set; }
    }
}
