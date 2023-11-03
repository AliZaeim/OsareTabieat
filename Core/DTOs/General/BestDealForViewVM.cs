using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;

namespace Core.DTOs.General
{
    public class BestDealForViewVM
    {
        public BestDeal? BestDeal { get; set; }
        public List<Product>? Products { get; set; }
        public List<ProductItem>? ProductItems { get; set; }
    }
}
