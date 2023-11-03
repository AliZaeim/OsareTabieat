using Core.DTOs.General;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("manage")]
    public class PageBannerItemsController : Controller
    {
       
        private readonly IPageBannerService _pbiService;

        public PageBannerItemsController(IPageBannerService pbiService)
        {
            _pbiService = pbiService;           
        }

        // GET: UsersPanel/PageBannerItems
        public async Task<IActionResult> Index(int? pbId)
        {
            List<PageBannerItem> pageBannerItems = await _pbiService.GetPageBannerItemsAsync();
            if (pbId == null)
            {
                ViewData["PTitle"] = "بنرهای صفحه اول سایت";
                return View(pageBannerItems);
            }
            else
            {
                PageBanner pageBanner = await _pbiService.GetPageBannerByIdAsync(pbId.Value);
                ViewData["PTitle"] = "بنرهای بسته " + pageBanner.Title;
                ViewData["pbanId"] = pbId.Value;
                pageBannerItems = pageBannerItems.Where(w => w.PBId == pbId).ToList();
                return View(pageBannerItems);
            }
        }

        // GET: UsersPanel/PageBannerItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _pbiService.GetPageBannerItemsAsync() == null)
            {
                return NotFound();
            }

            var pageBannerItem = await _pbiService.GetPageBannerItemByIncludeAsync(id.GetValueOrDefault());
            if (pageBannerItem == null)
            {
                return NotFound();
            }

            return View(pageBannerItem);
        }

        // GET: UsersPanel/PageBannerItems/Create
        public async Task<IActionResult> Create(int? pbId)
        {
            if (pbId == null)
            {
                ViewData["PBId"] = new SelectList(await _pbiService.GetPageBannersAsync(), "Id", "Title");
                ViewData["pbanId"] = null;
            }
            else
            {
                ViewData["PBId"] = new SelectList(await _pbiService.GetPageBannersAsync(), "Id", "Title", pbId.GetValueOrDefault());
                ViewData["pbanId"] = pbId.Value;
            }

            return View();
        }

        // POST: UsersPanel/PageBannerItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PageBannerItem pageBannerItem, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                if (Image == null)
                {
                    ModelState.AddModelError("Image", "تصویر اسلایدر رو انتخاب کنید !");
                    return View(pageBannerItem);
                }
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp" };
                int vwidth = 425; int vheight = 223;
                PageBanner pageBanner = await _pbiService.GetPageBannerByIdAsync(pageBannerItem.PBId.GetValueOrDefault());
                if (pageBanner != null)
                {
                    if (pageBanner.BannerCount == 1)
                    {
                        vwidth = 1310;
                        vheight = 165;
                    }
                    if (pageBanner.BannerCount == 2)
                    {
                        vwidth = 635;
                        vheight = 300;
                    }
                    if (pageBanner.BannerCount == 3)
                    {
                        vwidth = 425;
                        vheight = 225;
                    }
                }

                FileValidation fileValidation1 = await Image.UploadedImageValidation(50, vwidth, vheight, ex);

                if (!fileValidation1.IsValid)
                {
                    ModelState.AddModelError("Image", fileValidation1.Message);
                    return View(pageBannerItem);
                }
                pageBannerItem.Image = Image.SaveUploadedImage("wwwroot/images/pagebanners", true);
                _pbiService.CreatePageBannerItem(pageBannerItem);
                await _pbiService.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "PageBanners");
            }
            ViewData["PBId"] = new SelectList(await _pbiService.GetPageBannersAsync(), "Id", "Title", pageBannerItem.PBId);
            return View(pageBannerItem);
        }

        // GET: UsersPanel/PageBannerItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _pbiService.GetPageBannerItemsAsync() == null)
            {
                return NotFound();
            }

            var pageBannerItem = await _pbiService.GetPageBannerItemByIncludeAsync(id.Value);
            if (pageBannerItem == null)
            {
                return NotFound();
            }
            ViewData["PBId"] = new SelectList(await _pbiService.GetPageBannersAsync(), "Id", "Title", pageBannerItem.PBId);
            return View(pageBannerItem);
        }

        // POST: UsersPanel/PageBannerItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( PageBannerItem pageBannerItem, IFormFile? Image)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image == null && string.IsNullOrEmpty(pageBannerItem.Image))
                    {
                        ModelState.AddModelError("Image", "تصویر اسلایدر رو انتخاب کنید !");
                        return View(pageBannerItem);
                    }
                    if (Image != null)
                    {
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp" };
                        int vwidth = 425; int vheight = 223;
                        PageBanner pageBanner = await _pbiService.GetPageBannerByIdAsync(pageBannerItem.PBId.GetValueOrDefault());
                        if (pageBanner != null)
                        {
                            if (pageBanner.BannerCount == 1)
                            {
                                vwidth = 1310;
                                vheight = 165;
                            }
                            if (pageBanner.BannerCount == 2)
                            {
                                vwidth = 635;
                                vheight = 300;
                            }
                            if (pageBanner.BannerCount == 3)
                            {
                                vwidth = 425;
                                vheight = 225;
                            }
                            FileValidation fileValidation1 = await Image.UploadedImageValidation(50, vwidth, vheight, ex);

                            if (!fileValidation1.IsValid)
                            {
                                ModelState.AddModelError("Image", fileValidation1.Message);
                                return View(pageBannerItem);
                            }
                            pageBannerItem.Image = Image.SaveUploadedImage("wwwroot/images/pagebanners", true);
                            
                        }                        
                    }

                    _pbiService.UpdatePageBannerItem(pageBannerItem);
                    await _pbiService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await PageBannerItemExists(pageBannerItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PBId"] = new SelectList(await _pbiService.GetPageBannersAsync(), "Id", "Title", pageBannerItem.PBId);
            return View(pageBannerItem);
        }

        // GET: UsersPanel/PageBannerItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _pbiService.GetPageBannerItemsAsync() == null)
            {
                return NotFound();
            }

            var pageBannerItem = await _pbiService.GetPageBannerItemByIdAsync(id.GetValueOrDefault());
            if (pageBannerItem == null)
            {
                return NotFound();
            }

            return View(pageBannerItem);
        }

        // POST: UsersPanel/PageBannerItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _pbiService.GetPageBannerItemsAsync() == null)
            {
                return Problem("Entity set 'MyContext.PageBannerItems'  is null.");
            }
            await _pbiService.DeletePageBannerItemAsync(id);

            _pbiService.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PageBannerItemExists(int id)
        {
            var pageBannerItems = await _pbiService.GetPageBannerItemsAsync();
            return (pageBannerItems?.Any(x => x.Id == id)).GetValueOrDefault();
        }
    }
}
