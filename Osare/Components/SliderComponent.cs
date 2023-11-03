using Core.DTOs.General;
using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NatureExtract_Web.Components
{
    public class SliderComponent : ViewComponent
    {

        private readonly ISiteToolsService _siteToolsService;
        public SliderComponent(ISiteToolsService siteToolsService)
        {
            _siteToolsService = siteToolsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Slider> sliders = await _siteToolsService.GetSlidersAsync();
            List<BannerNextSlider> bannerNextSliders = await _siteToolsService.GetBannerNextSlidersAsync();
            SliderBanner sliderBanner = new()
            {
                Sliders = sliders.Where(x => x.IsActive).ToList(),
                BannerNextSliders = bannerNextSliders.Where(x => x.IsActive).ToList()
            };

            return await Task.FromResult(View("/Pages/Components/_GetSliders.cshtml", sliderBanner));

        }
    }
}
