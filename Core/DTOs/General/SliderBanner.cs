using DataLayer.Entities.Supplementary;

namespace Core.DTOs.General
{
    public class SliderBanner
    {
        public SliderBanner()
        {
            Sliders = new List<Slider>();
            BannerNextSliders = new List<BannerNextSlider>();
        }
        public List<Slider> Sliders { get; set; }
        public List<BannerNextSlider> BannerNextSliders { get; set; }
    }
}
