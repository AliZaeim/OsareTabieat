using DataLayer.Entities.Blogs;
using DataLayer.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IBlogService
    {
        #region Generic
        void SaveChanges();
        Task SaveChangesAsync();
        void DetachEntity(Blog blog);
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        #endregion Generic
        #region BlogGroup
        Task<List<BlogGroup>> GetBlogGroupsAsync();
        Task<BlogGroup?> GetBlogGroupByIdAsync(int id);
        void CreateBlogGroup(BlogGroup blogGroup);
        void UpdateBlogGroup(BlogGroup blogGroup);
        void DeleteBlogGroup(BlogGroup blogGroup);
        bool ExistBlogGroup(int id);

        #endregion BlogGroup
        #region Blog
        Task<List<Blog>> GetBlogsAsync();
        Task<Blog?> GetBlogByIdAsync(Guid id);
        void CreateBlog(Blog blog);
        void UpdateBlog(Blog blog);
        void DeleteBlog(Blog blog);
        bool ExistBlog(Guid id);
        Task<Blog?> GetBlogByKey(string Key);
        Task<bool> ExistBlogKey(string Key);
        Task RemoveBlogProducts(Guid blogId);
        Task<List<Blog>> GetLastBlogs(int nCount);
        
        #endregion Blog
    }
}
