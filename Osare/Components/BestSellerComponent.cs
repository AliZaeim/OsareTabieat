using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Mvc;

namespace NatureExtract_Web.Components
{
    public class BestSellerComponent : ViewComponent
    {
        private readonly IStoreService _storeService;
        public BestSellerComponent(IStoreService storeService)
        {
            _storeService = storeService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Product> products = await _storeService.GetBestSellingProducts();
            return await Task.FromResult(View("/Pages/Components/_GetBestSellers.cshtml", products.ToList()));
        }
    }
}
