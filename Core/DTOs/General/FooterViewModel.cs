using DataLayer.Entities.Supplementary;

namespace Core.DTOs.General
{
    public class FooterViewModel
    {
        public SiteInfo? SiteInfo { get; set; } = new();
        public CellphonesBank? CellphonesBank { get; set; }=new();
    }
}
