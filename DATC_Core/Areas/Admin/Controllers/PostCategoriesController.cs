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
        private readonly DATCCoreMineDBContext db = new DATCCoreMineDBContext();

        public PostCategoriesController(DATCCoreMineDBContext context)
        {
            db = context;
        }

        // GET: Admin/PostCategories
        public async Task<IActionResult> Index()
        {
              return db.PostCategorys != null ? 
                          View(await db.PostCategorys.ToListAsync()) :
                          Problem("Entity set 'DATCCoreMineDBContext.PostCategorys'  is null.");
        }

        // GET: Admin/PostCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.PostCategorys == null)
            {
                return NotFound();
            }

            var postCategory = await db.PostCategorys
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
            ViewBag.listCat = new SelectList(db.PostCategorys.Where(m => m.Published == true), "CateId", "CateName", 0);
            ViewBag.listLevel = new SelectList(db.PostCategorys.Where(m => m.Published == true), "Ordering", "CateName", 0);
            return View();
        }

        // POST: Admin/PostCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CateId,CateName,Description,ParentId,Levels,Ordering,Published,Cover")] PostCategory postCategory)
        {
            ViewBag.listCat = new SelectList(db.PostCategorys.Where(m => m.Published == true), "CateId", "CateName", 0);
            ViewBag.listLevel = new SelectList(db.PostCategorys.Where(m => m.Published == true), "Ordering", "CateName", 0);

            if (ModelState.IsValid)
            {
                db.Add(postCategory);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postCategory);
        }

        // GET: Admin/PostCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.PostCategorys == null)
            {
                return NotFound();
            }

            var postCategory = await db.PostCategorys.FindAsync(id);
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
                    db.Update(postCategory);
                    await db.SaveChangesAsync();
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
            if (id == null || db.PostCategorys == null)
            {
                return NotFound();
            }

            var postCategory = await db.PostCategorys
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
            if (db.PostCategorys == null)
            {
                return Problem("Entity set 'DATCCoreMineDBContext.PostCategorys'  is null.");
            }
            var postCategory = await db.PostCategorys.FindAsync(id);
            if (postCategory != null)
            {
                db.PostCategorys.Remove(postCategory);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostCategoryExists(int id)
        {
          return (db.PostCategorys?.Any(e => e.CateId == id)).GetValueOrDefault();
        }
    }
}
