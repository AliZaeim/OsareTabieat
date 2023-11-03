using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Blogs;
using DataLayer.Entities.Store;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class BlogService : IBlogService
    {
        private readonly MyContext _context;
        public BlogService(MyContext context)
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
        public void DetachEntity(Blog blog)
        {
            _context.Entry(blog).State = EntityState.Detached;
        }
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.Include(x => x.ProductGroup).Include(x => x.ProductItems).ToListAsync();
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.Include(x => x.ProductGroup).Include(x => x.ProductItems)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
        #endregion
        #region BlogGroup
        public void CreateBlogGroup(BlogGroup blogGroup)
        {
            _context.BlogGroups.Add(blogGroup);
        }

        public void DeleteBlogGroup(BlogGroup blogGroup)
        {
            _context.BlogGroups.Remove(blogGroup);
        }

        public bool ExistBlogGroup(int id)
        {
            return _context.BlogGroups.Any(x => x.BlogGroupId == id);
        }

        public async Task<BlogGroup?> GetBlogGroupByIdAsync(int id)
        {
            return await _context.BlogGroups.Include(x => x.Blogs).SingleOrDefaultAsync(x => x.BlogGroupId == id);
        }

        public async Task<List<BlogGroup>> GetBlogGroupsAsync()
        {
            return await _context.BlogGroups.Include(x => x.Blogs).ToListAsync();
        }

        public void UpdateBlogGroup(BlogGroup blogGroup)
        {
            _context.BlogGroups.Update(blogGroup);
        }


        #endregion BlogGroup
        #region Blog
        public Task<List<Blog>> GetBlogsAsync()
        {
            return _context.Blogs.Include(x => x.BlogGroup).Include(x => x.Products).ToListAsync();
        }

        public async Task<Blog?> GetBlogByIdAsync(Guid id)
        {
            return await _context.Blogs.Include(x => x.BlogGroup).Include(x => x.Products).SingleOrDefaultAsync(x => x.BlogId == id);
        }

        public void CreateBlog(Blog blog)
        {
            _context.Blogs.Add(blog);
        }

        public void UpdateBlog(Blog blog)
        {
            _context.Blogs.Update(blog);
        }

        public void DeleteBlog(Blog blog)
        {
            _context.Blogs.Remove(blog);
        }

        public bool ExistBlog(Guid id)
        {
            return _context.Blogs.Any(x => x.BlogId == id);
        }

        public async Task<Blog?> GetBlogByKey(string Key)
        {
            return await _context.Blogs.Include(x => x.BlogGroup).Include(x => x.Products).SingleOrDefaultAsync(x => x.BlogShortKey == Key);
        }

        public async Task<bool> ExistBlogKey(string Key)
        {
            return await _context.Blogs.AnyAsync(x => x.BlogShortKey == Key);
        }

        public async Task RemoveBlogProducts(Guid blogId)
        {
            Blog? blog = await _context.Blogs.Include(x => x.Products).SingleOrDefaultAsync(x => x.BlogId == blogId);
            if (blog != null)
            {
                foreach (var item in blog.Products)
                {
                    blog.Products.Remove(item);
                }
                _context.Update(blog);
                await _context.SaveChangesAsync();
                _context.Entry(blog).State = EntityState.Detached;
            }
        }

		public async Task<List<Blog>> GetLastBlogs(int nCount)
		{
			List<Blog> blogs = await _context.Blogs.Include(x => x.BlogGroup).Include(x => x.Products).ToListAsync();
            blogs = blogs.OrderByDescending(x => x.BlogDate).TakeLast(nCount).ToList();
            return blogs;
		}






		#endregion Blog

	}
}
