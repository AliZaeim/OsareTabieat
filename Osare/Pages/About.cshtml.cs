using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class AboutModel : PageModel
    {
        private readonly IStoreService _storeService;
        public AboutModel(IStoreService storeService)
        {
            _storeService = storeService;
        }
        public int ProductCount { get; set; } = 0;
        public int CustomerCount { get; set; } = 1200;
        public SiteInfo siteInfo { get; set; }
        [BindProperty]
        public CellphonesBank CellphonesBank { get; set; }
        public async Task OnGet()
        {
            List<Product> products = await _storeService.GetProductsAsync();
            ProductCount = products.Count();
            siteInfo = await _storeService.GetLastSiteInfo();
            if (siteInfo != null)
            {
                DateTime dt = siteInfo.RegDate!.Value;
                int Daydiff = DateTime.Now.Subtract(dt).Days;
                int addCusPlas = Daydiff * 50;
                CustomerCount += addCusPlas;
            }
        }
        
    }
}
