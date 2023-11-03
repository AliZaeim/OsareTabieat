using Core.Services.Interfaces;
using DataLayer.Entities.Blogs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    
    public class BlogsModel : PageModel
    {
		private readonly IBlogService _blogService;
		public BlogsModel(IBlogService blogService)
		{
			_blogService = blogService;
		}
		public List<Blog> Blogs { get; set; } = new();
		public List<BlogGroup> BlogGroups { get; set; } = new();
		public List<string> BlogKeys { get; set; } = new();
		public string Tag { get; set; }
		public BlogGroup? BlogGroup { get; set; } = new();
		public List<Blog> LastBlogs { get; set; } = new();
		[BindProperty(SupportsGet =true)]
		public string Search { get; set; }
		public async Task OnGet(int? grId, string tag)
        {
			Blogs = await _blogService.GetBlogsAsync();
			Blogs = Blogs.Where(w => w.BlogIsActive).ToList();
			LastBlogs =await _blogService.GetLastBlogs(3);
			BlogGroups =await _blogService.GetBlogGroupsAsync();
			BlogGroups = BlogGroups.Where(w => w.IsActive).ToList();

			if (grId != null)
			{
				BlogGroup = await _blogService.GetBlogGroupByIdAsync(grId.Value);
				if (BlogGroup != null)
				{
                    Blogs = Blogs.Where(w => w.BlogGroupId == grId.Value).ToList();
                }
				else
				{
					Blogs = new List<Blog>();
				}
			}
			if (!string.IsNullOrEmpty(tag))
			{
				Tag = tag;
				Blogs = Blogs.Where(w => w.TagsList.Any(z => z.Replace(" ", "-") == tag.Trim())).ToList();
			}
			if (!string.IsNullOrEmpty(Search))
			{
				
				Blogs = Blogs.Where(w => w.BlogTitle!.Contains(Search) || w.BlogSummary!.Contains(Search) || w.TagsList.Any(z => z.Contains(Search))).ToList();
			}
            BlogKeys = Blogs.SelectMany(x => x.TagsList).ToList();
            BlogKeys = BlogKeys.DistinctBy(x => x.Trim()).ToList();
        }
    }
}
