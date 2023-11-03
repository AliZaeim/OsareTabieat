using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;

namespace NatureExtract_Web.Components
{
    public class PageBannerComponent: ViewComponent
    {
        private readonly IPageBannerService _pbService;
        public PageBannerComponent(IPageBannerService pbService)
        {
            _pbService = pbService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int Bannercount, int Vieworder)
        {
            PageBanner pageBanner = await _pbService.GetPageBannerByCountOrder(Bannercount,Vieworder);
            return await Task.FromResult(View("/Pages/Components/_GetPageBanners.cshtml", pageBanner));
        }
    }
}
