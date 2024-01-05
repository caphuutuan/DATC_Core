using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DATC_Core.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using DATC_Core.Helper;

namespace DATC_Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCategoriesController : Controller
    {
        private readonly DATCCoreMineDBContext db = new DATCCoreMineDBContext();
        public INotyfService _notyfService { get; }

        public AdminCategoriesController(DATCCoreMineDBContext context, INotyfService notyfService)
        {
            db = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminCategories
        public async Task<IActionResult> Index()
        {
            ViewBag.countProdCate = db.Categoryies.Count();

            return db.Categoryies != null ?
                        View(await db.Categoryies.ToListAsync()) :
                        Problem("Entity set 'DATCCoreMineDBContext.Categoryies'  is null.");
        }

        // GET: Admin/AdminCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Categoryies == null)
            {
                return NotFound();
            }

            var categoryie = await db.Categoryies
                .FirstOrDefaultAsync(m => m.CateId == id);
            if (categoryie == null)
            {
                return NotFound();
            }

            return View(categoryie);
        }

        // GET: Admin/AdminCategories/Create
        public IActionResult Create()
        {
            ViewBag.listCat = new SelectList(db.Categoryies, "CateId", "CateName", 0);
            ViewBag.listOrder = new SelectList(db.Categoryies, "Ordering", "CateName", 0);
            //ViewBag.listStatus = new SelectList(db.Categoryies, "Ordering", "CateName", 0);

            List<SelectListItem> lsStatus = new List<SelectListItem>();
            lsStatus.Add(new SelectListItem() { Text = "Hiển thị", Value = "true" });
            lsStatus.Add(new SelectListItem() { Text = "Không hiển thị", Value = "false" });

            //ViewData["DanhMuc"] = new SelectList(db.Categoryies, "CateId", "CateName");

            return View();
        }

        // POST: Admin/AdminCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CateId,CateName,Description,ParentId,Levels,Ordering,Published,Thumb,Title,Alias,MetaDesc,MetaKey,Cover,SchemaMarkup")] Categoryie categoryie, IFormFile? fThumb, IFormFile? fCover)
        {
            ViewBag.listCat = new SelectList(db.Categoryies, "CateId", "CateName", 0);
            ViewBag.listOrder = new SelectList(db.Categoryies, "Ordering", "CateName", 0);
            //ViewData["DanhMuc"] = new SelectList(db.Categoryies, "CateId", "CateName");
            if (ModelState.IsValid)
            {

                categoryie.CateName = Utilities.ToTitleCase(categoryie.CateName);
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(categoryie.CateName) + extension;
                    categoryie.Thumb = await Utilities.UploadFile(fThumb, @"categories", image.ToLower());
                }
                if (string.IsNullOrEmpty(categoryie.Thumb))
                {
                    categoryie.Thumb = "placeholder-image.jpg";
                }

                if (fCover != null)
                {
                    string extension = Path.GetExtension(fCover.FileName);
                    string image = Utilities.SEOUrl(categoryie.CateName) + extension;
                    categoryie.Cover = await Utilities.UploadFile(fCover, @"categories", image.ToLower());
                }
                if (string.IsNullOrEmpty(categoryie.Cover))
                {
                    categoryie.Cover = "placeholder-image.jpg";
                }

                categoryie.Alias = Utilities.SEOUrl(categoryie.CateName);
                categoryie.MetaKey = categoryie.CateName;
                categoryie.MetaDesc = categoryie.CateName;
                categoryie.Title = categoryie.CateName;
                categoryie.Description = categoryie.CateName;
                db.Add(categoryie);
                await db.SaveChangesAsync();
                _notyfService.Success("Thêm mới Danh mục thành công", 3);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryie);
        }

        // GET: Admin/AdminCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.listCat = new SelectList(db.Categoryies, "CateId", "CateName", 0);
            ViewBag.listOrder = new SelectList(db.Categoryies, "Ordering", "CateName", 0);
            //ViewData["DanhMuc"] = new SelectList(db.Categoryies, "CateId", "CateName");
            if (id == null || db.Categoryies == null)
            {
                return NotFound();
            }

            var categoryie = await db.Categoryies.FindAsync(id);
            if (categoryie == null)
            {
                return NotFound();
            }
            return View(categoryie);
        }

        // POST: Admin/AdminCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CateId,CateName,Description,ParentId,Levels,Ordering,Published,Thumb,Title,Alias,MetaDesc,MetaKey,Cover,SchemaMarkup")] Categoryie categoryie, IFormFile? fThumb, IFormFile? fCover)
        {
            ViewBag.listCat = new SelectList(db.Categoryies, "CateId", "CateName", 0);
            ViewBag.listOrder = new SelectList(db.Categoryies, "Ordering", "CateName", 0);
            //ViewData["DanhMuc"] = new SelectList(db.Categoryies, "CateId", "CateName");

            if (id != categoryie.CateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    categoryie.CateName = Utilities.ToTitleCase(categoryie.CateName);
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Utilities.SEOUrl(categoryie.CateName) + extension;
                        categoryie.Thumb = await Utilities.UploadFile(fThumb, @"categories", image.ToLower());
                    }
                    if (string.IsNullOrEmpty(categoryie.Thumb))
                    {
                        categoryie.Thumb = "placeholder-image.jpg";
                    }

                    if (fCover != null)
                    {
                        string extension = Path.GetExtension(fCover.FileName);
                        string image = Utilities.SEOUrl(categoryie.CateName) + extension;
                        categoryie.Cover = await Utilities.UploadFile(fCover, @"categories", image.ToLower());
                    }
                    if (string.IsNullOrEmpty(categoryie.Cover))
                    {
                        categoryie.Cover = "placeholder-image.jpg";
                    }

                    categoryie.Alias = Utilities.SEOUrl(categoryie.CateName);
                    categoryie.MetaKey = categoryie.CateName;
                    categoryie.MetaDesc = categoryie.CateName;
                    categoryie.Title = categoryie.CateName;
                    categoryie.Description = categoryie.CateName;

                    db.Update(categoryie);
                    await db.SaveChangesAsync();
                    _notyfService.Success("Cập nhật Danh mục thành công, ID = " + id, 3);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryieExists(categoryie.CateId))
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
            return View(categoryie);
        }

        // GET: Admin/AdminCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Categoryies == null)
            {
                return NotFound();
            }

            var categoryie = await db.Categoryies
                .FirstOrDefaultAsync(m => m.CateId == id);
            if (categoryie == null)
            {
                return NotFound();
            }

            return View(categoryie);
        }

        // POST: Admin/AdminCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Categoryies == null)
            {
                return Problem("Entity set 'DATCCoreMineDBContext.Categoryies'  is null.");
            }
            var categoryie = await db.Categoryies.FindAsync(id);
            if (categoryie != null)
            {
                db.Categoryies.Remove(categoryie);
            }

            await db.SaveChangesAsync();
            _notyfService.Success("Xoá Danh mục thành công, ID = " + id, 3);
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryieExists(int id)
        {
            return (db.Categoryies?.Any(e => e.CateId == id)).GetValueOrDefault();
        }

        // Trash
        public ActionResult Trash()
        {
            var list = db.Categoryies.ToList();
            return View(list);
        }
        //public ActionResult DelTrash(int id)
        //{
        //    Categoryie categoryie = db.Categoryies.Find(id);
        //    if (categoryie == null)
        //    {
        //        Notification.set_flash("Không tồn tại danh mục cần xóa vĩnh viễn!", "warning");
        //        return RedirectToAction("Index");
        //    }
        //    int count_child = db.Categoryies.Where(m => m.ParentId == id).Count();
        //    if (count_child != 0)
        //    {
        //        Notification.set_flash("Không thể xóa, danh mục có chứa danh mục con!", "warning");
        //        return RedirectToAction("Index");
        //    }
        //    categoryie.Published = true;

        //    db.SaveChanges();
        //    Notification.set_flash("Ném thành công vào thùng rác!" + " ID = " + id, "success");
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        //[CustomAuthorizeAttribute(RoleID = "ADMIN")]
        public JsonResult changeStatus(int id)
        {
            Categoryie categoryie = db.Categoryies.Find(id);
            categoryie.Published = (categoryie.Published == true) ? true : false;

            //categoryie.Updated_at = DateTime.Now;
            //categoryie.Updated_by = int.Parse(Session["Admin_ID"].ToString());
            db.Entry(categoryie).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                published = categoryie.Published
            });
        }
    }
}
