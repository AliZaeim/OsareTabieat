using Core.DTOs.General;
using Core.Services.Interfaces;
using DataLayer.Entities.Blogs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Web.Components
{
    public class LastBlogsComponent : ViewComponent
    {
        private readonly IBlogService _blogService;
        public LastBlogsComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Blog> blogs = await _blogService.GetBlogsAsync();
            blogs = blogs.OrderByDescending(x => x.BlogDate).TakeLast(30).ToList();
            return await Task.FromResult(View("/Pages/Components/_GetLastBlogs.cshtml", blogs));
        }
            
    }
}
