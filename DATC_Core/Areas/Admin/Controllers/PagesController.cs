using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DATC_Core.Models;
using DATC_Core.Helper;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace DATC_Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly DATCCoreMineDBContext _context;
        public INotyfService _notyfService { get; }

        public PagesController(DATCCoreMineDBContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/Pages
        public async Task<IActionResult> Index()
        {
            return _context.Pages != null ?
                        View(await _context.Pages.ToListAsync()) :
                        Problem("Entity set 'DATCCoreMineDBContext.Pages'  is null.");
        }

        // GET: Admin/Pages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pages == null)
            {
                return NotFound();
            }

            var page = await _context.Pages
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // GET: Admin/Pages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Pages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PageId,PageName,Contents,Thumb,Published,Title,MetaDesc,MetaKey,Alias,CreatedAt,Ordering")] Page page, IFormFile? fThumb)
        {
            if (ModelState.IsValid)
            {
                page.PageName = Utilities.ToTitleCase(page.PageName);
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(page.PageName) + extension;
                    page.Thumb = await Utilities.UploadFile(fThumb, @"pages", image.ToLower());
                }

                if (string.IsNullOrEmpty(page.Thumb))
                {
                    page.Thumb = "avatar_profile_null.jpg";
                }
                page.Title = Utilities.ToTitleCase(page.PageName);
                if (page.Contents == null)
                {
                    page.Contents = Utilities.SEOUrl(page.PageName);
                }
                page.Contents = Utilities.SEOUrl(page.PageName);
                page.CreatedAt = DateTime.Now;
                page.Alias = Utilities.SEOUrl(page.PageName);
                page.MetaDesc = page.PageName;
                page.MetaKey = page.PageName;
                page.Ordering = 0;
                _context.Add(page);
                await _context.SaveChangesAsync();
                _notyfService.Success("Tạo mới thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        // GET: Admin/Pages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pages == null)
            {
                return NotFound();
            }

            var page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        // POST: Admin/Pages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PageId,PageName,Contents,Thumb,Published,Title,MetaDesc,MetaKey,Alias,CreatedAt,Ordering")] Page page, IFormFile fThumb)
        {
            if (id != page.PageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    page.PageName = Utilities.ToTitleCase(page.PageName);
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Utilities.SEOUrl(page.PageName) + extension;
                        page.Thumb = await Utilities.UploadFile(fThumb, @"pages", image.ToLower());
                    }

                    if (string.IsNullOrEmpty(page.Thumb))
                    {
                        page.Thumb = "avatar_profile_null.jpg";
                    }
                    page.Title = Utilities.ToTitleCase(page.Title);
                    page.Alias = Utilities.SEOUrl(page.PageName);
                    page.MetaDesc = page.PageName;
                    page.MetaKey = page.PageName;
                    _context.Update(page);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Cập nhật thành công ID = " + id);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.PageId))
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
            return View(page);
        }

        // GET: Admin/Pages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pages == null)
            {
                return NotFound();
            }

            var page = await _context.Pages
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // POST: Admin/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pages == null)
            {
                return Problem("Entity set 'DATCCoreMineDBContext.Pages'  is null.");
            }
            var page = await _context.Pages.FindAsync(id);
            if (page != null)
            {
                _context.Pages.Remove(page);
            }

            await _context.SaveChangesAsync();
            _notyfService.Success("Xoá thành công ID = " + id);

            return RedirectToAction(nameof(Index));
        }

        private bool PageExists(int id)
        {
            return (_context.Pages?.Any(e => e.PageId == id)).GetValueOrDefault();
        }
    }
}
