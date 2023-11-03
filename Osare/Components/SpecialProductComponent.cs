using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Mvc;

namespace NatureExtract_Web.Components
{
    public class SpecialProductComponent : ViewComponent
    {
        private readonly IStoreService _storeService;
        public SpecialProductComponent(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Product> products =await _storeService.GetProductsAsync();
            products = products.Where(w => w.IsSpecial || w.TagList.Any(a => a == "ویژه")).ToList();
            return await Task.FromResult(View("/Pages/Components/_GetSpecialProducts.cshtml",products));
        }
    }
}
