using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DATC_Core.Models;

namespace DATC_Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostCategoriesController : Controller
    {
        private readonly DATCCoreMineDBContext _context;

        public PostCategoriesController(DATCCoreMineDBContext context)
        {
            _context = context;
        }

        // GET: Admin/PostCategories
        public async Task<IActionResult> Index()
        {
              return _context.PostCategorys != null ? 
                          View(await _context.PostCategorys.ToListAsync()) :
                          Problem("Entity set 'DATCCoreMineDBContext.PostCategorys'  is null.");
        }

        // GET: Admin/PostCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PostCategorys == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategorys
                .FirstOrDefaultAsync(m => m.CateId == id);
            if (postCategory == null)
            {
                return NotFound();
            }

            return View(postCategory);
        }

        // GET: Admin/PostCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PostCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CateId,CateName,Description,ParentId,Levels,Ordering,Published,Cover")] PostCategory postCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postCategory);
        }

        // GET: Admin/PostCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PostCategorys == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategorys.FindAsync(id);
            if (postCategory == null)
            {
                return NotFound();
            }
            return View(postCategory);
        }

        // POST: Admin/PostCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CateId,CateName,Description,ParentId,Levels,Ordering,Published,Cover")] PostCategory postCategory)
        {
            if (id != postCategory.CateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostCategoryExists(postCategory.CateId))
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
            return View(postCategory);
        }

        // GET: Admin/PostCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PostCategorys == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategorys
                .FirstOrDefaultAsync(m => m.CateId == id);
            if (postCategory == null)
            {
                return NotFound();
            }

            return View(postCategory);
        }

        // POST: Admin/PostCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PostCategorys == null)
            {
                return Problem("Entity set 'DATCCoreMineDBContext.PostCategorys'  is null.");
            }
            var postCategory = await _context.PostCategorys.FindAsync(id);
            if (postCategory != null)
            {
                _context.PostCategorys.Remove(postCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostCategoryExists(int id)
        {
          return (_context.PostCategorys?.Any(e => e.CateId == id)).GetValueOrDefault();
        }
    }
}
