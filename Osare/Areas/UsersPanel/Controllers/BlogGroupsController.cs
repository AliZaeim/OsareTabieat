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
using Microsoft.AspNetCore.Authorization;
using Core.Security;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("manage")]
    public class BlogGroupsController : Controller
    {
        
        private readonly IBlogService _blogService;
        public BlogGroupsController(IBlogService blogService)
        {
            _blogService= blogService;
        }

        // GET: UsersPanel/BlogGroups
        public async Task<IActionResult> Index()
        {
              return View(await _blogService.GetBlogGroupsAsync());
        }

        // GET: UsersPanel/BlogGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _blogService.GetBlogGroupsAsync() == null)
            {
                return NotFound();
            }

            var blogGroup = await _blogService.GetBlogGroupByIdAsync(id.Value);
            if (blogGroup == null)
            {
                return NotFound();
            }

            return View(blogGroup);
        }

        // GET: UsersPanel/BlogGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersPanel/BlogGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogGroup blogGroup)
        {
            if (ModelState.IsValid)
            {
                _blogService.CreateBlogGroup(blogGroup);
                await _blogService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogGroup);
        }

        // GET: UsersPanel/BlogGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _blogService.GetBlogGroupsAsync() == null)
            {
                return NotFound();
            }

            var blogGroup = await _blogService.GetBlogGroupByIdAsync(id.Value);
            if (blogGroup == null)
            {
                return NotFound();
            }
            return View(blogGroup);
        }

        // POST: UsersPanel/BlogGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,BlogGroup blogGroup)
        {
            if (id != blogGroup.BlogGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _blogService.UpdateBlogGroup(blogGroup);
                    await _blogService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogGroupExists(blogGroup.BlogGroupId))
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
            return View(blogGroup);
        }

        // GET: UsersPanel/BlogGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _blogService.GetBlogGroupsAsync() == null)
            {
                return NotFound();
            }

            var blogGroup = await _blogService.GetBlogGroupByIdAsync(id.Value);
            if (blogGroup == null)
            {
                return NotFound();
            }

            return View(blogGroup);
        }

        // POST: UsersPanel/BlogGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _blogService.GetBlogGroupsAsync() == null)
            {
                return Problem("Entity set 'MyContext.BlogGroups'  is null.");
            }
            var blogGroup = await _blogService.GetBlogGroupByIdAsync(id);
            if (blogGroup != null)
            {
                blogGroup.IsDeleted = true;
                _blogService.UpdateBlogGroup(blogGroup);
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        private bool BlogGroupExists(int id)
        {
          return _blogService.ExistBlogGroup(id);
        }
    }
}
