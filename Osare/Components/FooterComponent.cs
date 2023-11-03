using Core.DTOs.General;
using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;

namespace Web.Components
{
    public class FooterComponent : ViewComponent
    {
        private readonly IStoreService _storeService;
        public FooterComponent(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            SiteInfo siteInfo = await _storeService.GetLastSiteInfo();
            FooterViewModel footerViewModel = new()
            {
                SiteInfo= siteInfo,
            };
            return await Task.FromResult(View("/Pages/Components/_GetFooter.cshtml", footerViewModel));
        }
    }
}
