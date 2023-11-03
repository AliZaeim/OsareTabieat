using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Osare.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IStoreService _storeService;

        public IndexModel(IStoreService storeService)
        {
            _storeService = storeService;
        }
        
        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? GroupName { get; set; }
        public string? GroupTitle { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<Product>? Products { get; set; }
        //[BindProperty(SupportsGet = true)]
        public ProductGroup? ProductGroup { get; set; }
        [BindProperty]
        public string? Currency { get; set; }
        public bool ExistSpecialProducts { get; set; }
        public bool ExistBestSellProducts { get; set; }       
        public async Task OnGet()
        {
            ExistSpecialProducts = await _storeService.ExistSpecialProduct();
            ExistBestSellProducts = await _storeService.ExistBestSellProduct();
            if (!string.IsNullOrEmpty(Search))
            {
                Products = await _storeService.GetProductsAsync();
                Products = Products.Where(w => w.Name!.Contains(Search)).ToList();
                if (!string.IsNullOrEmpty(GroupName))
                {
                    if (GroupName != "allgr")
                    {
                        Products = Products.Where(w => w.ProductGroup!.EnTitle == GroupName).ToList();
                        ProductGroup = await _storeService.GetProductGroupByEnTitleAsunc(GroupName);
                        GroupTitle = ProductGroup.Title ?? "همه دسته بندی ها";
                    }
                    else
                    {
                        GroupTitle = "همه دسته بندی ها";
                    }
                }
                SiteInfo siteInfo = await _storeService.GetLastSiteInfo();
                Currency = siteInfo.SiteCurrency ?? "ریال";
            }
        }
        
        public async Task<IActionResult> OnPostSaveCellphone(CellphonesBank cellphonesBank)
        {
            if (ModelState.IsValid)
            {
                if (await _storeService.ExistCellphoneinCellphoneBank(cellphonesBank.Cellphone!))
                {
                    TempData["Message"] = "تلفن همراه قبلا ثبت شده است !";
                    ModelState.AddModelError("cellphonesBank.Cellphone", "تلفن وارد شده قبلا ثبت شده است !");
                    return Page();
                }
                cellphonesBank.RegDate = DateTime.Now;
                _storeService.CreateCellphoneBank(cellphonesBank);
                await _storeService.SaveChangesAsync();
                TempData["SaveCell"] = "yes";
            }
            else
            {
                var errors = ModelState.Select(x => x.Value!.Errors)
                                       .Where(y => y.Count > 0)
                                       .ToList();
                foreach (var error in errors) {
                    ModelState.AddModelError("cellphonesBank.Cellphone", error.Select(x => x.ErrorMessage).ToString()!);
                }
                
                return Page();
            }
            
            

            return Page();
        }



    }
}