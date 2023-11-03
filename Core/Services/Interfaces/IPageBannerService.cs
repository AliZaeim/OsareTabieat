using DataLayer.Entities.Supplementary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IPageBannerService
    {
        #region General
        void SaveChanges();
        Task SaveChangesAsync();
        void DetachPageBanner(PageBanner banner);
        #endregion
        #region PageBannerItems
        Task<List<PageBannerItem>> GetPageBannerItemsAsync();
        Task<PageBannerItem> GetPageBannerItemByIdAsync(int id);
        Task<PageBannerItem> GetPageBannerItemByIncludeAsync(int id);
        void CreatePageBannerItem(PageBannerItem pageBannerItem);
        void UpdatePageBannerItem(PageBannerItem pageBannerItem);
        Task DeletePageBannerItemAsync(int id);
        #endregion
        #region PageBanners
        Task<PageBanner> GetPageBannerByIdAsync(int id);
        Task<List<PageBanner>> GetPageBannersAsync();
        Task<PageBanner> GetPageBannerByCountOrder(int count,int order);
        #endregion
        #region StorePageBanner
        Task<List<StorePageBanner>> GetStorePageBannersAsync();
        Task<StorePageBanner> GetStorePageBannerByIdAsync(int id);        
        void CreateStorePageBanner(StorePageBanner storePageBanner);
        void UpdateStorePageBanner(StorePageBanner storePageBanner);
        void DeleteStorePageBanner(StorePageBanner storePageBanner);
        bool ExistStorePageBanner(int id);
        Task<StorePageBanner> GetLastStorePageBanner();
        #endregion
    }
}
