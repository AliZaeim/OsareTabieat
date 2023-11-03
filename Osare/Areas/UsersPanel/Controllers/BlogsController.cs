using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Context;
using DataLayer.Entities.Blogs;
using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using Core.Utility;
using Core.DTOs.Admin;
using System.Text.RegularExpressions;
using System.Reflection.Metadata;
using Core.DTOs.General;
using Microsoft.AspNetCore.Authorization;
using Core.Security;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("manage")]
    public class BlogsController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService= blogService;
        }

        // GET: UsersPanel/Blogs
        public async Task<IActionResult> Index()
        {
            var blogs = await _blogService.GetBlogsAsync();
            blogs = blogs.Where(w => w.BlogIsActive).ToList();
            return View(blogs);
        }

        // GET: UsersPanel/Blogs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || await _blogService.GetBlogsAsync() == null)
            {
                return NotFound();
            }

            var blog = await _blogService.GetBlogByIdAsync(id.Value);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: UsersPanel/Blogs/Create
        public async Task<IActionResult> Create()
        {
            await PrepareErrorDataAsync(null, new List<int>());
            
            return View();
        }

        // POST: UsersPanel/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog, List<int> SelectedProducts, IFormFile? BlogImageInBlog, IFormFile? BlogImageInBlogDetails)
        {
            if (blog is null)
            {
                throw new ArgumentNullException(nameof(blog));
            }

            if (ModelState.IsValid)
            {
                if (BlogImageInBlog == null)
                {
                    ModelState.AddModelError("BlogImageInBlog", "تصویر صفحه بلاگ را انتخاب کنید !");
                    await PrepareErrorDataAsync(blog.BlogGroupId,SelectedProducts.ToList());
                    return View(blog);
                }
                if (BlogImageInBlogDetails == null)
                {
                    ModelState.AddModelError("BlogImageInBlogDetails", "تصویر صفحه جزئیات بلاگ را انتخاب کنید !");
                    await PrepareErrorDataAsync(blog.BlogGroupId, SelectedProducts.ToList());
                    return View(blog);
                }
                if (BlogImageInBlog != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation ImgValid = await BlogImageInBlog.UploadedImageValidation(50, ex);
                    if (!ImgValid.IsValid)
                    {                        
                        ModelState.AddModelError("BlogImageInBlog", ImgValid.Message??"");
                        await PrepareErrorDataAsync(blog.BlogGroupId,SelectedProducts);
                        return View(blog);
                    }
                    blog.BlogImageInBlog = BlogImageInBlog.SaveUploadedImage("wwwroot/images/blogs", true);
                }
                if (BlogImageInBlogDetails != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                    FileValidation ImgValid = await BlogImageInBlogDetails.UploadedImageValidation(50, ex);
                    if (!ImgValid.IsValid)
                    {
                        ModelState.AddModelError("BlogImageInBlogDetails", ImgValid.Message ?? "");
                        await PrepareErrorDataAsync(blog.BlogGroupId, SelectedProducts);
                        return View(blog);
                    }
                    blog.BlogImageInBlogDetails = BlogImageInBlogDetails.SaveUploadedImage("wwwroot/images/blogs", true);
                }
                blog.BlogDate= DateTime.Now;
                if (SelectedProducts != null )
                {
                    if (SelectedProducts.Any())
                    {
                        foreach (var pr in SelectedProducts)
                        {
                            Product product = await _blogService.GetProductByIdAsync(pr);
                            if (product != null)
                            {
                                blog.Products.Add(product);
                            }
                        }
                    }
                }
                blog.BlogDate = DateTime.Now;
                _blogService.CreateBlog(blog);
                await _blogService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Select(x => x.Value!.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
            await PrepareErrorDataAsync(blog.BlogGroupId, SelectedProducts);
            return View(blog);
        }
        public async Task PrepareErrorDataAsync(int? groupId, List<int> SelectedProducts)
        {
            List<BlogGroup> blogGroups = await _blogService.GetBlogGroupsAsync();
            blogGroups = blogGroups.Where(w => w.IsActive).ToList();
            ViewData["BlogGroupId"] = new SelectList(blogGroups.ToList(), "BlogGroupId", "BlogGroupTitle", groupId);
            List<Product> products = await _blogService.GetProductsAsync();
            products = products.Where(w => w.IsActive).ToList();
            ViewData["Products"] = products.ToList();
            ViewData["SelectedProducts"] = SelectedProducts.ToList();
        }
        // GET: UsersPanel/Blogs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || await _blogService.GetBlogsAsync() == null)
            {
                return NotFound();
            }

            var blog = await _blogService.GetBlogByIdAsync(id.Value);
            if (blog == null)
            {
                return NotFound();
            }
            await PrepareErrorDataAsync(blog.BlogGroupId, blog.Products.Select(x => x.Id).ToList());
            return View(blog);
        }

        // POST: UsersPanel/Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Blog blog, List<int> SelectedProducts, IFormFile? BlogImageInBlog, IFormFile? BlogImageInBlogDetails)
        {
            if (id != blog.BlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (BlogImageInBlog != null)
                    {
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                        FileValidation ImgValid = await BlogImageInBlog.UploadedImageValidation(50, ex);
                        if (!ImgValid.IsValid)
                        {
                            ModelState.AddModelError("BlogImageInBlog", ImgValid.Message ?? "");
                            await PrepareErrorDataAsync(blog.BlogGroupId, SelectedProducts);
                            return View(blog);
                        }
                        blog.BlogImageInBlog = BlogImageInBlog.SaveUploadedImage("wwwroot/images/blogs", true);
                    }
                    if (BlogImageInBlogDetails != null)
                    {
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp", ".avif" };
                        FileValidation ImgValid = await BlogImageInBlogDetails.UploadedImageValidation(50, ex);
                        if (!ImgValid.IsValid)
                        {
                            ModelState.AddModelError("BlogImageInBlogDetails", ImgValid.Message ?? "");
                            await PrepareErrorDataAsync(blog.BlogGroupId, SelectedProducts);
                            return View(blog);
                        }
                        blog.BlogImageInBlogDetails = BlogImageInBlogDetails.SaveUploadedImage("wwwroot/images/blogs", true);
                    }
                    if (SelectedProducts != null)
                    {
                        await _blogService.RemoveBlogProducts(id);
                        foreach (var pr in SelectedProducts)
                        {
                            Product product = await _blogService.GetProductByIdAsync(pr);
                            if (product != null)
                            {
                                blog.Products.Add(product);
                            }
                        }
                    }
                    _blogService.UpdateBlog(blog);
                    await _blogService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.BlogId))
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
            await PrepareErrorDataAsync(blog.BlogGroupId, SelectedProducts);
            return View(blog);
        }

        // GET: UsersPanel/Blogs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || await _blogService.GetBlogsAsync() == null)
            {
                return NotFound();
            }

            var blog = await _blogService.GetBlogByIdAsync(id.Value);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: UsersPanel/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (await _blogService.GetBlogsAsync() == null)
            {
                return Problem("Entity set 'MyContext.Blogs'  is null.");
            }
            var blog = await _blogService.GetBlogByIdAsync(id);
            if (blog != null)
            {
                blog.IsDeleted= true;
                _blogService.UpdateBlog(blog);
                await _blogService.SaveChangesAsync();
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(Guid id)
        {
          return _blogService.ExistBlog(id);
        }
    }
}
