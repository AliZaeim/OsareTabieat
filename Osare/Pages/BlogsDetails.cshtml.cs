using Core.Services.Interfaces;
using DataLayer.Entities.Blogs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class BlogsDetailsModel : PageModel
    {
        private readonly IBlogService _blogService;
        public BlogsDetailsModel(IBlogService blogService)
        {
            _blogService= blogService;
        }
        
        public Blog? Blog { get; set; } = new();
        public List<BlogGroup> BlogGroups { get; set; } = new();
        public List<Blog> LastBlogs { get; set; } = new();
        public async Task OnGet(string? key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                Blog = await _blogService.GetBlogByKey(key);
            }
            BlogGroups = await _blogService.GetBlogGroupsAsync();
            BlogGroups = BlogGroups.Where(w => w.IsActive).ToList();
            LastBlogs = await _blogService.GetLastBlogs(3);
            
        }
    }
}
