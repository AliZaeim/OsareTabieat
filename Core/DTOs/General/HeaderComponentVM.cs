using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;

namespace Core.DTOs.General
{
    public class HeaderComponentVM
    {
        public List<ProductGroup>? ProductGroups { get; set; }
        public SiteInfo? SiteInfo { get; set; }
        public int CartTotalValue { get; set; }
        public bool ExistCart { get; set; }
        public string? Search { get; set; }
        public string? GroupName { get; set; }
    }
}
