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
    public class PostsController : Controller
    {
        private readonly DATCCoreMineDBContext db = new DATCCoreMineDBContext();

        public PostsController(DATCCoreMineDBContext context)
        {
            db = context;
        }

        // GET: Admin/Posts
        public async Task<IActionResult> Index()
        {
            var dATCCoreMineDBContext = db.Posts.Include(p => p.Account).Include(p => p.Cate);
            return View(await dATCCoreMineDBContext.ToListAsync());
        }

        // GET: Admin/Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Posts == null)
            {
                return NotFound();
            }

            var post = await db.Posts
                .Include(p => p.Account)
                .Include(p => p.Cate)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Admin/Posts/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(db.Accounts, "AccountId", "AccountId");
            ViewData["CateId"] = new SelectList(db.PostCategorys, "CateId", "CateId");
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreateDate,Author,AccountId,Tags,CateId,IsHot,IsNew,MetaDesc,MetaKey,Views")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(db.Accounts, "AccountId", "AccountId", post.AccountId);
            ViewData["CateId"] = new SelectList(db.PostCategorys, "CateId", "CateId", post.CateId);
            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Posts == null)
            {
                return NotFound();
            }

            var post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(db.Accounts, "AccountId", "AccountId", post.AccountId);
            ViewData["CateId"] = new SelectList(db.PostCategorys, "CateId", "CateId", post.CateId);
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreateDate,Author,AccountId,Tags,CateId,IsHot,IsNew,MetaDesc,MetaKey,Views")] Post post)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(post);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
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
            ViewData["AccountId"] = new SelectList(db.Accounts, "AccountId", "AccountId", post.AccountId);
            ViewData["CateId"] = new SelectList(db.PostCategorys, "CateId", "CateId", post.CateId);
            return View(post);
        }

        // GET: Admin/Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Posts == null)
            {
                return NotFound();
            }

            var post = await db.Posts
                .Include(p => p.Account)
                .Include(p => p.Cate)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Posts == null)
            {
                return Problem("Entity set 'DATCCoreMineDBContext.Posts'  is null.");
            }
            var post = await db.Posts.FindAsync(id);
            if (post != null)
            {
                db.Posts.Remove(post);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
          return (db.Posts?.Any(e => e.PostId == id)).GetValueOrDefault();
        }

        public int listPostCate()
        {
            return 0;
        }
    }
}
