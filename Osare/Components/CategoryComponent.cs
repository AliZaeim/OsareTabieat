using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NatureExtract_Web.Components
{
    public class CategoryComponent:ViewComponent
    {
        private readonly IStoreService _pgroupService;
        public CategoryComponent(IStoreService pgroupService)
        {
            _pgroupService = pgroupService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ProductGroup> productGroups = await _pgroupService.GetProductGroupsAsync();
            productGroups = productGroups.Where(w => w.IsActive && w.Parent == null).ToList();
            return await Task.FromResult(View("/Pages/Components/_GetCategories.cshtml",productGroups));
        }
    }
}
