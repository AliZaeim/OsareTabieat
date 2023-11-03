using DataLayer.Entities.Store;

namespace Core.DTOs.Admin
{
    public class WareHouseVM
    {
        public DateTime? RegDate { get; set; }
        public Product? Product { get; set; }
        public ProductItem? ProductItem { get; set; }
        public float? Input { get; set; }
        public float? Export { get; set; }
        public float Remain { get; set; }
        public string? OrderDedicatedNumber { get; set; }
        public string? Type { get; set; }
        public string? UnitofM { get; set; }
    }
}
