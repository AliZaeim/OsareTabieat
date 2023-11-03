using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class TermsModel : PageModel
    {
        private readonly ISiteToolsService _siteToolsService;
        public TermsModel(ISiteToolsService siteToolsService)
        {
            _siteToolsService = siteToolsService;
        }
        public Term? SiteTerm { get; set; } 
        public async Task OnGet()
        {
            SiteTerm = await _siteToolsService.GetLastTermAsync();
        }
    }
}
