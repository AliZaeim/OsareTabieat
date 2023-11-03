using Core.DTOs.General;
using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;

namespace NatureExtract_Web.Components
{
    public class BestDealComponent: ViewComponent
    {
        private readonly IStoreService _storeService;
        public BestDealComponent(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            BestDeal bestDeal = await _storeService.GetLastBestDealAsync();
            List<ProductItem> productItems = await _storeService.GetProductItemsHasDiscountAsync();
            BestDealForViewVM bestDealForViewVM = new()
            {
                BestDeal= bestDeal,
                ProductItems= productItems
            };
            return await Task.FromResult(View("/Pages/Components/_GetBestDeals.cshtml",bestDealForViewVM));
            
        }
    }
}
