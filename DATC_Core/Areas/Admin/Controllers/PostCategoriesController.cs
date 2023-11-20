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
    public class PostCategoriesController : Controller
    {
        private readonly DATCCoreMineDBContext db = new DATCCoreMineDBContext();
        public INotyfService _notyfService { get; }

        public PostCategoriesController(DATCCoreMineDBContext context, INotyfService notyfService)
        {
            db = context;
            _notyfService = notyfService;
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
            ViewData["DanhMuc"] = new SelectList(db.PostCategorys, "CateId", "CateName");
            ViewBag.listCat = new SelectList(db.PostCategorys, "CateId", "CateName", 0);
            ViewBag.listLevel = new SelectList(db.PostCategorys, "Ordering", "CateName", 0);
            return View();
        }

        // POST: Admin/PostCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CateId,CateName,Description,ParentId,Levels,Ordering,Published,Cover")] PostCategory postCategory, IFormFile fCover)
        {
            ViewData["DanhMuc"] = new SelectList(db.PostCategorys, "CateId", "CateName");
            ViewBag.listCat = new SelectList(db.PostCategorys, "CateId", "CateName", 0);
            ViewBag.listLevel = new SelectList(db.PostCategorys, "Ordering", "CateName", 0);

            if (ModelState.IsValid)
            {
                postCategory.CateName = Utilities.ToTitleCase(postCategory.CateName);
                if (fCover != null)
                {
                    string extension = Path.GetExtension(fCover.FileName);
                    string image = Utilities.SEOUrl(postCategory.CateName) + extension;
                    postCategory.Cover = await Utilities.UploadFile(fCover, @"postCatgories", image.ToLower());
                }
                if (string.IsNullOrEmpty(postCategory.Cover))
                {
                    postCategory.Cover = "placeholder-image.jpg";
                }
                postCategory.Description = postCategory.CateName;
                //postCategory.Levels = 0;
                //postCategory.ParentId = postCategory.ParentId;
                db.Add(postCategory);
                await db.SaveChangesAsync();
                _notyfService.Success("Thêm mới Danh mục bài viết", 3);
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
            ViewData["DanhMuc"] = new SelectList(db.PostCategorys, "CateId", "CateName");
            ViewBag.listCat = new SelectList(db.PostCategorys, "CateId", "CateName", 0);
            ViewBag.listLevel = new SelectList(db.PostCategorys, "Ordering", "CateName", 0);
            return View(postCategory);
        }

        // POST: Admin/PostCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CateId,CateName,Description,ParentId,Levels,Ordering,Published,Cover")] PostCategory postCategory/*, IFormFile fCover*/)
        {
            if (id != postCategory.CateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    postCategory.CateName = Utilities.ToTitleCase(postCategory.CateName);
                    //if (fCover != null)
                    //{
                    //    string extension = Path.GetExtension(fCover.FileName);
                    //    string image = Utilities.SEOUrl(postCategory.CateName) + extension;
                    //    postCategory.Cover = await Utilities.UploadFile(fCover, @"postCatgories", image.ToLower());
                    //}
                    //if (string.IsNullOrEmpty(postCategory.Cover))
                    //{
                    //    postCategory.Cover = "placeholder-image.jpg";
                    //}
                    db.Update(postCategory);
                    await db.SaveChangesAsync();
                    _notyfService.Success("Cập nhật Danh mục bài viết ID = " + id, 3);

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
            ViewData["DanhMuc"] = new SelectList(db.PostCategorys, "CateId", "CateName");
            ViewBag.listCat = new SelectList(db.PostCategorys, "CateId", "CateName", 0);
            ViewBag.listLevel = new SelectList(db.PostCategorys, "Ordering", "CateName", 0);
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
            _notyfService.Success("Xoá Danh mục bài viết ID = " + id, 3);
            return RedirectToAction(nameof(Index));
        }

        private bool PostCategoryExists(int id)
        {
            return (db.PostCategorys?.Any(e => e.CateId == id)).GetValueOrDefault();
        }


        //[HttpPost]
        ////[CustomAuthorizeAttribute(RoleID = "ADMIN")]
        //public JsonResult changeStatus(int id)
        //{
        //    PostCategory postCategory = db.PostCategorys.Find(id);
        //    postCategory.Published = (postCategory.Published == true) ? true : false;
        //    //categoryie.Updated_at = DateTime.Now;
        //    //categoryie.Updated_by = int.Parse(Session["Admin_ID"].ToString());
        //    db.Entry(postCategory).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return Json(new
        //    {
        //        published = postCategory.Published
        //    });
        //}
    }
}
