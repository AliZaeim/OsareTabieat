using Core.DTOs.General;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Components
{
    public class MenuComponent : ViewComponent
    {
        private readonly IStoreService _storeService;
        public MenuComponent(IStoreService storeService)
        {
            _storeService = storeService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            MenuComponentVM menuComponentVM = new()
            {
                ProductGroups = await _storeService.GetProductGroupsAsync(),
            };
            return await Task.FromResult(View("/Pages/Components/_GetMenu.cshtml",menuComponentVM));
        }
    }
}
