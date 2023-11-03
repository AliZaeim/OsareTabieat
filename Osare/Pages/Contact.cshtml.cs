using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IGenericService<CustomerContact> _customerService;
        private readonly IGenericService<SiteInfo> _siteInfoService;
        public ContactModel(IGenericService<CustomerContact> customerService, IGenericService<SiteInfo> siteInfoService)
        {
            _customerService = customerService;
            _siteInfoService = siteInfoService;
        }
        [BindProperty]
        public CustomerContact CustomerContact { get; set; }
        public SiteInfo? SiteInfo { get; set; }
        public async Task OnGet()
        {
            List<SiteInfo> siteInfos =(List<SiteInfo>) await _siteInfoService.GetAllAsync();
            SiteInfo = siteInfos.OrderByDescending(x => x.RegDate).FirstOrDefault();
        }
        public async Task<IActionResult> OnPostAsync() 
        { 
            if (!ModelState.IsValid)
            {
                return Page();
            }
            CustomerContact.RegDate= DateTime.Now;
            _customerService.Create(CustomerContact);
            await _customerService.SaveChangesAsync();
            TempData["saved"] = "yes";
            CustomerContact = new();
            ModelState.Clear();
            List<SiteInfo> siteInfos = (List<SiteInfo>)await _siteInfoService.GetAllAsync();
            SiteInfo = siteInfos.OrderByDescending(x => x.RegDate).FirstOrDefault();
            return Page();
        }
    }
}
