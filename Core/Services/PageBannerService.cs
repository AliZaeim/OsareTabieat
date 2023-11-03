using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Supplementary;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class PageBannerService : IPageBannerService
    {
        private readonly MyContext _context;
        public PageBannerService(MyContext context)
        {
            _context = context;
        }
        #region General
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion
        #region PageBannerItems
        public void CreatePageBannerItem(PageBannerItem pageBannerItem)
        {
            _context.PageBannerItems.Add(pageBannerItem);
        }

        public async Task<List<PageBannerItem>> GetPageBannerItemsAsync()
        {
            return await _context.PageBannerItems.Include(x => x.PageBanner).ToListAsync();
        }

        public async Task<PageBannerItem> GetPageBannerItemByIdAsync(int id)
        {
            return await _context.PageBannerItems.Include(x => x.PageBanner).SingleOrDefaultAsync(x => x.Id == id);
        }
        public void UpdatePageBannerItem(PageBannerItem pageBannerItem)
        {
            _context.PageBannerItems.Update(pageBannerItem);
        }

        public async Task DeletePageBannerItemAsync(int id)
        {
            PageBannerItem? pageBannerItem = await _context.PageBannerItems.FindAsync(id);
            if (pageBannerItem != null)
            {
                _context.PageBannerItems.Remove(pageBannerItem);
            }
        }

        #endregion
        #region PageBanners
        public async Task<PageBanner> GetPageBannerByIdAsync(int id)
        {
            //don't use include because in Update pageBannerItems get attaching error
            return await _context.PageBanners.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<PageBanner>> GetPageBannersAsync()
        {
            return await _context.PageBanners.Include(x => x.PageBannerItems).ToListAsync();
        }

        public async Task<PageBanner> GetPageBannerByCountOrder(int count, int order)
        {
            return await _context.PageBanners.Include(x => x.PageBannerItems).OrderByDescending(x => x.RegDate)
                .FirstOrDefaultAsync(w => w.BannerCount == count && w.ViewOrder == order);
        }

        public void DetachPageBanner(PageBanner banner)
        {
            _context.Entry(banner).State = EntityState.Detached;
        }

        public async Task<PageBannerItem> GetPageBannerItemByIncludeAsync(int id)
        {
            return await _context.PageBannerItems.Include(x => x.PageBanner).SingleOrDefaultAsync(x => x.Id == id);
        }




        #endregion
        #region StorePageBanner
        public async Task<List<StorePageBanner>> GetStorePageBannersAsync()
        {
            return await _context.StorePageBanners.ToListAsync();
        }

        public async Task<StorePageBanner> GetStorePageBannerByIdAsync(int id)
        {
            return await _context.StorePageBanners.FindAsync(id);
        }

        public void CreateStorePageBanner(StorePageBanner storePageBanner)
        {
            _context.StorePageBanners.Add(storePageBanner);
        }

        public void UpdateStorePageBanner(StorePageBanner storePageBanner)
        {
            _context.StorePageBanners.Update(storePageBanner);
        }

        public void DeleteStorePageBanner(StorePageBanner storePageBanner)
        {
            _context.StorePageBanners.Remove(storePageBanner);
        }

        public bool ExistStorePageBanner(int id)
        {
            return _context.StorePageBanners.Any(x => x.Id == id);
        }

        public async Task<StorePageBanner> GetLastStorePageBanner()
        {
            return await _context.StorePageBanners.Where(w => w.IsActive).OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();
        }
        #endregion


    }
}
